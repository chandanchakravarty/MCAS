<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Page language="c#" Codebehind="AddPartyDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddPartyDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>CLM_PARTIES</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
			<script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>

		<script language='javascript'>
		var Other					= 111;
		var Claimant				= 9;
		var ExpertServiceProviders	= 11;
		var Insured					= 10;
		var Adjuster				= 12;
		var Injured					= 13;
		var Witness					= 14;
		var Passenger				= 15;
		var Additional_Insured		= 239;
	
		function DoLookUp()
		{
			if(document.getElementById('cmbPARTY_TYPE_ID').selectedIndex==-1)
				return;
			
			document.getElementById('hidCONCAT_DATA').value='';
			var SelectedOption = parseInt(document.getElementById('cmbPARTY_TYPE_ID').value);
			var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";			
			if(SelectedOption==Additional_Insured)
			{
				//OpenLookup(URL,'NAME','NAME','txtNAME','txtNAME','PartyName_Claimant','Claimants','@CLAIM_ID=<%=gStrClaimID%>');											
				OpenLookupWithFunction( URL,'ADDITIONAL_INSURED_DATA','NAME','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Additional_Insured','Co-applicants','@CLAIM_ID=<%=gStrClaimID%>','Fetch_Additional_Insured_Data()');
			}
			/*Commented by Asfa (28-Apr-2008) - iTrack issue #4091
			else if(SelectedOption==Claimant)
			{
				//OpenLookup(URL,'NAME','NAME','txtNAME','txtNAME','PartyName_Claimant','Claimants','@CLAIM_ID=<%=gStrClaimID%>');											
				OpenLookupWithFunction( URL,'CLAIMANT_DATA','CLAIMANT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Claimant','Claimants','@CLAIM_ID=<%=gStrClaimID%>','FetchData()');
			}
			*/
			else if(SelectedOption==ExpertServiceProviders)
			{
				//OpenLookup(URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','');				
				if(document.getElementById('hidEXPERT_SERVICE_ID').value=="")
					document.getElementById('hidEXPERT_SERVICE_ID').value="NEW";
				OpenLookupWithFunction(URL,'EXPERT_DATA_PARTY','EXPERT_DATA_PARTY','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','','FetchExpertData()');
			}			
		}
		function HideShowTypeDesc()
		{	
			combo = document.getElementById('cmbEXPERT_SERVICE_TYPE');
			
			if(combo==null || combo.selectedIndex==-1)
				return;
			
			
			if(combo.options[combo.selectedIndex].value=='<%=EXPERT_SERVICE_PROVIDER_TYPE_OTHER.ToString()%>')
			{
				document.getElementById('capEXPERT_SERVICE_TYPE_DESC').style.display="inline";
				document.getElementById('txtEXPERT_SERVICE_TYPE_DESC').style.display="inline";
				//document.getElementById('spnEXPERT_SERVICE_TYPE_DESC').style.display="inline";
				//EnableValidator('rfvEXPERT_SERVICE_TYPE_DESC',true);
			}
			else
			{
				document.getElementById('capEXPERT_SERVICE_TYPE_DESC').style.display="none";
				document.getElementById('txtEXPERT_SERVICE_TYPE_DESC').style.display="none";
				//document.getElementById('spnEXPERT_SERVICE_TYPE_DESC').style.display="none";
				//EnableValidator('rfvEXPERT_SERVICE_TYPE_DESC',false);
			}				
		}
		function FetchExpertData()
		{
			if(document.getElementById("hidCONCAT_DATA").value=="" || document.getElementById("hidCONCAT_DATA").value=="0")
				return;
			var Concat_String = new String(document.getElementById("hidCONCAT_DATA").value);
			var array = Concat_String.split('^');			
			var i=0;
			if(array.length>0)
			{
				document.getElementById('hidEXPERT_SERVICE_ID').value=array[i++];
				
				document.getElementById("hidEXPERT_SERVICE_TYPE").value = array[i++];
				var strServiceType = new String(document.getElementById("hidEXPERT_SERVICE_TYPE").value);
				
				if(strServiceType.split('~').length>0)
				{
					SelectComboOption("cmbEXPERT_SERVICE_TYPE",strServiceType.split('~')[0].toString());									
					document.getElementById("txtEXPERT_SERVICE_TYPE_DESC").value = strServiceType.split('~')[1].toString();
					
				}
				document.getElementById("txtNAME").value = array[i++];
				document.getElementById("txtADDRESS1").value = array[i++];
				document.getElementById("txtADDRESS2").value = array[i++];
				document.getElementById("txtCITY").value = array[i++];
				SelectComboOption("cmbSTATE",array[i++]);									
				document.getElementById("txtZIP").value = array[i++];
				//document.getElementById("txtVENDOR_CODE").value = array[i++];
				//document.getElementById("txtVENDOR_CODE").readOnly=true;//Added by Sibin for Itrack Issue 5055 on 17 Dec 08
				document.getElementById("txtCONTACT_NAME").value = array[i++];
				document.getElementById("txtCONTACT_PHONE").value = array[i++];
				document.getElementById("txtCONTACT_EMAIL").value = array[i++];
				//document.getElementById("txtFEDRERAL_ID").value = array[i++];
				document.getElementById("hidFEDRERAL_ID").value = array[i++];				
				SelectComboOption("cmbPROCESSING_OPTION_1099",array[i++]);					
				SelectComboOption("cmbCOUNTRY",array[i++]);
				document.getElementById("hidMASTER_VENDOR_CODE_ID").value = array[i++];	//Added by Sibin for Itrack Issue 5055 on 17 Dec 08				
				//document.getElementById("txtMASTER_VENDOR_CODE").value = array[i++];
				i++;
				SelectComboOption("cmbPARTY_DETAIL",array[i++]);
				if(document.getElementById("cmbPARTY_DETAIL").options[document.getElementById("cmbPARTY_DETAIL").selectedIndex].value !=0)	//Modified by Sibin for Itrack Issue 5216 on 22 
					document.getElementById("hidPARTY_DETAIL").value = document.getElementById("cmbPARTY_DETAIL").value;//Added by Sibin for Itrack Issue 5055 on 17 Dec 08	
				else
				   document.getElementById("hidPARTY_DETAIL").value=0;
				// Modified uptil here
				document.getElementById("txtAGE").value = array[i++];
				document.getElementById("txtEXTENT_OF_INJURY").value = array[i++];
				document.getElementById("txtOTHER_DETAILS").value = array[i++];
				//document.getElementById("txtBANK_NAME").value = array[i++];
				document.getElementById("txtACCOUNT_NUMBER").value = array[i++];
				document.getElementById("txtACCOUNT_NAME").value = array[i++];
				document.getElementById("txtCONTACT_PHONE_EXT").value = array[i++];
				document.getElementById("txtCONTACT_FAX").value = array[i++];
				document.getElementById("hidPARENT_ADJUSTER_ID").value = array[i++];//Added by Sibin for Itrack Issue 5055 on 17 Dec 08	
				document.getElementById("txtPARENT_ADJUSTER").value = array[i++];	
//				document.getElementById("txtMAIL_1099_NAME").value = array[i++];	
//				document.getElementById("txtMAIL_1099_ADD1").value = array[i++];	
//				document.getElementById("txtMAIL_1099_ADD2").value = array[i++];	
//				document.getElementById("txtMAIL_1099_CITY").value = array[i++];	
//				SelectComboOption("cmbMAIL_1099_STATE",array[i++]);									
//				SelectComboOption("cmbMAIL_1099_COUNTRY",array[i++]);									
//				SelectComboOption("cmbW9_FORM",array[i++]);									
//				document.getElementById("txtMAIL_1099_ZIP").value = array[i++];	
//				document.getElementById("capFEDRERAL_ID_HID").innerHTML = array[i++];
				//document.getElementById("txtFEDRERAL_ID").value = '';
//				if(document.getElementById("hidFEDRERAL_ID").value != '')
//					FEDRERAL_ID_hide();
//				else
//					FEDRERAL_ID_change();
				HideShowTypeDesc();			
			}
		}
		function FetchData()
		{
			if(document.getElementById("hidCONCAT_DATA").value=="" || document.getElementById("hidCONCAT_DATA").value=="0")
				return;
			var Concat_String = new String(document.getElementById("hidCONCAT_DATA").value);
			var array = Concat_String.split('^');
			if(array.length>0)
			{
				document.getElementById("txtNAME").value = array[0];
				document.getElementById("txtADDRESS1").value = array[1];
				document.getElementById("txtADDRESS2").value = array[2];
				document.getElementById("txtCITY").value = array[3];
				SelectComboOption("cmbSTATE",array[4]);	
				document.getElementById("txtZIP").value = array[5];
				
				if(parseInt(document.getElementById('cmbPARTY_TYPE_ID').value)==ExpertServiceProviders)			
					document.getElementById('hidEXPERT_SERVICE_ID').value=array[6];				
				
				ChangeColor();
			}
			//alert(document.getElementById('hidEXPERT_SERVICE_ID').value);
			
		}
		function Fetch_Additional_Insured_Data()
		{
			if(document.getElementById("hidCONCAT_DATA").value=="" || document.getElementById("hidCONCAT_DATA").value=="0")
				return;
			var Concat_String = new String(document.getElementById("hidCONCAT_DATA").value);
			var array = Concat_String.split('^');
			if(array.length>0)
			{
				document.getElementById("txtNAME").value = array[0];
				document.getElementById("txtADDRESS1").value = array[1];
				document.getElementById("txtADDRESS2").value = array[2];
				document.getElementById("txtCITY").value = array[3];
				SelectComboOption("cmbSTATE",array[4]);	
				document.getElementById("txtZIP").value = array[5];
				document.getElementById("txtCONTACT_PHONE").value = array[6];
				document.getElementById("txtCONTACT_EMAIL").value = array[7];
				document.getElementById("txtCONTACT_PHONE_EXT").value = array[8];
				
				if(parseInt(document.getElementById('cmbPARTY_TYPE_ID').value)==ExpertServiceProviders)			
					document.getElementById('hidEXPERT_SERVICE_ID').value=array[6];				
				
				ChangeColor();
			}
			//alert(document.getElementById('hidEXPERT_SERVICE_ID').value);
			
		}
		function GenerateVendorCode()
		{
			if(document.getElementById('hidPARTY_ID').value=="New" && (document.getElementById('hidEXPERT_SERVICE_ID').value=="" || document.getElementById('hidEXPERT_SERVICE_ID').value=="NEW"))
			{
				var VendorCode = GenerateRandomCode(document. getElementById('txtNAME').value,"");
				//document.getElementById('txtVENDOR_CODE').value = VendorCode;
			}
		}
//		function MakeExpertDataMandatory(flag)
//		{
//			var DisplayStyle="none";
//			var CityDisplayStyle="inline";			
//			if(flag)
//			{
//				DisplayStyle = "inline";
//				CityDisplayStyle = "none";				
//			}	
//			document.getElementById("capVENDOR_CODE").style.display=DisplayStyle;
//			document.getElementById("txtVENDOR_CODE").style.display=DisplayStyle;
//			document.getElementById("spnVENDOR_CODE").style.display=DisplayStyle;
//			document.getElementById("spnPROCESSING_OPTION_1099").style.display=DisplayStyle;
//			document.getElementById("spnFEDRERAL_ID").style.display=DisplayStyle;
//			//document.getElementById("spnNAME").style.display=DisplayStyle;
//			//document.getElementById("spnNAME").style.display=DisplayStyle;			
//			//document.getElementById("spnCITY").style.display=CityDisplayStyle;
//			document.getElementById("trEXPERT_SERVICE_TYPE").style.display=DisplayStyle;
//			
//			EnableValidator("rfvPROCESSING_OPTION_1099",flag);
//			EnableValidator("rfvFEDRERAL_ID",flag);
//			if(document.getElementById('txtFEDRERAL_ID').style.display == 'none')
//				EnableValidator("rfvFEDRERAL_ID",false);
//			//EnableValidator("revFEDRERAL_ID",flag);
//			EnableValidator("rfvVENDOR_CODE",flag);				
//			//EnableValidator("rfvNAME",flag);					
//			//EnableValidator("rfvCITY",!flag);			
//			EnableValidator("rfvEXPERT_SERVICE_TYPE",flag);				
//			EnableValidator("rfvEXPERT_SERVICE_TYPE_DESC",flag);
//			
//			// Added by Mohit Agarwal 22 Sep 08 ITrack 4784	
//			EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),flag);				
//			EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),flag);				
//			EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),flag);				
//			EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),flag);				
//			EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),flag);				
//			EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),flag);						
//			EnableDisableEFT(document.getElementById('cmbW9_FORM'),document.getElementById('rfvW9_FORM'),document.getElementById('spnW9_FORM'),flag);

//			var cmbPROCESSValue   = document.getElementById('cmbPROCESSING_OPTION_1099').selectedIndex;

//			if(cmbPROCESSValue != 0 && cmbPROCESSValue != -1)
//				MakeNonMandatoryFields();
//	
//			HideShowTypeDesc();
//			ChangeColor();
//			
//		}
		
		function EnableLookUp(o, init)
		{
			var SelectedOption = parseInt(o.value);
			var enableLookUp   = false;
			//document.getElementById('hidEXPERT_SERVICE_ID').value="";
			
			/*Commented by Asfa (28-Apr-2008) - iTrack issue #4091
			if(SelectedOption==Claimant || SelectedOption==ExpertServiceProviders || SelectedOption==Additional_Insured)
			*/
			if(SelectedOption==ExpertServiceProviders || SelectedOption==Additional_Insured)
				enableLookUp=true;
//			if(SelectedOption==Adjuster)
//			{
//				EnableValidator("revSA_ZIPCODE",true);
//				EnableValidator("revSA_PHONE",true);
//				EnableValidator("revSA_FAX",true);
//				document.getElementById("tbSubAdjusterDetails").style.display="inline";
//			}
//			else
//			{
//				EnableValidator("revSA_ZIPCODE",false);
//				EnableValidator("revSA_PHONE",false);
//				EnableValidator("revSA_FAX",false);
//				document.getElementById("tbSubAdjusterDetails").style.display="none";
//			}
			/*if(SelectedOption==Claimant)
			{
				document.getElementById('rfvNAME').setAttribute("enabled",true);
				document.getElementById('rfvNAME').setAttribute("isValid",true);								
				document.getElementById('spnNAME').style.display = "inline";
			}
			else
			{
				document.getElementById('rfvNAME').setAttribute("enabled",false);
				document.getElementById('rfvNAME').setAttribute("isValid",false);				
				document.getElementById('rfvNAME').style.display = "none";
				document.getElementById('spnNAME').style.display = "none";
			}*/			
			if(SelectedOption==ExpertServiceProviders)
			{
				if(document.getElementById('hidEXPERT_SERVICE_ID').value=="")
					document.getElementById('hidEXPERT_SERVICE_ID').value="NEW";
				//MakeExpertDataMandatory(true);			
			}
//			else
//				MakeExpertDataMandatory(false);
			
				
			document.getElementById('imgLookup').style.display = enableLookUp ? '':'None';
			var oName = document.getElementById("txtNAME");
			
			if(init)
			{
				if(SelectedOption==Other)							
					EnableOtherPartyDesc(true);
				else
					EnableOtherPartyDesc(false);
				return;
			}
				
			
			var arrData = document.getElementById("hidDefaultData").value.toString().split('^^^^');
			
			if(SelectedOption==Other)
			{
				oName.value="";
				EnableOtherPartyDesc(true);
			}
			else if(SelectedOption==Insured){
				
				var Insured_Name	='';
				var Insured_Address1='';
				var Insured_Address2='';
				var Insured_City	='';
				var Insured_State	='';
				var Insured_Country	='';
				var Insured_ZipCode	='';
				var Insured_Phone	='';
				var Insured_EMail	='';
				
			
				Insured_Name	=arrData[0];
				Insured_Address1=arrData[1];
				Insured_Address2=arrData[2];
				Insured_City	=arrData[3];
				Insured_State	=arrData[4];
				Insured_Country	=arrData[5];
				Insured_ZipCode	=arrData[6];
				Insured_Phone	=arrData[7];
				Insured_EMail	=arrData[8];
				
				document.getElementById('txtNAME').value		 = Insured_Name;				
				document.getElementById('txtADDRESS1').value	 = Insured_Address1;
				document.getElementById('txtADDRESS2').value	 = Insured_Address2;
				document.getElementById('txtCITY').value		 = Insured_City;
				document.getElementById('txtZIP').value			 = Insured_ZipCode;
				document.getElementById('txtCONTACT_PHONE').value= Insured_Phone;
				document.getElementById('txtCONTACT_EMAIL').value= Insured_EMail;
				
				document.getElementById('cmbSTATE').options.selectedIndex = -1;
				//document.getElementById('cmbCOUNTRY').options.selectedIndex = -1;
				
				SelectListOptions('cmbSTATE'	, Insured_State);
				SelectListOptions('cmbCOUNTRY'	, Insured_Country);
				EnableOtherPartyDesc(false);
			}
			else if(SelectedOption==Claimant || SelectedOption==Additional_Insured)
			{
				//oName.value=arrData[9];	
				oName.value='';	
				EnableOtherPartyDesc(false);
			}
			else
			{
				EnableOtherPartyDesc(false);	
				SetBlank();
			}
			
			EnableDisableCtrls();			
		}
		
		function CopyData(fromFieldID,ToFieldID)
		{
			
				if (document.getElementById('chkCopyData').checked == true)
				{ 
						var str=new String(fromFieldID);
   						if (str.substr(0,3)=='cmb')
						{
							if (document.getElementById(fromFieldID).options.selectedIndex != -1)
							{
							var selectedvalue;
							selectedvalue=document.getElementById(fromFieldID).options[document.getElementById(fromFieldID).options.selectedIndex].value;
							SelectComboOption(ToFieldID,selectedvalue);
							} 
						}
						else
						{
						document.getElementById(ToFieldID).value =  document.getElementById(fromFieldID).value;   
						}   
				}
				else
				{
				document.getElementById(ToFieldID).value = '';
				}
			
		}

		function SetBlank()
		{
			document.getElementById('txtNAME').value				= '';				
			document.getElementById('txtPARENT_ADJUSTER').value		= '';				
			document.getElementById('txtADDRESS1').value			= '';				
			document.getElementById('txtADDRESS2').value			= '';				
			document.getElementById('txtCITY').value				= '';				
			document.getElementById('txtZIP').value					= '';				
			document.getElementById('txtCONTACT_NAME').value		= '';				
			document.getElementById('txtCONTACT_PHONE').value		= '';				
			document.getElementById('txtCONTACT_PHONE_EXT').value	= '';				
			document.getElementById('txtCONTACT_FAX').value			= '';				
			document.getElementById('txtCONTACT_EMAIL').value		= '';				
			document.getElementById('txtAGE').value					= '';				
			document.getElementById('txtEXTENT_OF_INJURY').value	= '';				
			document.getElementById('txtOTHER_DETAILS').value		= '';				
			//document.getElementById('txtBANK_NAME').value			= '';				
			document.getElementById('txtACCOUNT_NUMBER').value		= '';				
			document.getElementById('txtACCOUNT_NAME').value		= '';				
			//document.getElementById('txtFEDRERAL_ID').value			= '';				
			//document.getElementById('txtMASTER_VENDOR_CODE').value	= '';	
			//document.getElementById('txtSUB_ADJUSTER').value	= '';	
			//document.getElementById('txtSUB_ADJUSTER_CONTACT_NAME').value	= '';	
//			document.getElementById('txtSA_ADDRESS1').value	= '';	
//			document.getElementById('txtSA_ADDRESS2').value	= '';	
//			document.getElementById('txtSA_CITY').value	= '';	
//			document.getElementById('txtSA_FAX').value	= '';	
//			document.getElementById('txtSA_ZIPCODE').value	= '';	
//			document.getElementById('txtSA_PHONE').value	= '';	
			
//			document.getElementById('cmbSA_STATE').options.selectedIndex				= -1;						
//			document.getElementById('cmbSTATE').options.selectedIndex					= -1;
//			document.getElementById('cmbPARTY_DETAIL').options.selectedIndex			= 0;
//			document.getElementById('cmbPROCESSING_OPTION_1099').options.selectedIndex	= -1;

//			document.getElementById('txtMAIL_1099_NAME').value  = '';
//			document.getElementById('txtMAIL_1099_ADD1').value  = '';
//			document.getElementById('txtMAIL_1099_ADD2').value  = '';
//			document.getElementById('txtMAIL_1099_CITY').value  = '';
//			document.getElementById('txtMAIL_1099_ZIP').value  = '';
//			document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex = 0;
//			document.getElementById('cmbMAIL_1099_STATE').options.selectedIndex = -1;
//			document.getElementById('cmbW9_FORM').options.selectedIndex = -1;
			//FEDRERAL_ID_change();

		}
		function EnableOtherPartyDesc(flag)
		{
			if(flag)
			{
				document.getElementById('txtPARTY_TYPE_DESC').style.display="inline";
				document.getElementById('capPARTY_TYPE_DESC').style.display="inline";
				document.getElementById('spnPARTY_TYPE_DESC').style.display="inline";
				EnableValidator('rfvPARTY_TYPE_DESC',true);
			}
			else
			{
				document.getElementById('txtPARTY_TYPE_DESC').style.display="none";
				document.getElementById('capPARTY_TYPE_DESC').style.display="none";
				document.getElementById('spnPARTY_TYPE_DESC').style.display="none";
				EnableValidator('rfvPARTY_TYPE_DESC',false);
			}
		}
		function EnableDisableCtrls(SelectedOption)
		{
			var SelectedOption = parseInt(document.getElementById('cmbPARTY_TYPE_ID').value);
			
			if(SelectedOption==Witness || SelectedOption==Claimant || SelectedOption==Injured || SelectedOption==Additional_Insured)
			{
				/*Commented by Asfa (15-Jan-2008) - iTrack issue #3381
				document.getElementById('txtEXTENT_OF_INJURY').value = '';
				*/
				document.getElementById('cmbPARTY_DETAIL').setAttribute("disabled",false);
				/*if(SelectedOption==Claimant)
					document.getElementById('txtEXTENT_OF_INJURY').disabled = false;
				else{
					document.getElementById('txtEXTENT_OF_INJURY').text = '';
					document.getElementById('txtEXTENT_OF_INJURY').disabled = true;					
				}*/
			}
			else
			{
				if(SelectedOption != ExpertServiceProviders)//Added by Sibin for Itrack Issue 5055 on 17 Dec 08
				document.getElementById('cmbPARTY_DETAIL').selectedIndex = -1;
				document.getElementById('cmbPARTY_DETAIL').setAttribute("disabled",true);	
				
				//document.getElementById('txtEXTENT_OF_INJURY').value = '';
				//document.getElementById('txtEXTENT_OF_INJURY').disabled = true;				
			}
			
		}
		function AddData()
		{
			
			DisableValidators();
			
			var arrData = document.getElementById("hidDefaultData").value.toString().split('^^^^');
			document.getElementById('hidPARTY_ID').value	=	'New';			
			document.getElementById('txtNAME').value		 = '';
			document.getElementById('txtADDRESS1').value	 = '';
			document.getElementById('txtADDRESS2').value	 = '';
			document.getElementById('txtCITY').value		 = '';
			document.getElementById('txtZIP').value			 = '';
			document.getElementById('txtCONTACT_PHONE').value= '';
			document.getElementById('txtCONTACT_EMAIL').value= '';
			document.getElementById('txtOTHER_DETAILS').value= '';
			document.getElementById('txtCONTACT_FAX').value= '';
			document.getElementById('txtCONTACT_PHONE_EXT').value= '';			
			document.getElementById('cmbSTATE').options.selectedIndex = -1;
			//document.getElementById('cmbCOUNTRY').options.selectedIndex = -1;
			document.getElementById('cmbPARTY_TYPE_ID').options.selectedIndex = -1;
			document.getElementById('cmbPARTY_DETAIL').options.selectedIndex = -1;
			document.getElementById('txtAGE').value= '';
			document.getElementById('txtEXTENT_OF_INJURY').value= '';
		//	document.getElementById('txtBANK_NAME').value= '';
			document.getElementById('txtACCOUNT_NUMBER').value= '';
			document.getElementById('txtACCOUNT_NAME').value= '';
			document.getElementById('txtPARTY_TYPE_DESC').value= '';	
			//document.getElementById('txtFEDRERAL_ID').value ='';		
			if(document.getElementById('cmbPARTY_TYPE_ID').value != '')			
				document.getElementById('cmbPARTY_TYPE_ID').focus();	
					
//			document.getElementById('txtMAIL_1099_NAME').value  = '';
//			document.getElementById('txtMAIL_1099_ADD1').value  = '';
//			document.getElementById('txtMAIL_1099_ADD2').value  = '';
//			document.getElementById('txtMAIL_1099_CITY').value  = '';
//			document.getElementById('txtMAIL_1099_ZIP').value  = '';
//			document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex = 0;
//			document.getElementById('cmbMAIL_1099_STATE').options.selectedIndex = -1;
//			document.getElementById('cmbW9_FORM').options.selectedIndex = -1;	
			//FEDRERAL_ID_change();
		}		
		function populateXML()
		{
			//MakeNonMandatoryFields(); 
			
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				var tempXML;
				
				//if(top.frames[1].strXML!="")
				if(document.getElementById("hidOldData").value!="" && document.getElementById("hidOldData").value!="0")
				{
					//document.getElementById('btnReset').style.display='none';
					tempXML=document.getElementById("hidOldData").value;
					//Storing the XML in hidRowId hidden fields
					//document.getElementById('hidOldData').value		=	 tempXML;

					populateFormData(tempXML, CLM_PARTIES);
					document.getElementById("txtACCOUNT_NUMBER").value = document.getElementById("hidNEW_ACCOUNT_NUMBER").value; //Added by aditya for TFS BUG # 2522 
					EnableDisableCtrls();
					//FEDRERAL_ID_hide();
				}
				else
				{
					AddData();
				}				
			}
//			else
//			{
//				if(document.getElementById('hidFEDRERAL_ID').value != '')
//					FEDRERAL_ID_hide();
//				else
//					FEDRERAL_ID_change();
//			}

			//MakeNonMandatoryFields();

			return false;
		}		
		function InitPartyDetails()
		{
			populateXML();			
			EnableLookUp(document.getElementById('cmbPARTY_TYPE_ID'),true);
			HideShowTypeDesc();
			ApplyColor();
			ChangeColor();			
		}
		function OpenParentAdjusterLookup()
		{	
			var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";
			document.getElementById('hidCONCAT_DATA').value='';
			var srClaimID = "<%=gStrClaimID%>";
			var strHeader = document.getElementById('hidHeader').value;
			//OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','','FetchParentAdjusterData()');			
			OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyNameClaims_ParentAdjuster',strHeader,'@CLAIM_ID=\''  + srClaimID + '\'','FetchParentAdjusterData()');
		}
		function FetchParentAdjusterData()
		{
			if(document.getElementById("hidCONCAT_DATA").value=="" || document.getElementById("hidCONCAT_DATA").value=="0")
				return;
			var Concat_String = new String(document.getElementById("hidCONCAT_DATA").value);
			var array = Concat_String.split('^');
			if(array.length>0)
			{
				document.getElementById("txtPARENT_ADJUSTER").value = array[0];
				document.getElementById("hidPARENT_ADJUSTER_ID").value = array[6];				
			}

            }


            function zipcodeval() {

                if (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '6') {
                    document.getElementById('revZIP').setAttribute('enabled', false);
                }
            }

            function zipcodeval1() {

                if (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5') {
                    document.getElementById('revZIP').setAttribute('enabled', true);
                }
            }

            function FormatZipCode(vr) {


                var vr = new String(vr.toString());
                if (vr != "" && (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5')) {
                    //|| (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {

                    vr = vr.replace(/[-]/g, "");
                    num = vr.length;
                    if (num == 8 && (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5')) {
                        //|| (num == 8 && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        document.getElementById('revZIP').setAttribute('enabled', false);
                        //document.getElementById('revMAIL_1099_ZIP').setAttribute('enabled', false);

                    }


                }

                return vr;
            }

		
		
//		function MakeNonMandatoryFields()
//		{
//			var selIndex = document.getElementById('cmbPROCESSING_OPTION_1099').selectedIndex;
//			if(selIndex != 0 && selIndex != -1)
//			{
//		
//				var cmbPROCESSValue   = document.getElementById('cmbPROCESSING_OPTION_1099').options[document.getElementById('cmbPROCESSING_OPTION_1099').selectedIndex].value;
//				if(cmbPROCESSValue == 11733 || cmbPROCESSValue == 11734)
//				{
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),false);				
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),false);				
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),false);				
//					EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),false);				
//					EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),false);				
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),false);				
//					EnableDisableEFT(document.getElementById('cmbW9_FORM'),document.getElementById('rfvW9_FORM'),document.getElementById('spnW9_FORM'),false);				
//					
//				}
//				else
//				{
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),true);				
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),true);				
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),true);				
//					EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),true);				
//					EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),true);				
//					EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),true);						
//					EnableDisableEFT(document.getElementById('cmbW9_FORM'),document.getElementById('rfvW9_FORM'),document.getElementById('spnW9_FORM'),true);				
//				}
//			}
//			else
//			{
//				EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),false);				
//				EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),false);				
//				EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),false);				
//				EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),false);				
//				EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),false);				
//				EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),false);				
//				EnableDisableEFT(document.getElementById('cmbW9_FORM'),document.getElementById('rfvW9_FORM'),document.getElementById('spnW9_FORM'),false);				
//			}
//			if(document.getElementById('txtMAIL_1099_ADD1').value != '')
//			{
//				document.getElementById('chkCopyData').setAttribute('disabled',true);	
//				//alert(document.getElementById('chkCopyData').disabled);
//			}
//		}

		////////////
		function EnableDisableEFT(txtDesc,rfvDesc,spnDesc,flag)
		{		
			
			if (flag==false)
			{			
				if(rfvDesc!=null)
				{					
					rfvDesc.setAttribute('enabled',false);	
					rfvDesc.setAttribute('isValid',false);					
					rfvDesc.style.display="none";	
					spnDesc.style.display = "none";	
					txtDesc.className = "";	
				//	txtDesc.value="";		
				}					
			}
			else
			{	
				if(rfvDesc!=null)
				{
					rfvDesc.setAttribute('enabled',true);					
					rfvDesc.setAttribute('isValid',true);
					spnDesc.style.display = "inline";
					txtDesc.className = "MandatoryControl";
				}
			}	
			//alert(spnDesc.id + " style " + spnDesc.style.display);
			ChangeColor();
		}

//			function FEDRERAL_ID_change()
//			{
//				document.getElementById('txtFEDRERAL_ID').value = '';
//				document.getElementById('txtFEDRERAL_ID').style.display = 'inline';
//				//document.getElementById("rfvFEDRERAL_ID").setAttribute('enabled',true); - Commented by Sibin for Itrack Issue 5157 on 9 Dec 08
//				document.getElementById("revFEDRERAL_ID").setAttribute('enabled',true);
//				if(document.getElementById("btnFEDRERAL_ID").value == 'Edit')
//					document.getElementById("btnFEDRERAL_ID").value = 'Cancel';
//				else if(document.getElementById("btnFEDRERAL_ID").value == 'Cancel')
//					FEDRERAL_ID_hide();
//				else
//					document.getElementById("btnFEDRERAL_ID").style.display = 'none';
//					
//			}
//			
//			function FEDRERAL_ID_hide()
//			{
//				document.getElementById("btnFEDRERAL_ID").style.display = 'inline';
//				document.getElementById('txtFEDRERAL_ID').value = '';
//				document.getElementById('txtFEDRERAL_ID').style.display = 'none';
//				document.getElementById("rfvFEDRERAL_ID").style.display='none';
//				document.getElementById("rfvFEDRERAL_ID").setAttribute('enabled',false);
//				document.getElementById("revFEDRERAL_ID").style.display='none';
//				document.getElementById("revFEDRERAL_ID").setAttribute('enabled',false);
//				document.getElementById("btnFEDRERAL_ID").value = 'Edit';
//			}

	//Added functions for Itrack Issue 5055 on 20 Nov 08 to fill Master Vendor Code TextBox
		/*function OpenMasterVendorCodeLookup()
		{	
			var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";
			document.getElementById('hidCONCAT_DATA').value='';					
			var srClaimID = "<%=gStrClaimID%>";	
			OpenLookupWithFunction( URL,'EXPERT_SERVICE_MASTER_VENDOR_CODE','EXPERT_SERVICE_MASTER_VENDOR_CODE','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','@CLAIM_ID=\''  + srClaimID + '\'','FetchMasterVendorCode()');
		}*/ //Commented by Sibin for Itrack Issue 5157 on 8 Dec 08
		
		//Added functions for Itrack Issue 5055 on 8 Dec 08 to fill Master Vendor Code TextBox
		function OpenMasterVendorCodeLookup()
			{	
				var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";						
				OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','','FetchMasterVendorCode()');
				
			}
		
		function FetchMasterVendorCode()
		{	//Commented by Sibin for Itrack Issue 5157 on 8 Dec 08
			/*if(document.getElementById("hidCONCAT_DATA").value=="" || document.getElementById("hidCONCAT_DATA").value=="0")
				return;
			var Concat_String = new String(document.getElementById("hidCONCAT_DATA").value);
				document.getElementById("txtMASTER_VENDOR_CODE").value =Concat_String;*/
				
				if(document.getElementById("hidCONCAT_DATA").value=="" || document.getElementById("hidCONCAT_DATA").value=="0")
					return;
				var Concat_String = new String(document.getElementById("hidCONCAT_DATA").value);				
				var array = Concat_String.split('^');				
				if(array.length>0)
				{
					document.getElementById("hidMASTER_VENDOR_CODE_ID").value = array[6];
					document.getElementById("txtMASTER_VENDOR_CODE").value = array[7];
					
				}	
		}



		//validate CPf / CNPJ #;
		//For personal customer it accepts 14 digits CPF No
		//And For Commercial Customer it must bus accept only 18 digit CNPJ NO
		//we call a common function validar() for validate both CPF/CNPJ No

		function validatCPF_CNPJ(objSource, objArgs) {
		    //get error message for xml on culture base. 
		    var cpferrormsg = '<%=javasciptCPFmsg %>';
		    var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
		    var CPF_invalid = '<%=CPF_invalid %>';
		    var CNPJ_invalid = '<%=CNPJ_invalid %>';
		    
		    var valid = false;
		    var idd = objSource.id;
		    var rfvid = idd.replace('csv', 'rev');
		    if (document.getElementById(rfvid) != null)
		        if (document.getElementById(rfvid).isvalid == true) {
		        var theCPF = document.getElementById(objSource.controltovalidate)
		        var len = theCPF.value.length;
		        if (document.getElementById('cmbPARTY_TYPE').value == '11110') {

		            //for CPF # in if customer type is personal
		            //it check cpf format & valdate bia validar() function, CPF is valid or not
		            if (len == '14') {
		                valid = validar(objSource, objArgs);
		                if (valid) { objSource.innerText = ''; } else { objSource.innerText = CPF_invalid; }
		            }
		            else {

		                if (document.getElementById(rfvid) != null) {
		                    if (document.getElementById(rfvid).isvalid == true) {
		                        objArgs.IsValid = false;
		                        objSource.innerHTML = cpferrormsg; //'Please enter 14 digit CPF No';
		                    } else {
		                        objSource.innerHTML = '';
		                    }
		                }
		            }
		        } //for CNPJ # in if customer type is commercial
		        //it check CNPJ format & valdate bia validar() function CNPJ is valid or not
		        else if (document.getElementById('cmbPARTY_TYPE').value == '11109' || document.getElementById('cmbPARTY_TYPE').value == '14725') {
		            if (len == '18') {
		                valid = validar(objSource, objArgs);
		                if (!valid) {
		                    objSource.innerText = CNPJ_invalid;
		                } else { objSource.innerText = ''; }
		            }
		            else {
		                if (document.getElementById(rfvid) != null) {
		                    if (document.getElementById(rfvid).isvalid == true) {
		                        objArgs.IsValid = false;
		                        objSource.innerHTML = cnpjerrormsg; //'validate';
		                    } else { objSource.innerHTML = ''; }
		                }
		            }
		        }
		    } else { objSource.innerHTML = ''; }
		}



		function OnCustomerTypeChange() {

		    if (document.getElementById('cmbPARTY_TYPE').options.selectedIndex == -1)
		        document.getElementById('cmbPARTY_TYPE').options.selectedIndex = 0;
		    if (document.getElementById('cmbPARTY_TYPE').options[document.getElementById('cmbPARTY_TYPE').options.selectedIndex].value == '11110') {
		        //Type is personal

		        document.getElementById("txtPARTY_CPF_CNPJ").setAttribute('maxLength', '14');		      
		        document.getElementById("revCPF_CNPJ").innerText = document.getElementById("revCPF_CNPJ").getAttribute("ErrMsgcpf");
		      

		    }
		    else {
		        //Type is commercial

		        //Changing error message of validation control

		        document.getElementById("txtPARTY_CPF_CNPJ").setAttribute('maxLength', '18');
		        document.getElementById("revCPF_CNPJ").innerText = document.getElementById("revCPF_CNPJ").getAttribute("ErrMsgCNPJ");
		        
		    }



		}



		//function validate CPF/CNPJ # .
		//created by Brazil team 
		function validar(objSource, objArgs) {

		    var theCPF = document.getElementById(objSource.controltovalidate)
		    var errormsg = '<%=javasciptmsg  %>'
		    var ermsg = errormsg.split(',');
		    var intval = "0123456789";
		    var val = theCPF.value;
		    var flag = false;
		    var realval = "";
		    for (l = 0; l < val.length; l++) {
		        ch = val.charAt(l);
		        flag = false;
		        for (m = 0; m < intval.length; m++) {
		            if (ch == intval.charAt(m)) {
		                flag = true;
		                break;
		            }
		        } if (flag)
		            realval += val.charAt(l);
		    }

		    if (((realval.length == 11) && (realval == 11111111111) || (realval == 22222222222) || (realval == 33333333333) || (realval == 44444444444) || (realval == 55555555555) || (realval == 66666666666) || (realval == 77777777777) || (realval == 88888888888) || (realval == 99999999999) || (realval == 00000000000))) {

		        objArgs.IsValid = false;
		        objSource.innerHTML = ermsg[1];
		        return (false);
		    }

		    if (!((realval.length == 11) || (realval.length == 14))) {
		        objSource.innerHTML = ermsg[1];
		        objArgs.IsValid = false;
		        return (false);
		    }

		    var checkOK = "0123456789";
		    var checkStr = realval;
		    var allValid = true;
		    var allNum = "";
		    for (i = 0; i < checkStr.length; i++) {
		        ch = checkStr.charAt(i);
		        for (j = 0; j < checkOK.length; j++)
		            if (ch == checkOK.charAt(j))
		            break;
		        if (j == checkOK.length) {
		            allValid = false;
		            break;
		        }
		        allNum += ch;
		    }
		    if (!allValid) {
		        objSource.innerHTML = ermsg[2];
		        objArgs.IsValid = false;
		        return (false);
		    }

		    var chkVal = allNum;
		    var prsVal = parseFloat(allNum);
		    if (chkVal != "" && !(prsVal > "0")) {
		        objSource.innerHTML = ermsg[3];
		        objArgs.IsValid = false;
		        return (false);
		    }

		    if (realval.length == 11) {
		        var tot = 0;
		        for (i = 2; i <= 10; i++)
		            tot += i * parseInt(checkStr.charAt(10 - i));

		        if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(9))) {
		            objSource.innerHTML = ermsg[1];
		            objArgs.IsValid = false;
		            return (false);
		        }

		        tot = 0;

		        for (i = 2; i <= 11; i++)
		            tot += i * parseInt(checkStr.charAt(11 - i));
		        if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(10))) {
		            objSource.innerHTML = ermsg[1];
		            objArgs.IsValid = false;
		            return (false);
		        }
		    }
		    else {
		        var tot = 0;
		        var peso = 2;

		        for (i = 0; i <= 11; i++) {
		            tot += peso * parseInt(checkStr.charAt(11 - i));
		            peso++;
		            if (peso == 10) {
		                peso = 2;
		            }
		        }

		        if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(12))) {
		            objSource.innerHTML = ermsg[1];
		            objArgs.IsValid = false;
		            return (false);
		        }

		        tot = 0;
		        peso = 2;

		        for (i = 0; i <= 12; i++) {
		            tot += peso * parseInt(checkStr.charAt(12 - i));
		            peso++;
		            if (peso == 10) {
		                peso = 2;
		            }
		        }

		        if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(13))) {
		            objSource.innerHTML = ermsg[1];
		            objArgs.IsValid = false;
		            return (false);
		        }
		    }
		    return (true);
		}
	//Added till here
		</script>
</HEAD>
	<BODY oncontextmenu="return false;" leftMargin='0' topMargin='0' onload="InitPartyDetails();OnCustomerTypeChange();">
		<FORM id='CLM_PARTIES' method='post' runat='server'>
		 <P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
        <TBODY>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label id="capheaders" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPARTY_TYPE_ID" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbPARTY_TYPE_ID" onChange="EnableLookUp(this,false);" runat='server'></asp:DropDownList><br>
									<asp:requiredfieldvalidator id="rfvPARTY_TYPE_ID" runat="server" Display="Dynamic" ControlToValidate="cmbPARTY_TYPE_ID"></asp:requiredfieldvalidator>
								</TD>
								<td class="midcolora" width="18%"><asp:label id="capPARTY_TYPE_DESC" runat="server"></asp:label><span class="mandatory" id="spnPARTY_TYPE_DESC">*</span></td>
									<td class="midcolora" width="32%"><asp:textbox id="txtPARTY_TYPE_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvPARTY_TYPE_DESC" runat="server" ControlToValidate="txtPARTY_TYPE_DESC"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>							
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capNAME" runat="server"></asp:Label><span class="mandatory" id="spnNAME">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtNAME' runat='server' size="40" onBlur="javascript:GenerateVendorCode();return false;" maxlength="60"></asp:textbox>
									<img src="../../cmsweb/images/selecticon.gif" id="imgLookup" style="DISPLAY: none;CURSOR: hand"
										onclick="javascript:DoLookUp();"><br>
									<asp:RequiredFieldValidator ID="rfvNAME" Runat="server" ControlToValidate="txtNAME" Display="Dynamic"></asp:RequiredFieldValidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capPARENT_ADJUSTER" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPARENT_ADJUSTER" ReadOnly="True" runat="server" maxlength="15" size="17"></asp:textbox>
											<IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"												
													runat="server">
								</TD>
							</tr>
							<tr id="trEXPERT_SERVICE_TYPE">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_TYPE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXPERT_SERVICE_TYPE" onfocus="SelectComboIndex('cmbEXPERT_SERVICE_TYPE')"
											runat="server"></asp:dropdownlist><br>
										
									<td class="midcolora"><asp:label id="capEXPERT_SERVICE_TYPE_DESC" runat="server"></asp:label></td>
									<td class="midcolora">
											<asp:TextBox ID="txtEXPERT_SERVICE_TYPE_DESC" Runat="server" MaxLength="35" size="40"></asp:TextBox>
										<br>
										
								</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capADDRESS1" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtADDRESS1' runat='server' size='40' maxlength='50'></asp:textbox><br>
									<asp:RegularExpressionValidator id="revADDRESS1" Display="Dynamic" runat="server" ControlToValidate="txtADDRESS1"></asp:RegularExpressionValidator>
								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capADDRESS2" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtADDRESS2' runat='server' size='40' maxlength='50'></asp:textbox><BR>									
									<asp:RegularExpressionValidator id="revADDRESS2" Display="Dynamic" runat="server" ControlToValidate="txtADDRESS2"></asp:RegularExpressionValidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCITY" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtCITY' runat='server' size='40' maxlength='50'></asp:textbox><br>
									<%--<asp:requiredfieldvalidator id="rfvCITY" Display="Dynamic" runat="server" ControlToValidate="txtCITY"></asp:requiredfieldvalidator>--%>
									<%--<asp:RegularExpressionValidator id="revCITY" Display="Dynamic" runat="server" ControlToValidate="txtCITY"></asp:RegularExpressionValidator>--%>
								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capSTATE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbSTATE' OnFocus="SelectComboIndex('cmbSTATE')" runat='server'></asp:DropDownList><br>
									<%--<asp:requiredfieldvalidator id="rfvSTATE" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE"></asp:requiredfieldvalidator>--%>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capZIP" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtZIP' runat='server' size='15' maxlength='8' OnBlur="this.value=FormatZipCode(this.value);zipcodeval();zipcodeval1();"></asp:textbox><br>
									<%--<asp:requiredfieldvalidator id="rfvZIP" runat="server" Display="Dynamic" ControlToValidate="txtZIP"></asp:requiredfieldvalidator>--%>
									<asp:RegularExpressionValidator id="revZIP" Display="Dynamic" runat="server" ControlToValidate="txtZIP"></asp:RegularExpressionValidator>
								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCOUNTRY" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbCOUNTRY" OnFocus="SelectComboIndex('cmbCOUNTRY')" runat='server'></asp:DropDownList><br>
									<%--<asp:requiredfieldvalidator id="rfvCOUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbCOUNTRY"></asp:requiredfieldvalidator>--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCONTACT_NAME" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_NAME" runat="server" maxlength="50" size="40"></asp:textbox>
								</TD>	
								<td class="midcolora" width="18%">
									<asp:Label id="capDISTRICT" runat="server"></asp:Label>
									</td>
									<td class="midcolora" width="32%">
									<asp:textbox id="txtDISTRICT" runat="server" size='20' maxlength='20'></asp:textbox>
									</td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCONTACT_PHONE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtCONTACT_PHONE" runat='server' size='20' maxlength='12' onblur="FormatBrazilPhone()"></asp:textbox><br>
									<asp:RegularExpressionValidator id="revCONTACT_PHONE" Display="Dynamic" runat="server" ControlToValidate="txtCONTACT_PHONE"></asp:RegularExpressionValidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCONTACT_PHONE_EXT" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtCONTACT_PHONE_EXT" runat='server' size='8' maxlength='6'></asp:textbox><br>
									<asp:RegularExpressionValidator id="revCONTACT_PHONE_EXT" Display="Dynamic" runat="server" ControlToValidate="txtCONTACT_PHONE_EXT"></asp:RegularExpressionValidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCONTACT_FAX" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtCONTACT_FAX" runat='server' size='20' maxlength='12' onblur="FormatBrazilPhone()" ></asp:textbox><br>
									<asp:RegularExpressionValidator id="revCONTACT_FAX" Display="Dynamic" runat="server" ControlToValidate="txtCONTACT_FAX"></asp:RegularExpressionValidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCONTACT_EMAIL" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtCONTACT_EMAIL" runat='server' size='40' maxlength='40'></asp:textbox><br>									
									<asp:RegularExpressionValidator id="revCONTACT_EMAIL" Display="Dynamic" runat="server" ControlToValidate="txtCONTACT_EMAIL"></asp:RegularExpressionValidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capMARITAL_STATUS" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbMARITAL_STATUS" 
                                        Onfocus="SelectComboIndex('cmbPARTY_DETAIL')" runat="server"></asp:DropDownList>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capGENDER" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbGENDER" Onfocus="SelectComboIndex('cmbPARTY_DETAIL')" 
                                        runat="server"></asp:DropDownList>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPARTY_DETAIL" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbPARTY_DETAIL" Onfocus="SelectComboIndex('cmbPARTY_DETAIL')" runat="server"></asp:DropDownList><%--Added Onfocus="SelectComboIndex('cmbPARTY_DETAIL')" by Sibin for Itrack Issue 5055 on 17 Dec 08--%>
								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capAGE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtAGE" runat='server' size='4' maxlength='3'></asp:textbox><br>
									<asp:RangeValidator ID="rngAGE" MinimumValue="0" MaximumValue="150" ControlToValidate="txtAGE" Runat="server" Display="Dynamic" ErrorMessage="Error" Type="Integer"></asp:RangeValidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capEXTENT_OF_INJURY" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtEXTENT_OF_INJURY" runat='server' size='40' maxlength='200'></asp:textbox><br>
									<asp:RegularExpressionValidator id="revEXTENT_OF_INJURY" Display="Dynamic" runat="server" ControlToValidate="txtEXTENT_OF_INJURY"></asp:RegularExpressionValidator>
								</TD>							
								<%--<TD class='midcolora' width='18%'><asp:Label id="capREFERENCE" runat="server"></asp:Label></TD>	
								<TD class='midcolora' width='32%'><asp:textbox id="txtREFERENCE" runat='server' size='40' maxlength='30'></asp:textbox></TD>--%>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capOTHER_DETAILS" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtOTHER_DETAILS" runat='server' size='40' 
                                        TextMode="MultiLine" Cols="50" Rows="4" onkeypress="MaxLength(this,500);"  
										maxlength='500' Width="293px"></asp:textbox><br>
									<asp:RegularExpressionValidator id="revOTHER_DETAILS" runat="server" Display="Dynamic" ControlToValidate="txtOTHER_DETAILS"></asp:RegularExpressionValidator>									
								</TD>								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPARTY_TYPE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbPARTY_TYPE" 
                                        runat='server'></asp:DropDownList>
								</TD>							
								<TD class='midcolora' width='18%'>
                                                <asp:label id="capPARTY_CPF_CNPJ" runat="server"></asp:label>
                                                <%--<span class="mandatory" >*</span>--%>
									            </TD>
								<TD class='midcolora' width='32%'>
                                                <asp:textbox id="txtPARTY_CPF_CNPJ" runat="server" 
                        size="32" maxlength="40" Width="120px" OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" Enabled=false ></asp:textbox>
                                                <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvCPF_CNPJ" ErrorMessage="" Display="Dynamic" 
                                                    ControlToValidate="txtPARTY_CPF_CNPJ" Enabled=false></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" 
                                                    ControlToValidate="txtPARTY_CPF_CNPJ" Enabled=false></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvCPF_CNPJ" ErrorMessage="" Display="Dynamic" 
                                                    ControlToValidate="txtPARTY_CPF_CNPJ" 
                                                    ClientValidationFunction="validatCPF_CNPJ" Enabled=false></asp:CustomValidator>
                                                    
                                            </TD>								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capREGIONAL_ID" runat="server"></asp:Label>
									</TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtREGIONAL_ID" runat='server' size='20' maxlength='20' 
                                        AutoCompleteType="Disabled"></asp:textbox>

								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capREGIONAL_ID_ISSUE_DATE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtREGIONAL_ID_ISSUE_DATE" runat='server' size='20' 
                                        maxlength='10' AutoCompleteType="Disabled"></asp:textbox>
                                         <asp:HyperLink ID="hlkREGIONAL_ID_ISSUE_DATE" runat="server" 
                                                    CssClass="HotSpot">
                                                    <asp:Image ID="imgREGIONAL_ID_ISSUE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                                
                                            </asp:HyperLink>
                                            
                                            <asp:RegularExpressionValidator ID="revREGIONAL_ID_ISSUE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtREGIONAL_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                        </TD>								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capREGIONAL_ID_ISSUANCE" runat="server"></asp:Label>
									</TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtREGIONAL_ID_ISSUANCE" runat='server' size='20' 
                                        maxlength='20' AutoCompleteType="Disabled"></asp:textbox>

								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPAYMENT_METHOD" runat="server"></asp:Label>
									</TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbPAYMENT_METHOD" 
                                        Onfocus="SelectComboIndex('cmbPARTY_DETAIL')" runat="server"></asp:DropDownList>
                                        </TD>								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:label id="capACCOUNT_TYPE" runat="server"></asp:label><%--<span class="mandatory">*</span>--%>
									</TD>
								<TD class='midcolora' width='32%'>
                                                 <asp:DropDownList ID="cmbACCOUNT_TYPE" runat="server"></asp:DropDownList>
                                                 <br />
                                                <%-- <asp:RequiredFieldValidator ID="rfvACCOUNT_TYPE" ControlToValidate="cmbACCOUNT_TYPE" runat="server" Display="dynamic"></asp:RequiredFieldValidator>--%>

								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capBANK_NUMBER" runat="server"></asp:Label>
									</TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtBANK_NUMBER" runat='server'  maxlength='5'></asp:textbox>

                                        </TD>								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capBANK_BRANCH" runat="server"></asp:Label>
									</TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtBANK_BRANCH" runat='server' maxlength='10' 
                                        ></asp:textbox>
                                    <br />
                                 <%--<asp:RegularExpressionValidator ID="revBANK_BRANCH" ControlToValidate="txtBANK_BRANCH" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>--%>

								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capACCOUNT_NUMBER" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtACCOUNT_NUMBER" runat='server' size='40' maxlength='20'></asp:textbox>
									
									 <br />
									
									 <%--<asp:RegularExpressionValidator ID="revACCOUNT_NUMBER" ControlToValidate="txtACCOUNT_NUMBER" runat="server" Display="Dynamic" ></asp:RegularExpressionValidator>--%>
                                        </TD>								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capACCOUNT_NAME" runat="server"></asp:Label></TD>	
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtACCOUNT_NAME" runat='server' size='40' maxlength='100'></asp:textbox></TD>
								<TD class='midcolora' width='18%'>
                                    <asp:Label ID="CapPARTY_PERCENTAGE" Visible="false" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
                                    <asp:TextBox ID="txtPARTY_PERCENTAGE" Visible="false" runat="server" size = '40' MaxLength ='100' ReadOnly = "true" ></asp:TextBox></TD>																			
							</TR>
							
							<tr>
                                 <TD class='midcolora' width='18%'>
									<asp:Label id="capIS_BENEFICIARY" runat="server" ></asp:Label></TD>	
                            <td class="midcolora" width="32%">
                            <asp:checkbox id="chkIS_BENEFICIARY" runat="server"></asp:checkbox>
                            <TD class='midcolora' width='18%'>&nbsp;</td>
                            <TD class='midcolora' width='32%'>&nbsp;</td>
             
                            </td>
         
                            </tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capBANK_NAME" runat="server"></asp:Label></TD>	
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtBANK_NAME" runat='server' size='50' maxlength='100'></asp:textbox>
									</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capAGENCY_BANK" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtAGENCY_BANK" runat='server' 
                                        size='40' maxlength='100'></asp:textbox></TD>																			
								</TD>
							</TR>
							<tr>
								<TD class='midcolora' colspan="4">
								
			<asp:Panel ID="PnlPartyOtherDetails" runat="server">
								
								 <table >
								 
								 <tr>
								<TD class='midcolora' width='18%'><asp:label id="capFEDRERAL_ID" runat="server"></asp:label><span class="mandatory" id="spnFEDRERAL_ID">*</span></TD>	
								<TD class='midcolora' width='32%'><asp:label id="capFEDRERAL_ID_HID" runat="server" size="15" maxlength="9"></asp:label><input class="clsButton" id="btnFEDRERAL_ID" text="Edit" onclick="FEDRERAL_ID_change();" type="button"><asp:textbox id="txtFEDRERAL_ID" runat="server" maxlength="9" size="15"></asp:textbox>
			
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capPROCESSING_OPTION_1099" runat="server"></asp:label><span class="mandatory" id="spnPROCESSING_OPTION_1099">*</span></TD>
									<TD class="midcolora" width="32%"></input><asp:dropdownlist id="cmbPROCESSING_OPTION_1099" onfocus="SelectComboIndex('cmbPROCESSING_OPTION_1099')"
										 runat="server"></asp:dropdownlist>
									
								        <br>
									</TD>
							</tr>
							<tr>
								<td class="pageHeader" colSpan="4">
								<asp:Label ID="capAdd" runat="server"></asp:Label>&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkCopyData" runat="server" Text="Copy Default Details"></asp:checkbox>
								</td>
							</tr>
								<TR>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_NAME" runat="server">1099 Business Name</asp:label><span class="mandatory" id="spnMAIL_1099_NAME">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_NAME" runat="server" maxlength="75" size="50"></asp:textbox><BR>
										</TD>
									<TD class="midcolora" width="18%"><asp:label id="capW9_FORM" runat="server">W-9 Form</asp:label><span class="mandatory" id="spnW9_FORM">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbW9_FORM" onfocus="SelectComboIndex('cmbW9_FORM')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										</TD>
							</tr>
							<TR>
								<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ADD1" runat="server">1099 Mail Address1</asp:label><span class="mandatory" id="spnMAIL_1099_ADD1">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ADD1" runat="server" maxlength="70" size="50"></asp:textbox><BR>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ADD2" runat="server">1099 Mail Address2</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ADD2" runat="server" maxlength="70" size="50"></asp:textbox><BR>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_CITY" runat="server">1099 Mail City</asp:label><span class="mandatory" id="spnMAIL_1099_CITY">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_STATE" runat="server">1099 Mail State</asp:label><span class="mandatory" id="spnMAIL_1099_STATE">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMAIL_1099_STATE" onfocus="SelectComboIndex('cmbMAIL_1099_STATE')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									
										</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_COUNTRY" runat="server">1099 Mail Country</asp:label><span class="mandatory" id="spnMAIL_1099_COUNTRY">*</span>
								</TD>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbMAIL_1099_COUNTRY" onfocus="SelectComboIndex('cmbMAIL_1099_COUNTRY')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
								
										</td>
								<TD class="midcolora" width="18%">
								<asp:label id="capMAIL_1099_ZIP" runat="server">1099 Mail Zip</asp:label>
								<span class="mandatory" id="spnMAIL_1099_ZIP">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ZIP" runat="server" maxlength="10" size="13"></asp:textbox><asp:hyperlink id="hlkMAIL_1099_ZIP" runat="server" CssClass="HotSpot">
										<asp:image id="imgMAIL_1099_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink><BR>
								
										</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMASTER_VENDOR_CODE" runat="server"></asp:label>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMASTER_VENDOR_CODE" ReadOnly="True" runat="server" maxlength="15" size="17"></asp:textbox>
										<IMG id="imgMASTER_VENDOR_CODE" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif" runat="server">
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capVENDOR_CODE" runat="server"></asp:label><span class="mandatory" id="spnVENDOR_CODE">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_CODE" runat="server" maxlength="15" size="17"></asp:textbox><BR>
								
									</TD>									
							</tr>
						<tbody id="tbSubAdjusterDetails">
							<tr>
								<TD class="pageHeader" colSpan="4">Sub-Adjuster Details</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER" runat="server" maxlength="35" size="35"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_CONTACT_NAME" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_CONTACT_NAME" runat="server" maxlength="50" size="50"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSA_ADDRESS1" runat="server"></asp:label></TD>
								<TD class="midcolora"><asp:textbox id="txtSA_ADDRESS1" runat="server" size="35" MAXLENGTH="75"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_ADDRESS2" runat="server"></asp:label></TD>
								<TD class="midcolora"><asp:textbox id="txtSA_ADDRESS2" runat="server" size="35" MAXLENGTH="75"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSA_CITY" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_CITY" runat="server" maxlength="75" size="35"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_COUNTRY" runat="server">Country</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSA_COUNTRY" onfocus="SelectComboIndex('cmbSA_COUNTRY')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSA_STATE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSA_STATE" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_ZIPCODE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_ZIPCODE" runat="server" maxlength="10" size="15"></asp:textbox>
								
								<asp:hyperlink id="hlkSAZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgSAZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
								</asp:hyperlink><BR>
									
									</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSA_PHONE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_PHONE" runat="server" maxlength="13" size="15"></asp:textbox><BR>
									
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_FAX" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_FAX" runat="server" maxlength="10" size="15"></asp:textbox><BR>
									
									</TD>
							</tr>
						</tbody>
							
								 </table>
								</asp:Panel>
								
								</TD>	
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>&nbsp;
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text=''></cmsb:cmsbutton>
								</td>								
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE></TD></TR></TBODY></TABLE>
			<INPUT id="hidEXPERT_SERVICE_TYPE" type="hidden" name="hidEXPERT_SERVICE_TYPE" runat="server">
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCONCAT_DATA" type="hidden" name="hidCONCAT_DATA" runat="server">
			<INPUT id="hidMASTER_VENDOR_CODE_ID" type="hidden" name="hidMASTER_VENDOR_CODE_ID" runat="server">
			<INPUT id="hidEXPERT_SERVICE_ID" type="hidden" name="hidEXPERT_SERVICE_ID" runat="server">
			<INPUT id="hidPARENT_ADJUSTER_ID" type="hidden" name="hidPARENT_ADJUSTER_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPARTY_ID" type="hidden" value="0" name="hidPARTY_ID" runat="server">
			<INPUT id="hidPROPERTY_DAMAGED_ID" type="hidden" value="0" name="hidPROPERTY_DAMAGED_ID" runat="server">
			<INPUT id="hidFEDRERAL_ID" type="hidden" value="" name="hidFEDRERAL_ID" runat="server">
            <INPUT id="hidACCOUNT_NUMBER" type="hidden" value="" name="hidACCOUNT_NUMBER" runat="server"> <%--Added by Aditya for tfs bug # 2522 --%>
            <INPUT id="hidNEW_ACCOUNT_NUMBER" type="hidden" value="" name="hidNEW_ACCOUNT_NUMBER" runat="server">  <%--Added by Aditya for tfs bug # 2522 --%>           
			<input id="hidHeader" type="hidden" runat="server" />
			<asp:TextBox ID="hidCLAIMANT_NAME" Style="DISPLAY:none" Runat="server"></asp:TextBox>
			<asp:TextBox ID="hidCUSTOMER_NAME" Style="DISPLAY:none" Runat="server"></asp:TextBox>
			<asp:TextBox ID="hidDefaultData" Style="DISPLAY:none" Runat="server"></asp:TextBox>
			<INPUT id="hidPARTY_DETAIL" type="hidden" name="hidPARTY_DETAIL" value="0" Runat="server"><%--Added by Sibin for Itrack Issue 5055 on 17 Dec 08--%>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPARTY_ID').value,true);
		</script>
	</BODY>
</HTML>

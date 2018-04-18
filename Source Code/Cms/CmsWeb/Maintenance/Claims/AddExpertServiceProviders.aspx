<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddExpertServiceProviders.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddExpertServiceProviders" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>MNT_AGENCY_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
			<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
	    <script type="text/javascript" src="/cms/cmsweb/scripts/CmsHelpScript/jQueryPageHelpFile.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function AddData()
			{
				ChangeColor();
				DisableValidators();
				document.getElementById('hidEXPERT_SERVICE_ID').value	=	'New';
				document.getElementById('cmbEXPERT_SERVICE_TYPE').options.selectedIndex=0;
				document.getElementById('txtEXPERT_SERVICE_NAME').value = '';
				document.getElementById('txtEXPERT_SERVICE_ADDRESS1').value = '';
				document.getElementById('txtEXPERT_SERVICE_ADDRESS2').value = '';	
				document.getElementById('txtEXPERT_SERVICE_CITY').value ='';
				document.getElementById('cmbEXPERT_SERVICE_STATE').options.selectedIndex=-1;				
				document.getElementById('txtEXPERT_SERVICE_ZIP').value ='';
				//document.getElementById('txtEXPERT_SERVICE_VENDOR_CODE').value ='';
				document.getElementById('txtEXPERT_SERVICE_CONTACT_NAME').value ='';
				document.getElementById('txtEXPERT_SERVICE_CONTACT_PHONE').value ='';
				document.getElementById('txtEXPERT_SERVICE_CONTACT_EMAIL').value ='';
				document.getElementById('txtEXPERT_SERVICE_FEDRERAL_ID').value ='';
				document.getElementById('txtEXPERT_SERVICE_MASTER_VENDOR_CODE').value ='';				
				document.getElementById('cmbEXPERT_SERVICE_1099_PROCESSING_OPTION').options.selectedIndex=0;				
				document.getElementById('cmbEXPERT_SERVICE_TYPE').focus();
				document.getElementById('txtMAIL_1099_NAME').value  = '';
				document.getElementById('txtMAIL_1099_ADD1').value  = '';
				document.getElementById('txtMAIL_1099_ADD2').value  = '';
				document.getElementById('txtMAIL_1099_CITY').value  = '';
				document.getElementById('txtMAIL_1099_ZIP').value  = '';
				document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex = 0;
				document.getElementById('cmbMAIL_1099_STATE').options.selectedIndex = -1;
				document.getElementById('cmbW9_FORM').options.selectedIndex = -1;
				EXPERT_SERVICE_FEDRERAL_ID_change();
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

			function GenerateVendorCode()
			{
				if(document.getElementById('hidEXPERT_SERVICE_ID').value=="New" || document.getElementById('hidEXPERT_SERVICE_ID').value=="" || document.getElementById('hidEXPERT_SERVICE_ID').value=="0")
				{
					var VendorCode = GenerateRandomCode(document.getElementById('txtEXPERT_SERVICE_NAME').value,"");
					document.getElementById('txtEXPERT_SERVICE_VENDOR_CODE').value = VendorCode;
				}
			}
			function OpenExpertParentLookup()
			{
			    var strExp = document.getElementById('hid_ExpertServicePro').value;
				var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";						
				OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert',strExp,'','FetchData()');
				
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
					document.getElementById('spnEXPERT_SERVICE_TYPE_DESC').style.display="inline";
					EnableValidator('rfvEXPERT_SERVICE_TYPE_DESC',true);
				}
				else
				{
					document.getElementById('capEXPERT_SERVICE_TYPE_DESC').style.display="none";
					document.getElementById('txtEXPERT_SERVICE_TYPE_DESC').style.display="none";
					document.getElementById('spnEXPERT_SERVICE_TYPE_DESC').style.display="none";
					EnableValidator('rfvEXPERT_SERVICE_TYPE_DESC',false);
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
					document.getElementById("hidEXPERT_SERVICE_MASTER_VENDOR_CODE_ID").value = array[6];
					document.getElementById("txtEXPERT_SERVICE_MASTER_VENDOR_CODE").value = array[7];
					
				}				
			}
			//function OpenExpertParentLookup()
			//{	
			//	var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";						
			//	OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','','FetchData()');
				
			//}
			function OpenParentAdjusterLookup()
			{var strpar = document.getElementById('hidPartiesAdjuster').value;
				var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";					
				document.getElementById('hidCONCAT_DATA').value='';				
				//var srClaimID = document.getElementById('hidCLAIM_ID').value;
				
				//OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_Expert','Experts/Service Provider Name','','FetchParentAdjusterData()');			
				//OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyName_ParentAdjuster','Parties - Adjuster','@CLAIM_ID=\''  + srClaimID + '\'','FetchParentAdjusterData()');//Commented by Sibin for Itrack Issue 5055 on 17 Dec 08
				OpenLookupWithFunction( URL,'EXPERT_DATA','EXPERT_DATA','hidCONCAT_DATA','hidCONCAT_DATA','PartyNameClaims_ParentAdjuster',strpar,'','FetchParentAdjusterData()');//Added by Sibin for Itrack Issue 5055 on 17 Dec 08
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
			
			function populateXML()
			{		
			
				MakeNonMandatoryFields();				
				if(document.getElementById('hidFormSaved').value == '0')
				{
					var tempXML;
					if(document.getElementById("hidOldData").value!="")
					{
						tempXML=document.getElementById("hidOldData").value;						 						
						populateFormData(tempXML,CLM_EXPERT_SERVICE_PROVIDERS);							
						EXPERT_SERVICE_FEDRERAL_ID_hide();
				
				   if(document.getElementById('hidREQ_SPECIAL_HANDLING').value == 10963)
					    document.getElementById('chkREQ_SPECIAL_HANDLING').checked = true;
					else
						document.getElementById('chkREQ_SPECIAL_HANDLING').checked = false;			
				
					}	
					else
					{
						//SetTimeOut has been added as the page gives javascript error at control focus
						setTimeout("AddData();",500);
					}
					
				}
				else
				{
					if(document.getElementById('hidEXPERT_SERVICE_FEDRERAL_ID').value != '')
						EXPERT_SERVICE_FEDRERAL_ID_hide();
					else
						EXPERT_SERVICE_FEDRERAL_ID_change();					
				
				}				       
				    

				MakeNonMandatoryFields();
				return false;
			}
			function EnableDisableVendorCode()
			{
				if (parseInt(document.getElementById('hidEXPERT_SERVICE_ID').value) > 0)
					document.getElementById('txtEXPERT_SERVICE_VENDOR_CODE').readOnly = true;
				else
					document.getElementById('txtEXPERT_SERVICE_VENDOR_CODE').readOnly = false;
			}	
			
			function MakeNonMandatoryFields()
			{
			
				var cmbPROCESSValue   = document.getElementById('cmbEXPERT_SERVICE_1099_PROCESSING_OPTION').options[document.getElementById('cmbEXPERT_SERVICE_1099_PROCESSING_OPTION').selectedIndex].value;
				if (cmbPROCESSValue == 11733 || cmbPROCESSValue == 11734 || cmbPROCESSValue == "")
				{
					EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),false);				
					EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),false);				
					EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),false);				
					EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),false);				
					EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),false);				
					EnableDisableEFT(document.getElementById('cmbW9_FORM'),document.getElementById('rfvW9_FORM'),document.getElementById('spnW9_FORM'),false);				
					EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),false);				
					
				}
				else
				{
					EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),true);				
					EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),true);				
					EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),true);				
					EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),true);				
					EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),true);				
					EnableDisableEFT(document.getElementById('cmbW9_FORM'),document.getElementById('rfvW9_FORM'),document.getElementById('spnW9_FORM'),true);				
					EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),true);						
				}
				if(document.getElementById('txtMAIL_1099_ADD1').value != '')
				{
					document.getElementById('chkCopyData').setAttribute('disabled',true);	
					//alert(document.getElementById('chkCopyData').disabled);
				}
			}

			function ResetTheForm()
			{
				ResetForm('CLM_EXPERT_SERVICE_PROVIDERS');
				EnableDisableVendorCode();
				HideShowTypeDesc();
				ApplyColor();
				return false;
			}
			function Init()
			{
				populateXML();
				EnableDisableVendorCode();
				HideShowTypeDesc();
				ApplyColor();
			}
			
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

			function EXPERT_SERVICE_FEDRERAL_ID_change() 
            {
			    var Cancel = document.getElementById("hidCancel").value;
			    var Edit = document.getElementById("hidEdit").value;

			    var strSysID = '<%=GetSystemId()%>'; 

			    document.getElementById('txtEXPERT_SERVICE_FEDRERAL_ID').value = '';
			    document.getElementById('txtEXPERT_SERVICE_FEDRERAL_ID').style.display = 'inline';
			    document.getElementById("rfvEXPERT_SERVICE_FEDRERAL_ID").setAttribute('enabled', true);

			    
			    if (strSysID == "S001" || strSysID == "SUAT")
                {
                    document.getElementById("revEXPERT_SERVICE_FEDRERAL_ID").setAttribute('enabled', false);
                }
                else
                {
                    document.getElementById("revEXPERT_SERVICE_FEDRERAL_ID").setAttribute('enabled', true);
                }

               // document.getElementById("revEXPERT_SERVICE_FEDRERAL_ID").setAttribute('enabled', true);

			    if (document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").value == 'Edit' || document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").value == 'Editar')
			        document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").value = Cancel;
			    else if (document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").value == 'Cancel' || document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").value == 'Cancelar')
			        EXPERT_SERVICE_FEDRERAL_ID_hide();
			    else
			        document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").style.display = 'none';

			}

			function EXPERT_SERVICE_FEDRERAL_ID_hide() {
			    var Cancel = document.getElementById("hidCancel").value;
			    var Edit = document.getElementById("hidEdit").value;
			    document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").style.display = 'inline';
			    document.getElementById('txtEXPERT_SERVICE_FEDRERAL_ID').value = '';
			    document.getElementById('txtEXPERT_SERVICE_FEDRERAL_ID').style.display = 'none';
			    document.getElementById("rfvEXPERT_SERVICE_FEDRERAL_ID").style.display = 'none';
			    document.getElementById("rfvEXPERT_SERVICE_FEDRERAL_ID").setAttribute('enabled', false);
			    document.getElementById("revEXPERT_SERVICE_FEDRERAL_ID").style.display = 'none';
			    document.getElementById("revEXPERT_SERVICE_FEDRERAL_ID").setAttribute('enabled', false);
			    document.getElementById("btnEXPERT_SERVICE_FEDRERAL_ID").value = Edit;
			}
			


			

			function FormatZipCode(vr) {
			
			    var vr = new String(vr.toString());
			    if (vr != "") {

			        vr = vr.replace(/[-]/g, "");
			        num = vr.length;
			        if (num == 8) {
			            vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
			            document.getElementById('revEXPERT_SERVICE_ZIP').setAttribute('enabled', false);

			        }

			    }

			    return vr;
			}

			function ValidateDateOfBirth(objSource, objArgs) {

                //Added by Pradeep on 07-10-2011 as per the TFS # 1219
			    if (document.getElementById("revDATE_OF_BIRTH").isvalid == true) {

			        var effdate = document.CLM_EXPERT_SERVICE_PROVIDERS.txtDATE_OF_BIRTH.value;
			        objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
			    }
			    else
			        objArgs.IsValid = true;
               //Added till here 

			}

			function ValidateREG_ID_ISSUE_DATE(objSource, objArgs) {
			    var Reg_Id_Issue_Date = document.getElementById(objSource.controltovalidate).value;
			    Reg_Id_Issue_Date = new Date(Reg_Id_Issue_Date);
			    var DateOfBirth = document.getElementById('txtDATE_OF_BIRTH').value;
			    DateOfBirth = new Date(DateOfBirth);
			    if (DateOfBirth <= Reg_Id_Issue_Date)
			        objArgs.IsValid = true;
			    else
			        objArgs.IsValid = false;
			}

			function validatCPF_CNPJ(objSource, objArgs) {
			    
			    var cpferrormsg = '<%=javasciptCPFmsg %>';
			    var CNPJ_invalid = '<%=CNPJ_invalid %>';
			    var CPF_invalid = '<%=CPF_invalid %>';
			   
			    
			    var valid = false;
			    var idd = objSource.id;
			    var rfvid = idd.replace('csv', 'rev');
			    if (document.getElementById(rfvid) != null)
			        if (document.getElementById(rfvid).isvalid == true) {
			        var theCPF = document.getElementById(objSource.controltovalidate)
			        var len = theCPF.value.length;
			        if (len == '14'|| len == '18') {
			            valid = validar(objSource, objArgs);
			            if (valid) {
			                objSource.innerText = '';
			            }
			            else {
			                if (len == '14') {
			                    objSource.innerText = CPF_invalid;
			                }
			                else {
			                    objSource.innerText = CNPJ_invalid;
			                }
			            }
			        }
			        else {
			            if (document.getElementById(rfvid) != null) {
			                if (document.getElementById(rfvid).isvalid == true) {
			                    objArgs.IsValid = false;
			                    objSource.innerHTML = cpferrormsg;
			                } else { objSource.innerHTML = ''; }
			            }
			        }
			    }
			}

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

			function validate2() {


			    if (document.getElementById('revREGIONAL_ID_ISSUE_DATE').isvalid == false) {
			        document.getElementById('csvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', false);

			    }
			    else
			        document.getElementById('csvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', true);
			}

		</script>
		
		        <script type="text/javascript" language="javascript">
		            $(document).ready(function () {
		                $("#txtEXPERT_SERVICE_ZIP").change(function () {
		                    //debugger;
		                    if (trim($('#txtEXPERT_SERVICE_ZIP').val()) != '') {
		                        var ZIPCODE = $("#txtEXPERT_SERVICE_ZIP").val();
		                        var COUNTRYID = "5";
		                        ZIPCODE = ZIPCODE.replace(/[-]/g, "");
		                        PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
		                    }
		                    else {
		                        $("#txtEXPERT_SERVICE_ADDRESS1").val('');
		                        $("#txtEXPERT_SERVICE_ADDRESS2").val('');
		                        $("#txtEXPERT_SERVICE_CITY").val('');
		                    }
		                });
		            });
		            function PageMethod(fn, paramArray, successFn, errorFn) {
		                var pagePath = window.location.pathname;
		                var paramList = '';
		                if (paramArray.length > 0) {
		                    for (var i = 0; i < paramArray.length; i += 2) {
		                        if (paramList.length > 0) paramList += ',';
		                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
		                    }
		                }
		                paramList = '{' + paramList + '}';
		                //Call the page method  
		                $.ajax({
		                    type: "POST",
		                    url: pagePath + "/" + fn,
		                    contentType: "application/json; charset=utf-8",
		                    data: paramList,
		                    dataType: "json",
		                    success: successFn,
		                    error: errorFn
		                });
		            }
		            function AjaxSucceeded(result) {
		                var Addresses = result.d;
		                var Addresse = Addresses.split('^');
		                if (result.d != "" && Addresse[1] != 'undefined') {
		                    $("#cmbEXPERT_SERVICE_STATE").val(Addresse[1]);
		                    $("#hidTEST_STATE_ID").val(Addresse[1]);
		                    $("#hidSTATE_ID").val(Addresse[1]);
		                    //  $("#txtCONTACT_ZIP").val(Addresse[2]);
		                    $("#txtEXPERT_SERVICE_ADDRESS1").val(Addresse[3] + ' ' + Addresse[4]);
		                    //$("#txtDISTRICT").val(Addresse[5]);
		                    $("#txtEXPERT_SERVICE_CITY").val(Addresse[6]);
		                }
		                else if (document.getElementById("cmbEXPERT_SERVICE_COUNTRY").value == "7") {
                            // do nothing for singapore
		                }
		                else {
		                    		                    alert($("#hidZipeCodeVerificationMsg").val());
		                    		                    $("#txtEXPERT_SERVICE_ZIP").val('');
		                    //                    $("#txtCONTACT_ADD1").val('');
		                    //                    $("#txtCONTACT_ADD2").val('');
		                    //                    $("#txtCONTACT_CITY").val('');
		                    //alert(document.getElementById("cmbEXPERT_SERVICE_COUNTRY").value);
		                }
		            }



		            function AjaxFailed(result) {
		                alert(result.d);
		            }
        </script>
        <%-- End jQuery Implimentation for ZipeCode --%>
		
</HEAD>
	<BODY oncontextmenu = "return true;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_EXPERT_SERVICE_PROVIDERS" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
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
								<tr id="tr001" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_TYPE" runat="server"></asp:label><span id="spnEXPERT_SERVICE_TYPE" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXPERT_SERVICE_TYPE" onfocus="SelectComboIndex('cmbEXPERT_SERVICE_TYPE')"
											runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_TYPE" runat="server" ControlToValidate="cmbEXPERT_SERVICE_TYPE"
											Display="Dynamic" ErrorMessage=""></asp:requiredfieldvalidator></TD><%--Please select EXPERT_SERVICE_TYPE.--%>
									<td class="midcolora"><asp:label id="capEXPERT_SERVICE_TYPE_DESC" runat="server"></asp:label><span class="mandatory" id="spnEXPERT_SERVICE_TYPE_DESC">*</span></td>
									<td class="midcolora">
											<asp:TextBox ID="txtEXPERT_SERVICE_TYPE_DESC" Runat="server" MaxLength="35" size="40"></asp:TextBox>
										<br>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_TYPE_DESC" runat="server" ControlToValidate="txtEXPERT_SERVICE_TYPE_DESC"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
								<tr id="tr002" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_NAME" runat="server"></asp:label><span  id="spnEXPERT_SERVICE_NAME" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_NAME" onBlur="javascript:GenerateVendorCode();return false;" runat="server" size="40" maxlength="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_NAME" runat="server" ControlToValidate="txtEXPERT_SERVICE_NAME"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr id="tr003" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_ADDRESS1" runat="server"></asp:label><span id="spnEXPERT_SERVICE_ADDRESS1" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_ADDRESS1" runat="server" size="40" maxlength="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_ADDRESS1" runat="server" ControlToValidate="txtEXPERT_SERVICE_ADDRESS1"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_ADDRESS2" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_ADDRESS2" runat="server" size="40" maxlength="50"></asp:textbox>
									</TD>
								</tr>
								<tr id="tr004" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_CITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_CITY" runat="server" size="20" maxlength="25"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_COUNTRY" runat="server">Country</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXPERT_SERVICE_COUNTRY" onfocus="SelectComboIndex('cmbEXPERT_SERVICE_COUNTRY')"
											runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr id="tr005" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_STATE" runat="server"></asp:label><span id="spnEXPERT_SERVICE_STATE" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%" style="margin-left: 40px"><asp:dropdownlist id="cmbEXPERT_SERVICE_STATE" onfocus="SelectComboIndex('cmbEXPERT_SERVICE_STATE')"
											runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbEXPERT_SERVICE_STATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_ZIP" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_ZIP" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();" runat="server" maxlength="8" size="12"></asp:textbox>
									<%-- Added by Swarup on 30-mar-2007 --%>
										<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
										<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtEXPERT_SERVICE_ZIP"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revEXPERT_SERVICE_ZIP" runat="server" ControlToValidate="txtEXPERT_SERVICE_ZIP"
											ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
								</tr>
								<tr id="tr006" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_MASTER_VENDOR_CODE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_MASTER_VENDOR_CODE" ReadOnly="True" runat="server" maxlength="15" size="17"></asp:textbox>
											<IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../images/selecticon.gif"												
													runat="server">
										</TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_VENDOR_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_VENDOR_CODE" runat="server" maxlength="15" size="17"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_VENDOR_CODE" runat="server" ControlToValidate="txtEXPERT_SERVICE_VENDOR_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
										</TD>									
								</tr>
								<tr id="tr007" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_CONTACT_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_CONTACT_NAME" runat="server" maxlength="50" size="40"></asp:textbox>
									</TD>								
									<TD class='midcolora' width='18%'>
										<asp:Label id="capEXPERT_SERVICE_CONTACT_FAX" runat="server"></asp:Label></TD>
									<TD class='midcolora' width='32%'>
										<asp:textbox id="txtEXPERT_SERVICE_CONTACT_FAX" runat='server' size='20' maxlength='13'></asp:textbox><br>
										<asp:RegularExpressionValidator id="revEXPERT_SERVICE_CONTACT_FAX" Display="Dynamic" runat="server" ControlToValidate="txtEXPERT_SERVICE_CONTACT_FAX"></asp:RegularExpressionValidator>
								</TD>
								</tr>
								<tr id="tr008" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_CONTACT_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_CONTACT_PHONE" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone()" ></asp:textbox><br>
										<asp:regularexpressionvalidator id="revEXPERT_SERVICE_CONTACT_PHONE" runat="server" ControlToValidate="txtEXPERT_SERVICE_CONTACT_PHONE"
											ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class='midcolora' width='18%'>
										<asp:Label id="capEXPERT_SERVICE_CONTACT_PHONE_EXT" runat="server"></asp:Label></TD>
									<TD class='midcolora' width='32%'>
										<asp:textbox id="txtEXPERT_SERVICE_CONTACT_PHONE_EXT" runat='server' size='5' maxlength='4'></asp:textbox><br>
										<asp:RegularExpressionValidator id="revEXPERT_SERVICE_CONTACT_PHONE_EXT" Display="Dynamic" runat="server" ControlToValidate="txtEXPERT_SERVICE_CONTACT_PHONE_EXT"></asp:RegularExpressionValidator>
									</TD>
								</tr>
								<tr id="tr009" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_CONTACT_EMAIL" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPERT_SERVICE_CONTACT_EMAIL" runat="server" maxlength="50" size="40"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revEXPERT_SERVICE_CONTACT_EMAIL" Runat="server" ControlToValidate="txtEXPERT_SERVICE_CONTACT_EMAIL"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>								
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_FEDRERAL_ID" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="capEXPERT_SERVICE_FEDRERAL_ID_HID" runat="server" size="15" maxlength="9"></asp:label><input class="clsButton" id="btnEXPERT_SERVICE_FEDRERAL_ID" text="Edit" onclick="EXPERT_SERVICE_FEDRERAL_ID_change();" type="button"></input><asp:textbox id="txtEXPERT_SERVICE_FEDRERAL_ID" runat="server" maxlength="9" size="15"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_FEDRERAL_ID" runat="server" ControlToValidate="txtEXPERT_SERVICE_FEDRERAL_ID"
											Display="Dynamic" ErrorMessage="Please select txtEXPERT_SERVICE_FEDRERAL_ID." Enabled="False" 
                                            EnableTheming="True"></asp:requiredfieldvalidator>
										<asp:RegularExpressionValidator ID="revEXPERT_SERVICE_FEDRERAL_ID" Runat="server" ControlToValidate="txtEXPERT_SERVICE_FEDRERAL_ID" Display="Dynamic"></asp:RegularExpressionValidator>
									</TD>
								</tr>
								<tr id="tr010" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capEXPERT_SERVICE_1099_PROCESSING_OPTION" runat="server"></asp:label><span id="spnEXPERT_SERVICE_1099_PROCESSING_OPTION" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXPERT_SERVICE_1099_PROCESSING_OPTION" onfocus="SelectComboIndex('cmbEXPERT_SERVICE_1099_PROCESSING_OPTION')"
											OnChange="MakeNonMandatoryFields();" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvEXPERT_SERVICE_1099_PROCESSING_OPTION" runat="server" ControlToValidate="cmbEXPERT_SERVICE_1099_PROCESSING_OPTION"
											Display="Dynamic" ErrorMessage=""></asp:requiredfieldvalidator><%--Please select cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.--%>
									</TD>									
									<TD class="midcolora" width="18%"><asp:label id="capREQ_SPECIAL_HANDLING" runat="server"></asp:label></TD>																
									<TD class="midcolora" width="32%"><asp:checkbox id="chkREQ_SPECIAL_HANDLING" runat="server"></asp:checkbox></TD>					
								</tr>
								<tr id="tr011" runat="server">
								<td class="midcolora" width="18%"><asp:Label ID="capCPF" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtCPF" runat="server" MaxLength = "14" OnBlur="this.value=FormatCPFCNPJ(this.value);ValidatorOnChange();"></asp:TextBox>
								</br><asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF"></asp:RegularExpressionValidator>
								<asp:CustomValidator runat="server" ID="csvCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF" ClientValidationFunction="validatCPF_CNPJ"></asp:CustomValidator>
								</td>
								<td class="midcolora" width="18%">
                                <asp:Label ID="capDATE_OF_BIRTH" runat="server" ></asp:Label>
                                 </td>
                                <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtDATE_OF_BIRTH" runat="server" size="12"  MaxLength="10"></asp:TextBox>
                                <asp:HyperLink ID="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
                                <asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                 <asp:RegularExpressionValidator  ID="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:RegularExpressionValidator>
                                 <asp:CustomValidator ID="csvDATE_OF_BIRTH" ErrorMessage="" runat="server" ControlToValidate="txtDATE_OF_BIRTH" ClientValidationFunction="ValidateDateOfBirth" ></asp:CustomValidator>
                                </td>
								</tr>
								
								<tr id="tr012" runat="server">
								<td class="midcolora" width="18%"><asp:Label ID="capACTIVITY" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:DropDownList ID="cmbACTIVITY" runat="server" Width="400px"></asp:DropDownList>
								</td>
								<td class="midcolora" width="18%"><asp:Label ID="capREGIONAL_IDENTIFICATION" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREGIONAL_IDENTIFICATION" runat="server"></asp:TextBox>
								</td>
								</tr>
								
								<tr id="tr013" runat="server">
								 <td class="midcolora" width="18%">
                                 <asp:Label runat="server" ID="capREG_ID_ISSUE_DATE">Regional ID Issue Date</asp:Label></td>
                                 <td class="midcolora" width="32%"><asp:TextBox runat="server" ID="txtREG_ID_ISSUE_DATE" AutoCompleteType="Disabled" CausesValidation="true" onBlur="validate2();"></asp:TextBox><asp:HyperLink ID="hlkREG_ID_ISSUE_DATE" runat="server" CssClass="HotSpot">
                                 <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic" ControlToValidate="txtREG_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvREGIONAL_ID_ISSUE_DATE" ErrorMessage="" runat="server" ControlToValidate="txtREG_ID_ISSUE_DATE" ClientValidationFunction="ValidateREG_ID_ISSUE_DATE" ></asp:CustomValidator>
                                </td>
								<td class="midcolora" width="18%"><asp:Label ID="capREG_ID_ISSUE" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREG_ID_ISSUE" runat="server"></asp:TextBox>
								</td>
								</tr>
								<tr id="tr014" runat = "server" visible="true">
									<td class="pageHeader" colSpan="4"><asp:Label ID="capMAIL" runat="server"></asp:Label> 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkCopyData" runat="server" Text=""></asp:checkbox><asp:Label ID="capCOPY" runat="server"></asp:Label>                    <%--1099 Mailing Details--%><%--Copy Default Details--%> 
									</td>
								</tr>
								<TR id="tr015" runat = "server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_NAME" runat="server">1099 Business Name</asp:label><span class="mandatory" id="spnMAIL_1099_NAME" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_NAME" runat="server" maxlength="75" size="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_NAME" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_NAME"></asp:requiredfieldvalidator></TD><%--Please enter Business Name.--%>
									<TD class="midcolora" width="18%"><asp:label id="capW9_FORM" runat="server">W-9 Form</asp:label><span id="spnW9_FORM" class="mandatory" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbW9_FORM" onfocus="SelectComboIndex('cmbW9_FORM')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvW9_FORM" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbW9_FORM"></asp:requiredfieldvalidator></TD><%--Please enter W-9 Form.--%>
								</tr>
								<TR id="tr016" runat = "server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ADD1" runat="server">1099 Mail Address1</asp:label><span class="mandatory" id="spnMAIL_1099_ADD1" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ADD1" runat="server" maxlength="70" size="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_ADD1" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_ADD1"></asp:requiredfieldvalidator></TD><%--Please enter address 1--%>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ADD2" runat="server">1099 Mail Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ADD2" runat="server" maxlength="70" size="50"></asp:textbox><BR>
									</TD>
								</tr>
								<tr id="tr017" runat = "server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_CITY" runat="server">1099 Mail City</asp:label><span class="mandatory" id="spnMAIL_1099_CITY" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_CITY" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_CITY"></asp:requiredfieldvalidator></TD><%--1099 Mailing city can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_STATE" runat="server">1099 Mail State</asp:label><span class="mandatory" id="spnMAIL_1099_STATE" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMAIL_1099_STATE" onfocus="SelectComboIndex('cmbMAIL_1099_STATE')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_STATE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbMAIL_1099_STATE"></asp:requiredfieldvalidator></TD><%--1099 Mailing state can't be blank--%>
								</tr>
								<tr id="tr018" runat = "server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_COUNTRY" runat="server">1099 Mail Country</asp:label><span class="mandatory" id="spnMAIL_1099_COUNTRY" runat="server">*</span></TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbMAIL_1099_COUNTRY" onfocus="SelectComboIndex('cmbMAIL_1099_COUNTRY')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_COUNTRY" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbMAIL_1099_COUNTRY"></asp:requiredfieldvalidator></td><%--1099 mailng Country can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ZIP" runat="server">1099 Mail Zip</asp:label><span class="mandatory" id="spnMAIL_1099_ZIP" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ZIP" runat="server" maxlength="10" size="13"></asp:textbox><asp:hyperlink id="hlkMAIL_1099_ZIP" runat="server" CssClass="HotSpot">
											<asp:image id="imgMAIL_1099_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_ZIP" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revMAIL_1099_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtMAIL_1099_ZIP"></asp:regularexpressionvalidator></TD><%--1099 Mailing Zip can't be blank.--%>
								</tr>
								<tr id="tr019" runat = "server" visible="true">
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPARTY_DETAIL" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbPARTY_DETAIL" runat='server'></asp:DropDownList>
								</TD>							
								<TD class='midcolora' width='18%' visible="true">
									<asp:Label id="capAGE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtAGE" runat='server' size='4' maxlength='3'></asp:textbox><br>
									<asp:RangeValidator ID="rngAGE" MinimumValue="0" MaximumValue="150" ControlToValidate="txtAGE" Runat="server" Display="Dynamic" ErrorMessage="Error" Type="Integer"></asp:RangeValidator>
								</TD>
							</tr>
							<tr id="tr020" runat = "server">
								<TD class='midcolora' width='18%' visible="true">
									<asp:Label id="capEXTENT_OF_INJURY" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtEXTENT_OF_INJURY" runat='server' size='40' maxlength='200'></asp:textbox><br>
									<asp:RegularExpressionValidator id="revEXTENT_OF_INJURY" Display="Dynamic" runat="server" ControlToValidate="txtEXTENT_OF_INJURY"></asp:RegularExpressionValidator>
								</TD>															
								<TD class='midcolora' width='18%'>
									<asp:Label id="capOTHER_DETAILS" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtOTHER_DETAILS" runat='server' size='40' TextMode="MultiLine" Cols="50" Rows="4" onkeypress="MaxLength(this,500);"  
										maxlength='500'></asp:textbox><br>
									<asp:RegularExpressionValidator id="revOTHER_DETAILS" runat="server" Display="Dynamic" ControlToValidate="txtOTHER_DETAILS"></asp:RegularExpressionValidator>									
								</TD>								
							</tr>
							<tr id="tr021" runat = "server" visible="true">
								<TD class='midcolora' width='18%'><asp:Label id="capBANK_NAME" runat="server"></asp:Label></TD>	
								<TD class='midcolora' width='32%'><asp:textbox id="txtBANK_NAME" runat='server' size='40' maxlength='100'></asp:textbox></TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capACCOUNT_NUMBER" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtACCOUNT_NUMBER" runat='server' size='40' maxlength='20'></asp:textbox></TD>																			
								</tr>
							
							<tr id="tr022" runat = "server" visible="true">
								<TD class='midcolora' width='18%'><asp:Label id="capACCOUNT_NAME" runat="server"></asp:Label></TD>	
								<TD class='midcolora' width='32%'><asp:textbox id="txtACCOUNT_NAME" runat='server' size='40' maxlength='100'></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capPARENT_ADJUSTER" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPARENT_ADJUSTER" ReadOnly="True" runat="server" maxlength="15" size="17"></asp:textbox>
											<IMG id="imgPARENT_ADJUSTER" style="CURSOR: hand" alt="" src="../../images/selecticon.gif"												
													runat="server"></TD>
                                                    </tr>
								<tr id="tr023" runat="server">
									<td class="midcolora" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text=" " CausesValidation="false"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>

                            </TBODY>
						</TABLE>
                    </TD>
                </TR>
                
             </TABLE>
			<INPUT id="hidCONCAT_DATA" type="hidden" name="hidCONCAT_DATA" runat="server">
			<INPUT id="hidEXPERT_SERVICE_MASTER_VENDOR_CODE_ID" type="hidden" name="hidEXPERT_SERVICE_MASTER_VENDOR_CODE_ID" runat="server">
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidPARENT_ADJUSTER_ID" type="hidden" name="hidPARENT_ADJUSTER_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidEXPERT_SERVICE_ID" type="hidden" value="0" name="hidEXPERT_SERVICE_ID" runat="server">
			<INPUT id="hidEXPERT_SERVICE_FEDRERAL_ID" type="hidden" name="hidEXPERT_SERVICE_FEDRERAL_ID" runat="server">
		    <INPUT id="hidREQ_SPECIAL_HANDLING" type="hidden" value="0" name="hidREQ_SPECIAL_HANDLING" runat="server">
		    <INPUT id="hid_ExpertServicePro" type="hidden"  runat="server">
            <input id="hidPartiesAdjuster" type="hidden" runat="server" >
            <input type="hidden" id="hidSTATE_ID" name="hidSTATE_ID" runat="server" />
            <input type="hidden" runat="server" ID="hidCancel" name=""/> 	
            <input type="hidden" runat="server" ID="hidEdit" name=""/> 	
            <input id="hidZipeCodeVerificationMsg" type="hidden" name="hidZipeCodeVerificationMsg"  runat="server"> 

             
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidEXPERT_SERVICE_ID').value,true);			
		</script>
	</BODY>
</HTML>

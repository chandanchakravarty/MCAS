<%@ Page language="c#" Codebehind="AddAdjusterDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddAdjusterDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_ADJUSTER</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
	   <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
	    <script type="text/javascript" src="/cms/cmsweb/scripts/CmsHelpScript/jQueryPageHelpFile.js"></script>
		<script language="javascript">
		
		function setTab()
		{	
			var strADJUSTER_TYPE = "";
			if(document.getElementById("cmbADJUSTER_TYPE").selectedIndex!=-1)
				strADJUSTER_TYPE = document.getElementById("cmbADJUSTER_TYPE").options[document.getElementById("cmbADJUSTER_TYPE").selectedIndex].value;
				
		 	if (document.getElementById("hidADJUSTER_ID").value == "0" || document.getElementById("hidADJUSTER_ID").value == "" || document.getElementById("hidADJUSTER_ID").value == "New" || strADJUSTER_TYPE == <%=((int)enumClaimAdjusterTypes.THIRD_PARTY_ADJUSTER).ToString()%>)
			{						
				RemoveTab(2,top.frames[1]);	
			} 
			else
			{			
			  var AuthorityTitle=document.getElementById("hidAuthorityMessage").value;
			  
				Url="ClaimsAdjusterAuthorityIndex.aspx?ADJUSTER_NAME=" + document.getElementById("txtADJUSTER_NAME").value + "&" + "ADJUSTER_TYPE=" + document.getElementById("cmbADJUSTER_TYPE").options[document.getElementById("cmbADJUSTER_TYPE").selectedIndex].text+ "&";										
				DrawTab(2,top.frames[1],AuthorityTitle,Url);		
			}
		}
		
		function ValidateLength(objSource , objArgs)
		{
			if(document.getElementById('txtSUB_ADJUSTER_NOTES').value.length>100)
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		
		
		}
		function OpenUrl()
		{
			var url = document.getElementById('txtSUB_ADJUSTER_WEBSITE').value;
			if(url!="")
				window.open("http://" + url);
		}
		function AddData()
		{        
			//ChangeColor();
			strADJUSTER_TYPE = document.getElementById("cmbADJUSTER_TYPE").options[document.getElementById("cmbADJUSTER_TYPE").selectedIndex].value;
			if(strADJUSTER_TYPE!="")
			 return;
			 
			DisableValidators();
			document.getElementById('hidADJUSTER_ID').value	=	'New';			
        	document.getElementById('cmbADJUSTER_TYPE').selectedIndex  = 3;
			document.getElementById('txtADJUSTER_NAME').value  = '';
			document.getElementById('cmbADJUSTER_CODE').selectedIndex  = -1;
			document.getElementById('txtSUB_ADJUSTER').value  = '';
			document.getElementById('txtSUB_ADJUSTER_LEGAL_NAME').value  = '';
			document.getElementById('txtSUB_ADJUSTER_ADDRESS1').value  = '';
			document.getElementById('txtSUB_ADJUSTER_ADDRESS2').value  = '';
			document.getElementById('txtSUB_ADJUSTER_CITY').value  = '';
			//document.getElementById('cmbSUB_ADJUSTER_STATE').selectedIndex  = -1;
			document.getElementById('txtSUB_ADJUSTER_ZIP').value  = '';
			document.getElementById('txtSUB_ADJUSTER_PHONE').value  = '';
			document.getElementById('txtSUB_ADJUSTER_FAX').value  = '';
			document.getElementById('txtSUB_ADJUSTER_EMAIL').value  = '';
			document.getElementById('txtSUB_ADJUSTER_WEBSITE').value  = '';
			document.getElementById('txtSUB_ADJUSTER_NOTES').value  = '';
			document.getElementById('txtSUB_ADJUSTER_CONTACT_NAME').value  = '';						
			document.getElementById('cmbADJUSTER_TYPE').focus();
		}		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML;
				if(document.getElementById("hidOldData").value !="")
				{
					//document.getElementById('btnReset').style.display='none';
					tempXML=document.getElementById("hidOldData").value;
					
					
					//populateFormData(tempXML,CLM_ADJUSTER);
					//Set the adjuster dropdown with the hidden value					
					SetComboValueForConcatenatedString("cmbADJUSTER_CODE",document.getElementById("hidUSER_ID").value,'^',0)				
				}
				else
				{
					AddData();
				}
			}
			
			//if(document.getElementById('txtSUB_ADJUSTER_WEBSITE').value=='')
				//document.getElementById('txtSUB_ADJUSTER_WEBSITE').value = 'http://';
			try{ document.getElementById('cmbADJUSTER_TYPE').focus(); } //try-catch block added by Charles on 26-Oct-09 for Itrack 6636
			catch(e) { }
			return false;
		}
		
		function CheckAdjusterType()
		{
	
			/*Lookup values for Adjuster Type
			11736>Inside Adjuster
			11737>Outside Adjuster
			11738>Third Party Adjuster*/

            var strSysID = '<%=GetSystemId()%>';            
		
			combo = document.getElementById("cmbADJUSTER_TYPE");
			if(combo.selectedIndex==-1) return;
			if(combo.options[combo.selectedIndex].value=="11738") 
			{
				document.getElementById("trRow0").style.display = "inline";
				document.getElementById("trRow1").style.display = "inline";
				document.getElementById("trRow2").style.display = "inline";
				document.getElementById("trRow3").style.display = "inline";
				document.getElementById("trRow4").style.display = "inline";
				document.getElementById("trRow5").style.display = "inline";
				document.getElementById("trRow6").style.display = "inline";
				//Enable the validators
				document.getElementById("rfvADJUSTER_CODE").setAttribute('isValid',false); 
				document.getElementById("rfvADJUSTER_CODE").style.display='none'; 
				document.getElementById("cmbADJUSTER_CODE").style.display='none'; 
				document.getElementById("rfvADJUSTER_CODE").setAttribute('enabled',false); 
				
				document.getElementById("rfvADJUSTER_NAME").setAttribute('isValid',true); 				
				document.getElementById("txtADJUSTER_NAME").style.display='inline'; 
				document.getElementById("rfvADJUSTER_NAME").setAttribute('enabled',true); 
				
				 
				
				document.getElementById("revADJUSTER_NAME").setAttribute('isValid',true); 								
				document.getElementById("revADJUSTER_NAME").setAttribute('enabled',true); 
				
				
				document.getElementById("rfvSUB_ADJUSTER_LEGAL_NAME").setAttribute('isValid',true); 				
				document.getElementById("rfvSUB_ADJUSTER_LEGAL_NAME").setAttribute('enabled',true); 
				
				document.getElementById("rfvSUB_ADJUSTER_ADDRESS1").setAttribute('isValid',true); 				
				document.getElementById("rfvSUB_ADJUSTER_ADDRESS1").setAttribute('enabled',true); 
				

                //Modified by Ruchika Chauhan on 30/11/2011 for TFS# 966
                if(strSysID=="S001" || strSysID=="SUAT")
                {
                    document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('isValid',false); 				
				    document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('enabled',false); 
                }
                else
                {
                    document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('isValid',true); 				
				    document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('enabled',true); 
                }


//				document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('isValid',true); 				
//				document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('enabled',true); 
				
				document.getElementById("rfvSUB_ADJUSTER_STATE").setAttribute('isValid',true); 				
				document.getElementById("rfvSUB_ADJUSTER_STATE").setAttribute('enabled',true); 
				
				document.getElementById("rfvSUB_ADJUSTER_ZIP").setAttribute('isValid',true); 				
				document.getElementById("rfvSUB_ADJUSTER_ZIP").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_LEGAL_NAME").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_LEGAL_NAME").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_CITY").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_CITY").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_ZIP").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_ZIP").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_PHONE").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_PHONE").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_FAX").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_FAX").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_EMAIL").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_EMAIL").setAttribute('enabled',true); 
				
				document.getElementById("revSUB_ADJUSTER_WEBSITE").setAttribute('isValid',true); 				
				document.getElementById("revSUB_ADJUSTER_WEBSITE").setAttribute('enabled',true); 
				
				document.getElementById("csvSUB_ADJUSTER_NOTES").setAttribute('isValid',true); 				
				document.getElementById("csvSUB_ADJUSTER_NOTES").setAttribute('enabled',true); 
				ChangeColor();
			}
			else //Hide the controls,set their values to blank and disable their validators
			{
				document.getElementById("trRow0").style.display = "none";
				document.getElementById("trRow1").style.display = "none";
				document.getElementById("trRow2").style.display = "none";
				document.getElementById("trRow3").style.display = "none";
				document.getElementById("trRow4").style.display = "none";
				document.getElementById("trRow5").style.display = "none";
				document.getElementById("trRow6").style.display = "none";
				
				document.getElementById("rfvADJUSTER_CODE").setAttribute('isValid',true); 				
				document.getElementById("cmbADJUSTER_CODE").style.display='inline'; 
				document.getElementById("rfvADJUSTER_CODE").setAttribute('enabled',true); 
				
				document.getElementById("rfvADJUSTER_NAME").setAttribute('isValid',false); 				
				document.getElementById("txtADJUSTER_NAME").style.display='none'; 
						document.getElementById("rfvADJUSTER_NAME").style.display='none'; 
				document.getElementById("rfvADJUSTER_NAME").setAttribute('enabled',false); 
				
				document.getElementById("revADJUSTER_NAME").setAttribute('isValid',false); 								
				document.getElementById("revADJUSTER_NAME").setAttribute('enabled',false); 
				
				
				
				document.getElementById("rfvSUB_ADJUSTER_LEGAL_NAME").setAttribute('isValid',false); 
				document.getElementById("rfvSUB_ADJUSTER_LEGAL_NAME").style.display='none'; 
				document.getElementById("rfvSUB_ADJUSTER_LEGAL_NAME").setAttribute('enabled',false); 
				
				document.getElementById("rfvSUB_ADJUSTER_ADDRESS1").setAttribute('isValid',false); 
				document.getElementById("rfvSUB_ADJUSTER_ADDRESS1").style.display='none'; 
				document.getElementById("rfvSUB_ADJUSTER_ADDRESS1").setAttribute('enabled',false); 
				
				document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('isValid',false); 
				document.getElementById("rfvSUB_ADJUSTER_CITY").style.display='none'; 
				document.getElementById("rfvSUB_ADJUSTER_CITY").setAttribute('enabled',false); 
				
				document.getElementById("rfvSUB_ADJUSTER_STATE").setAttribute('isValid',false); 
				document.getElementById("rfvSUB_ADJUSTER_STATE").style.display='none'; 
				document.getElementById("rfvSUB_ADJUSTER_STATE").setAttribute('enabled',false); 
				
				document.getElementById("rfvSUB_ADJUSTER_ZIP").setAttribute('isValid',false); 
				document.getElementById("rfvSUB_ADJUSTER_ZIP").style.display='none'; 
				document.getElementById("rfvSUB_ADJUSTER_ZIP").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_LEGAL_NAME").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_LEGAL_NAME").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_LEGAL_NAME").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_CITY").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_CITY").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_CITY").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_ZIP").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_ZIP").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_ZIP").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_PHONE").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_PHONE").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_PHONE").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_FAX").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_FAX").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_FAX").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_EMAIL").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_EMAIL").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_EMAIL").setAttribute('enabled',false); 
				
				document.getElementById("revSUB_ADJUSTER_WEBSITE").setAttribute('isValid',false); 
				document.getElementById("revSUB_ADJUSTER_WEBSITE").style.display='none'; 
				document.getElementById("revSUB_ADJUSTER_WEBSITE").setAttribute('enabled',false); 
				
				document.getElementById("csvSUB_ADJUSTER_NOTES").setAttribute('isValid',false); 
				document.getElementById("csvSUB_ADJUSTER_NOTES").style.display='none'; 
				document.getElementById("csvSUB_ADJUSTER_NOTES").setAttribute('enabled',false); 
				
			}
		}
		
		function GetUserData(combo)
		{
			//if(Page_ClientValidate()==false)return false;//Done by Sibin for Itrack Issue 6947 on 18 Jan 2010
			//Structure of the encoded string is as follows : 
			/*USER_ID + '^' + USER_FNAME + ' ' + USER_LNAME + '^' + USER_ADD1  + '^' + USER_ADD2  + '^' + 
			USER_CITY + '^' + COUNTRY + '^' + USER_STATE + '^' + 	USER_ZIP + '^' + USER_PHONE + '^' + 
			USER_FAX + '^' + USER_EMAIL   AS USER_ID  */  
			//We will split the user_id field to obtain the relavent information
			if(combo.selectedIndex==-1) return;
			encoded_string = new String(combo.options[combo.selectedIndex].value);
			if(encoded_string.length<1) return;
			array = encoded_string.split('^');
			//alert(encoded_string);

			//Traverse through the array and put the values in relavent fields
				document.getElementById("hidUSER_ID").value = array[0];
				document.getElementById("hidADJUSTER_CODE").value = array[1];
				document.getElementById("txtADJUSTER_NAME").value = array[2];
				
				
//				
//				document.getElementById("txtSA_ADDRESS1").value = array[3];
//				document.getElementById("txtSA_ADDRESS2").value = array[4];
//				document.getElementById("txtSA_CITY").value = array[5];
//				//SelectComboOption(comboId,selectedValue)	
				
				
							
//				SelectComboOption("cmbSA_COUNTRY",array[6]);				
//				
//				  
//				 if(array[6]!="" && array[6]!="0")
//				  SubAdjusterCountryChanged();
//				  
//                  document.getElementById('cmbSA_STATE').options[document.getElementById('cmbSA_STATE').selectedIndex].value=array[7];
////				SelectComboOption('cmbSA_STATE',array[7]);
//				
//				document.getElementById("txtSA_ZIPCODE").value = array[8];
//				document.getElementById("txtSA_PHONE").value = array[9];
//				document.getElementById("txtSA_FAX").value = array[10];
//				document.getElementById("txtSUB_ADJUSTER_EMAIL").value = array[11];
			
 
		
		}
		
		function formReset()
		{
			DisableValidators();
			document.CLM_ADJUSTER.reset();
			populateXML();			
			CheckAdjusterType();
			ChangeColor();
			return false;
		}
		</script>
		<%-- End jQuery Implimentation for ZipeCode --%>
		    <script type="text/javascript" language="javascript">
		        // for txtSUB_ADJUSTER_ZIP
		        $(document).ready(function () {
                    var strSysID = '<%=GetSystemId()%>';
                    if (strSysID != "S001" && strSysID != "SUAT" && strSysID != "I001" && strSysID != "IUAT")
                    {
		            $("#txtSUB_ADJUSTER_ZIP").change(function() {
		            if (trim($('#txtSUB_ADJUSTER_ZIP').val()) != '') {
		                var ZIPCODE = $("#txtSUB_ADJUSTER_ZIP").val();
		                        var COUNTRYID = "5";
		                        ZIPCODE = ZIPCODE.replace(/[-]/g, "");
		                        PageMethod("GetAdjusterZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
		                    }
		                    else {
		                        $("#txtSUB_ADJUSTER_ZIP").val('');
		                        
		                         }
		                });
                        }
		            });

		            function AjaxSucceeded(result) {
		                var Addresses = result.d;
		                var Addresse = Addresses.split('^');
		                if (result.d != "" && Addresse[1] != 'undefined') {
		                    $("#cmbEXPERT_SERVICE_STATE").val(Addresse[1]);


		                }
		                else {
                        var strSysID = '<%=GetSystemId()%>';
                        if (strSysID != "S001" && strSysID != "SUAT") {
                            alert($("#hidZipeCodeVerificationMsg").val());
                            $("#txtSUB_ADJUSTER_ZIP").val('');

                        }
		                    //                    $("#txtCONTACT_ADD1").val('');
		                    //                    $("#txtCONTACT_ADD2").val('');
		                    //                    $("#txtCONTACT_CITY").val('');
		                }
		            }


		            
		            // for txtSA_ZIPCODE
		            $(document).ready(function() {
		                $("#txtSA_ZIPCODE").change(function () {
                    var strSysID = '<%=GetSystemId()%>';
                    if (strSysID != "S001" && strSysID != "SUAT" && strSysID != "I001" && strSysID != "IUAT") 
                    {
		            if (trim($('#txtSA_ZIPCODE').val()) != '') {
		                var ZIPCODE = $("#txtSA_ZIPCODE").val();
		                        var COUNTRYID = "5";
		                        ZIPCODE = ZIPCODE.replace(/[-]/g, "");
		                        PageMethod("GetAdjusterZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded_1, AjaxFailed); //With parameters
		                    }
		                    else {
		                        $("#txtSA_ZIPCODE").val('');

		                    }
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

		            function AjaxSucceeded_1(result) {
		               
		                var Addresses = result.d;
		                var Addresse = Addresses.split('^');
		                if (result.d != "" && Addresse[1] != 'undefined') {
		                    $("#cmbEXPERT_SERVICE_STATE").val(Addresse[1]);


		                }
		                else {
                        var strSysID = '<%=GetSystemId()%>';
                    if(strSysID != "S001" && strSysID != "SUAT")
                    {
		                    alert($("#hidZipeCodeVerificationMsg").val());
		                    $("#txtSA_ZIPCODE").val('');

                            }
		                    //                    $("#txtCONTACT_ADD1").val('');
		                    //                    $("#txtCONTACT_ADD2").val('');
		                    //                    $("#txtCONTACT_CITY").val('');
		                }
		            }



		            function AjaxFailed(result) {
                    var strSysID = '<%=GetSystemId()%>';
                    if (strSysID != "S001" && strSysID != "SUAT") {
                        alert(result.d);
                    }
		            }


		            /// FOR COUNTRY SELECTION
		            function AdjusterCountryChanged() {
                     var strSysID = '<%=GetSystemId()%>';
                    
                         GlobalError = true;
                         var CountryID = document.getElementById('cmbSUB_ADJUSTER_COUNTRY').options[document.getElementById('cmbSUB_ADJUSTER_COUNTRY').selectedIndex].value;
                         if (strSysID != "S001" && strSysID != "SUAT") {
                             AddAdjusterDetails.AjaxFillState(CountryID, FillAdjusterState);
                         }

                             if (GlobalError) {
                                 return false;
                             }
                             else {
                                 return true;
                             }
                        
		            }
		            function FillAdjusterState(Result) {
		                //var strXML;
		                if (Result.error) {
		                    var xfaultcode = Result.errorDetail.code;
		                    var xfaultstring = Result.errorDetail.string;
		                    var xfaultsoap = Result.errorDetail.raw;
		                }
		                else {		                    
		                    var statesList = document.getElementById("cmbSUB_ADJUSTER_STATE");
		                    statesList.options.length = 0;
		                    oOption = document.createElement("option");
		                    oOption.value = "";
		                    oOption.text = "";
		                    statesList.add(oOption);
		                    ds = Result.value;
		                    if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		                        for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {

		                            statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
		                        }

		                    }
		                    if (statesList.options.length > 0) {
		                        statesList.remove(0);
		                        document.getElementById('hidSUB_ADJUSTER_STATE').value = statesList.options[0].value;
		                    }
		                    document.getElementById("cmbSUB_ADJUSTER_STATE").value = document.getElementById("cmbSUB_ADJUSTER_STATE").value;
		                }

		                return false;
		            }


		            /// FOR SUb adjuster COUNTRY SELECTION
		            function SubAdjusterCountryChanged() {
                    //By kuldeep , not to fetch states for singapore implementation
		           var strSysID = '<%=GetSystemId()%>';
		        
		               GlobalError = true;
		               var CountryID = document.getElementById('cmbSA_COUNTRY').options[document.getElementById('cmbSA_COUNTRY').selectedIndex].value;
		               if (strSysID != "S001" && strSysID != "SUAT") {
		                   AddAdjusterDetails.AjaxFillState(CountryID, FillSubAdjusterState);
		               }
		               if (GlobalError) {
		                   return false;
		               }
		               else {
		                   return true;
		               }
		             
		            }
		            function FillSubAdjusterState(Result) {
		             
		                if (Result.error) {
		                    var xfaultcode = Result.errorDetail.code;
		                    var xfaultstring = Result.errorDetail.string;
		                    var xfaultsoap = Result.errorDetail.raw;
		                }
		                else {
		                    var statesList = document.getElementById("cmbSA_STATE");
		                    statesList.options.length = 0;
		                    oOption = document.createElement("option");
		                    oOption.value = "";
		                    oOption.text = "";
		                    statesList.add(oOption);
		                    ds = Result.value;
		                    if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		                        for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {

		                            statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
		                        }

		                    }
		                    if (statesList.options.length > 0) {
		                        statesList.remove(0);
		                        document.getElementById('hidSA_STATE').value = statesList.options[0].value;
		                    }
		                   //document.getElementById("cmbSA_STATE").value = document.getElementById("cmbSA_STATE").value;
		                }

		                return false;
		            }
		            function SetSA_STATE() {
		              
		                document.getElementById('hidSA_STATE').value = document.getElementById('cmbSA_STATE').options[document.getElementById('cmbSA_STATE').selectedIndex].value;
		            
		            }

		            function SetSUB_ADJUSTER_STATE()
		             {
		             
		                document.getElementById('hidSUB_ADJUSTER_STATE').value = document.getElementById('cmbSUB_ADJUSTER_STATE').options[document.getElementById('cmbSUB_ADJUSTER_STATE').selectedIndex].value;

		            }
        </script>
        
		
	</HEAD>
	<BODY oncontextmenu = "return true;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();CheckAdjusterType();setTab();">
		<FORM id="CLM_ADJUSTER" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server" Text=""></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capADJUSTER_TYPE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbADJUSTER_TYPE" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvADJUSTER_TYPE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="cmbADJUSTER_TYPE"></asp:requiredfieldvalidator>
                                    </TD>
								<TD class="midcolora" colSpan="2"><asp:label id="capDISPLAY_ON_CLAIM" 
                                        runat="server"></asp:label>
                                    <br />
                                    <asp:dropdownlist id="cmbDISPLAY_ON_CLAIM" 
                                        onfocus="SelectComboIndex('cmbDISPLAY_ON_CLAIM')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
                                    <asp:label id="capADJUSTER_CODE" runat="server"></asp:label>
                                    <asp:label id="capADJUSTER_NAME" runat="server" Visible="False"></asp:label><span class="mandatory">*</span>
                                </TD>
								<TD class="midcolora" width="32%">
                                    <asp:textbox id="txtADJUSTER_NAME" runat="server" maxlength="35" size="35"></asp:textbox>
                                    <asp:DropDownList ID="cmbADJUSTER_CODE" runat="server"   AutoPostBack="True" onselectedindexchanged="cmbADJUSTER_CODE_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <BR>
									<asp:requiredfieldvalidator id="rfvADJUSTER_CODE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="cmbADJUSTER_CODE"></asp:requiredfieldvalidator>
                                    <asp:requiredfieldvalidator id="rfvADJUSTER_NAME" runat="server" Display="Dynamic" ControlToValidate="txtADJUSTER_NAME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revADJUSTER_NAME" runat="server" Display="Dynamic" ControlToValidate="txtADJUSTER_NAME"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr id="trRow0">
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_LEGAL_NAME" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_LEGAL_NAME" runat="server" maxlength="50" size="50"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvSUB_ADJUSTER_LEGAL_NAME" runat="server" Display="Dynamic" ErrorMessage="SUB_ADJUSTER_LEGAL_NAME can't be blank."
										ControlToValidate="txtSUB_ADJUSTER_LEGAL_NAME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSUB_ADJUSTER_LEGAL_NAME" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_LEGAL_NAME"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr id="trRow1"  >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_ADDRESS1" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:textbox id="txtSUB_ADJUSTER_ADDRESS1" runat="server" size="35" MAXLENGTH="75"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvSUB_ADJUSTER_ADDRESS1" runat="server" Display="Dynamic" ErrorMessage="SUB_ADJUSTER_ADDRESS1 can't be blank."
										ControlToValidate="txtSUB_ADJUSTER_ADDRESS1"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_ADDRESS2" runat="server"></asp:label></TD>
								<TD class="midcolora"><asp:textbox id="txtSUB_ADJUSTER_ADDRESS2" runat="server" size="35" MAXLENGTH="75"></asp:textbox></TD>
							</tr>
							<tr id="trRow2" >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_CITY" runat="server"></asp:label><span class="mandatory" id="spnSUB_ADJUSTER_CITY" runat="server">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_CITY" runat="server" maxlength="10" size="10"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvSUB_ADJUSTER_CITY" runat="server" Display="Dynamic" ErrorMessage="SUB_ADJUSTER_CITY can't be blank."
										ControlToValidate="txtSUB_ADJUSTER_CITY"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSUB_ADJUSTER_CITY" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_CITY"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_COUNTRY" runat="server">Country</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSUB_ADJUSTER_COUNTRY" onfocus="SelectComboIndex('cmbSUB_ADJUSTER_COUNTRY')"
									onchange="javascript:AdjusterCountryChanged();"	runat="server"></asp:dropdownlist>
								</TD>
							</tr>
							<tr id="trRow3"  >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_STATE" runat="server"></asp:label><span id="spnSUB_ADJUSTER_STATE" runat="server" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%">
                                    <asp:dropdownlist id="cmbSUB_ADJUSTER_STATE" 
                                        runat="server"  onchange="SetSUB_ADJUSTER_STATE()" ></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvSUB_ADJUSTER_STATE" runat="server" Display="Dynamic" ErrorMessage="SUB_ADJUSTER_STATE can't be blank."
										ControlToValidate="cmbSUB_ADJUSTER_STATE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_ZIP" runat="server"></asp:label><span id="spnSUB_ADJUSTER_ZIP" runat="server" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_ZIP" runat="server" maxlength="10" size="15"></asp:textbox>
								<%-- Added by Swarup on 30-mar-2007 --%>
									<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
									<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvSUB_ADJUSTER_ZIP" runat="server" Display="Dynamic" ErrorMessage="SUB_ADJUSTER_ZIP can't be blank."
										ControlToValidate="txtSUB_ADJUSTER_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSUB_ADJUSTER_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_ZIP"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trRow4"  >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_PHONE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_PHONE" runat="server" maxlength="13" size="15" onblur="FormatBrazilPhone()" ></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revSUB_ADJUSTER_PHONE" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_PHONE"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_FAX" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_FAX" runat="server" maxlength="13" size="15" onblur="FormatBrazilPhone()"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revSUB_ADJUSTER_FAX" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_FAX"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trRow5"  >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_EMAIL" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_EMAIL" runat="server" maxlength="50" size="35"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revSUB_ADJUSTER_EMAIL" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_EMAIL"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_WEBSITE" runat="server"></asp:label></TD>
								<TD class="midcolora"><asp:textbox id="txtSUB_ADJUSTER_WEBSITE" runat="server" size="35" MAXLENGTH="150"></asp:textbox>&nbsp;
									<INPUT class="clsButton" id="cmdGO" onclick="OpenUrl();" type="button" value="Go" name="cmdGO" 
										runat="server">
									<BR>
									<asp:regularexpressionvalidator id="revSUB_ADJUSTER_WEBSITE" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_WEBSITE"></asp:regularexpressionvalidator></TD>
							</tr>
                            <!-- Added by Agniswar for Singapore implementation-->
                            <tr id="trRow7" runat="server" visible="true" >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_GST" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,100);" id="txtSUB_ADJUSTER_GST" runat="server"></asp:textbox>
								</TD>								
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_GST_REG_NO" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,100);" id="txtSUB_ADJUSTER_GST_REG_NO" runat="server"></asp:textbox>									
								</TD>
							</tr>
                            <tr id="trRow8" runat="server" visible="true" >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_MOBILE_NO" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,100);" id="txtSUB_ADJUSTER_MOBILE_NO" runat="server"></asp:textbox>
								</TD>								
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_CLASSIFICATION" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,100);" id="txtSUB_ADJUSTER_CLASSIFICATION" runat="server"></asp:textbox>									
								</TD>
							</tr>

                            <!-- Till Here -->
							<tr id="trRow6" >
								<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_NOTES" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,100);" id="txtSUB_ADJUSTER_NOTES" runat="server" TextMode="MultiLine"
										MaxLength="100" Rows="5" Columns="45"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revSUB_ADJUSTER_NOTES" runat="server" Display="Dynamic" ControlToValidate="txtSUB_ADJUSTER_NOTES"
										Enabled="False"></asp:regularexpressionvalidator><asp:customvalidator id="csvSUB_ADJUSTER_NOTES" Display="Dynamic" ErrorMessage="Error" ControlToValidate="txtSUB_ADJUSTER_NOTES"
										ClientValidationFunction="ValidateLength" Runat="server"></asp:customvalidator></TD>								
								<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:ListBox id="cmbLOB_ID" runat="server" SelectionMode="Multiple"></asp:ListBox>										
										</td>
							</tr>
                            
							<tr>
								<TD class="pageHeader" colSpan="4">
                                    <asp:Label ID="capsubadjuster" runat="server" Text="Label"></asp:Label></TD>
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
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_CITY" runat="server" maxlength="10" size="10"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_COUNTRY" runat="server">Country</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSA_COUNTRY" onfocus="SelectComboIndex('cmbSA_COUNTRY')" onchange="javascript:SubAdjusterCountryChanged();" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSA_STATE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSA_STATE" runat="server" onchange="SetSA_STATE()"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_ZIPCODE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_ZIPCODE" runat="server" maxlength="10" size="15"></asp:textbox><%-- OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();"--%>
								<%-- Added by Swarup on 30-mar-2007 --%>
								<asp:hyperlink id="hlkSAZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgSAZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
								</asp:hyperlink><BR>
									<asp:regularexpressionvalidator id="revSA_ZIPCODE" runat="server" Display="Dynamic" ControlToValidate="txtSA_ZIPCODE"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSA_PHONE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_PHONE" runat="server" maxlength="13" size="15" onblur="FormatBrazilPhone()"   ></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revSA_PHONE" runat="server" Display="Dynamic" ControlToValidate="txtSA_PHONE"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSA_FAX" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSA_FAX" runat="server" maxlength="13" size="15" onblur="FormatBrazilPhone()"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revSA_FAX" runat="server" Display="Dynamic" ControlToValidate="txtSA_FAX"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
										CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidADJUSTER_ID" type="hidden" value="0" name="hidADJUSTER_ID" runat="server">
							<INPUT id="hidADJUSTER_CODE" type="hidden" value="0" name="hidADJUSTER_CODE" runat="server">
							<INPUT id="hidUSER_ID" type="hidden" value="0" name="hidUSER_ID" runat="server">
							<INPUT id="hidLOB_ID_LIST" type="hidden" value="" name="hidLOB_ID_LIST" runat="server">
							<INPUT id="hidAuthorityMessage" type="hidden" value="" name="hidAuthorityMessage" runat="server">
							<INPUT id="hidZipeCodeVerificationMsg" type="hidden" value="" name="hidZipeCodeVerificationMsg" runat="server">
							<INPUT id="hidSA_STATE" type="hidden" value="" name="hidSA_STATE" runat="server">
							
							<INPUT id="hidSUB_ADJUSTER_STATE" type="hidden" value="" name="hidSUB_ADJUSTER_STATE" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>			
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidADJUSTER_ID').value,true);
		</script>
	</BODY>
</HTML>

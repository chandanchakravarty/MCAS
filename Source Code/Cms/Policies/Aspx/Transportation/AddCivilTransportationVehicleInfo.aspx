<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCivilTransportationVehicleInfo.aspx.cs" Inherits="Cms.Policies.Aspx.Transportation.AddCivilTransportationVehicleInfo" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>POL_CIVIL_TRANSPORT_VEHICLES</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		 <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
		  <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script type="text/javascript" language="javascript">
		    var isPagePost=false;
		    

		    function ResetTheForm() {
		        document.POL_CIVIL_TRANSPORT_VEHICLES.reset();
		    }
		    function initPage() {
		        
		        ApplyColor();
		       

		    }
		    function showMsg(calledfrom) {

		         
		        if (document.getElementById('lblMessage') != null)
		        document.getElementById("lblMessage").innerHTML= "";


		            if (document.getElementById('txtFIPE_CODE').value == "") {
                        document.getElementById("rfvFIPE_CODE").setAttribute('enabled', false);
		                document.getElementById("rfvFIPE_CODE").setAttribute('isValid', true);
		                document.getElementById("rfvFIPE_CODE").style.display = "inline";
		                $('#txtMAKE_MODEL').val('');
		                $("#hidMAKE_MODEL").val('');
                        return false;
		               
        		        
		            }
		            else {
		                //Call The page Method to get the MakeModel Based on the Fipe code enter by user
		                $('#txtMAKE_MODEL').val('');
		                $("#hidMAKE_MODEL").val('');
		                var FipeCodeValue = $('#txtFIPE_CODE').val();
		                PageMethod("GetMakeModelUsingFipeCode", ["FipeCode", FipeCodeValue], AjaxSucceeded, AjaxFailed); //With parameters

		              
		               return false;
		               
		            }
		       
		     }
		    /* function CheckMaxLengthOfRemark(objSource, objArgs) {
		         var RemarkValue = document.POL_CIVIL_TRANSPORT_VEHICLES.txtREMARKS.value;
		         if (RemarkValue.length > 500)
		         { objArgs.IsValid = false; }
		         else
		         { objArgs.IsValid = true; }
		      }*/
		      function ChkOccurenceDate(objSource, objArgs) {
		          
		        var effdate = document.POL_CIVIL_TRANSPORT_VEHICLES.txtMANUFACTURED_YEAR.value;
		        var date = '<%=DateTime.Now.Year%>';
     
                
                if (effdate.length < 4 || effdate.length != 4 || effdate==0 || effdate<1900 || effdate>2100) {
                    objArgs.IsValid = false;
                    
                    
		        }
		        else {
		            if (effdate > date)
		                objArgs.IsValid = false;
		            else
		                objArgs.IsValid = true;
		        }

		    }
		    

		    
		</script>
     
     
     <script type="text/javascript">
         function setTab() {
             
             var firsttab = '<%=FIRSTTAB %>'
             if (document.getElementById('hidVEHICLE_ID') != null && document.getElementById('hidVEHICLE_ID').value != "NEW" && document.getElementById('hidVEHICLE_ID').value != "") {
                 var CalledFrom = '';
                 if (document.getElementById('hidCALLED_FROM') != null)
                     CalledFrom = document.getElementById('hidCALLED_FROM').value;

                 
                 if (document.getElementById('hidVEHICLE_ID') != null)
                     riskId = document.getElementById('hidVEHICLE_ID').value;
                 
                 var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                 var tabtitles = TAB_TITLES.split(',');
                 if (pagefrom == "QAPP") {// changed by sonal to implemt this in quick app

                     Url = parent.document.getElementById('hidFrameUrl').value + riskId + "&";
                     DrawTab(1, top.frames[1], firsttab, Url);
                 }

                 if (document.getElementById('hidCO_APPLICANT_ID') != null)
                     CO_APPLICANT_ID = document.getElementById('hidCO_APPLICANT_ID').value;
                 
                 Url = "/Cms/Policies/Aspx/AddDiscountSurcharge.aspx?CalledFrom=" + CalledFrom + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID+"&";
                 DrawTab(2, top.frames[1], tabtitles[0], Url);

                 Url = "/Cms/Policies/Aspx/AddpolicyCoverages.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[1] + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID+"&";
                 DrawTab(3, top.frames[1], tabtitles[1], Url); 


              
             }
             else {
                 RemoveTab(3, top.frames[1]);
                 RemoveTab(2, top.frames[1]);

             }
             return false;
         }

         function getQuerystring(key, default_) {
             if (default_ == null) default_ = "";
             key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
             var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
             var qs = regex.exec(window.location.href);
             if (qs == null)
                 return default_;
             else
                 return qs[1];
         }
         function ResetTheForm() {
             document.POL_CIVIL_TRANSPORT_VEHICLES.reset();
         }  
    </script>
    <%--Populate the MakeModel based on the FipeCode using jQuery ------- Added by Pradeep Kushwaha on 04 May 2010--%>
     <script type="text/javascript" language="javascript">

         $(document).ready(function() {

             var CALLED_FROM = '<%=CalledFrom%>';
             $("#txtFIPE_CODE").change(function() {
                    if (CALLED_FROM != "AERO") {
                     $("#txtMAKE_MODEL").val('');
                     $("#hidMAKE_MODEL").val('');
                 }
             });
             $("#txtZIP_CODE").change(function() {

                 if (trim($('#txtZIP_CODE').val()) != '') {
                     var ZIPCODE = $("#txtZIP_CODE").val();
                     var COUNTRYID = "5";
                     PageMethod("GetValidateZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                 }
                 // changes by praveer for itrack no 1473
//                 else {
//                     $("#txtZIP_CODE").val('');
//                 }
             });


             $("#txtCLIENT_ORDER").change(function() {
                 if ($("#txtCLIENT_ORDER").val() != $("#hidOLD_CLIENT_ORDER").val()) {
                     if (trim($('#txtCLIENT_ORDER').val()) != '') {
                         var CUSTOMER_ID = $('#hidCUSTOMER_ID').val();
                         var POLICY_ID = $('#hidPOLICY_ID').val();
                         var POLICY_VERSION_ID = $('#hidPOLICY_VERSION_ID').val();
                         var CLIENTORDER = $('#txtCLIENT_ORDER').val();
                         var VEHICLENUMBER = $('#txtVEHICLE_NUMBER').val();
                         var flag = "2";

                         PageMethod("GetMaxIdofClientOrderAndVehicleNumber", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID, "CLIENTORDER", CLIENTORDER, "VEHICLENUMBER", VEHICLENUMBER, "flag", flag], AjaxSucceeded, AjaxFailed); //With parameters
                     }
                 }
             });

             $("#txtVEHICLE_NUMBER").change(function() {
                 if ($("#txtVEHICLE_NUMBER").val() != $("#hidOLD_VEHICLE_NUMBER").val()) {
                     if (trim($('#txtVEHICLE_NUMBER').val()) != '') {
                         var CUSTOMER_ID = $('#hidCUSTOMER_ID').val();
                         var POLICY_ID = $('#hidPOLICY_ID').val();
                         var POLICY_VERSION_ID = $('#hidPOLICY_VERSION_ID').val();
                         var CLIENTORDER = $('#txtCLIENT_ORDER').val();
                         var VEHICLENUMBER = $('#txtVEHICLE_NUMBER').val();
                         var flag = "3";

                         PageMethod("GetMaxIdofClientOrderAndVehicleNumber", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID, "CLIENTORDER", CLIENTORDER, "VEHICLENUMBER", VEHICLENUMBER, "flag", flag], AjaxSucceeded, AjaxFailed); //With parameters
                     }

                 }
             });

         });
         function PageMethod(fn, paramArray, successFn, errorFn) {
              
             var pagePath = window.location.pathname;
             //Create list of parameters in the form:  
             //{"paramName1":"paramValue1","paramName2":"paramValue2"}  
             var paramList = '';
             if (paramArray.length > 0) {
                 for (var i = 0; i < paramArray.length; i += 2) {
                     if (paramList.length > 0) paramList += ',';
                     paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                     if (paramArray[i] == "FipeCode") {
                         $('#hidFipeCodeParam').val(paramArray[i]);
                     }
                     if (paramArray[i] == "ZIPCODE") {
                         $('#hidZIPCODE').val(paramArray[i]);
                     }
                     
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
             
             var numbers = result.d;
            
             if ($('#hidZIPCODE').val() == "ZIPCODE") {
                 var Address = numbers.split('^');
                 var Zipecode = $("#txtZIP_CODE").val().replace('-','');

                 if (result.d != "" && Address[1] != undefined ) {
                     $("#txtZIP_CODE").val(Address[2]);
                     $("#txtZIP_CODE").blur();
                 }
                 else {
                     // changes by praveer for itrack no 1473
                   //  $("#txtZIP_CODE").val('');
                  //   $("#txtZIP_CODE").focus();
                  //   ValidatorEnable(rfvZIP_CODE, true)
                     alert($("#hidZIP_CodeMsg").val());
                     return false;
                     
                    
                 }
                 $('#hidZIPCODE').val('');
             }
             else {

                 var number = numbers.split(',');
                 if ($('#hidFipeCodeParam').val() == "FipeCode" && number[3] != "false" && number[4] != "true") {

                     if (result.d == "") {
                         var msg = $('#hidMsg').val(); //get the Message Code
                         $('#lblmsg').text(msg); //Display the message
                         $('#txtMAKE_MODEL').val(""); //Set blank in makemodel textbox
                         $("#hidMAKE_MODEL").val('');
                         $("#hidCATEGORY").val('');
                         $("#hidCAPACITY").val('');
                         $("#cmbCATEGORY").removeAttr('disabled');
                         $("#txtCAPACITY").removeAttr('disabled');

                     }
                     else {
                         $('#lblmsg').text('');
                         $('#txtMAKE_MODEL').val(result.d.split('~')[0]); //Set value in makemodel textbox  (if fipe code matches )
                         $("#hidMAKE_MODEL").val(result.d.split('~')[0]);
                         $("#cmbCATEGORY").val(result.d.split('~')[1]);
                         $("#hidCATEGORY").val(result.d.split('~')[1]);
                         $("#hidCAPACITY").val(result.d.split('~')[2]);
                         $("#txtCAPACITY").val(result.d.split('~')[2]);

                         $("#cmbCATEGORY").attr("disabled", "disabled");
                         $("#txtCAPACITY").attr("disabled", "disabled");

                         $('#lblmsg').text(''); //Set blank in message label  (if fipe code matches )
                     }
                     $('#hidFipeCodeParam').val('');
                 }
                 else {
                     if ((number[3] == "4") && (number[2] == "2")) {

                         //Client order exist
                         document.getElementById("rfvCLIENT_ORDER").setAttribute('enabled', false);
                         document.getElementById("rfvCLIENT_ORDER").setAttribute('isValid', true);
                         document.getElementById("rfvCLIENT_ORDER").style.display = "inline";
                         $('#rfvCLIENT_ORDER').text($('#hidCliendOrderMsg').val());
                     }
                     else if ((number[3] == "5") && (number[2] == "2")) {

                         //client order not exist
                         document.getElementById("rfvCLIENT_ORDER").setAttribute('enabled', false);
                         document.getElementById("rfvCLIENT_ORDER").setAttribute('isValid', true);
                         document.getElementById("rfvCLIENT_ORDER").style.display = "none";


                     }
                     else if ((number[3] == "6") && (number[2] == "3")) {

                         //vehicle number exist
                         document.getElementById("rfvVEHICLE_NUMBER").setAttribute('enabled', false);
                         document.getElementById("rfvVEHICLE_NUMBER").setAttribute('isValid', true);
                         document.getElementById("rfvVEHICLE_NUMBER").style.display = "inline";
                         $('#rfvVEHICLE_NUMBER').text($('#hidVehicleMsg').val());

                     }
                     else if ((number[3] == "7") && (number[2] == "3")) {

                         //vehicle number not exist
                         document.getElementById("rfvVEHICLE_NUMBER").setAttribute('enabled', false);
                         document.getElementById("rfvVEHICLE_NUMBER").setAttribute('isValid', true);
                         document.getElementById("rfvVEHICLE_NUMBER").style.display = "none";

                     }

                 } 
             }
         }
         function AjaxFailed(result) {
              
             //alert(result.status + ' ' + result.statusText);//Display the error message (If there is any error while retriving record)
         }
      
</script>  
    <%-- End jQuery Implimentation for FipeCode --%>
     
</head>
<body  leftMargin="0" topMargin="0" onload="initPage();setTab();">
    <form id="POL_CIVIL_TRANSPORT_VEHICLES" runat="server" method="post">
  
    <table cellspacing="2" cellpadding="2" width="100%" border="0" class="tableWidthHeader">
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblmsg" runat="server" CssClass="errmsg" ></asp:Label>
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
       <td>
       <table cellspacing="2" cellpadding="2" width="100%" border="0" border="0">  
        <td class="pageHeader" colspan="3">
             <asp:Label ID="capMandatoryNotes" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capCLIENT_ORDER" runat="server" Text="Client Order #"></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtCLIENT_ORDER" runat="server" MaxLength="10" 
                       ></asp:TextBox><br />
                <asp:regularexpressionvalidator id="revCLIENT_ORDER" runat="server" 
                    Display="Dynamic" ControlToValidate="txtCLIENT_ORDER"></asp:regularexpressionvalidator>
                <asp:RequiredFieldValidator ID="rfvCLIENT_ORDER" runat="server" 
                    ControlToValidate="txtCLIENT_ORDER" Display="Dynamic"></asp:RequiredFieldValidator>
                 
               
               
											
            </td>
            <td  width="33%" class="midcolora">
                
                <asp:Label ID="capVEHICLE_NUMBER" runat="server" Text="Vehicle #"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtVEHICLE_NUMBER" runat="server" MaxLength="10" 
                      ></asp:TextBox><br />
                <asp:regularexpressionvalidator id="revVEHICLE_NUMBER" runat="server" 
                    Display="Dynamic" ControlToValidate="txtVEHICLE_NUMBER"></asp:regularexpressionvalidator>
             
               
											
                <asp:RequiredFieldValidator ID="rfvVEHICLE_NUMBER" runat="server" 
                    ControlToValidate="txtVEHICLE_NUMBER" Display="Dynamic"></asp:RequiredFieldValidator>
               
               
											
            </td>
            <td width="33%"class="midcolora">
                <asp:Label ID="capMANUFACTURED_YEAR" runat="server" Text="Year"></asp:Label><span class="mandatory">*</span> </br>
              
                <asp:TextBox ID="txtMANUFACTURED_YEAR" runat="server" MaxLength="4"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revMANUFACTURED_YEAR" runat="server" 
                    Display="Dynamic" ControlToValidate="txtMANUFACTURED_YEAR"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvMANUFACTURED_YEAR" Runat="server" ControlToValidate="txtMANUFACTURED_YEAR" Display="Dynamic"
											ClientValidationFunction="ChkOccurenceDate"></asp:customvalidator>
              
                <asp:RequiredFieldValidator ID="rfvMANUFACTURED_YEAR" runat="server" 
                    ControlToValidate="txtMANUFACTURED_YEAR" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td  width="33%" class="midcolora">
                
                <asp:Label ID="capFIPE_CODE" runat="server" Text="Fipe Code"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtFIPE_CODE" runat="server" Width="40%" MaxLength="20" ></asp:TextBox>
                <cmsb:cmsbutton class="clsButton" id="btnMakeModel" runat="server" 
                    text="Make/Model" onclientclick="return showMsg(this);"   
                    CausesValidation="False"></cmsb:cmsbutton> <br />
               
                <asp:regularexpressionvalidator id="revFIPE_CODE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtFIPE_CODE"></asp:regularexpressionvalidator> 
                <asp:RequiredFieldValidator ID="rfvFIPE_CODE" runat="server" 
                    ControlToValidate="txtFIPE_CODE" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capCATEGORY" runat="server" Text="Category"></asp:Label></br>
                <asp:DropDownList ID="cmbCATEGORY" runat="server" Width="99%">
                </asp:DropDownList>
                
                 
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capCAPACITY" runat="server" Text="Capacity"></asp:Label></br>
                <asp:TextBox ID="txtCAPACITY" runat="server" MaxLength="3"></asp:TextBox><br />
                <asp:regularexpressionvalidator id="revCAPACITY" runat="server" 
                    Display="Dynamic" ControlToValidate="txtCAPACITY"></asp:regularexpressionvalidator>
            </td>
        </tr>
        <tr>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capMAKE_MODEL" runat="server" Text="Make/Model"></asp:Label></br>
                <asp:TextBox ID="txtMAKE_MODEL" runat="server" Width="55%" MaxLength="50" 
                    ReadOnly="True"></asp:TextBox>
                
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capLICENSE_PLATE" runat="server" Text="License Plate"></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtLICENSE_PLATE" runat="server" MaxLength="7"></asp:TextBox><br />
                 <asp:RequiredFieldValidator ID="rfvLICENSE_PLATE" runat="server" 
                    ControlToValidate="txtLICENSE_PLATE" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:regularexpressionvalidator id="revLICENSE_PLATE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtLICENSE_PLATE"></asp:regularexpressionvalidator>
             
               
											
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capCHASSIS" runat="server" Text="Chassis"></asp:Label><span class="mandatory">*</span> </br>
                <asp:TextBox ID="txtCHASSIS" runat="server" Width="55%" MaxLength="25"></asp:TextBox><br />
                 <asp:RequiredFieldValidator ID="rfvCHASSIS" runat="server" 
                    ControlToValidate="txtCHASSIS" Display="Dynamic"></asp:RequiredFieldValidator>
               <asp:regularexpressionvalidator id="revCHASSIS" runat="server" Display="Dynamic" ControlToValidate="txtCHASSIS"></asp:regularexpressionvalidator>
             
            </td>
        </tr>
        <%--<tr>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capMANDATORY_DEDUCTIBLE" runat="server" Text="Mandatory Deductible" Visible="false"></asp:Label> </br>
                <asp:TextBox ID="txtMANDATORY_DEDUCTIBLE" runat="server" Width="33%" Visible="false"></asp:TextBox><br />
               
                <asp:regularexpressionvalidator id="revMANDATORY_DEDUCTIBLE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtMANDATORY_DEDUCTIBLE" Visible="false"></asp:regularexpressionvalidator>
             
               
											
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capFACULTATIVE_DEDUCTIBLE" runat="server" Text="Facultative Deductible" Visible="false"></asp:Label>   </br>
                <asp:TextBox ID="txtFACULTATIVE_DEDUCTIBLE" runat="server" Width="33%" Visible="false"></asp:TextBox><br />
                <asp:regularexpressionvalidator id="revFACULTATIVE_DEDUCTIBLE" runat="server" Display="Dynamic" ControlToValidate="txtFACULTATIVE_DEDUCTIBLE" Visible="false"></asp:regularexpressionvalidator>
                
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capSUB_BRANCH" runat="server" Text="Sub Branch"></asp:Label></br>
                <asp:DropDownList ID="cmbSUB_BRANCH" runat="server">
                </asp:DropDownList>
                
            </td>
        </tr>--%>
        <tr>
            <td  width="33%" class="midcolora">
            
             <%--<asp:Label ID="capREGION" runat="server" Text="Region"></asp:Label> </br>
                <asp:DropDownList ID="cmbREGION" runat="server">
                </asp:DropDownList>--%>
                
                   <asp:Label ID="capZIP_CODE" runat="server" Text="" ></asp:Label><span class="mandatory">*</span> </br>
                      <%--  changes by praveer for itrack no 1473--%>
                <asp:TextBox ID="txtZIP_CODE" runat="server" MaxLength="8" OnBlur="this.value=FormatZipCode(this.value);"></asp:TextBox>
                   <%--<asp:HyperLink ID="hlkZipLookup" Visible="true" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                        ImageAlign="Bottom" Visible="true"></asp:Image>
                </asp:HyperLink>--%>
                <br>              
                <asp:RequiredFieldValidator ID="rfvZIP_CODE" runat="server" ControlToValidate="txtZIP_CODE"
                    Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revZIP_CODE"
                        runat="server" ControlToValidate="txtZIP_CODE" Display="Dynamic"></asp:RegularExpressionValidator> 
            </td>
            <td  width="33%" class="midcolora">
                
                <asp:Label ID="capRISK_EFFECTIVE_DATE" runat="server" Text="Risk Effective Date"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtRISK_EFFECTIVE_DATE"  runat="server"></asp:TextBox>
                 <asp:HyperLink ID="hlkRISK_EFFECTIVE_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgRISK_EFFECTIVE_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
                
               
                <asp:regularexpressionvalidator id="revRISK_EFFECTIVE_DATE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtRISK_EFFECTIVE_DATE"></asp:regularexpressionvalidator> 
                <asp:RequiredFieldValidator ID="rfvRISK_EFFECTIVE_DATE" runat="server" 
                    ControlToValidate="txtRISK_EFFECTIVE_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
                
               
            </td>
            <td width="33%" class="midcolora">
            
            <asp:Label ID="capRISK_EXPIRE_DATE" runat="server" Text="Risk Expire Date"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtRISK_EXPIRE_DATE" runat="server"  ></asp:TextBox>
                <asp:HyperLink ID="hlkRISK_EXPIRE_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgRISK_EXPIRE_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image> </asp:HyperLink><br />
               
               
                <asp:regularexpressionvalidator id="revRISK_EXPIRE_DATE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtRISK_EXPIRE_DATE"></asp:regularexpressionvalidator> 
             
               
											
                <asp:RequiredFieldValidator ID="rfvRISK_EXPIRE_DATE" runat="server" 
                    ControlToValidate="txtRISK_EXPIRE_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
               
               
                <asp:CompareValidator ID="cvRISK_EXPIRE_DATE" runat="server" 
                    ControlToCompare="txtRISK_EFFECTIVE_DATE" 
                    ControlToValidate="txtRISK_EXPIRE_DATE" Display="Dynamic" 
                    Operator="GreaterThan" Type="Date"></asp:CompareValidator>
               
               
            </td>
        </tr>
        <tr>
            <td  width="33%" class="midcolora">
            
                <asp:Label ID="capCOV_GROUP_CODE" runat="server" Text="Cov Group Code"></asp:Label>  </br>
                <asp:TextBox ID="txtCOV_GROUP_CODE" runat="server" Width="33%" MaxLength="15"></asp:TextBox><br />
                <asp:regularexpressionvalidator id="revCOV_GROUP_CODE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtCOV_GROUP_CODE"></asp:regularexpressionvalidator>
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capFINANCE_ADJUSTMENT" runat="server" Text="Finance Adjustment"></asp:Label> </br>
                <asp:TextBox ID="txtFINANCE_ADJUSTMENT" runat="server" Width="33%" 
                    MaxLength="50"></asp:TextBox><br />
               
                <asp:regularexpressionvalidator id="revFINANCE_ADJUSTMENT" runat="server" 
                    Display="Dynamic" ControlToValidate="txtFINANCE_ADJUSTMENT"></asp:regularexpressionvalidator>
             
               
											
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capREFERENCE_PROPOSASL" runat="server" Text="Reference Proposal"></asp:Label> </br>
                <asp:TextBox ID="txtREFERENCE_PROPOSASL" runat="server" Width="33%" 
                    MaxLength="50"></asp:TextBox><br />
               
                <asp:regularexpressionvalidator id="revREFERENCE_PROPOSASL" runat="server" 
                    Display="Dynamic" ControlToValidate="txtREFERENCE_PROPOSASL"></asp:regularexpressionvalidator>
             
               
											
            </td>
        </tr>
        <tr   >
         <%-- itrack 1400, modified by naveen--%>
            <td id="tdcoapplicant" class="midcolora"  runat="server">
                <asp:Label ID="capCoApplicant" runat="server" Text=""></asp:Label><span class="mandatory" id="spn_mandatory">*</span><br />
                <asp:DropDownList ID="cmbCO_APPLICANT_ID" runat="server" >
                </asp:DropDownList>
                <br />
                 <asp:requiredfieldvalidator id="rfvCO_APPLICANT" runat="server" ControlToValidate="cmbCO_APPLICANT_ID" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                </td>

                      <td class="midcolora"  colspan="2" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
               
          
        </tr>
        <tr>
         <td id="tdsubbranch"  colspan="3" class="midcolora" runat="server">
                <asp:Label ID="capSUB_BRANCH" runat="server" Text="Sub Branch"></asp:Label></br>
                <asp:DropDownList ID="cmbSUB_BRANCH" runat="server">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
         <td  width="33%" class="midcolora" colspan="3" >
                <asp:Label ID="capREMARKS" runat="server" Text="Remarks"></asp:Label> </br>
                <asp:TextBox ID="txtREMARKS" runat="server" TextMode="MultiLine" Rows="3" 
                    Width="400px" Height="70px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox><br />
          
              
         
											
            </td>
        </tr>
        <tr>
            <td   class="midcolora" >
                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" text="Reset"  CausesValidation="False"></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" causesvalidation="false"
                    text="Activate/Deactivate" onclick="btnActivateDeactivate_Click"></cmsb:cmsbutton>
            </td>
            <td   class="midcolora">
            </td>
            <td  class="midcolorr" >
                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" text="Delete" 
                    causesvalidation="false" onclick="btnDelete_Click"></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server"   text="Save" onclick="btnSave_Click" 
                    ></cmsb:cmsbutton>
            </td>
        </tr>
    </table>
     
	<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
    <input id="hidVEHICLE_ID" type="hidden" value="" name="hidVEHICLE_ID" runat="server"/>
    <input id="hidMAKE_MODEL" type="hidden" value="" name="hidMAKE_MODEL" runat="server"/>   
    <input id="hidMsg" type="hidden" value="0" name="hidMsg" runat="server"/>    
    <input id="hidTAB_TITLES" type="hidden" value="0" name="hidTAB_TITLES" runat="server"/>         
    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>    
    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>  
    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>  
	<input id="hidFipeCodeParam" type="hidden" value="0" name="hidFipeCodeParam" runat="server"/> 
	<input id="hidCliendOrderMsg" type="hidden" value="0" name="hidCliendOrderMsg" runat="server"/> 
	<input id="hidVehicleMsg" type="hidden" value="0" name="hidVehicleMsg" runat="server"/> 
	<input type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	<input type="hidden" runat="server" ID="hidCATEGORY"  value=""  name="hidCATEGORY"/>   
	<input type="hidden" runat="server" ID="hidCAPACITY"  value=""  name="hidCAPACITY"/>  
	<input type="hidden" runat="server"  id="hidOLD_CLIENT_ORDER" value="" name="hidOLD_CLIENT_ORDER" /> 
	<input type="Hidden" runat="server" ID="hidOLD_VEHICLE_NUMBER"  value=""  name="hidOLD_VEHICLE_NUMBER"/>   
	<input type="hidden" runat="server" ID="hidZIPCODE"    name="hidZIPCODE"/>  
	<input type="hidden" runat="server" ID="hidZIP_CodeMsg"    name="hidZIP_CodeMsg"/>
	<input type="hidden" runat="server" ID="hidCO_APPLICANT_ID"    name="hidCO_APPLICANT_ID"/>  
	            
		  
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {
        
        
          var pagefrom = '<%=PAGEFROM %>'
          if (pagefrom == "QAPP") {

              parent.BindRisk();
              for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {

                  if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidVEHICLE_ID').value)
                      parent.document.getElementById('cmbRisk').options[i].selected = true;

              }
          }
          else {
              try {


                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidVEHICLE_ID').value);
              }
              catch (err) {

              }
          }
            
        }
		</script>
</body>
</html>

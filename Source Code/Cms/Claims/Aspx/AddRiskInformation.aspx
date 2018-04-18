<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRiskInformation.aspx.cs" Inherits="Cms.Claims.Aspx.AddRiskInformation" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RiskInformation</title>
    
      <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
 <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	 <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
   
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function LocationCountryChanged() {
			   	   
			    GlobalError = true;
			    var CountryID = document.getElementById('cmbLOCATION_COUNTRY').options[document.getElementById('cmbLOCATION_COUNTRY').selectedIndex].value;
			    AddRiskInformation.AjaxFillState(CountryID, FillState);
			    if (GlobalError) {
			        return false;
			    }
			    else {
			        return true;
			    }
			}
			function VoyageDestinationCountryChanged() {
			    
			    GlobalError = true;
			    var CountryID = document.getElementById('cmbVOYAGE_DESTINATION_COUNTRY').options[document.getElementById('cmbVOYAGE_DESTINATION_COUNTRY').selectedIndex].value;
			    AddRiskInformation.AjaxFillState(CountryID, FillVoyageDestinationState);
			    if (GlobalError) {
			        return false;
			    }
			    else {
			        return true;
			    }
			}

			function VoyageOriginCountryChanged() {
			    
			    GlobalError = true;
			    var CountryID = document.getElementById('cmbVOYAGE_ORIGIN_COUNTRY').options[document.getElementById('cmbVOYAGE_ORIGIN_COUNTRY').selectedIndex].value;
			    AddRiskInformation.AjaxFillState(CountryID, FillVoyageOriginState);
			    if (GlobalError) {
			        return false;
			    }
			    else {
			        return true;
			    }
			}
			function FillVoyageDestinationState(Result) {
			    //var strXML;
			    if (Result.error) {
			        var xfaultcode = Result.errorDetail.code;
			        var xfaultstring = Result.errorDetail.string;
			        var xfaultsoap = Result.errorDetail.raw;
			    }
			    else {
			        var statesList = document.getElementById("cmbVOYAGE_DESTINATION_STATE");
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
			            document.getElementById('hidVOYAGE_DESTINATION_STATE').value = statesList.options[0].value;
			        }
			        document.getElementById("cmbVOYAGE_DESTINATION_STATE").value = document.getElementById("cmbVOYAGE_DESTINATION_STATE").value;
			    }

			    return false;
			}


			function FillVoyageOriginState(Result) {
			    //var strXML;
			    if (Result.error) {
			        var xfaultcode = Result.errorDetail.code;
			        var xfaultstring = Result.errorDetail.string;
			        var xfaultsoap = Result.errorDetail.raw;
			    }
			    else {
			        var statesList = document.getElementById("cmbVOYAGE_ORIGIN_STATE");
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
			            document.getElementById('hidVOYAGE_ORIGIN_STATE').value = statesList.options[0].value;
			        }
			        document.getElementById("cmbVOYAGE_ORIGIN_STATE").value = document.getElementById("cmbVOYAGE_ORIGIN_STATE").value;
			    }

			    return false;
			}
			function FillState(Result) 
			{
			    //var strXML;
			    if (Result.error) {
			        var xfaultcode = Result.errorDetail.code;
			        var xfaultstring = Result.errorDetail.string;
			        var xfaultsoap = Result.errorDetail.raw;
			    }
			    else {
			        var statesList = document.getElementById("cmbLOCATION_STATE");
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
			        if (statesList.options.length > 0)
			        {
			            statesList.remove(0);
			            document.getElementById('hidLOCATION_STATE').value = statesList.options[0].value;			            
			        }
			        document.getElementById("cmbLOCATION_STATE").value = document.getElementById("cmbLOCATION_STATE").value;
			    }

			    return false;
			}

			function ValidateYear(objSource, objArgs) {

			    var effdate = document.getElementById("txtVESSEL_MANUFACTURED_YEAR").value ;
			    var date = '<%=DateTime.Now.Year%>';
			    
			    if (effdate.length < 4 ||isNaN(effdate)) {
			        objArgs.IsValid = false;
			        
			    }
			    else {
			        if (effdate > date || effdate < 1900)
			            objArgs.IsValid = false;
			        else
			            objArgs.IsValid = true;
			    }

			}

			function ValidateVehicleYear(objSource, objArgs) {

			    var effdate = document.getElementById("txtVEHICLE_YEAR").value;
			    var date = '<%=DateTime.Now.Year%>';

			    

			    if (effdate.length < 4 || isNaN(effdate)) {
			        objArgs.IsValid = false;

			    }
			    else {
			        if (effdate > date || effdate < 1900)
			            objArgs.IsValid = false;
			        else
			            objArgs.IsValid = true;
			    }

			}


			function GetLocationStateValue(cmbLocation) {

			    var strLocationState = cmbLocation.options[cmbLocation.selectedIndex].value;
			    if (strLocationState != "")
			        document.getElementById('hidLOCATION_STATE').value = strLocationState;


			}

			function GetVoyageDestinationStateValue(cmbState) {

			    var strState = cmbState.options[cmbState.selectedIndex].value;
			    if (strState != "")
			        document.getElementById('hidVOYAGE_DESTINATION_STATE').value = strState;


			}

			function GetVoyageOriginStateValue(cmbState) {

			    var strState = cmbState.options[cmbState.selectedIndex].value;
			    if (strState != "")
			        document.getElementById('hidVOYAGE_ORIGIN_STATE').value = strState;


			}
			function ResetTheForm() {
			    document.RiskInformation.reset();
			    return false;
			}
			
			function initPage() {

			    ApplyColor();

                 }

			function ValidateFESRChkList(source, arguments) {
			    if ($("#chkRURAL_FESR_COVERAGE").attr("checked") == true) {
			        arguments.IsValid = true;
			    }
			    else {
			        arguments.IsValid = false;
			    }
			}

			function ValidateDiseaseDate(objSource, objArgs) {

			    if (document.getElementById('csvPERSON_DISEASE_DATE').isvalid == false)//Added By Abhinav For Itrack-971
			        return 
			   
			    var DiseaseDate   = document.getElementById(objSource.controltovalidate).value;
			    var PolicyEffDate = document.getElementById('hidPOLICY_EFFECTIVE_DATE').value;
			    var PolicyExpDate = document.getElementById('hidPOLICY_EXPIRATION_DATE').value;
			   // DiseaseDate = new Date(DiseaseDate);
			    var TodayDate = '<%=DateTime.Now.ToShortDateString()%>';
			 //   TodayDate = new Date(TodayDate);

			    var aDateArr;
			    var strDay;
			    var strMonth;
			    var strYear;
			    var sDiseaseDate;
			    var sTodayDate;
			    var sPolicyEffDate;
			    var sPolicyExpDate;
			    
			    //Added for Multilingual Support
			    if (sCultureDateFormat == 'DD/MM/YYYY') {
			        aDateArr = DiseaseDate.split('/');

			        strDay         = aDateArr[0];
			        strMonth       = aDateArr[1];
			        strYear        = aDateArr[2];

			        sDiseaseDate   = strMonth + '/' + strDay + '/' + strYear

			        aDateArr       = TodayDate.split('/');
			        strDay         = aDateArr[0];
			        strMonth       = aDateArr[1];
			        strYear        = aDateArr[2];
			        sTodayDate     = strMonth + '/' + strDay + '/' + strYear

			        aDateArr       = PolicyEffDate.split('/');
			        strDay         = aDateArr[0];
			        strMonth       = aDateArr[1];
			        strYear        = aDateArr[2];
			        sPolicyEffDate = strMonth + '/' + strDay + '/' + strYear

			        aDateArr       = PolicyExpDate.split('/');
			        strDay         = aDateArr[0];
			        strMonth       = aDateArr[1];
			        strYear        = aDateArr[2];
			        sPolicyExpDate = strMonth + '/' + strDay + '/' + strYear

			        PolicyEffDate  = new Date(sPolicyEffDate);
			        PolicyExpDate  = new Date(sPolicyExpDate);
			        TodayDate      = new Date(sTodayDate);
			        DiseaseDate    = new Date(sDiseaseDate);
			    }
			    else
			    {
			        PolicyEffDate  = new Date(PolicyEffDate);
			        PolicyExpDate  = new Date(PolicyExpDate);
			        TodayDate      = new Date(TodayDate);
			        DiseaseDate    = new Date(DiseaseDate);
			    }
			    // IF DISEASE DATE IS FUTURE DATE
			    if (DiseaseDate > TodayDate) {
			        objArgs.IsValid = false;
			        objSource.innerHTML = document.getElementById('hidFUTURE_DATE_MSG').value;
			        
			    }
			    // IF DISEASE DATE SHOULD BE BETWEEN POLICY EFFECTIVE AND EXPIRY DATE
			    else if (DiseaseDate < PolicyEffDate || DiseaseDate > PolicyExpDate) {
			       objArgs.IsValid = false;
			       objSource.innerHTML = document.getElementById('hidPOLICY_DATE_MSG').value;
			    }
			    // NO ERROR
			    else 
			    {
			        objArgs.IsValid = true;
			        return true;
			    }


			}

            

		</script>
	    
	    <style type="text/css">
            .midcolora
            {
                width: 0%;
            }
        </style>
	    
	    </head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();">
<div id="bodyHeight" class="pageContent" style='width:100%; overflow-x: auto;'>
    <form id="RiskInformation" runat="server">
    	<table width="100%" align="center" border="0">
							<tr>
								<td class="pageHeader"><asp:label id="lblRequiredFieldsInformation" runat="server" 
                                        ></asp:label>
                              </td>
							</tr>
							<tr>
								<td class="midcolorc" align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                </td>
							</tr>
						
							<tr>
								<td >
								   <table id="TblVehicle" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td  class="midcolora"  colspan="4" width="25%">
                                                <asp:label id="capPOL_VEHICLE_ID" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbPOL_VEHICLE_ID" 
                                                     runat="server" 
                                                    AutoPostBack="True" onselectedindexchanged="cmbPOL_VEHICLE_ID_SelectedIndexChanged" 
                                                   >
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvVehicle_ID" runat="server" 
                                                    ControlToValidate="cmbPOL_VEHICLE_ID"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVEHICLE_VIN" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVEHICLE_VIN" runat="server" size="32" maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora"  width="25%" valign="top">
                                                <asp:label id="capVEHICLE_YEAR" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVEHICLE_YEAR" runat="server" size="4" maxlength="4" 
                                                    Width="80px"></asp:textbox>
                                                <br />
                                            <asp:customvalidator id="csvVEHICLE_YEAR" Runat="server" 
                                                    ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic"
											ClientValidationFunction="ValidateVehicleYear"></asp:customvalidator>
              
                                                <br />
              
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                              <asp:label id="capVEHICLE_MODEL" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVEHICLE_MODEL" runat="server" size="32" maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora"  width="25%" valign="top">
                                                <asp:label id="capVEHICLE_LIC_PT_NUMBER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVEHICLE_LIC_PT_NUMBER" runat="server" size="32" 
                                                    maxlength="50"></asp:textbox>
				                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVEHICLE_DAMAGE_TYPE" runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbVEHICLE_DAMAGE_TYPE" 
                                                     runat="server" 
                                                   >
									</asp:dropdownlist>
              
                                            </td>
                                            <td class="midcolora"  width="25%" valign="top" colspan="2" style="width: 50%">
                                                <asp:label id="capVEHICLE_INSURED_PLEADED_GUILTY" runat="server"></asp:label>
                                                <br />
                                                <asp:CheckBox ID="chkVEHICLE_INSURED_PLEADED_GUILTY" runat="server" />
                                                <asp:label id="capYES" runat="server"></asp:label>
              
                                            </td>
                                            <td class="midcolora" valign="top">
						                    <asp:Label id="capVEHICLE_MAKER" style="display:none;" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtVEHICLE_MAKER" style="display:none;"  runat="server" size="32" maxlength="150"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" colspan="4">
                                                <br />
                                            </td>
                                        </tr>
                                        </table>
								</td>					
				                </tr>
								
                                    <tr>
								<td  >
								  <table id="TblVessel" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" colspan="4" width="25%">
                                                <asp:label id="capPOL_VESSEL_ID" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbPOL_VESSEL_ID" 
                                                    runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPOL_VESSEL_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvPOL_VESSEL_ID" runat="server" 
                                                    ControlToValidate="cmbPOL_VESSEL_ID"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVESSEL_NAME" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVESSEL_NAME" runat="server" size="32" maxlength="70"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVESSEL_TYPE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVESSEL_TYPE" runat="server" size="32" maxlength="70"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
						<asp:Label id="capVESSEL_MANUFACTURED_YEAR" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtVESSEL_MANUFACTURED_YEAR" runat="server" size="32" 
                                                    maxlength="4" Width="80px"></asp:textbox>
                                                <br />
                                            <asp:customvalidator id="csvVESSEL_MANUFACTURED_YEAR" Runat="server" ControlToValidate="txtVESSEL_MANUFACTURED_YEAR" Display="Dynamic"
											ClientValidationFunction="ValidateYear"></asp:customvalidator>
              
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVESSEL_MANUFACTURER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVESSEL_MANUFACTURER" runat="server" size="32" 
                                                    maxlength="50"></asp:textbox>
				                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVESSEL_NUMBER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVESSEL_NUMBER" runat="server" size="32" 
                                                    maxlength="50"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                &nbsp;</td>
                                            <td class="midcolora" width="25%" valign="top">
						                        &nbsp;</td>
                                            <td class="midcolora" width="25%" valign="top">
                                                &nbsp;</td>
                                        </tr>
                                       
                                        </table>
								</td>					
				                </tr>
                                         <tr>
								<td  >
                                      <table id="TblLocation" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora"  colspan="4" width="25%">
                                                <asp:label id="capPOL_LOCATION_ID" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbPOL_LOCATION_ID" 
                                                    runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPOL_LOCATION_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvPOL_LOCATION_ID" runat="server" 
                                                    ControlToValidate="cmbPOL_LOCATION_ID"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora"  width="25%" valign="top">
                                                <asp:label id="capLOCATION_ADDRESS" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtLOCATION_ADDRESS" runat="server" size="32" maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capLOCATION_COMPLIMENT" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtLOCATION_COMPLIMENT" runat="server" size="32" 
                                                    MaxLength="75"></asp:textbox>
                                            </td>
                                            <td class="midcolora"  width="25%" valign="top">
						<asp:Label id="capLOCATION_DISTRICT" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtLOCATION_DISTRICT" runat="server" size="32" 
                                                    maxlength="75"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capLOCATION_ZIPCODE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtLOCATION_ZIPCODE" runat="server" size="32" 
                                                    maxlength="11" Width="120px"></asp:textbox>
				                                <br />
                                                <asp:regularexpressionvalidator id="revZIP_CODE" runat="server" ControlToValidate="txtLOCATION_ZIPCODE" Display="Dynamic"></asp:regularexpressionvalidator>
				                                <br />
				                           </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora"  width="25%" valign="top">
                                                <asp:label id="capLOCATION_COUNTRY" runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbLOCATION_COUNTRY" 
                                                    onchange="javascript:LocationCountryChanged();" runat="server">
									</asp:dropdownlist>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capLOCATION_STATE" runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbLOCATION_STATE" onchange="javascript:GetLocationStateValue(this);"
                                                    runat="server">
									</asp:dropdownlist>
                                            </td>
                                            <td class="midcolora"  width="25%" valign="top">
                                                <asp:label id="capLOCATION_ITEM_NUMBER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtLOCATION_ITEM_NUMBER" runat="server" size="32" 
                                                    maxlength="9" Width="135px"></asp:textbox>
		                                        <br />
                 <asp:regularexpressionvalidator id="revLOCATION_ITEM_NUMBER" runat="server" Display="Dynamic" 
                                                    ControlToValidate="txtLOCATION_ITEM_NUMBER"></asp:regularexpressionvalidator>
                                                <br />
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <br />
				                           </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora"  width="25%" valign="top">
    	 <asp:Label ID="capACTUAL_INSURED_OBJECT" runat="server" ></asp:Label>
                                                <br />
               <asp:TextBox ID="txtACTUAL_INSURED_OBJECT"   Width="200px" MaxLength="250" Height="70px"  TextMode="MultiLine" Rows="3"  runat="server" onkeypress="MaxLength(this,250)" onpaste="MaxLength(this,250)" 
                     ></asp:TextBox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                &nbsp;</td>
                                            <td class="midcolora"  width="25%" valign="top">
						                        &nbsp;</td>
                                            <td class="midcolora" width="25%" valign="top">
                                                &nbsp;</td>
                                        </tr>
                                        </table>
                                       </td>					
				                </tr>
                                            <tr>
								<td >
                                      <table id="TblVoyage" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" colspan="4" width="25%">
                                                <asp:label id="capPOL_VOYAGE_ID" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbPOL_VOYAGE_ID" 
                                                     runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPOL_VOYAGE_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvPOL_VOYAGE_ID" runat="server" 
                                                    ControlToValidate="cmbPOL_VOYAGE_ID"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%" valign="TOP">
                                                <asp:label id="capVOYAGE_CONVEYENCE_TYPE" runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbVOYAGE_CONVEYENCE_TYPE" onchange="javascript:GetLocationStateValue(this);"
                                                    runat="server">
									</asp:dropdownlist>
                                                <br />
                                            </td>
                                            <td class="midcolora" width="25%" valign="TOP">
                                                <asp:label id="capVOYAGE_DEPARTURE_DATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_DEPARTURE_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="80px"></asp:textbox>
                                                 <asp:HyperLink ID="hlkVOYAGE_DEPARTURE_DATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgVOYAGE_DEPARTURE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            
                                            <asp:RegularExpressionValidator ID="revVOYAGE_DEPARTURE_DATE" runat="server" Display="Dynamic"
                                              ControlToValidate="txtVOYAGE_DEPARTURE_DATE"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="25%" valign="TOP">
						<asp:Label id="capVOYAGE_ORIGIN_COUNTRY" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_ORIGIN_COUNTRY" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="TOP">
                                                <asp:label id="capVOYAGE_ORIGIN_STATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_ORIGIN_STATE" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
				                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%">
                                                <asp:label id="capVOYAGE_ORIGIN_CITY" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_ORIGIN_CITY" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%">
						<asp:Label id="capVOYAGE_DESTINATION_COUNTRY" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_DESTINATION_COUNTRY" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%">
                                                <asp:label id="capVOYAGE_DESTINATION_STATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_DESTINATION_STATE" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%">
						<asp:Label id="capVOYAGE_DESTINATION_CITY" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_DESTINATION_CITY" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
				                            </td>
                                        </tr>
                                         <tr>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_CERT_NUMBER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_CERT_NUMBER" runat="server" size="32" 
                                                    maxlength="50"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_LIC_PT_NUMBER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_LIC_PT_NUMBER" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_PREFIX" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_PREFIX" runat="server" size="32" 
                                                    maxlength="50"></asp:textbox>
              
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_TRAN_COMPANY" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_TRAN_COMPANY" runat="server" size="32" 
                                                    maxlength="150"></asp:textbox>
				                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_IO_DESC" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_IO_DESC" runat="server" size="32" 
                                                    maxlength="256"></asp:textbox>
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_ARRIVAL_DATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_ARRIVAL_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                                     <asp:HyperLink ID="hlkVOYAGE_ARRIVAL_DATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imbVOYAGE_ARRIVAL_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            
                                                <br />
                                            
                                            <asp:RegularExpressionValidator ID="revVOYAGE_ARRIVAL_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtVOYAGE_ARRIVAL_DATE"></asp:RegularExpressionValidator>
              
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <asp:label id="capVOYAGE_SURVEY_DATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtVOYAGE_SURVEY_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                                          <asp:HyperLink ID="hlkVOYAGE_SURVEY_DATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgVOYAGE_SURVEY_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            
                                                <br />
                                            
                                            <asp:RegularExpressionValidator ID="revVOYAGE_SURVEY_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtVOYAGE_SURVEY_DATE"></asp:RegularExpressionValidator>
              
                                            </td>
                                            <td class="midcolora" width="25%" valign="top">
                                                <br />
              
				                            </td>
                                        </tr>
                                        </table>
                                       </td>					
				                </tr>
				                  <tr>
								    <td>
                                      <table id="TblDpvat" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" colspan="3" width="25%">
                                            &nbsp;<asp:label id="capDPVAT" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbDPVAT" 
                                                     runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbDPVAT_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvDPVAT" runat="server" 
                                                    ControlToValidate="cmbDPVAT"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capDP_TICKET_NUMBER" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtDP_TICKET_NUMBER" runat="server" size="32" 
                                                    maxlength="2"></asp:textbox>
                                                <br />
		              <asp:RequiredFieldValidator ID="rfvDP_TICKET_NUMBER" runat="server" 
                    ControlToValidate="txtDP_TICKET_NUMBER" Display="Dynamic"></asp:RequiredFieldValidator>
                       <asp:regularexpressionvalidator id="revDP_TICKET_NUMBER" runat="server" 
                    Display="Dynamic" ControlToValidate="txtDP_TICKET_NUMBER"></asp:regularexpressionvalidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capDP_CATEGORY" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:dropdownlist id="cmbDP_CATEGORY" 
                                                     runat="server" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <br />
		              <asp:RequiredFieldValidator ID="rfvDP_CATEGORY" runat="server" 
                    ControlToValidate="cmbDP_CATEGORY" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
						<asp:Label id="capDP_STATE_ID" runat="server"></asp:Label>
                                                <br />
                                                <asp:dropdownlist id="cmbDP_STATE_ID" 
                                                     runat="server" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <br />
		              <asp:RequiredFieldValidator ID="rfvDP_STATE_ID" runat="server" 
                    ControlToValidate="cmbDP_STATE_ID" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        </table>
                                    </td>					
				                </tr>
				                    <tr>
								    <td>
                                      <table id="TblRuralLien" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" colspan="2" width="25%">
                                                <asp:label id="capRURAL_LIEN" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbRURAL_LIEN" 
                                                     runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbRURAL_LIEN_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvRURAL_LIEN" runat="server" 
                                                    ControlToValidate="cmbRURAL_LIEN"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capRURAL_ITEM_NUMBER" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtRURAL_ITEM_NUMBER" runat="server" size="32" 
                                                    maxlength="2" Width="135px"></asp:textbox>
		         <asp:RequiredFieldValidator ID="rfvRURAL_ITEM_NUMBER" runat="server" ControlToValidate="txtRURAL_ITEM_NUMBER" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revRURAL_ITEM_NUMBER" runat="server" Display="Dynamic" 
                                                    ControlToValidate="txtRURAL_ITEM_NUMBER"></asp:regularexpressionvalidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capRURAL_FESR_COVERAGE" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:CheckBox ID="chkRURAL_FESR_COVERAGE" runat="server" />
                <asp:CustomValidator ID="cvRURAL_FESR_COVERAGE" runat="server" ClientValidationFunction="ValidateFESRChkList" ></asp:CustomValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
						<asp:Label id="capRURAL_MODE" runat="server"></asp:Label>
                                                <span class="mandatory">*</span><br />
                                                <asp:dropdownlist id="cmbRURAL_MODE" 
                                                     runat="server" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                 <asp:RequiredFieldValidator ID="rfvRURAL_MODE" runat="server" ControlToValidate="cmbRURAL_MODE" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capRURAL_PROPERTY" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:dropdownlist id="cmbRURAL_PROPERTY" 
                                                     runat="server" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvRURAL_PROPERTY" runat="server" 
                                                    ControlToValidate="cmbRURAL_PROPERTY"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capRURAL_CULTIVATION" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:dropdownlist id="cmbRURAL_CULTIVATION" 
                                                     runat="server" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                 <asp:RequiredFieldValidator ID="rfvRURAL_CULTIVATION" runat="server" ControlToValidate="cmbRURAL_CULTIVATION" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capRURAL_STATE_ID" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:dropdownlist id="cmbRURAL_STATE_ID" 
                                                     runat="server" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                 <asp:RequiredFieldValidator ID="rfvRURAL_STATE_ID" runat="server" ControlToValidate="cmbRURAL_STATE_ID" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capRURAL_CITY" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtRURAL_CITY" runat="server" size="32" 
                                                    maxlength="250"></asp:textbox>
		         <asp:RequiredFieldValidator ID="rfvRURAL_CITY" runat="server" ControlToValidate="txtRURAL_CITY" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revRURAL_CITY" runat="server" Display="Dynamic" 
                                                    ControlToValidate="txtRURAL_CITY"></asp:regularexpressionvalidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capRURAL_INSURED_AREA" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtRURAL_INSURED_AREA" runat="server" size="32" 
                                                    maxlength="9" ></asp:textbox>
		         <asp:RequiredFieldValidator ID="rfvRURAL_INSURED_AREA" runat="server" 
                                                    ControlToValidate="txtRURAL_INSURED_AREA" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revRURAL_INSURED_AREA" runat="server" Display="Dynamic" 
                                                    ControlToValidate="txtRURAL_INSURED_AREA"></asp:regularexpressionvalidator>
                 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capRURAL_SUBSIDY_PREMIUM" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtRURAL_SUBSIDY_PREMIUM" runat="server" size="32" 
                                                    maxlength="9" ></asp:textbox>
		         <asp:regularexpressionvalidator id="revRURAL_SUBSIDY_PREMIUM" runat="server" Display="Dynamic" 
                                                    ControlToValidate="txtRURAL_SUBSIDY_PREMIUM"></asp:regularexpressionvalidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capRURAL_SUBSIDY_STATE" runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbRURAL_SUBSIDY_STATE" 
                                                     runat="server" 
                                                   >
									</asp:dropdownlist>
                                            </td>
                                        </tr>
                                        </table>
                                    </td>					
				                </tr>
				                   <tr>
								    <td width="100%">
                                      <table id="TblPersonalAccident" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" colspan="3" width="25%">
                                                <asp:label id="capPERSONAL_ACCIDENT" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbPERSONAL_ACCIDENT" 
                                                     runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPERSONAL_ACCIDENT_SelectedIndexChanged">
									            </asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvPERSONAL_ACCIDENT" runat="server" 
                                                    ControlToValidate="cmbPERSONAL_ACCIDENT"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capPA_START_DATE" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtPA_START_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                                     <asp:HyperLink ID="hlkPA_START_DATE" runat="server" CssClass="HotSpot">
                                                
                                            <asp:Image ID="imgPA_START_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                                    </asp:HyperLink>
                                            
                                                <br />
                                            <asp:regularexpressionvalidator id="revPA_START_DATE" runat="server" 
                                                Display="Dynamic" ControlToValidate="txtPA_START_DATE"></asp:regularexpressionvalidator> 
                                            <asp:RequiredFieldValidator ID="rfvPA_START_DATE" runat="server" 
                                                ControlToValidate="txtPA_START_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                             <asp:comparevalidator id="cpvPA_END_DATE" ControlToValidate="txtPA_END_DATE" Display="Dynamic" 
                                                                                Runat="server" ControlToCompare="txtPA_START_DATE" Type="Date"
										                            Operator="GreaterThanEqual"></asp:comparevalidator>
	        
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capPA_END_DATE" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtPA_END_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="130px"></asp:textbox>
                                                     <asp:HyperLink ID="hlkPA_END_DATE" runat="server" 
                                                    CssClass="HotSpot">
                                                    
                                             <asp:Image ID="imgPA_END_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle" />    
                                            </asp:HyperLink>
                                            
                                                <br />
                
               
                                            <asp:regularexpressionvalidator id="revPA_END_DATE" runat="server" 
                                                Display="Dynamic" ControlToValidate="txtPA_END_DATE"></asp:regularexpressionvalidator> 
                                            <asp:RequiredFieldValidator ID="rfvPA_END_DATE" runat="server" 
                                                ControlToValidate="txtPA_END_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
       
    	 
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capPA_NUM_OF_PASS" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtPA_NUM_OF_PASS" runat="server" size="32" 
                                                    maxlength="12"></asp:textbox>
                                                <br />
                                            <asp:RequiredFieldValidator ID="rfvPA_NUM_OF_PASS" runat="server" 
                                                ControlToValidate="txtPA_NUM_OF_PASS" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPA_NUM_OF_PASS" runat="server"   
                                                ControlToValidate="txtPA_NUM_OF_PASS" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        </table>
                                    </td>					
				                </tr>
                                            <tr>
								<td  >
								  <table id="TblPerson" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" colspan="3" width="100%">
                                                <asp:label id="capPOL_PERSON_ID" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbPOL_PERSON_ID" 
                                                     runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPOL_PERSON_ID_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvPOL_PERSON_ID" runat="server" 
                                                    ControlToValidate="cmbPOL_PERSON_ID"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capEFFECTIVE_DATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtEFFECTIVE_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                                <asp:HyperLink ID="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgEFFECTIVE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            
                                            <asp:RegularExpressionValidator ID="revEFFECTIVE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtEFFECTIVE_DATE"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
						<asp:Label id="capEXPIRE_DATE" runat="server"></asp:Label>
                                                <br />
                                                <asp:textbox id="txtEXPIRE_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                               <asp:HyperLink ID="hlkEXPIRE_DATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgEXPIRE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                             <asp:RegularExpressionValidator ID="revEXPIRE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtEXPIRE_DATE"></asp:RegularExpressionValidator>
                                                  <asp:CompareValidator ID="csvRISK_EXPIRE_DATE" runat="server" 
                                                    ControlToCompare="txtEFFECTIVE_DATE" 
                                                    ControlToValidate="txtEXPIRE_DATE" Display="Dynamic" 
                                                    Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="34%">
                                                <asp:label id="capPERSON_DOB" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtPERSON_DOB" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                                     <asp:HyperLink ID="hlkPERSON_DOB" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgPERSON_DOB" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            
                                            <asp:RegularExpressionValidator ID="revPERSON_DOB" runat="server" Display="Dynamic"
                                                ControlToValidate="txtPERSON_DOB"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capPERSON_DISEASE_DATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtPERSON_DISEASE_DATE" runat="server" size="4" maxlength="10" 
                                                    Width="120px"></asp:textbox>
                                                     <asp:HyperLink ID="hlkPERSON_DISEASE_DATE" runat="server" 
                                                    CssClass="HotSpot">
                                                    <asp:Image ID="imgPERSON_DISEASE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                                
                                            </asp:HyperLink>
                                            
                                            <asp:RegularExpressionValidator ID="revPERSON_DISEASE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtPERSON_DiSEASE_DATE"></asp:RegularExpressionValidator>
                                                  <asp:CompareValidator ID="csvPERSON_DiSEASE_DATE" runat="server" 
                                                    ControlToCompare="txtPERSON_DOB" 
                                                    ControlToValidate="txtPERSON_DISEASE_DATE" Display="Dynamic" 
                                                    Operator="GreaterThan" Type="Date"></asp:CompareValidator><br />
                                                <asp:CustomValidator ID="csvDISEASE_DATE" ErrorMessage="Disease date cannot be future date." runat="server" ControlToValidate="txtPERSON_DISEASE_DATE" ClientValidationFunction="ValidateDiseaseDate" ></asp:CustomValidator>
                                            </td>
                                              <td class="midcolora" width="34%">
                                                <asp:label id="capINSURED_NAME" runat="server" Visible ="false"  ></asp:label>
                                                <br />
                                                <asp:textbox id="txtINSURED_NAME" runat="server" size="32" 
                                                    maxlength="150" Visible = "false" ></asp:textbox>
                                            </td>
                                        </tr>
                                        </table>
            <table id="TblRentalSecurity" runat="server" visible="false" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="midcolora" width="25%" colspan="2">
                                                <asp:label id="capRENTAL_SECURITY" runat="server"></asp:label><span class="mandatory">*<br />
                                                </span>
                                                <asp:dropdownlist id="cmbRENTAL_SECURITY" 
                                                     runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbRENTAL_SECURITY_SelectedIndexChanged">
									</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvRENTAL_SECURITY" runat="server" 
                                                    ControlToValidate="cmbRENTAL_SECURITY"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="50%" valign="top">
    	 <asp:Label ID="capITEM_NUMBER" runat="server" ></asp:Label>
                                                <br />
                                                <asp:textbox id="txtITEM_NUMBER" runat="server" size="32" 
                                                    maxlength="8"  Width="145px" ></asp:textbox>
                                                <br />
                 <asp:regularexpressionvalidator id="revITEM_NUMBER" runat="server" Display="Dynamic" 
                                                    ControlToValidate="txtITEM_NUMBER"></asp:regularexpressionvalidator>
                                            </td>
                                            <td class="midcolora" valign="top">
    	 <asp:Label ID="capRENTAL_INSURED_OBJECT" runat="server" ></asp:Label>
                                                <br />
               <asp:TextBox ID="txtRENTAL_INSURED_OBJECT"   Width="225px" MaxLength="300" Height="70px"  TextMode="MultiLine" 
                                                    Rows="3"  runat="server" onkeypress="MaxLength(this,250)" 
                                                    onpaste="MaxLength(this,250)" AutoCompleteType="Disabled" 
                     ></asp:TextBox>
                                            </td>
                                        </tr>
                                        </table>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        
                                        <tr>
                                            <td class="midcolora"  align="left">
                                          <asp:label id="capDAMAGE_DESCRIPTION" runat="server"></asp:label>
                                                <br />
                 <asp:TextBox ID="txtDAMAGE_DESCRIPTION" runat="server" Height="66px" MaxLength="300" 
                     onkeypress="MaxLength(this,500)" onpaste="MaxLength(this,300)" Rows="3" 
                     TextMode="MultiLine" Width="326px"></asp:TextBox>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td class="midcolora"  align="left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" width="50%">
                                                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" />
					                        </td>
                                            <td width="50%" align="right">
                                                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                                                    onclick="btnSave_Click"></cmsb:cmsbutton>
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td width="15%">
                                                &nbsp;</td>
                                        </tr>--%>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                    </td>					
				                </tr>
							
								
					
							<%--<tr>
								<td >
								    &nbsp;</td>
				            </tr>--%>
				
				
				</table>

							
							
				<INPUT id="hidLOCATION_STATE" type="hidden" value="0" name="hidLOCATION_STATE" runat="server">
				<INPUT id="hidLOCATION_COUNTRY" type="hidden" value="0" name="hidLOCATION_COUNTRY" runat="server">
			    <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			    <INPUT id="hidVOYAGE_ORIGIN_COUNTRY" type="hidden" value="0" name="hidVOYAGE_ORIGIN_COUNTRY" runat="server">
                <INPUT id="hidVOYAGE_ORIGIN_STATE" type="hidden" value="0" name="hidVOYAGE_ORIGIN_STATE" runat="server">
                <INPUT id="hidVOYAGE_DESTINATION_STATE" type="hidden" value="0" name="hidVOYAGE_DESTINATION_STATE" runat="server">
                <INPUT id="hidVOYAGE_DESTINATION_COUNTRY" type="hidden" value="0" name="hidVOYAGE_DESTINATION_COUNTRY" runat="server">
                <INPUT id="hidINSURED_PRODUCT_ID" type="hidden" value="0" name="hidINSURED_PRODUCT_ID" runat="server">
                <INPUT id="hidRISK_CO_APP_ID" type="hidden" value="0" name="hidRISK_CO_APP_ID" runat="server">
                <INPUT id="hidPOLICY_EFFECTIVE_DATE" type="hidden" value="" name="hidPOLICY_EFFECTIVE_DATE" runat="server">
                <INPUT id="hidPOLICY_EXPIRATION_DATE" type="hidden" value="" name="POLICY_EXPIRATION_DATE" runat="server">
                <INPUT id="hidFUTURE_DATE_MSG" type="hidden" value="" name="hidFUTURE_DATE_MSG" runat="server">
                <INPUT id="hidPOLICY_DATE_MSG" type="hidden" value="" name="hidPOLICY_DATE_MSG" runat="server">
                
                
                         
    </form>
</div>
</body>
</html>

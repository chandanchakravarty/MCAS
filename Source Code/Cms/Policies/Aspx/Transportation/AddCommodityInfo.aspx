<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCommodityInfo.aspx.cs"
    Inherits="Cms.Policies.Aspx.Transportation.AddCommodityInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>AddCommodityInfo</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet" />
    <script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/calendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
    <script type="text/javascript">
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        /*
        function CheckMaxLengthOfRemark(objSource, objArgs) {
            var RemarkValue = document.POL_COMMODITY_INFO.txtREMARKS.value;
            if (RemarkValue.length > 500)
            { objArgs.IsValid = false; }
            else
            { objArgs.IsValid = true; }
        }*/
        function CheckMaxLengthOfInsuredObject(objSource, objArgs) {
            var CommodityValue = document.POL_COMMODITY_INFO.txtCOMMODITY.value;
            if (CommodityValue.length > 500)
            { objArgs.IsValid = false; }
            else
            { objArgs.IsValid = true; }
        }
        function ResetTheForm() {
            document.POL_COMMODITY_INFO.reset();
        }  

        function Origin_fillstateFromCountry() {
            GlobalError = true;
            var CountryID = document.getElementById('cmbORIGIN_COUNTRY').options[document.getElementById('cmbORIGIN_COUNTRY').selectedIndex].value;

            AddCommodityInfo.AjaxFillState(CountryID, fillState);
            if (GlobalError) {
                return false;
            }
            else {
                return true;
            }
        }
        function Destination_fillstateFromCountry() {
            GlobalError = true;
            var CountryID = document.getElementById('cmbDESTINATION_COUNTRY').options[document.getElementById('cmbDESTINATION_COUNTRY').selectedIndex].value;
            AddCommodityInfo.AjaxFillState(CountryID, fillState1);
            if (GlobalError) 
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        function fillState(Result) {
            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var statesList = document.getElementById("cmbORIGIN_STATE");
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

                document.getElementById("cmbORIGIN_STATE").value = document.getElementById("hidORIGIN_STATE").value;
            }

            return false;
        }

        function fillState1(Result) {
            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var statesList = document.getElementById("cmbDESTINATION_STATE");
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

                document.getElementById("cmbDESTINATION_STATE").value = document.getElementById("hidDESTINATION_STATE").value;
            }

            return false;
        }
        function setTab() {
            var pagefrom = '<%=PAGEFROM %>'
            
            var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
            
            if (document.getElementById('hidCommodity_ID') != null && document.getElementById('hidCommodity_ID').value != "NEW" && document.getElementById('hidCommodity_ID').value != "") {
                if (document.getElementById('hidCALLED_FROM') != null) {

                    var CalledFrom = document.getElementById('hidCALLED_FROM').value;
                    
                    if (document.getElementById('hidCommodity_ID') != null)
                        riskId = document.getElementById('hidCommodity_ID').value;

                    var tabtitles = TAB_TITLES.split(',');

                    if (pagefrom == "QAPP") {// changed by sonal to implemt this in quick app

                        Url = parent.document.getElementById('hidFrameUrl').value + riskId + "&";
                        DrawTab(1, top.frames[1], tabtitles[0], Url);
                    }

                    Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?RISK_ID=" + riskId + "&";
                    DrawTab(2, top.frames[1], tabtitles[2], Url);

                    Url = "/Cms/Policies/aspx/AddpolicyCoverages.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[0] + "&RISK_ID=" + riskId + "&";
                    DrawTab(3, top.frames[1], tabtitles[1], Url);

                   
                }
 
            }
            else {
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
            }
            return false;
        } 
        function InitPage() {
        //Commented for itrack 856
        //Origin_fillstateFromCountry();
            //Destination_fillstateFromCountry();
            ApplyColor();
        setTab();
    }

    function CheckDateInRange(objSource, objArgs) {

        if (document.getElementById('txtDEPARTING_DATE').value.length > 0) {

            var beginDate = document.getElementById('hidEFFECTIVEDATE').value;
            var endDate = document.getElementById('hidEXPIRATIONDATE').value;
            var DepartingDate =document.getElementById('txtDEPARTING_DATE').value;
            
            //var flag = DateComparer(DepartingDate,beginDate, "mm/dd/yyyy");
            //alert(flag);
            //alert(DateComparer(endDate,DepartingDate, "mm/dd/yyyy"));

            if (DateComparer(DepartingDate, beginDate, jsaAppDtFormat) && DateComparer(endDate, DepartingDate, jsaAppDtFormat)) {
                objArgs.IsValid = true;               
                return true;
            }
            else {
                objArgs.IsValid = false;
                return false;
            }
        }
    }

    
    </script>
   <%-- <script language="javascript" type="text/javascript">

        var IsChanged = '';
        $(document).ready(function() {
        $("input").bind('change', changeval);
        $("select").bind('change', changeval);
        $("textarea").bind('change', changeval);
        });
        function changeval() {
            IsChanged = 'true';
        }
        window.onbeforeunload = function() {
        if (IsChanged == 'true') {
            alert(getUserConfirm());
            }

        }


    </script>
    
    <script language="vbscript" type="text/vbscript">
	function getUserConfirm
		getUserConfirm= msgbox ("Do you want to save the contents of the Page?",35,"Ebix Advantage")
	End function
</script>--%>
   <%--To Check the Voyage number  using jQuery ------- Added by Pradeep Kushwaha on 27 May 2010--%>
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
        $("#txtCOMMODITY_NUMBER").change(function() {
        if ($("#txtCOMMODITY_NUMBER").val() != $("#hidOLD_COMMODITY_NUMBER").val()) {
            if (trim($("#txtCOMMODITY_NUMBER").val()) != '') {
                        var CUSTOMER_ID = $('#hidCUSTOMER_ID').val();
                        var POLICY_ID = $('#hidPOL_ID').val();
                        var POLICY_VERSION_ID = $('#hidPOL_VERSION_ID').val();
                        var COMMODITY_NUMBER = $('#txtCOMMODITY_NUMBER').val();
                        var flag = "2";

                        PageMethod("GetMaxIdOfVoyageNumber", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID, "COMMODITY_NUMBER", COMMODITY_NUMBER, "flag", flag], AjaxSucceeded, AjaxFailed); //With parameters

                    }
                    else if (trim($("#txtCOMMODITY_NUMBER").val()) == '') {

                    $('#rfvVESSEL_NUMBER').text($('#hidCOMMODITY_NUMBER_Msg').val());
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
            if (result.d != '') {
                var number = numbers.split(',');
                if (number[0] == "-2" && number[3] == "2") {
                    //Vessel number already exist
                    document.getElementById("rfvCOMMODITY_NUMBER").setAttribute('enabled', false);
                    document.getElementById("rfvCOMMODITY_NUMBER").setAttribute('isValid', true);
                    document.getElementById("rfvCOMMODITY_NUMBER").style.display = "inline";
                    $('#rfvCOMMODITY_NUMBER').text($('#hidCOMMODITY_NUMBER_Msg').val());

                }
                else if (number[0] == "-3" && number[3] == "2") {
                    //VESSEL_NUMBER does not exists

                    document.getElementById("rfvCOMMODITY_NUMBER").setAttribute('enabled', false);
                    document.getElementById("rfvCOMMODITY_NUMBER").setAttribute('isValid', true);
                    document.getElementById("rfvCOMMODITY_NUMBER").style.display = "none";
                    $('#rfvCOMMODITY_NUMBER').text($('#hidCOMMODITY_NUMBER_Msg').val());
                }
            }
        }
        function AjaxFailed(result) {

            //alert(result.status + ' ' + result.statusText); //Display the error message (If there is any error while retriving record)
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" onload="InitPage();">
    <form id="POL_COMMODITY_INFO" runat="server" method="post">
    <table cellspacing="2" cellpadding="2" width="100%" border="0" class="tableWidthHeader">
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
       <td><table cellspacing="2" cellpadding="2" width="100%" border="0"border="0">  
            <td class="pageHeader" colspan="3">
                <asp:Label ID="capmandatory" runat="server"></asp:Label>
            </td>
        </tr>
       
        <tr>
            <td colspan="3" class="headerEffectSystemParams">
                <asp:Label ID="capVoyage_Information" runat="server"></asp:Label>
            </td>
             
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capCOMMODITY_NUMBER" runat="server" Text="Voyage #"></asp:Label><span class="mandatory">*</span>
         </br>
                <asp:TextBox ID="txtCOMMODITY_NUMBER" runat="server" size="25" MaxLength="10"></asp:TextBox></br>
               
                 <asp:regularexpressionvalidator id="revCOMMODITY_NUMBER" runat="server" 
                    Display="Dynamic" ControlToValidate="txtCOMMODITY_NUMBER"></asp:regularexpressionvalidator>
                    <asp:requiredfieldvalidator id="rfvCOMMODITY_NUMBER" runat="server" ControlToValidate="txtCOMMODITY_NUMBER"
											Display="Dynamic"></asp:requiredfieldvalidator>  
            </td>
            <td width="37%" class="midcolora">
                <asp:Label ID="capCOMMODITY" Text="Insured Object" runat="server"></asp:Label><span class="mandatory">*</span></BR>
                <asp:TextBox runat="server" ID="txtCOMMODITY" TextMode="MultiLine" Rows="3" 
                    Width="400px" onkeypress="MaxLength(this,500)" onpaste="MaxLength(this,500)" 
                    Height="70px" MaxLength="500"></asp:TextBox></br>
               
                <asp:RequiredFieldValidator ID="rfvCOMMODITY" runat="server" 
                    ControlToValidate="txtCOMMODITY" Display="Dynamic"></asp:RequiredFieldValidator>
                
                   <asp:customvalidator id="csvCOMMODITY" Runat="server" 
                    ControlToValidate="txtCOMMODITY" Display="Dynamic"
											ClientValidationFunction="CheckMaxLengthOfInsuredObject"></asp:customvalidator> 
              
            </td>
            <td width="37%" class="midcolora">
                <asp:Label ID="capDEPARTING_DATE" Text="DEPARTING DATE" runat="server"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtDEPARTING_DATE" runat="server" MaxLength="10" size="20"></asp:TextBox>
                <asp:HyperLink ID="hlkDEPARTING_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgDEPARTING_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink>
                <br />
                <asp:CustomValidator ID="csvDEPARTING_DATE" Runat="server" ControlToValidate="txtDEPARTING_DATE" Display="Dynamic"
							ClientValidationFunction="CheckDateInRange"  ></asp:CustomValidator>
                 <asp:regularexpressionvalidator id="revDEPARTING_DATE" runat="server" Display="Dynamic"  ControlToValidate="txtDEPARTING_DATE"></asp:regularexpressionvalidator>
                <asp:RequiredFieldValidator ID="rfvDEPARTING_DATE" runat="server" 
                    ControlToValidate="txtDEPARTING_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
           
            <td class="midcolora">
           <%-- itrack 1400, modified by naveen--%>
            <asp:Label ID="capCoApplicant" runat="server" Text=""></asp:Label><span class="mandatory" id="spn_mandatory">*</span><br />
                <asp:DropDownList ID="cmbCO_APPLICANT_ID" runat="server" >
                </asp:DropDownList>
                <br />
                 <asp:requiredfieldvalidator id="rfvCO_APPLICANT" runat="server" ControlToValidate="cmbCO_APPLICANT_ID" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                &nbsp;</td>

                 <td class="midcolora"    colspan="2"  Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
            
        </tr>

        <tr>
        
        
         <td colspan="3"  class="midcolora">
               
                <asp:Label ID="capCONVEYANCE_TYPE" runat="server" Text="CONVEYANCE TYPE"></asp:Label><span class="mandatory">*</span></br>
                <asp:DropDownList ID="cmbCONVEYANCE_TYPE" runat="server">
                </asp:DropDownList><br /><asp:RequiredFieldValidator ID="rfvCONVEYANCE_TYPE" runat="server" 
                    ControlToValidate="cmbCONVEYANCE_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>
               
            </td>
        </tr>
        <tr>
            <td colspan="3" class="midcolora">
            </td>
        </tr>
        <tr>
            <td colspan="3" class="headerEffectSystemParams">
                <asp:Label ID="capORIGIN" Text="ORIGIN" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capORIGIN_COUNTRY" runat="server" Text="COUNTRY"></asp:Label><span class="mandatory">*</span></br>
                <%--<asp:DropDownList ID="cmbORIGIN_COUNTRY" runat="server" onfocus="SelectComboIndex('cmbORIGIN_COUNTRY');" onchange="javascript:Origin_fillstateFromCountry();"> </asp:DropDownList>--%>
                <asp:TextBox ID="txtORIGN_COUNTRY" runat="server" size="40" MaxLength="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvORIGN_COUNTRY" runat="server" ControlToValidate="txtORIGN_COUNTRY" Display="Dynamic"></asp:RequiredFieldValidator>
                
            </td>
            <td width="37%" class="midcolora">
                <asp:Label ID="capORIGIN_STATE" runat="server" Text="STATE"></asp:Label><span class="mandatory">*</span></br>
                <%--<asp:DropDownList ID="cmbORIGIN_STATE" runat="server" Width="120px"><asp:ListItem></asp:ListItem></asp:DropDownList>--%>
                <asp:TextBox ID="txtORIGN_STATE" runat="server" size="40" MaxLength="150"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvORIGIN_STATE" runat="server" ControlToValidate="txtORIGN_STATE" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td width="37%" class="midcolora">
                <asp:Label ID="capORIGIN_CITY" runat="server" Text="CITY"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtORIGIN_CITY" runat="server" size="40" MaxLength="100"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvORIGIN_CITY" runat="server" ControlToValidate="txtORIGIN_CITY" Display="Dynamic"></asp:RequiredFieldValidator><asp:regularexpressionvalidator id="revORIGIN_CITY" runat="server" 
                    Display="Dynamic" ControlToValidate="txtORIGIN_CITY"></asp:regularexpressionvalidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="midcolora">
            </td>
        </tr>
        <tr>
            <td colspan="3" class="headerEffectSystemParams">
                <asp:Label ID="capDESTINATION" Text="DESTINATION" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capDESTINATION_COUNTRY" runat="server" Text="COUNTRY"></asp:Label><span class="mandatory">*</span><br />
                 <asp:TextBox ID="txtDEST_COUNTRY" runat="server" size="40" MaxLength="150"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvDEST_COUNTRY" runat="server" ControlToValidate="txtDEST_COUNTRY" Display="Dynamic"></asp:RequiredFieldValidator>
                 
                <%--<asp:DropDownList ID="cmbDESTINATION_COUNTRY" runat="server" onfocus="SelectComboIndex('cmbDESTINATION_COUNTRY');" onchange="javascript:Destination_fillstateFromCountry();"> </asp:DropDownList>--%>
            </td>
            <td width="37%" class="midcolora">
                <asp:Label ID="capDESTINATION_STATE" runat="server" Text="STATE"></asp:Label><span class="mandatory">*</span></br>
               <%-- <asp:DropDownList ID="cmbDESTINATION_STATE" runat="server" Width="120px">
                    <asp:ListItem> </asp:ListItem>
                </asp:DropDownList>--%>
                  <asp:TextBox ID="txtDEST_STATE" runat="server" size="40" MaxLength="150"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvDESTINATION_STATE" runat="server" ControlToValidate="txtDEST_STATE" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td width="37%" class="midcolora">
                <asp:Label ID="capDESTINATION_CITY" runat="server" Text="CITY"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtDESTINATION_CITY" runat="server" size="40" MaxLength="100"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvDESTINATION_CITY" runat="server" 
                    ControlToValidate="txtDESTINATION_CITY" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revDESTINATION_CITY" runat="server" 
                    Display="Dynamic" ControlToValidate="txtDESTINATION_CITY"></asp:regularexpressionvalidator>
            </td>
        </tr>
        <tr>
            <td width="50%" class="midcolora" colspan="3">
                <asp:Label ID="capREMARKS" runat="server" Text="REMARKS"></asp:Label></br>
		    <asp:TextBox runat="server" ID="txtREMARKS"  TextMode="MultiLine" Rows="3" 
                    Width="400px" Height="70px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox>
	            <br />
                   
              
            </td>
            <tr>
                <td class="midcolora" align="left" width="30%">
                    <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset" 
                        CausesValidation="False"></cmsb:CmsButton>
                    <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" runat="server" CausesValidation="false"
                        Text="Activate/Deactivate" onclick="btnActivateDeactivate_Click"></cmsb:CmsButton>
                </td>
                <td class="midcolorr">
                </td>
                <td class="midcolorr" align="right">
                    <cmsb:CmsButton class="clsButton" ID="btnDelete" runat="server" Text="Delete" 
                        CausesValidation="false" onclick="btnDelete_Click">
                    </cmsb:CmsButton>
                    <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click"></cmsb:CmsButton>
               <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
               <input id="hidCommodity_ID" type="hidden" value="" name="hidCommodity_ID" runat="server"/> 
               <input id="hidORIGIN_STATE" type="hidden" value="0" name="hidORIGIN_STATE" runat="server"/>
               <input id="hidDESTINATION_STATE" type="hidden" value="" name="hidDESTINATION_STATE" runat="server"/>
               <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	           <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>   
	           
	           <input id="hidPOL_VERSION_ID" type="hidden" name="hidPOL_VERSION_ID" runat="server"/>
	           <input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server" />
	           <input id="hidPOL_ID" type="hidden" name="hidPOL_ID" runat="server"/>
	           <input id="hidCOMMODITY_NUMBER" type="hidden" name="hidCOMMODITY_NUMBER" runat="server"/>
	           <input id="hidOLD_COMMODITY_NUMBER" type="hidden" name="hidOLD_COMMODITY_NUMBER" runat="server"/>
	           <input id="hidCOMMODITY_NUMBER_Msg" type="hidden" name="hidCOMMODITY_NUMBER_Msg" runat="server"/>
	              <input id="hidEXPIRATIONDATE" type="hidden" name="hidEXPIRATIONDATE" runat="server"/>
	               <input id="hidEFFECTIVEDATE" type="hidden" name="hidEFFECTIVEDATE" runat="server"/>
	                <input id="hidAPP_ID" type="hidden" name="hidAPP_ID" runat="server"/>
	                  <input id="hidAPP_VERSION_ID" type="hidden" name="hidAPP_VERSION_ID" runat="server"/>
            	
                </td>
            </tr>
            </table></td></tr>
    </table>
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {

            var pagefrom = '<%=PAGEFROM %>'
            if (pagefrom == "QAPP") {
                parent.BindRisk();
                for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {
                    if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidCommodity_ID').value)
                        parent.document.getElementById('cmbRisk').options[i].selected = true;

                }
            }
            else {
                try {
                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCommodity_ID').value);
                } catch (err) {

                }
            }
        }
		</script>
</body>
</html>

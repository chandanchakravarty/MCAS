<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMeriTimeInfo.aspx.cs" Inherits="Cms.Policies.Aspx.MariTime.AddMeriTimeInfo" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL_MARITIME</title>
      
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		
    <script type="text/javascript">
      
        function showMe(it, box) {
           
            if (!box.checked) {
                document.getElementById(it).disabled = true;
                document.getElementById("rfvNAME_OF_CLUB").setAttribute('enabled', false);
                document.getElementById("rfvNAME_OF_CLUB").setAttribute('isValid', true);
                document.getElementById("rfvNAME_OF_CLUB").style.display = "none";
                document.getElementById("lblmand").style.display = "none";
            }
            else
            {
                document.getElementById(it).disabled = false;
                document.getElementById("rfvNAME_OF_CLUB").setAttribute('enabled', true);
                document.getElementById("rfvNAME_OF_CLUB").setAttribute('isValid', false);
                document.getElementById("rfvNAME_OF_CLUB").style.display = "none";
                document.getElementById("lblmand").style.display = "inline";
            }
        }
        function ResetTheForm() {
            document.POL_MARITIME.reset();            
            ChangeColor();
            return false;
        }
        //Validates the maximum length for comments
        /*function txtREMARKS_VALIDATE(source, arguments) {
            //debugger;
            var txtArea = arguments.Value;
            if (txtArea.length > 500) {
                arguments.IsValid = false;
                return false;
            }
        }*/
        
        function ChkOccurenceDate(objSource, objArgs) {
            var effdate = document.POL_MARITIME.txtMANUFACTURE_YEAR.value;
            var date = '<%=DateTime.Now.Year%>';
            if (effdate.length < 4) {
                objArgs.IsValid = false;
            }
            else {
                if (effdate > date)
                    objArgs.IsValid = false;
                else
                    objArgs.IsValid = true;
            }



        }
        function setTab() {

            if (document.getElementById('hidMariTimeID') != null && document.getElementById('hidMariTimeID').value != "NEW" && document.getElementById('hidMariTimeID').value != "") {
                if (document.getElementById('hidMariTimeID') != null)
                    riskId = document.getElementById('hidMariTimeID').value;

                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');
                var CALLEDFROM = '<%=CALLEDFROM %>'
                if (CALLEDFROM == "QAPP") {

                    Url = parent.document.getElementById('hidFrameUrl').value + riskId + "&";
                    DrawTab(1, top.frames[1], tabtitles[0], Url);
                }

                Url = "/Cms/Policies/Aspx/MariTime/AddAircraftInformation.aspx?CalledFrom=MERITIME&pageTitle=" + tabtitles[0] + "&RISK_ID=" + riskId + "&CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&POLICY_ID=" + document.getElementById('hidPOL_ID').value + "&POLICY_VERSION_ID=" + document.getElementById('hidPOL_VERSION_ID').value + "&"
                DrawTab(2, top.frames[1], tabtitles[1], Url);

                Url = "/Cms/CmsWeb/Construction.html";
                DrawTab(3, top.frames[1], tabtitles[2], Url);

                Url = "/Cms/Policies/aspx/AddpolicyCoverages.aspx?CalledFrom=MERITIME&pageTitle=" + tabtitles[0] + "&RISK_ID=" + riskId + "&";
                DrawTab(4, top.frames[1], "Coverages", Url);

                Url = "/Cms/Policies/aspx/PolMiscellaneousEquipmentValuesDetails.aspx?" + "&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOL_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOL_VERSION_ID").value + "&RISK_ID=" + document.getElementById("hidMariTimeID").value + "&VEHICLE_ID=" + document.getElementById("hidMariTimeID").value + "&";
                DrawTab(5, top.frames[1], "Miscellaneous Equipment", Url);

              
               
            }
            else {
                RemoveTab(5, top.frames[1]);
                RemoveTab(4, top.frames[1]);       
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
            }
            return false;
        }
        function initPage() {

            ApplyColor();
            showMe('txtNAME_OF_CLUB', document.getElementById('chkVESSEL_ACTION_NAUTICO_CLUB'));
            setTab();
            document.getElementById("tbl").style.display = "none";
        }

       
    </script>
    <%--To Check the Vessel number  using jQuery ------- Added by Pradeep Kushwaha on 27 May 2010--%>
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            $("#txtVESSEL_NUMBER").change(function() {
                if ($("#txtVESSEL_NUMBER").val() != $("#hidOLD_VESSEL_NUMBER").val()) {
                    if (trim($("#txtVESSEL_NUMBER").val()) != '') {
                        var CUSTOMER_ID = $('#hidCUSTOMER_ID').val();
                        var POLICY_ID = $('#hidPOL_ID').val();
                        var POLICY_VERSION_ID = $('#hidPOL_VERSION_ID').val();
                        var VESSEL_NUMBER = $('#txtVESSEL_NUMBER').val();
                        var flag = "4";

                        PageMethod("GetMaxIdOfVesselNumber", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID, "VESSEL_NUMBER", VESSEL_NUMBER, "flag", flag], AjaxSucceeded, AjaxFailed); //With parameters

                    }
                    else if (trim($("#txtVESSEL_NUMBER").val()) == '') {

                        $('#rfvVESSEL_NUMBER').text($('#hidVESSEL_NUMBER_Msg').val());
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
                if (number[0] == "-4" && number[3] == "4") {
                    //Vessel number already exist
                    document.getElementById("rfvVESSEL_NUMBER").setAttribute('enabled', false);
                    document.getElementById("rfvVESSEL_NUMBER").setAttribute('isValid', true);
                    document.getElementById("rfvVESSEL_NUMBER").style.display = "inline";
                    $('#rfvVESSEL_NUMBER').text($('#hidVESSEL_NUMBER').val());
                    
                }
                else if (number[0] == "-5" && number[3] == "4") {
                    //VESSEL_NUMBER does not exists
                  
                    document.getElementById("rfvVESSEL_NUMBER").setAttribute('enabled', false);
                    document.getElementById("rfvVESSEL_NUMBER").setAttribute('isValid', true);
                    document.getElementById("rfvVESSEL_NUMBER").style.display = "none";
                    $('#rfvVESSEL_NUMBER').text($('#hidVESSEL_NUMBER_Msg').val()); 
                }    
            }
        }
        function AjaxFailed(result) {

           // alert(result.status + ' ' + result.statusText); //Display the error message (If there is any error while retriving record)
        }
    </script>
    
</head>
<body leftMargin="0" topMargin="0" onload="initPage();">
    <form id="POL_MARITIME" runat="server" method="post">
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
       <td><table cellspacing="2" cellpadding="2" width="100%" border="0" border="0">  
        <td class="pageHeader" colspan="3"><asp:Label ID="capMessages" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capVESSEL_NUMBER" runat="server" Text="VESSEL NUMBER"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtVESSEL_NUMBER" runat="server"   MaxLength="10"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revVESSEL_NUMBER" runat="server" 
                    ControlToValidate="txtVESSEL_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
                 <asp:requiredfieldvalidator id="rfvVESSEL_NUMBER" runat="server" ControlToValidate="txtVESSEL_NUMBER"
											Display="Dynamic"></asp:requiredfieldvalidator>  
											
            </td>
            <td width="50%" class="midcolora" >
                <asp:Label ID="capNAME_OF_VESSEL" Text="NAME OF VESSEL " runat="server"></asp:Label><span class="mandatory">*</span></br>
                <asp:DropDownList runat="server" ID="cmbNAME_OF_VESSEL" 
                    onselectedindexchanged="cmbNAME_OF_VESSEL_SelectedIndexChanged" AutoPostBack ="true"></asp:DropDownList><br />
                <asp:RegularExpressionValidator ID="revNAME_OF_VESSEL" runat="server" 
                    ControlToValidate="cmbNAME_OF_VESSEL" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:requiredfieldvalidator id="rfvNAME_OF_VESSEL" runat="server" ControlToValidate="cmbNAME_OF_VESSEL"
											Display="Dynamic"></asp:requiredfieldvalidator>
            </td>
                <td width="25%" class="midcolora">
                <asp:Label ID="capMANUFACTURE_YEAR" runat="server">MANUFACTURE YEAR</asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox runat="server" ID="txtMANUFACTURE_YEAR" MaxLength="4"></asp:TextBox><br />
                <asp:requiredfieldvalidator id="rfvMANUFACTURE_YEAR" runat="server" ControlToValidate="txtMANUFACTURE_YEAR" 
												Display="Dynamic"></asp:requiredfieldvalidator>
               <asp:rangevalidator id="rngMANUFACTURE_YEAR" ControlToValidate="txtMANUFACTURE_YEAR" Display="Dynamic" Runat="server"
												Type="Integer" MinimumValue="1900"></asp:rangevalidator>
                   <%-- <asp:customvalidator id="csvMANUFACTURE_YEAR" Runat="server" ControlToValidate="txtMANUFACTURE_YEAR" Display="Dynamic"
											ClientValidationFunction="ChkOccurenceDate"></asp:customvalidator>
              --%>
            </td>
          
        </tr>
        <tr>
              <td width="25%"class="midcolora">
                <asp:Label ID="capTYPE_OF_VESSEL" runat="server" Text="FLAG"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtTYPE_OF_VESSEL" runat="server" size="40" MaxLength="20"></asp:TextBox><br />
                <asp:requiredfieldvalidator id="rfvTYPE_OF_VESSEL" runat="server" ControlToValidate="txtTYPE_OF_VESSEL"
											Display="Dynamic"></asp:requiredfieldvalidator><asp:RegularExpressionValidator ID="revTYPE_OF_VESSEL" runat="server" 
                    ControlToValidate="txtTYPE_OF_VESSEL" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td width="25%" class="midcolora">
                <asp:Label ID="capMANUFACTURER" runat="server">MANUFACTURER</asp:Label></br>
                <asp:TextBox runat="server" ID="txtMANUFACTURER" size="45" MaxLength="50"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revMANUFACTURER" runat="server" 
                    ControlToValidate="txtMANUFACTURER" Display="Dynamic"></asp:RegularExpressionValidator>
                 
            </td>
              <td  width="25%" class="midcolora">
                <asp:Label ID="capREMARKS" runat="server" Text="REMARKS"></asp:Label></br>
                 <asp:TextBox ID="txtREMARKS" runat="server" TextMode="MultiLine" Rows="3" 
                    Width="400px" Height="70px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox><br />
                <%--<asp:RegularExpressionValidator ID="revtREMARKS" runat="server" 
                    ControlToValidate="txtREMARKS" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                
            </td>
          
        </tr>
        <tr>
        <td class="midcolorc" colspan="3">
        <table id = "tbl" cellpadding ="2" cellspacing ="2" width ="100%">
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capCONSTRUCTION" runat="server" Text="CONSTRUCTION"></asp:Label>
                </br>
                <asp:TextBox ID="txtCONSTRUCTION" runat="server" size="25" MaxLength="100"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revCONSTRUCTION" runat="server" 
                    ControlToValidate="txtCONSTRUCTION" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="50%" class="midcolora">
                <asp:Label ID="capPROPULSION" Text="PROPULSION" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtPROPULSION" size="45" MaxLength="20"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPROPULSION" runat="server" 
                    ControlToValidate="txtPROPULSION" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="25%" class="midcolora">
                <asp:Label ID="capCLASSIFICATION" runat="server" Text="CLASSIFICATION"></asp:Label>
                </br>
                <asp:TextBox ID="txtCLASSIFICATION" runat="server" size="40" MaxLength="50"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revCLASSIFICATION" runat="server" 
                    ControlToValidate="txtCLASSIFICATION" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capLOCAL_OPERATION" Text="LOCAL OPERATION" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtLOCAL_OPERATION" size="30" MaxLength="100"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revLOCAL_OPERATION" runat="server" 
                    ControlToValidate="txtLOCAL_OPERATION" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="50%"class="midcolora">
                <asp:Label ID="capLIMIT_NAVIGATION" runat="server" Text="LIMIT NAVIGATION"></asp:Label>
                </br>
                <asp:TextBox ID="txtLIMIT_NAVIGATION" runat="server" size="45" MaxLength="100"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revLIMIT_NAVIGATION" runat="server" 
                    ControlToValidate="txtLIMIT_NAVIGATION" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="25%" class="midcolora">
                <asp:Label ID="capPORT_REGISTRATION" Text="PORT REGISTRATION" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtPORT_REGISTRATION" size="40" MaxLength="50"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPORT_REGISTRATION" runat="server" 
                    ControlToValidate="txtPORT_REGISTRATION" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capREGISTRATION_NUMBER" Text="REGISTRATION #" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtREGISTRATION_NUMBER" size="30" 
                    MaxLength="50"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revREGISTRATION_NUMBER" runat="server" 
                    ControlToValidate="txtREGISTRATION_NUMBER" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="50%" class="midcolora">
                <asp:Label ID="capTIE_NUMBER" Text="TIE #" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtTIE_NUMBER" size="45" MaxLength="50"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revTIE_NUMBER" runat="server" 
                    ControlToValidate="txtTIE_NUMBER" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="25%" class="midcolora">
                <asp:Label ID="capVESSEL_ACTION_NAUTICO_CLUB" runat="server" Text="VESSEL_ACTION_NAUTICO_CLUB"></asp:Label></br>
                <asp:CheckBox ID="chkVESSEL_ACTION_NAUTICO_CLUB" runat="server" onclick="showMe('txtNAME_OF_CLUB', this)" />
            </td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
            <div id="Div_Name_Of_Club"  runat="server">
                <asp:Label ID="capNAME_OF_CLUB" Text="NAME OF CLUB" runat="server"></asp:Label>
                <asp:Label ID="lblmand" Text="*" runat="server" ForeColor="Red" ></asp:Label></br>
                <asp:TextBox runat="server" ID="txtNAME_OF_CLUB" size="30" MaxLength="70"></asp:TextBox><br />
                <asp:requiredfieldvalidator id="rfvNAME_OF_CLUB" runat="server" ControlToValidate="txtNAME_OF_CLUB"
											Display="Dynamic"></asp:requiredfieldvalidator>
	            <asp:RegularExpressionValidator ID="revNAME_OF_CLUB" runat="server" 
                    ControlToValidate="txtNAME_OF_CLUB" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
	        </div>
            </td>
            <td width="50%" class="midcolora">
                <asp:Label ID="capLOCAL_CLUB" runat="server" Text="LOCAL CLUB"></asp:Label></br>
                <asp:TextBox ID="txtLOCAL_CLUB" runat="server" size="45" MaxLength="100"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revLOCAL_CLUB" runat="server" 
                    ControlToValidate="txtLOCAL_CLUB" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td width="25%" class="midcolora">
                <asp:Label ID="capNUMBER_OF_CREW" Text="#of Crew" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtNUMBER_OF_CREW" size="40" MaxLength="5"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revNUMBER_OF_CREW" runat="server" 
                    ControlToValidate="txtNUMBER_OF_CREW" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" class="midcolora">
                <asp:Label ID="capNUMBER_OF_PASSENGER" runat="server" Text="# OF PASSENGER"></asp:Label></br>
                <asp:TextBox ID="txtNUMBER_OF_PASSENGER" runat="server" MaxLength="5" ></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revNUMBER_OF_PASSENGER" runat="server" 
                    ControlToValidate="txtNUMBER_OF_PASSENGER" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
            <td width="25%" class="midcolora">
                <asp:Label ID="capBUILDER" Text="BUILDER" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtBUILDER" size="40" MaxLength="50"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revBUILDER" runat="server" 
                    ControlToValidate="txtBUILDER" Display="Dynamic" Enabled ="false"></asp:RegularExpressionValidator>
            </td>

        </tr>
        </table>
        </td>
        </tr>
        <tr>
            <td class="midcolora" nowrap ="nowrap">
                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" text="Reset"></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" causesvalidation="false"
                    text="Activate/Deactivate" onclick="btnActivateDeactivate_Click"></cmsb:cmsbutton>
            </td>
            <td class="midcolora" >
            </td>
            <td  class="midcolorr" >
                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" text="Delete" 
                    causesvalidation="false" onclick="btnDelete_Click"></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" text="Save" onclick="btnSave_Click" 
                    ></cmsb:cmsbutton>
            </td>
        </tr>
    </table>
    <input id="hidPOL_VERSION_ID" type="hidden" name="hidPOL_VERSION_ID" runat="server"/>
	<input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server" />
	<input id="hidPOL_ID" type="hidden" name="hidPOL_ID" runat="server"/>
	<input id="hidVESSEL_NUMBER" type="hidden" name="hidVESSEL_NUMBER" runat="server"/>
	<input id="hidOLD_VESSEL_NUMBER" type="hidden" name="hidOLD_VESSEL_NUMBER" runat="server"/>
	<input id="hidVESSEL_NUMBER_Msg" type="hidden" name="hidVESSEL_NUMBER_Msg" runat="server"/>
	
	<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
    <input id="hidMariTimeID" type="hidden" value="" name="hidMariTimeID" runat="server"/>
     <input id="hidTAB_TITLES" type="hidden" value="0" name="hidTAB_TITLES" runat="server"/>            
	
		
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {
          
            var CALLEDFROM = '<%=CALLEDFROM %>'
            if (CALLEDFROM == "QAPP") {
                //parent.location.href = parent.location.href;
                //parent.document.getElementById('cmbRisk').selectedIndex = 2;
                //alert(parent.document.getElementById('cmbRisk').options[1].value)
                //("parent.RiskChange()",100);
                parent.BindRisk();
                for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {

                    if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidMariTimeID').value)
                        parent.document.getElementById('cmbRisk').options[i].selected = true;

                }
                
            }
            else {
                try {
                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidMariTimeID').value);
                }
                catch (err) {

                }
            }
        }

		</script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProductLocationInfo.aspx.cs" Inherits="Cms.Policies.Aspx.AddProductLocationInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>POL_PRODUCT_LOCATION</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		
		<script language="javascript" type="text/javascript">
		    function Init() {
		        ApplyColor();
		        ChangeColor();
		    }
		    function setTab() {

		        var pagefrom = '<%=PAGEFROM %>'
		        if (pagefrom == 'QAPP') {
		            EnableValidator('rfvLOCATION', false);
		            document.getElementById("spn_mandatory").style.display = 'none';
		            document.getElementById("rfvLOCATION").style.display = 'none'
		        }
		        else {
		            EnableValidator('rfvLOCATION', true);

		        }
		      
		        if (document.getElementById('hidPRODUCT_RISK_ID') != null && document.getElementById('hidPRODUCT_RISK_ID').value != '' && document.getElementById('hidPRODUCT_RISK_ID').value != 'NEW') {
		            if (document.getElementById('hidCALLED_FROM') != null) {
		                var CalledFrom = document.getElementById('hidCALLED_FROM').value;
		                var RISK_ID = document.getElementById('hidPRODUCT_RISK_ID').value;
		                var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
		                var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
		                var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
		                var LOCATION = document.getElementById('hidLOCATION').value;
		                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
		                var tabtitles = TAB_TITLES.split(',');
		                ///


		                if (CalledFrom != '' && CalledFrom == "CompCondo" || CalledFrom == "RISK" || CalledFrom == "CompComp" || CalledFrom == "TFIRE" || CalledFrom == "GLBANK" || CalledFrom == "JDLGR") {
		                    if (RISK_ID != "NEW") {

		                        if (pagefrom == "QAPP") {// changed by sonal to implemt this in quick app

		                            Url = parent.document.getElementById('hidFrameUrl').value + RISK_ID + "&";
		                            DrawTab(1, top.frames[1], tabtitles[0], Url);
		                        }

		                        Url = "/Cms/Policies/aspx/ProtectiveDevicesInfo.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&LOCATION_ID=" + LOCATION + "&";
		                        DrawTab(2, top.frames[1], tabtitles[1], Url);


		                        Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&";
		                        DrawTab(3, top.frames[1], tabtitles[2], Url);

		                        Url = "/Cms/Policies/aspx/AddPolicyCoverages.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&";
		                        DrawTab(4, top.frames[1], tabtitles[3], Url);

		                    }
		                }
		                
		            }
		        } else {
		            RemoveTab(4, top.frames[1]);
		            RemoveTab(3, top.frames[1]);
		            RemoveTab(2, top.frames[1]);
		        }
		     }
		     function ResetTheForm() {
		         document.POL_PRODUCT_LOCATION.reset();
		     }
		     function HideValidator(id) {
		         document.getElementById(id).style.display = "none";
		         document.getElementById(id).setAttribute('isValid', false);
		         document.getElementById(id).setAttribute('enabled', false);
		     }
		     function HideShowTDBasedOnCalledFrom() 
		     {
                    
                    if (document.getElementById('hidCALLED_FROM') != null) {
                        var CalledFrom = document.getElementById('hidCALLED_FROM').value;
                    }

                    if (CalledFrom != '' && CalledFrom == "CompCondo") {

                        $('#td3').hide();
                        $('#td4').hide();
                        $('#td5').hide();

                        //HideValidator("rfvACTIVITY_TYPE");
                        //HideValidator("rfvOCCUPIED_AS");
                        HideValidator("revBUILDING_VALUE");
                        HideValidator("revCONTENTS_VALUE");
                        HideValidator("revRAW_MATERIAL_VALUE");
                        HideValidator("revMRI_VALUE");

                    }
                    else if (CalledFrom != '' && CalledFrom == "RISK") {

                        $('#td1').hide();
                        $('#td2').hide();
                        $('#td3').hide();
                        $('#td4').hide();
                        $('#td5').hide();

                        // HideValidator("rfvACTIVITY_TYPE");
                        // HideValidator("rfvOCCUPIED_AS");
                        HideValidator("revBUILDING_VALUE");
                        HideValidator("revCONTENTS_VALUE");
                        HideValidator("revRAW_MATERIAL_VALUE");
                        HideValidator("revMRI_VALUE");
                      //  HideValidator("revPARKING_SPACES");
                        HideValidator("txtPOSSIBLE_MAX_LOSS");



                    }
                    else if (CalledFrom != '' && CalledFrom == "CompComp") {

                        $('#td1').show();
                        $('#td2').show();
                        $('#td3').show();
                        $('#td4').show();
                        $('#td5').show();


                    }
                    else if (CalledFrom != '' && CalledFrom == "TFIRE") {
                    
                    
                    }


                }
                //Added by Pradeep itrack 837 on 03/03/2011
                function validateRange(objSource, objArgs) {
                    
                    var comm = parseFloat(document.getElementById(objSource.controltovalidate).value);
                    if (comm < 0 || comm > 100) {
                        document.getElementById(objSource.controltovalidate).select();
                        objArgs.IsValid = false;
                    }
                    else
                        objArgs.IsValid = true;

                }
                //Added till here 
		        function ShowAlertMessageWhileDelete(isDelete) {

		            if (isDelete) {
		           
		                $('#hidConfirmValue').val(vbMsg($('#lblDeletemsg').val()))

		            }
		        }
		        function CheckSlectedValue() {
		            var val = document.getElementById('cmbLOCATION').value;
		            if (val != "")
		                return true
		            else
		                return false;
		        }
		        //Added by Pradeep for itrack 1512/tfs#240
		        function fnFormatAmountForSum(num) {
		            num = ReplaceAll(num, sGroupSep, '');
		            num = ReplaceAll(num, sDecimalSep, '.');
		            return num;
		        }
		        function validateLimitRange(sender, args) {

		            var input = args.Value;
		            input = fnFormatAmountForSum(input)
		            var max = 999999999.99;
		            if (parseFloat(input) <= parseFloat(max)) {
		                args.IsValid = true;
		            }
		            else {
		                args.IsValid = false;
		            }
		        }
		        //Added till here 
		        $(document).ready(function() {
		            $("#txtBONUS").blur();
		            $("#txtCLAIM_RATIO").blur();
		            if ($("#hidCALLED_FROM").val() == "GLBANK" || $("#hidCALLED_FROM").val() == "RISK") {
		                $("#txtACTUAL_INSURED_OBJECT").show();
		                $("#capACTUAL_INSURED_OBJECT").show();
		            }
		            else {
		                $("#txtACTUAL_INSURED_OBJECT").hide();
		                $("#capACTUAL_INSURED_OBJECT").hide();
		            }

		            //Added by Pradeep for itrack#1152 / TFS# 2598
		            $("#cmbACTIVITY_TYPE").change(function () {

		                if ($("#cmbACTIVITY_TYPE option:selected").val() != '') {
		                    var _rubrica = $("#cmbACTIVITY_TYPE option:selected").val().split('^');
		                    $("#hidACTIVITY_TYPE").val(_rubrica[0].toString());
		                    var result = AddProductLocationInfo.AjaxGetOccupied(_rubrica[1].toString());
		                    fillDTCombo(result.value, document.getElementById('cmbOCCUPIED_AS'), 'OCCUPIED_ID', 'OCCUPIED_AS', 0);
		                }
		            });

		            $("#cmbOCCUPIED_AS").change(function () {
		                if ($("#cmbOCCUPIED_AS option:selected").val() != '')
		                    $("#hidOCCUPIED").val($("#cmbOCCUPIED_AS option:selected").val());
		            });
		            //Added till here 
		        });
		        //Added by Pradeep for itrack#1152 / TFS# 2598
		        function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
		            //debugger;
		            combo.innerHTML = "";
		            if (objDT != null) {

		                for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {
		                    if (i == 0) {
		                        oOption = document.createElement("option");
		                        oOption.value = "";
		                        oOption.text = "";
		                        combo.add(oOption);
		                    }
		                    oOption = document.createElement("option");
		                    oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
		                    oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
		                    combo.add(oOption);
		                }
		            }
		        }
		        //Added till here 
		</script>
		 <script language="vbscript" type="text/vbscript">

            Function vbMsg(isTxt)
                vbMsg = MsgBox(isTxt,4)
            End Function
        		         
        </script>
</head>
<body  leftMargin="0"  rightMargin="0" MS_POSITIONING="GridLayout"  onload="Init();setTab();HideShowTDBasedOnCalledFrom();">
    <form id="POL_PRODUCT_LOCATION"  runat="server" name="POL_PRODUCT_LOCATION" onsubmit="" method="post">
        <table id="Table1" cellspacing="2" cellpadding="0" width="100%" border="0" >
           <tr>
            <td  class="midcolorc" colspan="3"><asp:Label runat="server" ID="lblDelete" CssClass="errmsg"></asp:Label></td>
           </tr>
            <tbody runat="server" id="tbody">
             <tr>
                   <td  align="right" colspan="3">
	                 <asp:label id="lblFormLoadMessage" runat="server" Visible="False" 
                           CssClass="errmsg" ></asp:label>
	               </td>
	          </tr>
        
             <tr>
               <td class="pageHeader" align="left" colspan="3" >		           
		           <asp:label id="lblManHeader" Runat="server" ></asp:label>
		       </td>
	        </tr>
    	
	    <tr>
		    <td class="midcolorc" colspan="3"><asp:label id="lblMessage" runat="server" 
                    CssClass="errmsg" ></asp:label>
		    </td>
	    </tr>
	    
	    <tr>
		    <td class="midcolora" colspan="3" ><asp:label id="capLOCATION" runat="server" >Location</asp:label><span class="mandatory" id="spn_mandatory">*</span>
		    <br />
		    <asp:DropDownList runat="server" ID="cmbLOCATION" Width="100%"  >
		    <asp:ListItem Text="" Value=""></asp:ListItem>
		    </asp:DropDownList>
		        <cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server" OnClientClick="javascript:return CheckSlectedValue()"
                                onclick="btnSelect_Click" CausesValidation="false" ></cmsb:cmsbutton>
		    <asp:RequiredFieldValidator runat="server"  ID="rfvLOCATION"  ErrorMessage="" ControlToValidate="cmbLOCATION" Display="Dynamic"></asp:RequiredFieldValidator>
		    </td>
	    </tr>
	    
	    <tr>
		    <td class="midcolora" Width="33%" >
		    <asp:Label runat="server" id="capVALUE_AT_RISK"  >Value at Risk</asp:Label>
		    <br />  <%-- changed by praveer itrack no 1512/TFS#240--%>
		    <asp:TextBox runat="server" ID="txtVALUE_AT_RISK"  CssClass="INPUTCURRENCY" MaxLength="12" onblur="this.value=formatAmount(this.value)" 
                    CausesValidation="false"></asp:TextBox>
		    <br />
		    <asp:RegularExpressionValidator runat="server" ID="revVALUE_AT_RISK" Display="Dynamic" ErrorMessage="" ControlToValidate="txtVALUE_AT_RISK"></asp:RegularExpressionValidator>
            <%-- Added by Pradeep itrack no 1512/TFS#240--%>
            <asp:CustomValidator ID="csvVALUE_AT_RISK" runat="server" ControlToValidate="txtVALUE_AT_RISK" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
		    </td>
		    <td class="midcolora" Width="34%" >
		    <asp:Label runat="server" id="capMAXIMUM_LIMIT" >Maximum Limit</asp:Label>
		    <br />  <%-- changed by praveer itrack no 1512/TFS#240--%>
		    <asp:TextBox runat="server" ID="txtMAXIMUM_LIMIT" CssClass="INPUTCURRENCY" MaxLength="12" onblur="this.value=formatAmount(this.value)"></asp:TextBox>
		    <br />
   		    <asp:RegularExpressionValidator runat="server" ID="revMAXIMUM_LIMIT" Display="Dynamic" ErrorMessage="" ControlToValidate="txtMAXIMUM_LIMIT"></asp:RegularExpressionValidator>
            <%-- Added by Pradeep itrack no 1512/TFS#240--%>
            <asp:CustomValidator ID="csvMAXIMUM_LIMIT" runat="server" ControlToValidate="txtMAXIMUM_LIMIT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>

		    </td>	
		    <td class="midcolora" Width="33%" >
		    
		    <asp:Label runat="server" id="capMULTIPLE_DEDUCTIBLE" >Multiple Deductible</asp:Label>
		    <br />
		    <asp:DropDownList runat="server" ID="cmbMULTIPLE_DEDUCTIBLE" >
		    <asp:ListItem Text="" Value=""></asp:ListItem>
		    </asp:DropDownList>
		    </td>	
		    
	  </tr>
	  <tr id="td1">
		    <td class="midcolora" Width="33%" >
		    <asp:Label runat="server" id="capPARKING_SPACES" ># Parking Space</asp:Label>
		    <br />
		    <asp:TextBox runat="server" ID="txtPARKING_SPACES" MaxLength="20"></asp:TextBox>
		    <br />
  		   <%-- <asp:RegularExpressionValidator runat="server" ID="revPARKING_SPACES" Display="Dynamic" ErrorMessage="" ControlToValidate="txtPARKING_SPACES"></asp:RegularExpressionValidator>--%>

		    </td>	
		    
		     <td class="midcolora" Width="33%" >
		    <asp:Label runat="server" id="capCONSTRUCTION" >Construction</asp:Label>
		    <br />
		    <asp:DropDownList runat="server" ID="cmbCONSTRUCTION" >
		    <asp:ListItem Text="" Value=""></asp:ListItem>
		    </asp:DropDownList>
		    </td>	
		     <td class="midcolora" Width="34%" >
		     <asp:Label runat="server" id="capPOSSIBLE_MAX_LOSS" >Possible Max Loss</asp:Label>
		    <br />
		    <asp:TextBox runat="server" ID="txtPOSSIBLE_MAX_LOSS" CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatAmount(this.value)"></asp:TextBox><br />
   		    <asp:RegularExpressionValidator runat="server" ID="revPOSSIBLE_MAX_LOSS" Display="Dynamic" ErrorMessage="" ControlToValidate="txtPOSSIBLE_MAX_LOSS"></asp:RegularExpressionValidator>
   		    
		    </td>	
	  </tr>
	      
	    <tr id="td3" >
	    
	        <td class="midcolora" Width="33%" ><asp:Label runat="server" id="capBUILDING_VALUE" >Building Value</asp:Label><br />
	            <asp:TextBox runat="server" ID="txtBUILDING_VALUE" CssClass="INPUTCURRENCY" 
                    MaxLength="15" onblur="this.value=formatAmount(this.value)"  ></asp:TextBox> <br />
   		    <asp:RegularExpressionValidator runat="server" ID="revBUILDING_VALUE" 
                    Display="Dynamic" ErrorMessage="" ControlToValidate="txtBUILDING_VALUE"></asp:RegularExpressionValidator>

	        </td>
	        <td class="midcolora" Width="34%" >
	            <asp:Label runat="server" id="capCONTENTS_RAW_VALUES" >Contents/Raw Value</asp:Label><br />
	            <asp:CheckBox runat="server" ID="chkCONTENTS_RAW_VALUES" />
	        </td>
	        <td class="midcolora" Width="33%" >
	            <asp:Label runat="server" id="capMRI_VALUE" >MRI Value</asp:Label><br />
	            <asp:TextBox runat="server" ID="txtMRI_VALUE" CssClass="INPUTCURRENCY" 
                    MaxLength="15" onblur="this.value=formatAmount(this.value)" ></asp:TextBox><br />
   		    <asp:RegularExpressionValidator runat="server" ID="revMRI_VALUE" 
                    Display="Dynamic" ErrorMessage="" ControlToValidate="txtMRI_VALUE"></asp:RegularExpressionValidator>

	        </td>
	    
	    </tr>
	    <tr id="td4">
	     <td class="midcolora" Width="33%" >
	            <asp:Label ID="capCONTENTS_VALUE" runat="server">Contents Value</asp:Label>
                <br />
                <asp:TextBox ID="txtCONTENTS_VALUE" runat="server" CssClass="INPUTCURRENCY" 
                    MaxLength="15" onblur="this.value=formatAmount(this.value)"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="revCONTENTS_VALUE" runat="server" 
                    ControlToValidate="txtCONTENTS_VALUE" Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
                </td>
	           <td class="midcolora" Width="34%" >
    	 
	            <asp:Label ID="capRAW_MATERIAL_VALUE" runat="server">Raw Material Value</asp:Label>
                 <br />
                <asp:TextBox ID="txtRAW_MATERIAL_VALUE" runat="server" CssClass="INPUTCURRENCY" 
                    MaxLength="15" onblur="this.value=formatAmount(this.value)"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="revRAW_MATERIAL_VALUE" runat="server" 
                    ControlToValidate="txtRAW_MATERIAL_VALUE" Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
	        
	        <td class="midcolora" Width="33%" >
	             <asp:Label runat="server" id="capRUBRICA" >Rubrica</asp:Label><br />
	             <asp:TextBox runat="server" ID="txtRUBRICA" MaxLength="6" ></asp:TextBox>
	        </td>
	    </tr>
	    
	     <tr>
	  <td class="midcolora" Width="33%" >
	  <asp:Label ID="capBONUS" runat="server">Bonus</asp:Label>
                 <br />
      <asp:TextBox ID="txtBONUS" runat="server"  CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatRate(this.value,2)"></asp:TextBox>
      <br />
      <asp:RegularExpressionValidator runat="server"  ID="revBONUS"  ErrorMessage="" ControlToValidate="txtBONUS" Display="Dynamic"></asp:RegularExpressionValidator>
      <asp:CustomValidator ID="csvBONUS" runat="server" ControlToValidate="txtBONUS" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
      </td>
      <td class="midcolora" Width="33%" >
	  <asp:Label ID="capCLAIM_RATIO" runat="server">Claim Ratio</asp:Label>
                 <br />
        <asp:TextBox ID="txtCLAIM_RATIO" runat="server" CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatRate(this.value,2)"></asp:TextBox>
        <BR />
       <asp:RegularExpressionValidator runat="server"  ID="revCLAIM_RATIO"  ErrorMessage="" ControlToValidate="txtCLAIM_RATIO" Display="Dynamic"></asp:RegularExpressionValidator>
       <asp:CustomValidator ID="csvCLAIM_RATIO" runat="server" ControlToValidate="txtCLAIM_RATIO" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
      </td>
             
      <td class="midcolora" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
      </tr>
      <tr>
      
      <td class="midcolora" colspan="3">
     <%-- itrack 1400 , removed dropdown width--%>
      <asp:Label ID="capCoApplicant" runat="server" Text=""></asp:Label><span class="mandatory" id="Span1">*</span><br />
                <asp:DropDownList ID="cmbCO_APPLICANT_ID" runat="server"  >
                </asp:DropDownList>
                <br />
                 <asp:requiredfieldvalidator id="rfvCO_APPLICANT" runat="server" ControlToValidate="cmbCO_APPLICANT_ID" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
      </td>
    </tr>

      <tr>
      <td id="tdClassField" runat="server" visible="false" class="midcolora"  colspan="1" >
        <asp:Label ID="capCLASS_FIELD" runat="server" Text="Class Filed:"></asp:Label><br />
                <asp:DropDownList ID="cmbCLASS_FIELD" runat="server" Width="191px" >
                </asp:DropDownList>
                
	      </td>
	      
	      <td class="midcolora" Width="33%"  >
	  <asp:Label ID="capLOCATION_NUMBER" runat="server"></asp:Label><span class="mandatory" id="Span2">*</span>
                 <br />
        <asp:TextBox ID="txtLOCATION_NUMBER" runat="server"  MaxLength="9"> </asp:TextBox>
        <BR />
         <asp:requiredfieldvalidator id="rfvLOCATION_NUMBER" runat="server" ControlToValidate="txtLOCATION_NUMBER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
         <asp:RegularExpressionValidator ID="revLOCATION_NUMBER" runat="server" 
                    ControlToValidate="txtLOCATION_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
      
      </td>
      <td class="midcolora" Width="33%" colspan=2 >
	  <asp:Label ID="capITEM_NUMBER" runat="server"></asp:Label><span class="mandatory" id="Span3">*</span>
                 <br />
        <asp:TextBox ID="txtITEM_NUMBER" runat="server"  MaxLength="9"> </asp:TextBox>
        <BR />
          <asp:requiredfieldvalidator id="rfvITEM_NUMBER" runat="server" ControlToValidate="txtITEM_NUMBER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
        <asp:RegularExpressionValidator ID="revITEM_NUMBER" runat="server" 
                   ControlToValidate="txtITEM_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
     <%--    <asp:CompareValidator ID="cmpvITEM_NUMBER" runat="server" ControlToCompare="txtLOCATION_NUMBER" ControlToValidate="txtITEM_NUMBER"  Operator="NotEqual"   ErrorMessage=" " Display="Dynamic" ></asp:CompareValidator>--%>
      
      </td>
      
      
	   </tr>
      <tr>
      <td class="midcolora" Width="100%" colspan="3" >
	            <asp:Label runat="server" id="capACTIVITY_TYPE" >Activity Type</asp:Label><%--<span class="mandatory">*</span>--%><br />
	            <asp:DropDownList runat="server" ID="cmbACTIVITY_TYPE" Width="100%">
	            <asp:ListItem Text="" Value=""></asp:ListItem>
	            </asp:DropDownList><br />
		    <%--<asp:RequiredFieldValidator runat="server"  ID="rfvACTIVITY_TYPE"  ErrorMessage="" 
                    ControlToValidate="cmbACTIVITY_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    
	        </td>
	   </tr>
	   <tr>
	        <td class="midcolora" Width="100%" colspan="3" >
    	 
	            <asp:Label runat="server" id="capOCCUPIED_AS" >Occupied As</asp:Label><%--<span class="mandatory">*</span>--%><br />
	            <asp:DropDownList runat="server" ID="cmbOCCUPIED_AS" Width="100%">
	            <asp:ListItem Text="" Value=""></asp:ListItem>
	            </asp:DropDownList><br />
	           <%-- <asp:RequiredFieldValidator runat="server"  ID="rfvOCCUPIED_AS"  ErrorMessage="" 
                    ControlToValidate="cmbOCCUPIED_AS" Display="Dynamic"></asp:RequiredFieldValidator>--%>
	        </td>
         </tr>
         <tr id="td2">
		    <td class="midcolora" Width="33%" >
		    <asp:Label runat="server" id="capASSIST24" >24 Hour Assistance</asp:Label>
		    <br />
		   <asp:CheckBox runat="server" ID="chkASSIST24"  AccessKey="c"/>
		    </td>	
		    
		     <td class="midcolora" Width="33%" >
		   

		    </td>	
		     <td class="midcolora" Width="33%" >
		   

		    </td>	
	    </tr>
      
      <tr >
	        <td id="tdACTUAL_INSURED_OBJECT" runat = "server" class="midcolora" Width="33%"  colspan="2">
	            <asp:Label ID="capACTUAL_INSURED_OBJECT" runat="server" ></asp:Label><br />
               <asp:TextBox ID="txtACTUAL_INSURED_OBJECT"   Width="400px" MaxLength="250" Height="70px"  TextMode="MultiLine" Rows="3"  runat="server" onkeypress="MaxLength(this,250)" onpaste="MaxLength(this,250)" 
                     ></asp:TextBox>
                     </td>
                     
                     <td id="tdPortableEquipment" runat="server" class="midcolora" Width="33%" visible="false">
		    
		    <asp:Label runat="server" id="capPortableEquipment" >Portable Equipment</asp:Label><span class="mandatory" id="span4">*</span>
		    <br />
		    <asp:DropDownList runat="server" ID="cmbPORTABLE_EQUIPMENT" >
		    <asp:ListItem Text="" Value=""></asp:ListItem>
		    </asp:DropDownList><br />
		    <asp:RequiredFieldValidator runat="server"  ID="rfvPORTABLE_EQUIPMENT"  ErrorMessage="" 
                    ControlToValidate="cmbPORTABLE_EQUIPMENT" Display="Dynamic"></asp:RequiredFieldValidator>
		    </td>
	        
	    </tr>
      
      <tr >
	        <td class="midcolora" Width="33%"  colspan="3">
	        <asp:Label runat="server" id="capREMARKS" >Remarks</asp:Label>
		    <br />
		    <asp:TextBox runat="server" ID="txtREMARKS"  TextMode="MultiLine" Rows="3" 
                    Width="400px" Height="70px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox>
	        </td>
	         
	    </tr>
	        <td class="midcolora" >
	            <cmsb:CmsButton  runat="server" ID="btnReset" Text="Reset"  CssClass="clsButton"  CausesValidation="false"/>
                <cmsb:CmsButton runat="server" ID="btnActivateDeactivate" Text="Activate/Deactivate"  CssClass="clsButton" CausesValidation="false" onclick="btnActivateDeactivate_Click" />


	        </td> 
	        <td class="midcolorr" colspan="2" > 
	            <input  type="hidden"  runat="server" ID="hidCUSTOMER_ID" value=""  name="hidCUSTOMER_ID"/>
	            <input  type="hidden" runat="server" ID="hidPOLICY_ID"   value=""  name="hidPOLICY_ID"/>
	            <input  type="hidden" runat="server" ID="hidPOLICY_VERSION_ID"  value=""  name="hidPOLICY_VERSION_ID"/>
	            <input  type="hidden" runat="server" ID="hidPRODUCT_RISK_ID"   value=""  name="hidPRODUCT_RISK_ID"/>
	            <input  type="hidden" runat="server" ID="hidFormSaved"  value=""  name="hidFormSaved"/>  
	            <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	            <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>   
                <input id="lblDeletemsg" type="hidden" name="lblDeletemsg" runat="server" />
                <input id="hidConfirmValue" type="hidden" name="hidConfirmValue" runat="server" />
                <input id="hidLOCATION" type="hidden" name="hidLOCATION" runat="server" />
                 <%--Added for itrack-1152/TFS # 2598--%>
                <input id="hidACTIVITY_TYPE" type="hidden" value="0" name="hidACTIVITY_TYPE" runat="server"/>  
                <input id="hidOCCUPIED" type="hidden" value="0" name="hidOCCUPIED" runat="server"/>  
                <input id="hidRUBRICA_ID" type="hidden" value="0" name="hidRUBRICA_ID" runat="server"/>  
                <%--Added till here --%>
                <cmsb:CmsButton  runat="server" ID="btnDelete" Text="Delete"  CssClass="clsButton" CausesValidation="false" onclick="btnDelete_Click" />&nbsp;&nbsp;
                <cmsb:CmsButton  runat="server" ID="btnSave" Text="Save"  CssClass="clsButton"  CausesValidation="true" onclick="btnSave_Click"/>
               
 	        </td>
	    </tr>
	    </tbody>
	   </table>
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {

            var pagefrom = '<%=PAGEFROM %>'
            if (pagefrom == "QAPP") {

                parent.BindRisk();
                for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {

                    if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidPRODUCT_RISK_ID').value)
                        parent.document.getElementById('cmbRisk').options[i].selected = true;

                }
            }
            else {
                try {

                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPRODUCT_RISK_ID').value);
                }
                catch (err) {

                }
            }
               
        }
		</script>
</body>
</html>

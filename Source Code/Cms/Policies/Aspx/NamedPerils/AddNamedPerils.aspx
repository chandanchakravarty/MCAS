<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNamedPerils.aspx.cs"
    Inherits="Cms.Policies.Aspx.NamedPerils.AddNamedPerils" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AddNamedPerils</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQuery.FormatCurrency.js"></script> 
    <script type="text/javascript">
        //Added by Pradeep itrack 837 on 03/03/2011
        function validateRange(objSource, objArgs) {

            var comm = document.getElementById(objSource.controltovalidate).value;
            comm = FormatAmountForSum(comm);
            
            if (parseFloat(comm) < 0 || parseFloat(comm) > 100) {
                document.getElementById(objSource.controltovalidate).select();
                objArgs.IsValid = false;
            }
            else
                objArgs.IsValid = true;

        }
        function FormatAmountForSum(num) {
            num = ReplaceAll(num, ',', '.');
            return num;
        }
        //Added till here 
        /*function CheckMaxLengthOfRemark(objSource, objArgs) {
            var RemarkValue = document.POL_PERILS.txtREMARKS.value;
            if (RemarkValue.length > 500)
            { objArgs.IsValid = false; }
            else
            { objArgs.IsValid = true; }
        }*/
        function setTab() {

            var pagefrom = '<%=CALLEDFROM %>'

            if (pagefrom == 'QAPP') {
                EnableValidator('rfvLOCATION', false);
                document.getElementById("spn_mandatory").style.display = 'none';
                document.getElementById("rfvLOCATION").style.display = 'none'
            }
            else {
                EnableValidator('rfvLOCATION', true);
               
            }
          
           
            if (document.getElementById('hidPeril_Id') != null && document.getElementById('hidPeril_Id').value != "NEW" && document.getElementById('hidPeril_Id').value != "") {
                var CalledFrom = '';
                if (document.getElementById('hidCalledFrom') != null)
                    CalledFrom = document.getElementById('hidCalledFrom').value;
                if (document.getElementById('hidPeril_Id') != null)
                    riskId = document.getElementById('hidPeril_Id').value;
                if (document.getElementById('hidLOCATION') != null) {
                var  loc = document.getElementById('hidLOCATION').value;
                }
                    

                //Added By pradeep Kushwaha 13-05-2010
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');

                if (pagefrom == "QAPP") {// changed by sonal to implemt this in quick app

                    Url = parent.document.getElementById('hidFrameUrl').value + riskId + "&";
                    DrawTab(1, top.frames[1], tabtitles[0], Url);
                }

                Url = "/Cms/Policies/aspx/ProtectiveDevicesInfo.aspx?CalledFrom=PERIL&pageTitle=" + tabtitles[0] + "&RISK_ID=" + riskId + "&LOCATION_ID=" + loc + "&";
                DrawTab(2, top.frames[1], tabtitles[1], Url, '453_3');

                Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?CalledFrom=PERIL&pageTitle=" + tabtitles[1] + "&RISK_ID=" + riskId + "&";
                DrawTab(3, top.frames[1], tabtitles[2], Url, '453_1');

                Url = "/Cms/Policies/aspx/AddpolicyCoverages.aspx?CalledFrom=PERIL&pageTitle=" + tabtitles[2] + "&RISK_ID=" + riskId + "&";
                DrawTab(4, top.frames[1], tabtitles[3], Url, '453_2');

               
                
                //Added end 
            }
            else {
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
                
            }
            return false;
        }
        function ResetTheForm() {
            document.POL_PERILS.reset();
        }
        function ShowAlertMessageWhileDelete(isDelete) {

            if (isDelete) {

                $('#hidConfirmValue').val(vbMsg($('#hidDeletemsg').val())); 

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
        $(document).ready(function () {

            $("#txtBONUS").blur();
            $("#txtCLAIM_RATIO").blur();
            if ($("#hidCalledFrom").val() == "ERISK") {
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
                    var result = AddNamedPerils.AjaxGetOccupied(_rubrica[1].toString());
                    fillDTCombo(result.value, document.getElementById('cmbOCCUPANCY'), 'OCCUPIED_ID', 'OCCUPIED_AS', 0);
                }
            });

            $("#cmbOCCUPANCY").change(function () {
                if ($("#cmbOCCUPANCY option:selected").val() != '')
                    $("#hidOCCUPIED").val($("#cmbOCCUPANCY option:selected").val());
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
<body  oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload='ApplyColor();setTab();'>
    <form id="POL_PERILS" runat="server" method="post">
    <table cellspacing="2" cellpadding="2" width="100%" border="0">
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
        <td class="pageHeader" colspan="3"><asp:Label ID="CapMessages" runat="server"></asp:Label>
           <%-- Please note that all fields marked with * are mandatory --%>
        </td>
        </tr>
        <tr>
            <td width="33%" class="midcolora" colspan="3">
                <asp:Label ID="capLOCATION" runat="server"></asp:Label><span class="mandatory" id="spn_mandatory">*</span></br>
                <asp:DropDownList runat="server" ID="cmbLOCATION" Width="100%" Height="17px">
                </asp:DropDownList><cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server"  OnClientClick="javascript:return CheckSlectedValue()"
                                onclick="btnSelect_Click" CausesValidation="false" ></cmsb:cmsbutton>
										    <asp:requiredfieldvalidator id="rfvLOCATION" 
                    runat="server" Display="Dynamic" ControlToValidate="cmbLOCATION"></asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label ID="capVR" runat="server" Text="VALUE AT RISK"></asp:Label></br>
                 <%-- changed by praveer itrack no 1512/TFS#240--%>
                <asp:TextBox ID="txtVR" CssClass="INPUTCURRENCY" runat="server" MaxLength="12" onblur="this.value=formatAmount(this.value)"  ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_VR" runat="server" ControlToValidate="txtVR" Display="Dynamic"  ></asp:RegularExpressionValidator>
                 <%-- Added by Pradeep itrack no 1512/TFS#240--%>
                <asp:CustomValidator ID="csvVR" runat="server" ControlToValidate="txtVR" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
               </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capBUILDING" Text="BUILDING VALUE" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtBUILDING" CssClass="INPUTCURRENCY"  onblur="this.value=formatAmount(this.value)" MaxLength="15" ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_BUILDING" runat="server" 
                    ControlToValidate="txtBUILDING" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capCONTENTS_VALUE" runat="server" Text="CONTENTS VALUE"></asp:Label></br>
                <asp:TextBox ID="txtCONTENT_VALUE" runat="server" CssClass="INPUTCURRENCY"  onblur="this.value=formatAmount(this.value)" MaxLength="15" ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_CONTENTS_VALUE" runat="server" 
                    ControlToValidate="txtCONTENT_VALUE" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label ID="capRAW_MATERIAL_VALUE" runat="server">RAW MATERIAL VALUE</asp:Label></br>
                <asp:TextBox runat="server" ID="txtRAW_MATERIAL_VALUE"  onblur="this.value=formatAmount(this.value)" CssClass="INPUTCURRENCY" MaxLength="15" ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_RAW_MATERIAL_VALUE" runat="server" 
                    ControlToValidate="txtRAW_MATERIAL_VALUE" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capRAWVALUES" runat="server">CONTENTS/RAW VALUES</asp:Label>
                </br>
                <asp:CheckBox ID="chkRAWVALUES" value="0" runat="server" />
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capLMI" Text="LMI" runat="server"></asp:Label></br>
                 <%-- changed by praveer itrack no 1512/TFS#240--%>
                <asp:TextBox runat="server" ID="txtLMI" CssClass="INPUTCURRENCY"  onblur="this.value=formatAmount(this.value)" MaxLength="12" ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_LMI" runat="server" ControlToValidate="txtLMI" Display="Dynamic"></asp:RegularExpressionValidator>
                <%-- Added by Pradeep itrack no 1512/TFS#240--%>
                <asp:CustomValidator ID="csvLMI" runat="server" ControlToValidate="txtLMI" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label ID="capMRI" runat="server" Text="MRI"></asp:Label>
                </br>
                <asp:TextBox ID="txtMRI" runat="server" CssClass="INPUTCURRENCY"   onblur="this.value=formatAmount(this.value)" MaxLength="15" ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_MRI" runat="server" 
                    ControlToValidate="txtMRI" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td width="33%" class="midcolora">
               <asp:Label ID="capLOSS" runat="server" Text="POSSIBLE MAX LOSS"></asp:Label>
                </br>
                <asp:TextBox ID="txtLOSS" runat="server" CssClass="INPUTCURRENCY"   onblur="this.value=formatAmount(this.value)" MaxLength="15" ></asp:TextBox></br>
                <asp:RegularExpressionValidator ID="rev_LOSS" runat="server" 
                    ControlToValidate="txtLOSS" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td style="width: 25%;" class="midcolora">
               <asp:Label ID="capPARKING_SPACES" runat="server" Text="#PARKING SPACES"></asp:Label></br>
                <asp:TextBox ID="txtPARKING_SPACES" runat="server" MaxLength="20" ></asp:TextBox></br>
                <%--<asp:RegularExpressionValidator ID="rev_PARKING_SPACES" runat="server" 
                    ControlToValidate="txtPARKING_SPACES" Display="Dynamic"></asp:RegularExpressionValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="33%">
              
                <asp:Label ID="capTYPE" runat="server" Text="#TYPE"></asp:Label>
                <br />
                <asp:TextBox ID="txtTYPE" runat="server" MaxLength="9"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="rev_TYPE" runat="server" 
                    ControlToValidate="txtTYPE" Display="Dynamic"></asp:RegularExpressionValidator>
              
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capMULTIPLE_DEDUCTIBLE" Text="MULTIPLE DEDUCTIBLE" runat="server"></asp:Label></br>
                <asp:DropDownList ID="cmbMULTIPLE_DEDUCTIBLE" runat="server" Width="128px" >
                </asp:DropDownList>
           
            </td>
            <td class="midcolora" Width="33%" >
	  <asp:Label ID="capCLAIM_RATIO" runat="server">Claim Ratio</asp:Label>
                 <br />
        <asp:TextBox ID="txtCLAIM_RATIO" runat="server" CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatRate(this.value,2)"></asp:TextBox>
        <BR />
       <asp:RegularExpressionValidator runat="server"  ID="revCLAIM_RATIO"  ErrorMessage="" ControlToValidate="txtCLAIM_RATIO" Display="Dynamic"></asp:RegularExpressionValidator>
       <asp:CustomValidator ID="csvCLAIM_RATIO" runat="server" ControlToValidate="txtCLAIM_RATIO" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
      </td>
          
        </tr>
        <tr>
            <td class="midcolora" width="33%">
               
                <asp:Label ID="capCATEGORY" runat="server" Text="RUBRICA"></asp:Label>
                <br />
                <asp:TextBox ID="txtCATEGORY" runat="server" MaxLength="6"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="rev_CATEGORY" runat="server" 
                    ControlToValidate="txtCATEGORY" Display="Dynamic"></asp:RegularExpressionValidator>
               
            </td>
            <td class="midcolora" Width="33%" >
	        <asp:Label ID="capBONUS" runat="server">Bonus</asp:Label>
                 <br />
             <asp:TextBox ID="txtBONUS" runat="server"  CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatRate(this.value,2)"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator runat="server"  ID="revBONUS"  ErrorMessage="" ControlToValidate="txtBONUS" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="csvBONUS" runat="server" ControlToValidate="txtBONUS" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
            
      </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capCONSTRUCTION" Text="CONSTRUCTION" runat="server"></asp:Label></br>
                <asp:DropDownList ID="cmbCONSTRUCTION" runat="server" Width="128px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td class="midcolora" width="33%">
             
                <asp:Label ID="capASSIST24" runat="server" Text="24 HRS ASSISTANCE"></asp:Label>
                <br />
                <asp:CheckBox ID="chkASSIST24" runat="server" />
             
            </td>
            
             <td class="midcolora" Width="33%" >
	  <asp:Label ID="capLOCATION_NUMBER" runat="server"></asp:Label><span class="mandatory" id="Span1">*</span>
                 <br />
        <asp:TextBox ID="txtLOCATION_NUMBER" runat="server"  MaxLength="9"> </asp:TextBox>
        <BR />
        <asp:requiredfieldvalidator id="rfvLOCATION_NUMBER" runat="server" ControlToValidate="txtLOCATION_NUMBER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
         <asp:RegularExpressionValidator ID="revLOCATION_NUMBER" runat="server" 
                    ControlToValidate="txtLOCATION_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
      
      </td>
      <td class="midcolora" Width="33%" >
	  <asp:Label ID="capITEM_NUMBER" runat="server"></asp:Label><span class="mandatory" id="Span2">*</span>
                 <br />
        <asp:TextBox ID="txtITEM_NUMBER" runat="server"  MaxLength="9"> </asp:TextBox>
        <BR />
        <asp:requiredfieldvalidator id="rfvITEM_NUMBER" runat="server" ControlToValidate="txtITEM_NUMBER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
         <asp:RegularExpressionValidator ID="revITEM_NUMBER" runat="server" 
                    ControlToValidate="txtITEM_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
       <%-- <asp:CompareValidator ID="cmpvITEM_NUMBER" runat="server" ControlToCompare="txtLOCATION_NUMBER" ControlToValidate="txtITEM_NUMBER"  Operator="NotEqual"   ErrorMessage=" " Display="Dynamic" ></asp:CompareValidator>--%>
      
      </td>
        
        </tr>
        <tr>
         <td width="100%" class="midcolora" colspan="3">
                <asp:Label ID="capACTIVITY_TYPE" Text="ACTIVITY TYPE" runat="server"></asp:Label></br>
                <asp:DropDownList ID="cmbACTIVITY_TYPE" runat="server" Width="100%" >
                </asp:DropDownList></br>
										  
        </td>
        </tr>
        <tr>
       <td width="100%" class="midcolora" colspan="3">
                <asp:Label ID="capOCCUPANCY" runat="server" Text="OCCUPIED AS"></asp:Label></br>
                <asp:DropDownList ID="cmbOCCUPANCY" runat="server" Width="100%">
                </asp:DropDownList></br>
										    
            </td>
        </tr>
        <tr>
       <td width="100%" class="midcolora" colspan="2">
         <asp:Label ID="capACTUAL_INSURED_OBJECT" runat="server" ></asp:Label><br />
               <asp:TextBox ID="txtACTUAL_INSURED_OBJECT"   Width="400px" MaxLength="250" Height="70px"  TextMode="MultiLine" Rows="3"  runat="server" onkeypress="MaxLength(this,250)" onpaste="MaxLength(this,250)" 
                     ></asp:TextBox></td>

       <td class="midcolora" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
        </tr>
       <tr>
            
            <td width="33%" class="midcolora" colspan="3" >
               <asp:Label ID="capREMARKS" runat="server" Text="REMARKS"></asp:Label><br />
                
                 <asp:TextBox ID="txtREMARKS" runat="server" TextMode="MultiLine" Rows="3" 
                    Width="400px" Height="70px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox><br />
            
			</td>
          
        </tr>
        <tr>
            <td class="midcolora" width="33%">
                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" 
                    CausesValidation="False"></cmsb:cmsbutton>
				<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" 
                    Text="Activate/Deactivate" CausesValidation="false" 
                    onclick="btnActivateDeactivate_Click" />
            </td>
            <td class="midcolora" >
            </td>
            <td class="midcolorr" >
             <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false" onclick="btnDelete_Click"  ></cmsb:cmsbutton>
               <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" onclick="btnSave_Click"></cmsb:cmsbutton>
              
               <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
               <input id="hidPeril_Id" type="hidden" value="" name="hidPeril_Id" runat="server"/>
               <input id="hidTAB_TITLES" type="hidden" value="0" name="hidTAB_TITLES" runat="server"/>    
                <input id="hidDeletemsg" type="hidden" name="hidDeletemsg" runat="server" />
                  <input id="hidConfirmValue" type="hidden" name="hidConfirmValue" runat="server" />
                  <input id="hidLOCATION" type="hidden" name="hidLOCATION" runat="server" />
                  <input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server" />
                <%--Added for itrack-1152/TFS # 2598--%>
                <input id="hidACTIVITY_TYPE" type="hidden" value="0" name="hidACTIVITY_TYPE" runat="server"/>  
                <input id="hidOCCUPIED" type="hidden" value="0" name="hidOCCUPIED" runat="server"/>  
                <input id="hidRUBRICA_ID" type="hidden" value="0" name="hidRUBRICA_ID" runat="server"/>  
                <%--Added till here --%>
		</td>
        </tr>
      
          </table></td></tr>
    </table>
    </form>
    	<script type="text/javascript" >
    	    
    	    if (document.getElementById('hidFormSaved').value == "1") {

    	        var pagefrom = '<%=CALLEDFROM %>'
    	        if (pagefrom == "QAPP") {
    	            //parent.location.href = parent.location.href;
    	            //parent.document.getElementById('cmbRisk').selectedIndex = 2;
    	            //alert(parent.document.getElementById('cmbRisk').options[1].value)
    	            //("parent.RiskChange()",100);
    	            parent.BindRisk();
    	            for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {

    	                if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidPeril_Id').value)
    	                    parent.document.getElementById('cmbRisk').options[i].selected = true;

    	            }
    	        }
    	        else {
    	         try {
    	            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPeril_Id').value);
    	            }
    	            catch (err) {

    	            }
    	        }
    	    }
		</script>
</body>
</html>

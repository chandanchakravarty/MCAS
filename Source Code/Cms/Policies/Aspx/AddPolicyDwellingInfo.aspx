<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPolicyDwellingInfo.aspx.cs" Inherits="Cms.Policies.Aspx.AddPolicyDwellingInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>POL_PRODUCT_LOCATION</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		
		<script language="javascript" type="text/javascript">
		    $(document).ready(function() {
		        $("#txtBONUS").blur();
		        $("#txtCLAIM_RATIO").blur();
		        if ($("#hidCALLED_FROM").val() == "ROBBERY" || $("#hidCALLED_FROM").val() == "GenCvlLib") {
		            $("#trInsured").show();
		             
		        }
		        else {
		            $("#trInsured").hide();
		             
		        }

		    });
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

		                if (CalledFrom != '' && CalledFrom == "DWELLING" || CalledFrom != '' && CalledFrom == "ROBBERY" || CalledFrom == "GenCvlLib") {
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
		     function HideShowTDBasedOnCalledFrom() {

		         if (document.getElementById('hidCALLED_FROM') != null) {
		             var CalledFrom = document.getElementById('hidCALLED_FROM').value;
		             if (CalledFrom != '' && CalledFrom == "ROBBERY") {
		                 $('#divRoberry').hide();
		                 $('#divAssist').hide();
		                 $('#divMultiD').hide();
		                 $('#div1').hide();
		                 $('#div2').hide();
		                 $('#div3').hide();
		             }
		             else if (CalledFrom != '' && CalledFrom == "DWELLING") { $('#divMultiD').hide(); }
		             else if (CalledFrom != '' && CalledFrom == "GenCvlLib") {
		                $('#divRoberry').hide();
		                $('#divAssist').hide();
		                $('#div1').hide();
		                $('#div2').hide();
		                $('#div3').hide();
		                
		             }
		             
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
        </script>
        <script language="vbscript" type="text/vbscript">

            Function vbMsg(isTxt)
                vbMsg = MsgBox(isTxt,4)
            End Function
        		         
        </script>

	 
</head>
<body  leftMargin="0"  rightMargin="0" MS_POSITIONING="GridLayout"  onload="Init();setTab();HideShowTDBasedOnCalledFrom();">
    <form id="POL_PRODUCT_LOCATION"  runat="server" name="POL_PRODUCT_LOCATION" onsubmit="" method="post">
        <table id="Table1" cellpadding="0" width="100%" border="0" >
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
		    <asp:DropDownList runat="server" ID="cmbLOCATION" width="100%" Height="17px">
		    <asp:ListItem Text="" Value=""></asp:ListItem>
		    </asp:DropDownList><cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server" 
                                onclick="btnSelect_Click" CausesValidation="false" ></cmsb:cmsbutton><br />
		    <asp:RequiredFieldValidator runat="server"  ID="rfvLOCATION"  ErrorMessage="" ControlToValidate="cmbLOCATION" Display="Dynamic"></asp:RequiredFieldValidator>
		    </td>
	    </tr>
	    
	    <tr>
		    <td class="midcolora" Width="33%" >
		    <asp:Label runat="server" id="capVALUE_AT_RISK"  >Value at Risk</asp:Label>
		    <br />
		    <asp:TextBox runat="server" ID="txtVALUE_AT_RISK"  CssClass="INPUTCURRENCY" 
                    MaxLength="12" onblur="this.value=formatAmount(this.value)" 
                    CausesValidation="false"></asp:TextBox>
		    <br />
		    <asp:RegularExpressionValidator runat="server" ID="revVALUE_AT_RISK" Display="Dynamic" ErrorMessage="" ControlToValidate="txtVALUE_AT_RISK"></asp:RegularExpressionValidator>
            <%-- Added by Pradeep itrack no 1512/TFS#240--%>
            <asp:CustomValidator ID="csvVALUE_AT_RISK" runat="server" ControlToValidate="txtVALUE_AT_RISK" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
		    </td>
		    <td class="midcolora" Width="34%" >
		    <asp:Label runat="server" id="capMAXIMUM_LIMIT" >Maximum Limit</asp:Label>
		    <br />
		    <asp:TextBox runat="server" ID="txtMAXIMUM_LIMIT" CssClass="INPUTCURRENCY" 
                    MaxLength="12" onblur="this.value=formatAmount(this.value)"></asp:TextBox>
		    <br />
   		    <asp:RegularExpressionValidator runat="server" ID="revMAXIMUM_LIMIT" Display="Dynamic" ErrorMessage="" ControlToValidate="txtMAXIMUM_LIMIT"></asp:RegularExpressionValidator>
              <%-- Added by Pradeep itrack no 1512/TFS#240--%>
            <asp:CustomValidator ID="csvMAXIMUM_LIMIT" runat="server" ControlToValidate="txtMAXIMUM_LIMIT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>

		    </td>	
		    <td class="midcolora" Width="33%" >
		    
		        <div id="divRoberry">
		            <asp:Label ID="capCONSTRUCTION" runat="server">Construction</asp:Label>
                    <br />
                    <asp:DropDownList ID="cmbCONSTRUCTION" runat="server">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </Div>
		        <div id="divMultiD">
		            <asp:Label runat="server" id="capMULTIPLE_DEDUCTIBLE" >Multiple Deductible</asp:Label>
		            <br />
		            <asp:DropDownList runat="server" ID="cmbMULTIPLE_DEDUCTIBLE" >
		            <asp:ListItem Text="" Value=""></asp:ListItem>
		            </asp:DropDownList>
		        </div>
		    
		    </td>	
	 </tr>
	  <tr>
	  <td class="midcolora" Width="33%" >
	  <div id="div1">
	  <asp:Label ID="capBONUS" runat="server">Bonus</asp:Label>
                 <br />
      <asp:TextBox ID="txtBONUS" runat="server"  CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatRate(this.value,2)"></asp:TextBox>
      <br />
      <asp:RegularExpressionValidator runat="server"  ID="revBONUS"  ErrorMessage="" ControlToValidate="txtBONUS" Display="Dynamic"></asp:RegularExpressionValidator>
      <asp:CustomValidator ID="csvBONUS" runat="server" ControlToValidate="txtBONUS" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
     </div>
     </td>
      <td class="midcolora" Width="33%" >
       <div id="div2">
	  <asp:Label ID="capCLAIM_RATIO" runat="server">Claim Ratio</asp:Label>
                 <br />
        <asp:TextBox ID="txtCLAIM_RATIO" runat="server" CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatRate(this.value,2)"></asp:TextBox>
        <BR />
       <asp:RegularExpressionValidator runat="server"  ID="revCLAIM_RATIO"  ErrorMessage="" ControlToValidate="txtCLAIM_RATIO" Display="Dynamic"></asp:RegularExpressionValidator>
       <asp:CustomValidator ID="csvCLAIM_RATIO" runat="server" ControlToValidate="txtCLAIM_RATIO" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
       
     </div>
      </td>
    <td class="midcolora" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
     </tr>
     
		     <tr>
		    <td class="midcolora" Width="33%" >
	  <asp:Label ID="capLOCATION_NUMBER" runat="server"></asp:Label><span class="mandatory" id="Span2">*</span>
                 <br />
        <asp:TextBox ID="txtLOCATION_NUMBER" runat="server"  MaxLength="9"> </asp:TextBox>
        <BR />
        <asp:requiredfieldvalidator id="rfvLOCATION_NUMBER" runat="server" ControlToValidate="txtLOCATION_NUMBER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
        <asp:RegularExpressionValidator ID="revLOCATION_NUMBER" runat="server" 
                    ControlToValidate="txtLOCATION_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
      
      </td>
      <td class="midcolora" Width="33%" colspan="2" >
	  <asp:Label ID="capITEM_NUMBER" runat="server"></asp:Label><span class="mandatory" id="Span1">*</span>
                 <br />
        <asp:TextBox ID="txtITEM_NUMBER" runat="server"  MaxLength="9"> </asp:TextBox>
        <BR />
         <asp:requiredfieldvalidator id="rfvITEM_NUMBER" runat="server" ControlToValidate="txtITEM_NUMBER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
         <asp:RegularExpressionValidator ID="revITEM_NUMBER" runat="server" 
                    ControlToValidate="txtITEM_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
        <%--<asp:CompareValidator ID="cmpvITEM_NUMBER" runat="server" ControlToCompare="txtLOCATION_NUMBER" 
        ControlToValidate="txtITEM_NUMBER"  Operator="NotEqual"   ErrorMessage=" " Display="Dynamic" ></asp:CompareValidator>--%>
      </td>
	   
     </tr >
     
     
     <tr id="trClassField" runat="server" >
		    <td class="midcolora" Width="33%" colspan="3" >
		         <asp:Label ID="capCLASS_FIELD" runat="server" Text="Class Filed:"></asp:Label><br />
                <asp:DropDownList ID="cmbCLASS_FIELD" runat="server" Width="191px" >
                </asp:DropDownList></td>	
		     </tr>
		     
     
     <tr id="trInsured" >
		    <td class="midcolora" Width="33%" colspan="3" >
		     <asp:Label ID="capACTUAL_INSURED_OBJECT" runat="server" ></asp:Label><br />
               <asp:TextBox ID="txtACTUAL_INSURED_OBJECT"   Width="400px" MaxLength="250" Height="50px"  TextMode="MultiLine" Rows="3"  runat="server" onkeypress="MaxLength(this,250)" onpaste="MaxLength(this,250)" 
                     ></asp:TextBox>

		       </td>	
		     </tr>
		     
	  <tr id="td2">
		    <td class="midcolora" Width="33%" >
		      <asp:Label ID="capREMARKS" runat="server">Remarks</asp:Label>
                 <br />
                 <asp:TextBox ID="txtREMARKS" runat="server" Height="70px" Rows="3" 
                     TextMode="MultiLine" Width="400px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox>
		    </td>	
		    
		     <td class="midcolora" Width="33%" >
		     <div id="divAssist">
		       <asp:Label runat="server" id="capASSIST24" >24 Hour Assistance</asp:Label>
		        <br />
		       <asp:CheckBox runat="server" ID="chkASSIST24"  AccessKey="c"/>
            </div>
		    </td>	
		     <td class="midcolora" Width="33%" >
		   
		    </td>	
	    </tr>
	  
	    <tr>
	        <td class="midcolora" Width="33%" >
	            <cmsb:CmsButton  runat="server" ID="btnReset" Text="Reset"  CssClass="clsButton"  CausesValidation="false"/>
                <cmsb:CmsButton runat="server" ID="btnActivateDeactivate" Text="Activate/Deactivate"  CssClass="clsButton" CausesValidation="false" onclick="btnActivateDeactivate_Click" />


	        </td> 
	         <td class="midcolora" Width="33%" >
		   

		    </td>	
	        <td class="midcolorr" Width="33%" > 
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
                <cmsb:CmsButton  runat="server" ID="btnDelete" Text="Delete"  CssClass="clsButton" CausesValidation="false" onclick="btnDelete_Click" />
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
          try
          {

              RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPRODUCT_RISK_ID').value);
              }
              catch(err)
              {
              
              }
          }
        }
		</script>
</body>
</html>

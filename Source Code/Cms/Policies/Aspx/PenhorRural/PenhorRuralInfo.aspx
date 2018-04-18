<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenhorRuralInfo.aspx.cs" Inherits="Cms.Policies.Aspx.PenhorRural.PenhorRuralInfo" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>POL_PENHOR_RURAL_INFO</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
     <script src="/cms/cmsweb/scripts/xmldom.js"></script>
	 <script src="/cms/cmsweb/scripts/common.js"></script>
	 <script src="/cms/cmsweb/scripts/form.js"></script>
     <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	 <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
	 <script type="text/javascript" language="javascript">
	  function ResetTheForm() {
		        document.POL_PENHOR_RURAL_INFO.reset();
		    }
		    function initPage() {
		        ApplyColor();
		    }
		    function setTab() {
		       
		        if (document.getElementById('hidPENHOR_RURAL_ID') != null && document.getElementById('hidPENHOR_RURAL_ID').value != '' && document.getElementById('hidPENHOR_RURAL_ID').value != 'NEW') {
		            if (document.getElementById('hidCALLED_FROM') != null) {
		                 
		                var CalledFrom = document.getElementById('hidCALLED_FROM').value;
		                var RISK_ID = document.getElementById('hidPENHOR_RURAL_ID').value;
		                var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
		                var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
		                var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
		                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
		                var tabtitles = TAB_TITLES.split(',');

		                if (CalledFrom != '' && CalledFrom == "RLLE") {
		                    if (RISK_ID != "NEW") {
		                    //  for itrack 1192 by praveer
		                        Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?RISK_ID=" + RISK_ID + "&";
		                        DrawTab(2, top.frames[1], tabtitles[0], Url);
		                        Url = "/Cms/Policies/aspx/AddPolicyCoverages.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&";
		                        DrawTab(3, top.frames[1], tabtitles[1], Url);

		                    }
		                }
		                if (CalledFrom != '' && CalledFrom == "RETSURTY") {
		                    if (RISK_ID != "NEW") {

		                        Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?RISK_ID=" + RISK_ID + "&";
		                        DrawTab(2, top.frames[1], tabtitles[0], Url);
		                        
		                        Url = "/Cms/Policies/aspx/AddPolicyCoverages.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&";
		                        DrawTab(3, top.frames[1], tabtitles[1], Url);

		                    }
		                }

		            }
		        } else {
		            RemoveTab(3, top.frames[1]);
		            RemoveTab(2, top.frames[1]);
		        }
		    }
		    function ValidateChkList(source, arguments) {
		        if ($("#chkFESR_COVERAGE").attr("checked") == true) {
		            arguments.IsValid = true;
		        }
		        else {
		            arguments.IsValid = false;
		        }
		   }
		     
		</script>
     
     <script type="text/javascript" >

         $(document).ready(function() {

             if ($('#hidCALLED_FROM').val() == "RETSURTY") {

                 $('#tdFESR_COVERAGE').hide();
                 $('#tdMODE').hide();
                 $('#trPROPERTY').hide();
                 $('#trCITY').hide();
                 $('#tdSUBSIDY_STATE').hide();
                 $('#tdITEM_NUMBER').attr('colspan',3); 
                
                 ValidatorEnable(rfvMODE, false);
                 ValidatorEnable(rfvPROPERTY, false);
                 ValidatorEnable(rfvCULTIVATION, false);
                 ValidatorEnable(rfvSTATE_ID, false);
                 ValidatorEnable(rfvCITY, false);
                 ValidatorEnable(rfvINSURED_AREA, false);
                 ValidatorEnable(cvFESR_COVERAGE, false);

                 ValidatorEnable(rfvSUBSIDY_PREMIUM, false);
                 ValidatorEnable(rfvSUBSIDY_STATE, false);


             }
             else if ($('#hidCALLED_FROM').val() == "RLLE") {
                 $('#trREMARKS').hide();
                 ValidatorEnable(rfvREMARKS, false);
             }


         });
        
     </script>
</head>
<body  leftMargin="0" topMargin="0" onload="initPage();setTab();">
    <form id="POL_PENHOR_RURAL_INFO" runat="server" method="post">
  
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
       
        <tr>
            <td  width="33%" class="midcolora" id="tdITEM_NUMBER" >
              <asp:Label ID="capITEM_NUMBER" runat="server" Text="Item #:"></asp:Label><span class="mandatory">*</span><br />
		         <asp:TextBox ID="txtITEM_NUMBER" runat="server" MaxLength="4"></asp:TextBox><br />
		         <asp:RequiredFieldValidator ID="rfvITEM_NUMBER" runat="server" ControlToValidate="txtITEM_NUMBER" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revITEM_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtITEM_NUMBER"></asp:regularexpressionvalidator>
            </td>
            <td id="tdFESR_COVERAGE" width="33%" class="midcolora">
             <asp:Label ID="capFESR_COVERAGE" runat="server" Text="FESR Coverage:"></asp:Label><span id="spnFESR_COVERAGE"  runat="server" class="mandatory">*</span><br />
                <asp:CheckBox ID="chkFESR_COVERAGE" runat="server" /><br />
                <asp:CustomValidator ID="cvFESR_COVERAGE" runat="server" ClientValidationFunction="ValidateChkList" ></asp:CustomValidator>
            </td>
            <td id="tdMODE" width="33%"class="midcolora">
                 <asp:Label ID="capMODE" runat="server" Text="Mode:"></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbMODE" runat="server"></asp:DropDownList></br>
                 <asp:RequiredFieldValidator ID="rfvMODE" runat="server" ControlToValidate="cmbMODE" Display="Dynamic"></asp:RequiredFieldValidator>
             </td>
        </tr>
        <tr id="trPROPERTY">
            <td  width="33%" class="midcolora">
            <asp:Label ID="capPROPERTY" runat="server" Text="Property:"></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbPROPERTY" Width="100%" runat="server"></asp:DropDownList></br>
                 <asp:RequiredFieldValidator ID="rfvPROPERTY" runat="server" ControlToValidate="cmbPROPERTY" Display="Dynamic"></asp:RequiredFieldValidator>
                 
            </td>
            <td  width="33%" class="midcolora">
             <asp:Label ID="capCULTIVATION" runat="server" Text="Cultivation:"></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbCULTIVATION" Width="100%" runat="server"></asp:DropDownList></br>
                 <asp:RequiredFieldValidator ID="rfvCULTIVATION" runat="server" ControlToValidate="cmbCULTIVATION" Display="Dynamic"></asp:RequiredFieldValidator>
                 
              </td>
            <td width="33%" class="midcolora">
             <asp:Label ID="capSTATE_ID" runat="server" Text="State:"></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbSTATE_ID" runat="server"></asp:DropDownList></br>
                <asp:RequiredFieldValidator ID="rfvSTATE_ID" runat="server" ControlToValidate="cmbSTATE_ID" Display="Dynamic"></asp:RequiredFieldValidator>
                 
            </td>
        </tr>
        <tr id="trCITY"  >
            <td  width="33%" class="midcolora">
            <asp:Label ID="capCITY" runat="server" Text="City:"></asp:Label><span class="mandatory">*</span><br />
		         <asp:TextBox ID="txtCITY" runat="server" Width="100%" MaxLength="250" ></asp:TextBox><br />
		         <asp:RequiredFieldValidator ID="rfvCITY" runat="server" ControlToValidate="txtCITY" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revCITY" runat="server" Display="Dynamic" ControlToValidate="txtCITY"></asp:regularexpressionvalidator>
            </td>
            <td  class="midcolora">
             <asp:Label ID="capINSURED_AREA" runat="server" Text="Insured Area:"></asp:Label><span class="mandatory">*</span><br />
		         <asp:TextBox ID="txtINSURED_AREA" runat="server" MaxLength="9"></asp:TextBox><br />
		         <asp:RequiredFieldValidator ID="rfvINSURED_AREA" runat="server" ControlToValidate="txtINSURED_AREA" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:regularexpressionvalidator id="revINSURED_AREA" runat="server" Display="Dynamic" ControlToValidate="txtINSURED_AREA"></asp:regularexpressionvalidator>
                 
            </td>
                <td width="33%" class="midcolora">
                 <asp:Label ID="capSUBSIDY_PREMIUM" runat="server" Text="Subsidy Premium:"></asp:Label><span class="mandatory">*</span><br />
		         <asp:TextBox ID="txtSUBSIDY_PREMIUM" CssClass="INPUTCURRENCY" runat="server" 
                        MaxLength="15" onblur="this.value=formatAmount(this.value)" Width="161px"></asp:TextBox><br />
		         <asp:RequiredFieldValidator ID="rfvSUBSIDY_PREMIUM" runat="server" ControlToValidate="txtSUBSIDY_PREMIUM" Display="Dynamic"></asp:RequiredFieldValidator>
		         <asp:regularexpressionvalidator id="revSUBSIDY_PREMIUM" runat="server" Display="Dynamic" ControlToValidate="txtSUBSIDY_PREMIUM"></asp:regularexpressionvalidator>
            </td>
        </tr>
        <tr id="tdSUBSIDY_STATE" >
            <td  width="33%" class="midcolora">
            <asp:Label ID="capSUBSIDY_STATE" runat="server" Text="Subsidy State:"></asp:Label><span class="mandatory">*</span><br />
            <asp:DropDownList ID="cmbSUBSIDY_STATE" runat="server"></asp:DropDownList><br />
            <asp:RequiredFieldValidator ID="rfvSUBSIDY_STATE" runat="server" ControlToValidate="cmbSUBSIDY_STATE" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td  class="midcolora">&nbsp;</td>
            <td width="33%" class="midcolora">&nbsp;</td>
        </tr>
          <tr>
            
         <td class="midcolora" Width="32%" colspan="3">
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
        </tr>
        <tr id="trREMARKS" >
            <td  width="33%" class="midcolora" colspan="3" style="width: 66%">
            <asp:Label ID="capREMARKS" runat="server" Text="Remarks:" ></asp:Label><span class="mandatory">*</span></br>
		    <asp:TextBox ID="txtREMARKS" runat="server" Width="80%" Height="200px" TextMode="MultiLine" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)"></asp:TextBox></br>
		    <asp:RequiredFieldValidator ID="rfvREMARKS" runat="server" ControlToValidate="txtREMARKS" Display="Dynamic"></asp:RequiredFieldValidator>
		         </td>
        </tr>
        <tr>
            <td width="33%"  class="midcolora" >
                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" text="Reset" CausesValidation="False" ></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" causesvalidation="false" text="Activate/Deactivate" onclick="btnActivateDeactivate_Click"></cmsb:cmsbutton>
            </td>
            <td   class="midcolora">
            </td>
            <td width="33%" class="midcolorr" >
                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" text="Delete" causesvalidation="false" onclick="btnDelete_Click"></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server"   text="Save" onclick="btnSave_Click" ></cmsb:cmsbutton>
            </td>
        </tr>
    </table>
     </tr>
	<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
    <input id="hidPENHOR_RURAL_ID" type="hidden" value="" name="hidPENHOR_RURAL_ID" runat="server"/>
    <input id="hidMsg" type="hidden" value="0" name="hidMsg" runat="server"/>    
    <input id="hidTAB_TITLES" type="hidden" value="0" name="hidTAB_TITLES" runat="server"/>         
    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>    
    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>  
    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>  
	<input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {
                try {

                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPENHOR_RURAL_ID').value);
                }
                catch (err) {

                }
           

        }
		</script>
</body>
</html>

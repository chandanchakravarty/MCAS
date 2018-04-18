<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRemunerationDetails.aspx.cs" Inherits="Cms.Policies.Aspx.AddRemunerationDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Remunaration</title>
     <link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
	<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
		<script language="javascript">
		    var PobjectID = new Array();
		    var glCommissionType;
		    
		    
		    function ResetTheForm() {
		        document.POL_REMUNERATION.reset();
		    }

		    function init() {
		        ChangeColor();
		        ApplyColor();
		        FillAgency();
		      
		        if (document.getElementById('cmbCOMMISSION_TYPE').value == "") {
		            document.getElementById("cmbBROKER_ID_2").style.display = 'none';
		            document.getElementById("rfv2BROKER_ID").style.display = 'none';
		            document.getElementById('rfv2BROKER_ID').setAttribute('enabled', false);
		            document.getElementById("cmbBROKER_ID").style.display = 'inline';
		        }
		        if (document.getElementById('btnDelete')!= null) {
		            if (document.getElementById('hidRemunerationId').value == "NEW")
		                document.getElementById('btnDelete').setAttribute("disabled", true);
		            else
		                document.getElementById('btnDelete').setAttribute("disabled", false);
		        }
		    }

		    function FormatAmountForSum(num) {
		        num = ReplaceAll(num, sGroupSep, '');
		        num = ReplaceAll(num, sDecimalSep, '.');   
		        
		        
		        return num;
		    }
		    function Validate(objSource, objArgs) {
		       
                var comm = parseFloat(FormatAmountForSum(document.getElementById(objSource.controltovalidate).value));
		       
		        if (comm < 0 || comm > 100) {
		            document.getElementById(objSource.controltovalidate).select();
		            objArgs.IsValid = false;
		        }
		        else
		            objArgs.IsValid = true;
		    } 	//validate commission %

		    function FillAgency() {		     
		        GlobalError = true;
		        if (document.getElementById('cmbCOMMISSION_TYPE').selectedIndex != -1 && document.getElementById('cmbCOMMISSION_TYPE').selectedIndex != 0) {
		            var value = document.getElementById('cmbCOMMISSION_TYPE').options[document.getElementById('cmbCOMMISSION_TYPE').selectedIndex].value;
		            var type = "";	        	           

		            //Commission
		            if (value == "43")		         
		            {
		             document.getElementById("cmbBROKER_ID_2").style.display = 'none';
		             document.getElementById("cmbBROKER_ID").style.display = 'inline';
		           document.getElementById("rfv2BROKER_ID").style.display = 'none';
		           document.getElementById('rfv2BROKER_ID').setAttribute('enabled', false);
		           document.getElementById("rfvBROKER_ID").setAttribute('enabled', true);
		           if ($("#cmbLEADER").attr("disabled") == true) {
		               $("#cmbLEADER").removeAttr("disabled");
		           } 
		              
		               
		            }
		            //Enrollment fee and Pro-Labore		         

		            if (value == "44" || value == "45") {
		                document.getElementById("cmbBROKER_ID").setAttribute('visible', false);
		                document.getElementById("cmbBROKER_ID").style.display = 'none';
		                document.getElementById("rfvBROKER_ID").style.display = 'none';
		                document.getElementById("rfvBROKER_ID").setAttribute('enabled', false);
		                document.getElementById("cmbBROKER_ID_2").style.display = 'inline';
		                document.getElementById('rfv2BROKER_ID').setAttribute('enabled', true);
		                document.getElementById('cmbLEADER').options[1].selected = true;
		               // document.getElementById('cmbLEADER').setAttribute('enabled', false);		              
		                if($("#cmbLEADER").attr("disabled") == false)
		                { $("#cmbLEADER").attr("disabled", "disabled"); }
		                document.getElementById("rfvLEADER").style.display = 'none';
		                document.getElementById("rfvLEADER").setAttribute('enabled', false);
		            }
		            

		        }
		        else
		            return false;            

		      
		       
		        }
		    

		 
		    
		</script> 
</head>
<body  leftMargin="0" topMargin="0" onload="init();" >
    <form id="POL_REMURATION" runat="server" name="POL_REMUNERATION" onsubmit="" method="post">
    <div>
      <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tableWidthHeader">
      
      <tr>
                        <td class="pageHeader" colspan="4">
                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
      <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
       <tr>
         <td class="midcolorc" align="right" colSpan="4">
            <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
         </td>
       </tr>
       <tr id="trBody" runat="server">
       
       <td>
     <table width="100%"  class="tableWidthHeader">
     
   
      
    <tr id="trCoapplicantcommission" runat="server">
   
    
      
       <td id ="tdcommission" class="midcolora" runat="server" >
            <asp:Label ID="capPOLICY_LEVEL_COMMISSION" runat="server"></asp:Label>
            <br />
            <asp:TextBox ID="txtPOLICY_LEVEL_COMMISSION" CssClass="INPUTCURRENCY" runat="server"
                ReadOnly="true"></asp:TextBox>
            
        </td>     
        <td id="tdCoapplicant"  runat="server"  class="midcolora" width="50%" >
    <asp:Label ID="capCO_APPLICANT_ID" runat="server" Text=""></asp:Label><span class="mandatory">*</span>   <br />
     <asp:DropDownList ID="cmbCO_APPLICANT_ID" runat="server">
        </asp:DropDownList>        
         <br />
    <asp:RequiredFieldValidator  ID="rfvCO_APPLICANT_ID" runat="server" ErrorMessage="" Display="Dynamic"
        ControlToValidate="cmbCO_APPLICANT_ID"></asp:RequiredFieldValidator>
        </td> 
    
                                       
    </tr>
    
    <tr>
     
    <td class="midcolora" width="50%">
    <asp:Label ID="capCOMMISSION_TYPE" runat="server" Text="" ></asp:Label><span class="mandatory">*</span>  <br />
       <asp:DropDownList ID="cmbCOMMISSION_TYPE" runat="server" Display="Dynamic"> </asp:DropDownList>
     
       <br />
    <asp:RequiredFieldValidator  ID="rfvCOMMISSION_TYPE" runat="server" ErrorMessage="" Display="Dynamic"
        ControlToValidate="cmbCOMMISSION_TYPE"></asp:RequiredFieldValidator>
          </td> 
    
    <td class="midcolora" width="50%">
     <asp:Label ID="capBROKER_ID" runat="server" Text="" ></asp:Label> <span class="mandatory">*</span>   <br />
    <asp:DropDownList ID="cmbBROKER_ID" runat="server" onfocus="SelectComboIndex('cmbBROKER_ID');" >
        </asp:DropDownList>   
     <asp:DropDownList ID="cmbBROKER_ID_2" runat="server" onfocus="SelectComboIndex('cmbBROKER_ID_2');" >
        </asp:DropDownList>   
         <br />
    <asp:RequiredFieldValidator  ID="rfvBROKER_ID" runat="server" ErrorMessage="" Display="Dynamic"
        ControlToValidate="cmbBROKER_ID"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator  ID="rfv2BROKER_ID" runat="server" ErrorMessage="" Display="Dynamic"
        ControlToValidate="cmbBROKER_ID_2"></asp:RequiredFieldValidator>
        
        </td>  
        
  
       
    </tr>
    <tr>
    
  
        
   
    <td class="midcolora" width="50%">
     <asp:Label ID="capBRANCH" runat="server" Text="" ></asp:Label>   <br />
         <asp:TextBox ID="txtBRANCH" MaxLength="5" runat="server"></asp:TextBox><br />
         <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
        ID="revBRANCH" ControlToValidate="txtBRANCH"></asp:RegularExpressionValidator>  
       </td>  
       
       
    <td class="midcolora" width="50%">    
     <asp:Label ID="capCOMMISSION_PERCENT" runat="server" Text=""></asp:Label><span class="mandatory">*</span>   <br />
      <asp:TextBox runat="server" ID="txtCOMMISSION_PERCENT" CssClass="INPUTCURRENCY" MaxLength="10"
       onblur="this.value=formatAmount(this.value,2)"  onChange = "this.value=formatAmount(this.value,2)" CausesValidation="true"  AutoCompleteType="Disabled"
        ></asp:TextBox><br />
    <asp:RequiredFieldValidator runat="server" Enabled="true" ErrorMessage="" Display="Dynamic"
        ID="rfvCOMMISSION_PERCENT" ControlToValidate="txtCOMMISSION_PERCENT"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
        ID="revCOMMISSION_PERCENT" ControlToValidate="txtCOMMISSION_PERCENT"></asp:RegularExpressionValidator>  
         <asp:CustomValidator ID="csvCOMMISSION_PERCENT" Display="Dynamic" ControlToValidate="txtCOMMISSION_PERCENT"
        ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>  
        
        </td>  
       
    </tr>
    <tr>
    
    <td class="midcolora" width="50%" id="tdLeader"  runat="server"  colspan="2">
      <asp:Label ID="capLEADER" runat="server" Text="" ></asp:Label><span class="mandatory">*</span>  <br />
        <asp:DropDownList ID="cmbLEADER" runat="server">
        </asp:DropDownList>
        <br />
    <asp:RequiredFieldValidator  ID="rfvLEADER" runat="server" ErrorMessage="" Display="Dynamic"
        ControlToValidate="cmbLEADER"></asp:RequiredFieldValidator>
        
       </td>
       </tr>
    
    
   
    
    <tr>
  
    
    <td class="midcolora"  align="left" >					                   
           <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
            causesValidation="false" ></cmsb:cmsbutton>									    
	 </td>
    <td class="midcolorr" align="right" >
           <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
          ></cmsb:cmsbutton>										
    </td>							     

    </tr>
    </table>
     </td>
      </tr>
    
    <tr>
    
    <td colspan="2">
    
     <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
  <input type="hidden" id="hidCO_APPLICANT_ID" name="hidCO_APPLICANT_ID" runat="server" />
  
       <input type="hidden" id="hidRemunerationId" name="hidRemunerationId" runat="server" />
        <input type="hidden" id="hidSALES_AGENT" name="hidSALES_AGENT" runat="server" value="0" />
        <input type="hidden" id="hidBROKER" name="hidBROKER" runat="server" value="0" />
        </td></tr>
    
    
    </table>
    </div>
    </form>
    
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {

            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidRemunerationId').value);
        }
		</script>
</body>
</html>

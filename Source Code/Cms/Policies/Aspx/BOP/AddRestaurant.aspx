<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRestaurant.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddRestaurant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
         <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ > 
         <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
        <script src="/cms/cmsweb/scripts/Calendar.js"></script>
          <script type="text/javascript" language="javascript">
              function upperCase(x) {
//                  alert(x);
//                  var y = document.getElementById(x).value
//                  document.getElementById(x).value = '$' + y;


              }
              function ResetForm() {

                  document.POL_SUP_FORM_RESTAURANT.reset();
                  //populateXML();
                  //    BillType();
                  // populateXML();
                  //    BillType();
                  // DisableValidators();
                  //ChangeColor();   
                  //var updFrom = document.getElementById('cmbUpdatedFrom').options[document.getElementById('cmbUpdatedFrom').selectedIndex].value; 
                  //document.getElementById('cmbACCOUNT_ID').options.selectedIndex = -1; 

                  document.getElementById('txtSEATINGCAPACITY').value = ''; 
                  document.getElementById('txtBNK_LOANS_PAYABLE').value = '';
                  document.getElementById('txtNOTES_PAYABLE').value = '';
                  document.getElementById('txtBNK_LOANS_PAYABLE').value = '';
                  document.getElementById('txtACCNT_PAYABLE').value = '';
                  document.getElementById('txtNET_PROFIT').value = '';
                  document.getElementById('txtTOT_EXPNS_OTHERS').value = '';
                  document.getElementById('txtTOT_EXPNS_FOOD_LIQUOR').value = '';
                  
                  document.getElementById('chkEXTNG_SYS_COV_COOKNG').checked = false;
                  document.getElementById('chkBUS_TYP_RESTURANT').checked = false;
                  document.getElementById('chkBUS_TYP_FM_STYLE').checked = false;
                  document.getElementById('chkBUS_TYP_NGHT_CLUB').checked = false;
                  document.getElementById('chkWOOD_STOVE').checked = false;
                  document.getElementById('chkHIST_MARKER').checked = false;
                  document.getElementById('chkPRK_TYP_PREMISES').checked = false;
                  document.getElementById('chkOPR_ON_PREMISES').checked = false;
                  

                  document.getElementById('chkBUS_TYP_FRNCHSED').checked = false;
                  document.getElementById('chkBUS_TYP_NT_FRNCHSED').checked = false;
                  document.getElementById('chkBUS_TYP_SEASONAL').checked = false;
                  document.getElementById('chkBUS_TYP_YR_ROUND').checked = false;
                  document.getElementById('chkBUS_TYP_DINNER').checked = false;
                  document.getElementById('chkBUS_TYP_BNQT_HALL').checked = false;
                  document.getElementById('chkBUS_TYP_BREKFAST').checked = false;
                  document.getElementById('chkBUS_TYP_FST_FOOD').checked = false;
                  document.getElementById('chkBUS_TYP_TAVERN').checked = false;
                  document.getElementById('chkBUS_TYP_OTHER').checked = false;

                  document.getElementById('chkSTAIRWAYS').checked = false;
                  document.getElementById('chkELEVATORS').checked = false;
                  document.getElementById('chkESCALATORS').checked = false;
                  document.getElementById('chkGRILLING').checked = false;
                  document.getElementById('chkFRYING').checked = false;
                  document.getElementById('chkBROILING').checked = false;
                  document.getElementById('chkROASTING').checked = false;
                  document.getElementById('chkCOOKING').checked = false;
                  document.getElementById('chkPRK_TYP_VALET').checked = false;
                  document.getElementById('chkEMRG_LIGHTS').checked = false;

                   document.getElementById('chkEXTNG_SYS_COV_COOKNG').checked = false;
                  document.getElementById('chkEXTNG_SYS_MNT_CNTRCT').checked = false;
                  document.getElementById('chkGAS_OFF_COOKNG').checked = false;
                  document.getElementById('chkHOOD_FILTER_CLND').checked = false;
                  document.getElementById('chkHOOD_DUCTS_EQUIP').checked = false;
                  document.getElementById('chkHOOD_DUCTS_MNT_SCH').checked = false;
                  document.getElementById('chkBC_EXTNG_AVL').checked = false;
                  document.getElementById('chkADQT_CLEARANCE').checked = false;

                   document.getElementById('chkBEER_SALES').checked = false;
                  document.getElementById('chkWINE_SALES').checked = false;
                  document.getElementById('chkFULL_BAR').checked = false;

                                                   
                      
                  
                  return false;
              }
        
        
        </script>
</head>
<body leftMargin='0' topMargin='0' onload='ApplyColor();'>
   <form id='POL_SUP_FORM_RESTAURANT' method='post' runat='server'>
   
            <TABLE width='100%' border='0' align='center' border=0>
                <tr>
                    <td class="headereffectCenter" colspan="3" align="center">
                     <asp:Label ID="lblHeader" runat="server" >Restaurant Information</asp:Label>
                    </td>
                    
                </tr>
                <tr id="trErrorMsgs" runat="server">
                    <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colspan="3">
                      <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                    </td>
                </tr>
                            
                <tr>
                    <td   class="midcolora" colspan="3">
                        <asp:Label id="capSEATINGCAPACITY" runat="server">Seating Capacity</asp:Label> 
                        <br />									
                                    
                        <asp:textbox id='txtSEATINGCAPACITY' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px"></asp:textbox>
				                   
                    </td>   
                </tr> 
                <tr>
                    <td class="headerEffectSystemParams" colspan="3">
                        <asp:Label id="capBusiness" runat="server">Type of Business(Check all that apply)</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td   class="midcolora" colspan="3" >                       								
                                    
                        <asp:CheckBox ID="chkBUS_TYP_RESTURANT" runat="server" Text="Restaurant" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_FM_STYLE" runat="server" Text="Family Style" Checked="false"/>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_NGHT_CLUB" runat="server" Text="Night Club" Checked="false" />&nbsp;&nbsp;
             
                        <asp:CheckBox ID="chkBUS_TYP_FRNCHSED" runat="server" Text="Franchised" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_NT_FRNCHSED" runat="server" Text="Not Franchised" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_SEASONAL" runat="server" Text="Seasonal" Checked="false"/>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_YR_ROUND" runat="server" Text="Year Round" Checked="false" />&nbsp;&nbsp;<br />
                       
				                   
                    </td>  
                </tr> 
                <tr>
                    <td   class="midcolora" colspan="3" >
                         <asp:CheckBox ID="chkBUS_TYP_DINNER" runat="server" Text="Diner"  Checked="false"/>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_BNQT_HALL" runat="server" Text="Banquet Hall" Checked="false" />&nbsp;&nbsp;

                        <asp:CheckBox ID="chkBUS_TYP_BREKFAST" runat="server" Text="Bed & Breakfast" Checked="false"/>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_FST_FOOD" runat="server" Text="Fast Food" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_TAVERN" runat="server" Text="Tavern" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBUS_TYP_OTHER" runat="server" Text="Other"  Checked="false"/>&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td  class="headerEffectSystemParams" colspan="3" >
                        <asp:Label id="capAttributes" runat="server">Attributes(Check all that apply)</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td   class="midcolora" colspan="3" >
                          
                        <asp:CheckBox ID="chkSTAIRWAYS" runat="server" Text="Stairways" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkELEVATORS" runat="server" Text="Elevator(s)" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkESCALATORS" runat="server" Text="Escalator(s)"  Checked="false"/>&nbsp;&nbsp;
             
                        <asp:CheckBox ID="chkGRILLING" runat="server" Text="Grilling"  Checked="false"/>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkFRYING" runat="server" Text="Deep Fat Frying" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkBROILING" runat="server" Text="Open Broiling"  Checked="false"/>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkROASTING" runat="server" Text="Roasting" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkCOOKING" runat="server" Text="Tableside Cooking"  Checked="false"/>&nbsp;&nbsp;
                                                                         
                    </td> 
                </tr> 

                <tr>
                    <td class="midcolora" colspan="3" >
                          <asp:CheckBox ID="chkPRK_TYP_VALET" runat="server" Text="Valet Parking" Checked="false" />&nbsp;&nbsp;

                        <asp:CheckBox ID="chkEMRG_LIGHTS" runat="server" Text="Emergency Lighting System" Checked="false" />&nbsp;&nbsp;

                        <asp:CheckBox ID="chkWOOD_STOVE" runat="server" Text="Wood Burning Stove" 
                        Checked="false" />&nbsp;&nbsp;

                        <asp:CheckBox ID="chkHIST_MARKER" runat="server" Text="Historical Marker" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkPRK_TYP_PREMISES" runat="server" Text="Off Premise Parking" Checked="false" />&nbsp;&nbsp;<br />
                       
                    </td>
                </tr>

                <tr>
                    <td class="midcolora" colspan="3" >
                         <asp:CheckBox ID="chkOPR_ON_PREMISES" runat="server" Text="Catering/Banquet operations On Premise" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkOPR_OFF_PREMISES" runat="server" Text="Catering/Banquet operations Off Premise" Checked="false" />&nbsp;&nbsp; 
                    </td>
                </tr>

                <tr>
                    <td class="headerEffectSystemParams" colspan="3" >
                        <asp:Label id="capKitchen" runat="server">Kitchen Fire Protection(Check all that apply)</asp:Label>
                    </td>
                </tr>
       
                <tr>
            
                  <td   class="midcolora" colspan="3" >
                   
                    <asp:CheckBox ID="chkEXTNG_SYS_COV_COOKNG" runat="server" Text="UL 300 Approved Automatic Extinguishing system covers all cooking surfaces" Checked="false" />&nbsp;&nbsp;<br />
                    <asp:CheckBox ID="chkEXTNG_SYS_MNT_CNTRCT" runat="server" Text="UL 300 Approved Automatic Extinguishing system under maintanance contract" Checked="false" />&nbsp;&nbsp;<br />
                    <asp:CheckBox ID="chkGAS_OFF_COOKNG" runat="server" Text="Automatic Gas or Electic shutoffs for cooking Escalator(s)"  Checked="false"/>&nbsp;&nbsp;<br />
                    <asp:CheckBox ID="chkHOOD_FILTER_CLND" runat="server" Text="Hood and  Filter cleaned weekly by staff" Checked="false" />&nbsp;&nbsp;<br />
                    <asp:CheckBox ID="chkHOOD_DUCTS_EQUIP" runat="server" Text="Hood and  Ducts over all cooking equip" Checked="false"/>&nbsp;&nbsp;<br />
                    <asp:CheckBox ID="chkHOOD_DUCTS_MNT_SCH" runat="server" Text="Hood and  Ducts maintenance contract schedule" Checked="false" />&nbsp;&nbsp;<br />
                    <asp:CheckBox ID="chkBC_EXTNG_AVL" runat="server" Text="BC and  K Extingusihers available in  kitchen" Checked="false" />&nbsp;&nbsp;<br />

                    <asp:CheckBox ID="chkADQT_CLEARANCE" runat="server" Text="Adequate clerance between hoods, ducts,cooking equipment  and  combustible materials" />
           
               
                </td> 	
                </tr>

                <tr >
                    <td  class='headerEffectSystemParams' colspan="3">
                        <asp:Label id="capLiquorSales" runat="server">Liquor Sales(Check all that apply)</asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <TD   class="midcolora" colspan="3" >
                        
                        <asp:CheckBox ID="chkBEER_SALES" runat="server" Text="Beer Sales" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkWINE_SALES" runat="server" Text="Wine Sales " Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkFULL_BAR" runat="server" Text="Full Bar" Checked="false"/>&nbsp;&nbsp;
                    </TD>	
                 </tr>
               
                <tr>
                    <TD   class="midcolora" width="33%">
                        <asp:Label id="capTOT_EXPNS_FOOD_LIQUOR" runat="server">Total Operating Expenses(food and liquor only)</asp:Label> 
                        <br />									
                        <asp:textbox id='txtTOT_EXPNS_FOOD_LIQUOR' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px" CssClass="inputcurrency"></asp:textbox>
                    </TD>
                     <TD  class="midcolora" width="33%">
                      <asp:Label id="capTOT_EXPNS_OTHERS" runat="server">Total Operating Expenses(other than cost of food and liquor only)</asp:Label> 
                        <br /><asp:textbox id='txtTOT_EXPNS_OTHERS' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px" CssClass="inputcurrency"></asp:textbox>
                         <br />
                         <%-- <asp:RegularExpressionValidator ID="revTOT_EXPNS_OTHERS" runat="server" ControlToValidate="txtTOT_EXPNS_OTHERS"
                    Display="Dynamic"></asp:RegularExpressionValidator>		--%>
                     
                     
                     </TD>
                      <TD  class="midcolora" width="33%">
                        <asp:Label id="capNET_PROFIT" runat="server">Net Profit or Loss </asp:Label> 
                        <br />									
                                    
                        <asp:textbox id='txtNET_PROFIT' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px" CssClass="inputcurrency" onchange="upperCase(this.id)"></asp:textbox>
                         <br />
                         <%-- <asp:RegularExpressionValidator ID="revNET_PROFIT" runat="server" ControlToValidate="txtNET_PROFIT"
                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                      
                      </TD> 
                </tr>
                <tr>                   
                    <TD  class="midcolora" width="33%">
                    <asp:Label id="capACCNT_PAYABLE" runat="server">Accounts Payable</asp:Label> 
                        <br />
                        <asp:textbox id='txtACCNT_PAYABLE' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px" CssClass="inputcurrency"></asp:textbox>	
                         <br />
                         <%-- <asp:RegularExpressionValidator ID="revACCNT_PAYABLE" runat="server" ControlToValidate="txtACCNT_PAYABLE"
                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                       
                    </TD> 
                     <TD  class="midcolora" width="33%">
                     <asp:Label id="capNOTES_PAYABLE" runat="server">Notes Payable(not to banks) </asp:Label> 
                        <br />									
                                    
                        <asp:textbox id='txtNOTES_PAYABLE' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px" CssClass="inputcurrency"></asp:textbox>
                        <br />
                         <%-- <asp:RegularExpressionValidator ID="revNOTES_PAYABLE" runat="server" ControlToValidate="txtNOTES_PAYABLE"
                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                       
                    </TD> 
                     <TD  class="midcolora" width="33%">
                        <asp:Label id="capBNK_LOANS_PAYABLE" runat="server">Banks Loans Payable</asp:Label> 
                        <br />
                        <asp:textbox id='txtBNK_LOANS_PAYABLE' runat='server' size='50' maxlength='0' 
                        Width="200px" style="margin-left: 0px" CssClass="inputcurrency"></asp:textbox>	<br />
                         <%-- <asp:RegularExpressionValidator ID="revBNK_LOANS_PAYABLE" runat="server" ControlToValidate="txtBNK_LOANS_PAYABLE"
                    Display="Dynamic"></asp:RegularExpressionValidator>--%>
                    </TD> 
                    
                </tr>  		
           
                        
               
               		   		
                <%--Below Butoons--%>  
                <tr>
                    <td class="midcolora" colspan="1" valign="bottom" style="width:190px">
                        <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
                        <cmsb:CmsButton runat="server" CausesValidation="false"  ID="btndelete"  
                        CssClass="clsButton" onclick="btndelete_Click"></cmsb:CmsButton>
                        
                    </td>  
                    <td class="midcolorr" colspan="2">
                    <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
                    </td>       
                </tr> 
            </TABLE>

      
        
         <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
                            <INPUT id="hidRESTAURANT_ID" type="hidden" value="0" name="hidRESTAURANT_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
    </form>
</body>
</html>

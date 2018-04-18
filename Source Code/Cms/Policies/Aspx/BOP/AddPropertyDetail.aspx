<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPropertyDetail.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddPropertyDetail" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL PREMISES PROPERTY INFO</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta http-equiv="Cache-Control" content="no-cache">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script>
	<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 

      <script language="javascript" type="text/javascript">

          function ResetTheForm() {
              document.getElementById('cmbPROP_DEDUCT').value = '';
              document.getElementById('cmbOPT_CVG').value = '';
              document.getElementById('cmbPROP_WNDSTORM').value = '';
              document.getElementById('txtBLD_LMT').value = '';
              document.getElementById('txtBPP_LMT').value = '';
              document.getElementById('cmbBLD_VALU').value = '';
              document.getElementById('cmbBPP_VALU').value = '';
              document.getElementById('cmbBLD_INF').value = '';
              document.getElementById('txtBPP_STOCK').value = '';
              document.getElementById('txtYEAR_BUILT').value = '';
              document.getElementById('cmbCONST_TYPE').value = '';
              document.getElementById('txtBI_WIRNG_YR').value = '';
              document.getElementById('cmbNUM_STORIES').value = '';
              document.getElementById('txtBI_ROOFING_YR').value = '';
              document.getElementById('cmbBP_PRESENT').value = '';
              document.getElementById('cmbBI_ROOF_TYP').value = '';
              document.getElementById('cmbBP_FNSHD').value = '';
              document.getElementById('txtBI_HEATNG_YR').value = '';
              document.getElementById('cmbBP_OPEN').value = '';
              document.getElementById('cmbBI_WIND_CLASS').value = '';
              document.getElementById('cmbBLD_PERCENT_COINS').value = '';
              document.getElementById('cmbBPP_PERCENT_COINS').value = '';
              document.getElementById('cmbPERCENT_SPRINKLERS').value = '';
              document.getElementById('txtBI_PLMG_YR').value = '';
          }

          function ChkOccurenceDate_YEAR_BUILT(objSource, objArgs) {

              var effdate = document.getElementById('txtYEAR_BUILT').value;
              var date = '<%=DateTime.Now.Year%>';


              if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                  objArgs.IsValid = false;


              }
              else {
                  if (effdate > date)
                      objArgs.IsValid = false;
                  else
                      objArgs.IsValid = true;
              }

          }


          function ChkOccurenceDate_BI_ROOFING_YR(objSource, objArgs) {

              var effdate = document.getElementById('txtBI_ROOFING_YR').value;
              var date = '<%=DateTime.Now.Year%>';


              if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                  objArgs.IsValid = false;


              }
              else {
                  if (effdate > date)
                      objArgs.IsValid = false;
                  else
                      objArgs.IsValid = true;
              }

          }

          function ChkOccurenceDate_BI_WIRNG_YR(objSource, objArgs) {

              var effdate = document.getElementById('txtBI_WIRNG_YR').value;
              var date = '<%=DateTime.Now.Year%>';


              if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                  objArgs.IsValid = false;


              }
              else {
                  if (effdate > date)
                      objArgs.IsValid = false;
                  else
                      objArgs.IsValid = true;
              }

          }

          function ChkOccurenceDate_BI_PLMG_YR(objSource, objArgs) {

              var effdate = document.getElementById('txtBI_PLMG_YR').value;
              var date = '<%=DateTime.Now.Year%>';


              if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                  objArgs.IsValid = false;


              }
              else {
                  if (effdate > date)
                      objArgs.IsValid = false;
                  else
                      objArgs.IsValid = true;
              }

          }


          function ChkOccurenceDate_BI_HEATNG_YR(objSource, objArgs) {

              var effdate = document.getElementById('txtBI_HEATNG_YR').value;
              var date = '<%=DateTime.Now.Year%>';


              if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
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
</head>
<body>
    <form id="POL_PREMISES_PROPERTY_INFO" runat="server">
      <table  width='100%' border='0' align='center' border=1>
      <tr>
         <tr id="trMessages" runat="server">
                <td id="tdMessages" runat="server" class="pageHeader" colspan=4>
                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label><br />
                </td>
            </tr>
            <tr id="trErrorMsgs" runat="server">
                <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colspan=4>
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="true"></asp:label><br />
                </td>
            </tr>
             <tr id="trBody" runat="server">
                <td class="midcolorc" align="right" colspan=4>
                    <asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="true"></asp:label><br />
                </td>
            </tr>
      </tr>
      <tr>       
        <td class="headereffectCenter" colspan=3>
            <asp:label ID="Label1" Text="Property Information" runat="server" ></asp:label><br />
        </td>
           
      </tr>

      <tr>
                <td id="td001" class='midcolora' width='33%' runat="server">
                 <asp:Label ID="capYEAR_BUILT" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtYEAR_BUILT" runat="server"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revYEAR_BUILT" runat="server" 
                    Display="Dynamic" ControlToValidate="txtYEAR_BUILT"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvYEAR_BUILT" Runat="server" ControlToValidate="txtYEAR_BUILT" Display="Dynamic"></asp:customvalidator>
                </td>
                 <td class='midcolora' width='33%'>
                 <asp:Label ID="capCONST_TYPE" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbCONST_TYPE" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capSTRUCT_TYPE" runat="server" Text="Structure Type"></asp:Label><br />
                 <asp:DropDownList ID="cmbSTRUCT_TYPE" runat="server"></asp:DropDownList>
                </td>
                
            </tr>    

             <tr>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capEXT_FINISHED" runat="server" Text="Exterior Finish"></asp:Label><br />
                 <asp:DropDownList ID="cmbEXT_FINISHED" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capNUM_STORIES" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbNUM_STORIES" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capPERCENT_SPRINKLERS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbPERCENT_SPRINKLERS" runat="server"></asp:DropDownList>
                </td>
               
            </tr>             

             <tr>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBP_PRESENT" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbBP_PRESENT" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBP_FNSHD" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbBP_FNSHD" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBP_OPEN" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbBP_OPEN" runat="server"></asp:DropDownList>
                </td>
             </tr>
             
             <tr>
                 <td class='headerEffectSystemParams' width='33%'>
                 <asp:Label ID="Label2" runat="server">Building Improvements:</asp:Label>
                </td>
            </tr>

                <tr>
                
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBI_WIRNG_YR" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBI_WIRNG_YR" runat="server"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revBI_WIRNG_YR" runat="server" 
                    Display="Dynamic" ControlToValidate="txtBI_WIRNG_YR"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvBI_WIRNG_YR" Runat="server" ControlToValidate="txtBI_WIRNG_YR" Display="Dynamic"></asp:customvalidator>
                </td>               
                
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBI_ROOFING_YR" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBI_ROOFING_YR" runat="server"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revBI_ROOFING_YR" runat="server" 
                    Display="Dynamic" ControlToValidate="txtBI_ROOFING_YR"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvBI_ROOFING_YR" Runat="server" ControlToValidate="txtBI_ROOFING_YR" Display="Dynamic"></asp:customvalidator>
                </td>

                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBI_PLMG_YR" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBI_PLMG_YR" runat="server"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revBI_PLMG_YR" runat="server" 
                    Display="Dynamic" ControlToValidate="txtBI_PLMG_YR"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvBI_PLMG_YR" Runat="server" ControlToValidate="txtBI_PLMG_YR" Display="Dynamic"></asp:customvalidator>
                </td>

                
            </tr>          

             <tr>
                 <td class='midcolora' width='33%'>
                 <asp:Label ID="capBI_ROOF_TYP" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbBI_ROOF_TYP" runat="server"></asp:DropDownList>
                </td>

                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBI_HEATNG_YR" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBI_HEATNG_YR" runat="server"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revBI_HEATNG_YR" runat="server" 
                    Display="Dynamic" ControlToValidate="txtBI_HEATNG_YR"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvBI_HEATNG_YR" Runat="server" ControlToValidate="txtBI_HEATNG_YR" Display="Dynamic"></asp:customvalidator>
                </td>
                 <td class='midcolora' width='33%'>
                 <asp:Label ID="capBI_WIND_CLASS" runat="server"></asp:Label><br />
                  <asp:DropDownList ID="cmbBI_WIND_CLASS" runat="server"></asp:DropDownList>
                </td>
                
            </tr>           
             <tr>
                <td class='headerEffectSystemParams' colspan="3">
                  <asp:Label ID="Label3" runat="server"></asp:Label>
                </td>
            </tr> 
             <tr>
                
                
            </tr>   
             <tr>
                
               
            </tr> 

       <tr id="tr007" runat="server">
                <td class="headereffectCenter" colspan=3>
                    <asp:label ID="capHEADER" Text="Deductibles" runat="server" ></asp:label><br />
                </td>
            </tr>
            <tr><td class='midcolora' colspan=3></td></tr>
           
           
            <tr><td  colspan=4><table width='100%' border='0' align='center'>
            <tr id="tr001" runat="server">
              <td class='midcolora' width='33%'>
                <asp:Label ID="capPROP_DEDUCT" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbPROP_DEDUCT" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='33%'>
              <asp:Label ID="capOPT_CVG" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbOPT_CVG" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='33%'>
                    <asp:Label ID="capPROP_WNDSTORM" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbPROP_WNDSTORM" runat="server"></asp:DropDownList>
                    
                </td>
            </tr>
            </table></td></tr>

             <tr>
                <td class='headerEffectSystemParams' colspan="3">
                  <asp:Label ID="Label4" runat="server"></asp:Label>
                </td>
            </tr> 
            <tr  runat="server">
               <td class="headereffectCenter" colspan=4>
                   <asp:label ID="capHeader2" Text="Property Information" runat="server" ></asp:label><br />
               </td>
            </tr>
            <tr>
            <td class='headerEffectSystemParams' width='33%'>
             <asp:Label ID="capBuilding" runat="server"></asp:Label>
            </td>
            
            </tr>
            
            <tr>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBLD_LMT" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBLD_LMT" runat="server"></asp:TextBox><br />
                  <asp:RegularExpressionValidator ID="revBLD_LMT" runat="server" ControlToValidate="txtBLD_LMT"></asp:RegularExpressionValidator>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBLD_PERCENT_COINS" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbBLD_PERCENT_COINS" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBLD_VALU" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbBLD_VALU" runat="server"></asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBLD_INF" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbBLD_INF" runat="server">
                 <asp:ListItem Value='8'>8%</asp:ListItem>
                 </asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>&nbsp;</td>
                <td class='midcolora' width='33%'>&nbsp;</td>
            </tr>
            <tr>
            <td class='headerEffectSystemParams' width='33%'>
              <asp:Label ID="capBulD_Per_Prop" runat="server"></asp:Label>
            </td>
            </tr>
             <tr>
             <td class='midcolora' width='33%'>
                 <asp:Label ID="capBPP_LMT" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBPP_LMT" runat="server"></asp:TextBox><br />
                 <asp:RegularExpressionValidator ID="revBPP_LMT" runat="server" ControlToValidate="txtBPP_LMT"></asp:RegularExpressionValidator>
                </td>
                 <td class='midcolora' width='33%'>
                 <asp:Label ID="capBPP_PERCENT_COINS" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbBPP_PERCENT_COINS" runat="server"></asp:DropDownList>
                </td>
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBPP_VALU" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbBPP_VALU" runat="server"></asp:DropDownList>
                </td>
               
            </tr>
             <tr>
                
                <td class='midcolora' width='33%'>
                 <asp:Label ID="capBPP_STOCK" runat="server"></asp:Label><br />
                 <asp:TextBox ID="txtBPP_STOCK" runat="server"></asp:TextBox>
                </td>
                <td class='midcolora' width='33%'>&nbsp;</td>
                <td class='midcolora' width='33%'>&nbsp;</td>
            </tr>   
             <tr>
                <td class='headerEffectSystemParams' colspan="3">
                  <asp:Label ID="Label5" runat="server"></asp:Label>
                </td>
            </tr> 
             
               <tr>
						<td class='midcolora' width='24%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
						
						</td>
						<td class='midcolorr' colspan="3">
							<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' 
                                onclick="btnSave_Click"></cmsb:cmsbutton>
                                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" 
                                Text="Delete" CausesValidation="False"	causevalidation="false" 
                                onclick="btnDelete_Click"></cmsb:cmsbutton>
						</td>
					</tr>                                                                                                                              
      </table>
          <input id="hidCUSTOMER_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" />
         <input id="hidPREMISES_ID" type="hidden" runat="server" />
         <input id="hidPROPERTY_ID" type="hidden" runat="server" />
         <input id="hidFormSaved" type="hidden" runat="server" />
         <input id="hidLOCATION_ID" type="hidden" runat="server" />

    </form>
</body>
</html>

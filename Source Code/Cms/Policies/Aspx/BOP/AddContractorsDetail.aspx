<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContractorsDetail.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddContractorsDetail" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL_SUP_FORM_CONTRACTOR</title>
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
             document.getElementById('txtTYP_CONTRACTOR').value = '';
             document.getElementById('txtYR_EXP').value = '';
             document.getElementById('txtCONT_LICENSE').value = '';
             document.getElementById('cmbLICENSE_HOLDER').value = '';
             document.getElementById('cmbLMT_CONTRACTOR_OCC').value = '';
             document.getElementById('cmbLMT_CONTRACTOR_AGG').value = '';
             document.getElementById('txtTOT_CST_PST_YR').value = '';
             document.getElementById('chkIS_EXPL_ENVRNT').value = '';
             document.getElementById('chkIS_FIRE_ALARM').value = '';
             document.getElementById('chkIS_HOSPITALS').value = '';

             document.getElementById('chkIS_SWIMMING_POOL').value = '';
             document.getElementById('chkIS_BRG_ALARM').value = '';
             document.getElementById('chkIS_PWR_PLANTS').value = '';
             document.getElementById('chkIS_BCK_EQUIPMNT').value = '';
             document.getElementById('chkIS_LIVE_WIRES').value = '';
             document.getElementById('chkIS_ARPT_CONSTRCT').value = '';
             document.getElementById('chkIS_HIGH_VOLTAGE').value = '';
             document.getElementById('chkIS_TRAFFIC_WRK').value = '';
             document.getElementById('chkIS_LND_FILL').value = '';
             document.getElementById('chkIS_DAM_CONSTRNT').value = '';
             document.getElementById('chkIS_REFINERY').value = '';
             document.getElementById('chkIS_HZD_MATERIAL').value = '';
             document.getElementById('chkIS_HZD_MATERIAL').value = '';
             document.getElementById('chkIS_PETRO_PLNT').value = '';
             document.getElementById('chkIS_NUCL_PLNT').value = '';
             document.getElementById('chkIS_PWR_LINES').value = '';
             document.getElementById('cmbDRW_PLANS').value = '';
             document.getElementById('cmbOPR_BLASTING').value = '';
             document.getElementById('cmbOPR_TRENCHING').value = '';
             document.getElementById('cmbOPR_EXCAVACATION').value = '';
             document.getElementById('cmbIS_SECTY_POLICY').value = '';
             document.getElementById('cmbANY_DEMOLITION').value = '';
             document.getElementById('cmbANY_CRANES').value = '';
             document.getElementById('txtPERCENT_ROOFING').value = '';
             document.getElementById('cmbANY_SHOP_WRK').value = '';
             document.getElementById('txtPERCENT_RENOVATION').value = '';
             document.getElementById('cmbANY_GUTTING').value = '';
             document.getElementById('txtPERCENT_SNOWPLOWING').value = '';
             document.getElementById('cmbANY_WRK_LOAD').value = '';
             document.getElementById('txtPERCENT_PNTG_OUTSIDE').value = '';
             document.getElementById('txtPERCENT_PNTG_INSIDE').value = '';
             document.getElementById('cmbANY_PNTG_TNKS').value = '';
             document.getElementById('txtPERCENT_PNTG_SPRY').value = '';
             document.getElementById('cmbANY_EPOXIES').value = '';
             document.getElementById('cmbANY_ACID').value = '';
             document.getElementById('cmbANY_LEASE_EQUIPMNT').value = '';
             document.getElementById('cmbANY_BOATS_OWND').value = '';
             document.getElementById('cmbDRCT_SIGHT_WRK_IN_PRGRSS').value = '';
             document.getElementById('cmbPRDCT_SOLD_IN_APPL_NAME').value = '';
             document.getElementById('cmbUTILITY_CMPNY_CALLED').value = '';
             document.getElementById('txtTYP_IN_DGGN_PRCSS').value = '';
             document.getElementById('cmbPERMIT_OBTAINED').value = '';
             document.getElementById('txtPERCENT_SPRINKLE_WRK').value = '';
             document.getElementById('cmbANY_EXCAVAION').value = '';
             document.getElementById('cmbANY_PEST_SPRAY').value = '';
             document.getElementById('txtPERCENT_TREE_TRIMNG').value = '';
             document.getElementById('cmbANY_WRK_OFFSEASON').value = '';
             document.getElementById('cmbANY_MIX_TRANSIT').value = '';
             document.getElementById('cmbANY_CONTSRUCTION_WRK').value = '';
             document.getElementById('cmbANY_WRK_ABVE_THREE_STORIES').value = '';
             document.getElementById('cmbANY_SCAFHOLDING_ABVE_TWELVE_FEET').value = '';
             document.getElementById('cmbANY_PNTG_TOWERS').value = '';
             document.getElementById('cmbANY_SPRAY_GUNS').value = '';
             document.getElementById('cmbANY_REMOVAL_DONE').value = '';
             document.getElementById('cmbANY_WAXING_FLOORS').value = '';
             document.getElementById('txtPER_RESIDENT').value = '';
             document.getElementById('txtPER_COMMERICAL').value = '';
             document.getElementById('txtPER_CONST').value = '';
             document.getElementById('txtPER_REMODEL').value = '';
             document.getElementById('chkMAJOR_ELECT').value = '';
             document.getElementById('cmbCARRY_LIMITS').value = '';

         }
        
         function ChkOccurenceDate_YEAR_BUILT(objSource, objArgs) {

             var effdate = document.getElementById('txtYR_EXP').value;
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
    <form id="POL_SUP_FORM_CONTRACTOR" runat="server">
     <table  width='100%' border='0' align='center' border=1>
        <tr id="tr007" runat="server">
                <td class="headereffectCenter" colspan=4>
                    <asp:label ID="capHEADER" Text="Contractors" runat="server" ></asp:label><br />
                </td>
            </tr>
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

            <tr>
                <td class='midcolora' width='18%'>
                    <asp:Label ID="capTYP_CONTRACTOR" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtTYP_CONTRACTOR" runat="server"></asp:TextBox>
                </td>
                <td class='midcolora' width='18%'>
                 <asp:Label ID="capPER_RESIDENT" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_RESIDENT" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_RESIDENT" runat="server" ControlToValidate="txtPER_RESIDENT" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
                <td class='midcolora' width='18%'>
                    <asp:Label ID="capYR_EXP" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtYR_EXP" runat="server"></asp:TextBox><br />
                     <asp:regularexpressionvalidator id="revYR_EXP" runat="server" 
                    Display="Dynamic" ControlToValidate="txtYR_EXP"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvYR_EXP" Runat="server" ControlToValidate="txtYR_EXP" Display="Dynamic"></asp:customvalidator>
                </td>
            </tr>

            <tr>
                
                <td class='midcolora' width='18%'>
                 <asp:Label ID="capPER_COMMERICAL" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_COMMERICAL" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_COMMERICAL" runat="server" ControlToValidate="txtPER_COMMERICAL" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
                <td class='midcolora' width='18%'>
                    <asp:Label ID="capCONT_LICENSE" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtCONT_LICENSE" runat="server"></asp:TextBox>
                </td>
                <td class='midcolora' width='18%'>
                 <asp:Label ID="capPER_CONST" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_CONST" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_CONST" runat="server" ControlToValidate="txtPER_CONST" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr id="tr001" runat="server">
             <td class='midcolora' width='18%'>
                 <asp:Label ID="capLICENSE_HOLDER" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbLICENSE_HOLDER" runat="server"></asp:DropDownList>
            </td>
             <td class='midcolora' width='18%'>
            <asp:Label ID="capPER_REMODEL" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_REMODEL" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_REMODEL" runat="server" ControlToValidate="txtPER_REMODEL" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class='midcolora' width='18%'>
                 <asp:Label ID="capLMT_CONTRACTOR_OCC" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbLMT_CONTRACTOR_OCC" runat="server"></asp:DropDownList>
            </td>
           </tr>


            <tr id="tr3" runat="server">
             <td class='midcolora' width='18%'>
                 <asp:Label ID="capLMT_CONTRACTOR_AGG" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbLMT_CONTRACTOR_AGG" runat="server"></asp:DropDownList>
            </td>
             <td class='midcolora' width='18%'>
                    <asp:Label ID="capTOT_CST_PST_YR" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtTOT_CST_PST_YR" runat="server"></asp:TextBox>
                </td>
             <td class='midcolora' width='18%'>
               
                </td>
           </tr>

            <tr>
               
            </tr>
            <tr>
                <td class='midcolora' width='18%' colspan=3>
                  <asp:Label runat="server" ID="capheder1" >Indicate if any work is done in or around the following exposures (for all posts or present operations)</asp:Label>
                </td>
            </tr>
              <tr>
                <td class='midcolora' width='18%' colspan=3>
                
                    <asp:CheckBox ID="chkIS_EXPL_ENVRNT" runat="server" />
                    <asp:CheckBox ID="chkIS_FIRE_ALARM" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_HOSPITALS" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_SWIMMING_POOL" runat="server"></asp:CheckBox><br />
                    <asp:CheckBox ID="chkIS_BRG_ALARM" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_PWR_PLANTS" runat="server"></asp:CheckBox>
                    <asp:CheckBox ID="chkIS_BCK_EQUIPMNT" runat="server"></asp:CheckBox><br />
                    <asp:CheckBox ID="chkIS_LIVE_WIRES" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_ARPT_CONSTRCT" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_HIGH_VOLTAGE" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_TRAFFIC_WRK" runat="server"></asp:CheckBox><br />
                    <asp:CheckBox ID="chkIS_LND_FILL" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_DAM_CONSTRNT" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkMAJOR_ELECT" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_REFINERY" runat="server"></asp:CheckBox> <br />
                    <asp:CheckBox ID="chkIS_HZD_MATERIAL" runat="server"></asp:CheckBox>
                    <asp:CheckBox ID="chkIS_PETRO_PLNT" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_NUCL_PLNT" runat="server"></asp:CheckBox> 
                    <asp:CheckBox ID="chkIS_PWR_LINES" runat="server"></asp:CheckBox>
                </td>
            </tr>

            <tr id="tr4" runat="server">
              <td class='midcolora' width='18%'><br />
                <asp:Label ID="capDRW_PLANS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbDRW_PLANS" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'><br />
               <asp:Label ID="capANY_LEASE_EQUIPMNT" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_LEASE_EQUIPMNT" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capOPR_BLASTING" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbOPR_BLASTING" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr5" runat="server">
              
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_BOATS_OWND" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_BOATS_OWND" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capOPR_TRENCHING" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbOPR_TRENCHING" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
             <asp:Label ID="capDRCT_SIGHT_WRK_IN_PRGRSS" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbDRCT_SIGHT_WRK_IN_PRGRSS" runat="server"></asp:DropDownList>
              </td>
            </tr>
          
            <tr id="tr8" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capOPR_EXCAVACATION" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbOPR_EXCAVACATION" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capPRDCT_SOLD_IN_APPL_NAME" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbPRDCT_SOLD_IN_APPL_NAME" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capIS_SECTY_POLICY" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbIS_SECTY_POLICY" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr9" runat="server">
              
              <td class='midcolora' width='18%'>
              <asp:Label ID="capUTILITY_CMPNY_CALLED" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbUTILITY_CMPNY_CALLED" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_DEMOLITION" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_DEMOLITION" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capTYP_IN_DGGN_PRCSS" runat="server"></asp:Label><br />
             <asp:TextBox ID="txtTYP_IN_DGGN_PRCSS" runat="server"></asp:TextBox>
              </td>
            </tr>
            
            <tr id="tr11" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_CRANES" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_CRANES" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capPERMIT_OBTAINED" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbPERMIT_OBTAINED" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capCARRY_LIMITS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbCARRY_LIMITS" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr12" runat="server">
              <td class='midcolora' width='18%'>
              <asp:Label ID="capPERCENT_SPRINKLE_WRK" runat="server"></asp:Label><br />
              <asp:TextBox ID="txtPERCENT_SPRINKLE_WRK" runat='server'></asp:TextBox><br />
              <asp:RegularExpressionValidator ID="revPERCENT_SPRINKLE_WRK" runat="server" ControlToValidate="txtPERCENT_SPRINKLE_WRK" Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capPERCENT_ROOFING" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtPERCENT_ROOFING" runat="server"></asp:TextBox><br />
                 <asp:RegularExpressionValidator ID="revPERCENT_ROOFING" runat="server" ControlToValidate="txtPERCENT_ROOFING" Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_EXCAVAION" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_EXCAVAION" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr15" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_SHOP_WRK" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_SHOP_WRK" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_PEST_SPRAY" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_PEST_SPRAY" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capPERCENT_RENOVATION" runat="server"></asp:Label><br />
               <asp:TextBox ID="txtPERCENT_RENOVATION" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPERCENT_RENOVATION" runat="server" ControlToValidate="txtPERCENT_RENOVATION" Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
            </tr>

            <tr id="tr16" runat="server">
              
              <td class='midcolora' width='18%'>
             <asp:Label ID="capPERCENT_TREE_TRIMNG" runat="server"></asp:Label><br />
              <asp:TextBox ID="txtPERCENT_TREE_TRIMNG" runat='server'></asp:TextBox>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_GUTTING" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_GUTTING" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
               <asp:Label ID="capANY_WRK_OFFSEASON" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_WRK_OFFSEASON" runat="server"></asp:DropDownList>
              </td>
            </tr>

            
            <tr id="tr18" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capPERCENT_SNOWPLOWING" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtPERCENT_SNOWPLOWING" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPERCENT_SNOWPLOWING" runat="server" ControlToValidate="txtPERCENT_SNOWPLOWING" Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_MIX_TRANSIT" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_MIX_TRANSIT" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_WRK_LOAD" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_WRK_LOAD" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr19" runat="server">
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_CONTSRUCTION_WRK" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_CONTSRUCTION_WRK" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capPERCENT_PNTG_OUTSIDE" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtPERCENT_PNTG_OUTSIDE" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPERCENT_PNTG_OUTSIDE" runat="server" ControlToValidate="txtPERCENT_PNTG_OUTSIDE" Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
              <td class='midcolora' width='18%'>
               <asp:Label ID="capANY_WRK_ABVE_THREE_STORIES" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_WRK_ABVE_THREE_STORIES" runat="server"></asp:DropDownList>
              </td>
            </tr>

               <tr id="tr21" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capPERCENT_PNTG_INSIDE" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtPERCENT_PNTG_INSIDE" runat='server'></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revPERCENT_PNTG_INSIDE" runat="server" ControlToValidate="txtPERCENT_PNTG_INSIDE" Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_SCAFHOLDING_ABVE_TWELVE_FEET" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_SCAFHOLDING_ABVE_TWELVE_FEET" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_PNTG_TNKS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_PNTG_TNKS" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr22" runat="server">
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_PNTG_TOWERS" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_PNTG_TOWERS" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capPERCENT_PNTG_SPRY" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtPERCENT_PNTG_SPRY" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator id="revPERCENT_PNTG_SPRY" runat="server" ControlToValidate="txtPERCENT_PNTG_SPRY"  Display="Dynamic"></asp:RegularExpressionValidator>
              </td>
              <td class='midcolora' width='18%'>
             <asp:Label ID="capANY_SPRAY_GUNS" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_SPRAY_GUNS" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr24" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_EPOXIES" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_EPOXIES" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_REMOVAL_DONE" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_REMOVAL_DONE" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_ACID" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_ACID" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr25" runat="server">
              
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_WAXING_FLOORS" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_WAXING_FLOORS" runat="server"></asp:DropDownList>
              </td>
               <td class='midcolora' width='18%'></td>
               <td class='midcolora' width='18%'></td>
            </tr>
             <tr>
						<td class='midcolora' width='24%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
						
						</td>
                        <td class='midcolora' width='18%'></td>
						<td class='midcolorr' >
							<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' onclick="btnSave_Click" 
                                ></cmsb:cmsbutton>
                                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" 
                                Text="Delete" CausesValidation="False"	causevalidation="false" onclick="btnDelete_Click" 
                                ></cmsb:cmsbutton>
						</td>
					</tr>     
     </table>
         <input id="hidCUSTOMER_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" />
         <input id="hidPREMISES_ID" type="hidden" runat="server" />
         <input id="hidCONTRACTOR_ID" type="hidden" runat="server" />
         <input id="hidFormSaved" type="hidden" runat="server" />
         <input id="hidLOCATION_ID" type="hidden" runat="server" />

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOldBuildingDetails.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddOldBuildingDetails" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POL_SUP_FORM_OLDBUIDING</title>
     <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta http-equiv="Cache-Control" content="no-cache">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cms/cmsweb/css/css1.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script>
	<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
    <script language="javascript" type="text/javascript">
        function ChkOccurenceDate_YEAR_BUILT(objSource, objArgs)
         {
             var effdate = document.getElementById('txtWHN_WIRING_UPDT').value;
            
            var date = '<%=DateTime.Now.Year%>';
            if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) 
            {
                objArgs.IsValid = false;
            }
            else {
                 if (effdate > date) {
                      objArgs.IsValid = false;
                }
                else
                    objArgs.IsValid = true;
            }
        }
        function ChkOccurenceDate_WHN_ROOF_REPLCD(objSource, objArgs) {
            var effdate = document.getElementById('txtWHN_ROOF_REPLCD').value;

            var date = '<%=DateTime.Now.Year%>';
            if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                objArgs.IsValid = false;
            }
            else {
                if (effdate > date) {
                    objArgs.IsValid = false;
                }
                else
                    objArgs.IsValid = true;
            }
        }
        function ChkOccurenceDate_WHN_ROOF_REPRD(objSource, objArgs) {
            var effdate = document.getElementById('txtWHN_ROOF_REPRD').value;

            var date = '<%=DateTime.Now.Year%>';
            if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                objArgs.IsValid = false;
            }
            else {
                if (effdate > date) {
                    objArgs.IsValid = false;
                }
                else
                    objArgs.IsValid = true;
            }
        }


        function ChkOccurenceDate_WHN_PLBMG_MODRS(objSource, objArgs) {
            var effdate = document.getElementById('txtWHN_PLBMG_MODRS').value;

            var date = '<%=DateTime.Now.Year%>';
            if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                objArgs.IsValid = false;
            }
            else {
                if (effdate > date) {
                    objArgs.IsValid = false;
                }
                else
                    objArgs.IsValid = true;
            }
        }
        function ChkOccurenceDate_WHN_HEATNG_MODRS(objSource, objArgs) {
            var effdate = document.getElementById('txtWHN_HEATNG_MODRS').value;

            var date = '<%=DateTime.Now.Year%>';
            if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                objArgs.IsValid = false;
            }
            else {
                if (effdate > date) {
                    objArgs.IsValid = false;
                }
                else
                    objArgs.IsValid = true;
            }
        }
        
    </script>
</head>
<body>
    <form id="POL_SUP_FORM_OLDBUILDING" runat="server">
     <table  width='100%' border='0' align='center' >
        <tr id="tr007" runat="server">
                <td class="headereffectCenter" colspan="2">
                    <asp:label ID="capHEADER" Text="Old Building" runat="server" ></asp:label><br />
                </td>
            </tr>
            <tr id="trMessages" runat="server">
                <td id="tdMessages" runat="server" class="pageHeader" colspan="2">
                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label><br />
                </td>
            </tr>
            <tr id="trErrorMsgs" runat="server">
                <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colspan="2">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="true"></asp:label><br />
                </td>
            </tr>
            <tr id="trBody" runat="server">
                <td class="midcolorc" align="right" colspan="2">
                    <asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="true"></asp:label><br />
                </td>
            </tr>

            <tr id="trBody1" runat="server">
                <td class="headereffectCenter" >
                 <asp:label ID="capSubHeader" Text="Wiring" runat="server" ></asp:label><br />
                 </td>
                 <td class="headereffectCenter">
                  <asp:label ID="capSubHeader1" Text="Roof" runat="server" ></asp:label><br />
                 </td>
            </tr>

            <tr>
                <td id="ttdWHN_WIRING_UPDT" runat="server" class='midcolora' width='18%'>
                    <asp:Label ID="capWHN_WIRING_UPDT" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtWHN_WIRING_UPDT" runat="server"></asp:TextBox><br />
                     <asp:regularexpressionvalidator id="revWHN_WIRING_UPDT" runat="server" 
                        Display="Dynamic" ControlToValidate="txtWHN_WIRING_UPDT"></asp:regularexpressionvalidator>
                    <asp:customvalidator id="csvWHN_WIRING_UPDT" Runat="server" ControlToValidate="txtWHN_WIRING_UPDT" Display="Dynamic">
                    </asp:customvalidator>
                </td>
                <td class='midcolora' width='18%'>
                 <asp:Label ID="capWHN_ROOF_REPRD" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtWHN_ROOF_REPRD" runat="server"></asp:TextBox><br />
                    <asp:regularexpressionvalidator id="revWHN_ROOF_REPRD" runat="server" 
                        Display="Dynamic" ControlToValidate="txtWHN_ROOF_REPRD"></asp:regularexpressionvalidator>
                    <asp:customvalidator id="csvWHN_ROOF_REPRD" Runat="server" ControlToValidate="txtWHN_ROOF_REPRD" Display="Dynamic">
                    </asp:customvalidator>
                </td>
            </tr>

        <%--  <tr>
                <td class='midcolora' width='18%'>
                    
                </td>
                <td class='midcolora' width='18%'>
                 <asp:Label ID="capPER_COMMERICAL" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_COMMERICAL" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_COMMERICAL" runat="server" ControlToValidate="txtPER_COMMERICAL" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>--%>

            <%--<tr>
                <td class='midcolora' width='18%'>
                    <asp:Label ID="capCONT_LICENSE" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtCONT_LICENSE" runat="server"></asp:TextBox>
                </td>
                <td class='midcolora' width='18%'>
                 <asp:Label ID="capPER_CONST" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_CONST" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_CONST" runat="server" ControlToValidate="txtPER_CONST" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>--%>

            <tr id="tr001" runat="server">
             <td class='midcolora' width='18%'>
                 <asp:Label ID="capWIRING_IN_CNDCT" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbWIRING_IN_CNDCT" runat="server"></asp:DropDownList>
            </td>
            <td id="ttdWHN_ROOF_REPLCD" runat="server" class='midcolora' width='18%'>
            <asp:Label ID="capWHN_ROOF_REPLCD" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtWHN_ROOF_REPLCD" runat="server"></asp:TextBox><br />
                     <asp:regularexpressionvalidator id="revWHN_ROOF_REPLCD" runat="server" 
                    Display="Dynamic" ControlToValidate="txtWHN_ROOF_REPLCD"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvWHN_ROOF_REPLCD" Runat="server" ControlToValidate="txtWHN_ROOF_REPLCD" Display="Dynamic"></asp:customvalidator>
          <%--  <asp:Label ID="capPER_REMODEL" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtPER_REMODEL" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revPER_REMODEL" runat="server" ControlToValidate="txtPER_REMODEL" Display="Dynamic"></asp:RegularExpressionValidator>--%>
            </td>
           </tr>

            <tr id="tr2" runat="server">
             <td class='midcolora' width='18%'>
                 <asp:Label ID="capFUSES_RPLCD" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbFUSES_RPLCD" runat="server">
                    </asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                 <asp:Label ID="capROOF_MTRL" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbROOF_MTRL" runat="server"></asp:DropDownList>
            </td>
             </tr>

            <tr id="tr3" runat="server">
             
            <td class='midcolora' width='18%'>
                    <asp:Label ID="capALM_WIRING" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbALM_WIRING" runat="server"></asp:DropDownList>
                </td>
            <td class='midcolora' width='18%'>
                 <asp:Label ID="capSPF" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cmbSPF" runat="server"></asp:DropDownList>
            </td>
           </tr>

            <tr>
                <td class="headereffectCenter" >
                 <asp:label ID="lblPlumbing" Text="Plumbing" runat="server" ></asp:label><br />
                 </td>
                 <td class="headereffectCenter">
                  <asp:label ID="lblAsbestos" Text="Asbestos" runat="server" ></asp:label><br />
                 </td>
            </tr>

            <tr id="tr4" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capWHN_PLBMG_MODRS" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtWHN_PLBMG_MODRS" runat="server"></asp:TextBox><br />
                 <asp:regularexpressionvalidator id="revWHN_PLBMG_MODRS" runat="server" 
                    Display="Dynamic" ControlToValidate="txtWHN_PLBMG_MODRS"></asp:regularexpressionvalidator>
                 <asp:customvalidator id="csvWHN_PLBMG_MODRS" Runat="server" ControlToValidate="txtWHN_PLBMG_MODRS" Display="Dynamic"></asp:customvalidator>
              </td>
              <td class='midcolora' width='18%'>
               <asp:Label ID="capANY_ABSTS" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_ABSTS" runat="server"></asp:DropDownList>
              </td>
            </tr>

            <tr id="tr5" runat="server">
              <td class='midcolora' width='18%'>
                <asp:Label ID="capTYP_WTR_PIPS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbTYP_WTR_PIPS" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
              <asp:Label ID="capANY_FRB_ABSTS" runat="server"></asp:Label><br />
              <asp:DropDownList ID="cmbANY_FRB_ABSTS" runat="server"></asp:DropDownList>
              </td>
            </tr>
            <tr id="tr6" runat="server">
              <td class="headereffectCenter" colspan="2">
                <asp:Label ID="capHeating" Text="Heating" runat="server"></asp:Label>
              </td>
            
            </tr>

            <tr id="tr7" runat="server">
              <td class='midcolora' width='18%' colspan="2">
                  <table>
                      <tr>
                        <td  class='midcolora' width='18%'>
                            <asp:Label ID="capWHN_HEATNG_MODRS" runat="server"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtWHN_HEATNG_MODRS" runat="server"></asp:TextBox><br/>
                            <asp:regularexpressionvalidator id="revWHN_HEATNG_MODRS" runat="server" Display="Dynamic" ControlToValidate="txtWHN_HEATNG_MODRS"></asp:regularexpressionvalidator>
                            <asp:customvalidator id="csvWHN_HEATNG_MODRS" Runat="server" ControlToValidate="txtWHN_HEATNG_MODRS" Display="Dynamic"></asp:customvalidator>
                        </td>
                        <td class='midcolora' width='18%'>
                          <asp:Label ID="capTYP_FUEL" runat="server"></asp:Label>
                  <br />
              <asp:DropDownList ID="cmbTYP_FUEL" runat="server"></asp:DropDownList>
                        
                        </td>
                        <td class='midcolora' width='18%'>
                <asp:Label ID="capTYP_SYS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbTYP_SYS" runat="server"></asp:DropDownList>
              </td>         
                      </tr>
                  </table>
              </td>
           </tr>

       <%--      <tr id="tr8" runat="server">
              <td class='midcolora' width='18%' colspan="2">
            
              </td>
            </tr>

            <tr id="tr9" runat="server">
              
             
            </tr>--%>

             <tr>
						<td class='midcolora' width='24%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset' 
                                onclick="btnReset_Click"></cmsb:cmsbutton>
						
						</td>
						<td class='midcolorr' >
							<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' onclick="btnSave_Click" 
                                ></cmsb:cmsbutton>
                                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" onclick="btnDelete_Click"
                                Text="Delete" CausesValidation="False"	causevalidation="false"  
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


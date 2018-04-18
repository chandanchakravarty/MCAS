<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDiscountSurcharge.aspx.cs" ValidateRequest ="false" Inherits=" Cms.CmsWeb.Maintenance.AddDiscountSurcharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MNT_DISCOUNT_SURCHARGE</title>
<meta content="Microsoft Visual Studio 7.0" name="GENERATOR"/>
    <meta content="C#" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
 <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
 <script src="/cms/cmsweb/scripts/xmldom.js"></script>
 <script src="/cms/cmsweb/scripts/common.js"></script>
<script src="/cms/cmsweb/scripts/form.js"></script>
<script src="/cms/cmsweb/scripts/Calendar.js"></script>
<script language="javascript">

    function FillSubLOB() {
        var options = document.getElementById('hidSUB_GEN').value;
        
               document.getElementById('cmbSUBLOB_ID').innerHTML = '';
                var Xml = document.getElementById('hidLOBXML').value;
                var LOBId = "";
                if (document.getElementById('cmbLOB_ID').selectedIndex != -1) 
                {
                    LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
                    
                }
                
            //LOB id is not selected then returning
            if (document.getElementById('cmbLOB_ID').selectedIndex == -1) 
            {
                document.getElementById('hidLOBId').value = '';
                return false;
            }

            var objXmlHandler = new XMLHandler();
            if (Xml != "") {
                var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
                
                //adding a blank option
                oOption = document.createElement("option");
                oOption.value = "0";
                oOption.text = options;
               
                document.getElementById('cmbSUBLOB_ID').add(oOption);
               
                for (i = 0; i < tree.childNodes.length; i++) {
                    nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
                    
                    if (nodValue != null) {                       
                        if (nodValue[0].firstChild == null)
                            continue
                        			
                        if ((nodValue[0].firstChild.text == LOBId)) /*&& stateValue[0].firstChild.text==stID*/
                        {
                           
                            SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
                            SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
                            
                            if (SubLobId != null && SubLobDesc != null) {
                                if ((SubLobId[0] != null || SubLobId[0] == 'undefined')
									&& (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined')) {
                                    oOption = document.createElement("option");
                                    oOption.value = SubLobId[0].firstChild.text;
                                    oOption.text = SubLobDesc[0].firstChild.text;
                                    document.getElementById('cmbSUBLOB_ID').add(oOption);
                                    
                                }
                            }
                        }
                    }
                }
            }
            //document.getElementById('cmbSUBLOB_ID').selectedIndex = -1;
            
            if (document.getElementById('hidOldSUB_LOB').value != '') 
            {
                document.getElementById('cmbSUBLOB_ID').value = document.getElementById('hidOldSUB_LOB').value;
            }
            
            if (document.getElementById('cmbSUBLOB_ID').value != "") {
                ValidatorEnable(document.getElementById("rfvSUBLOB_ID"), false);
            }
            
        }

        function ResetTheForm() {
            document.MNT_DISCOUNT_SURCHARGE.reset();
        }

        function setHidSubLob() {
            if (document.getElementById('cmbSUBLOB_ID').selectedIndex != -1) {
                document.getElementById('hidSUB_LOB').value = document.getElementById('cmbSUBLOB_ID').options[document.getElementById('cmbSUBLOB_ID').selectedIndex].value;              
            }
        }
        function FormatAmountForSum(num) {
            num = ReplaceAll(num, sGroupSep, '');
            num = ReplaceAll(num, sDecimalSep, '.');   

            return num;

        }

        function Validate(objSource, objArgs) {
          //debugger
            var comm = parseFloat(FormatAmountForSum(document.getElementById(objSource.controltovalidate).value));
            //var comm = parseFloat(document.getElementById('txtPERCENTAGE').value);
            if (comm < 0 || comm > 100) {
                document.getElementById(objSource.controltovalidate).select();
                objArgs.IsValid = false;
            }
            else
                objArgs.IsValid = true;
        }

        function allnumeric(objSource, objArgs)
          {//debugger
              var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
              var inputxt = document.getElementById('txtFINAL_DATE').value;
              if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                  document.getElementById('revFINAL_DATE').setAttribute('enabled', true);
                 
              objArgs.IsValid = true;
          }
         else
          {
              document.getElementById('revFINAL_DATE').setAttribute('enabled', false);
              document.getElementById('revFINAL_DATE').style.display = 'none';
              document.getElementById('csvFINAL_DATE').setAttribute('enabled', true);
              document.getElementById('csvFINAL_DATE').style.display = 'inline';
           objArgs.IsValid = false;  
          }
          } 

</script>
</head>
<body leftmargin="0" topmargin="0" oncontextmenu="return false;" onload="FillSubLOB();ApplyColor();ChangeColor();setHidSubLob();">
    <form id="MNT_DISCOUNT_SURCHARGE" runat="server" method="post">
    <table  width="100%" class="tableWidthHeader">
        <tr>
            <td>
                <table width="100%" class="tableWidthHeader">
                    <tr>
                     <td class="midcolorc" align="right" colSpan="3">
                        <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                     </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr id="trBody" runat="server">
        <td>
        <table width="100%" class="tableWidthHeader">
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
        <td class="pageHeader" colspan="3" class="midcolora">
         <asp:Label runat="server" ID="capMAN_MSG"></asp:Label>
        </td>
        </tr>
        <tr>
            <td width="34%" class="midcolora">
                <asp:Label ID="capTYPE" runat="server"></asp:Label><span class="mandatory">*</span></br>
                <asp:DropDownList runat="server" ID="cmbTYPE_ID" Width="200px">
                </asp:DropDownList>
                 <br />
             <asp:requiredfieldvalidator id="rfvTYPE" runat="server" ControlToValidate="cmbTYPE_ID" 
											Display="Dynamic" AutoPostBack="false"></asp:requiredfieldvalidator>
            </td>
                <td width="33%" class="midcolora">
                <asp:Label ID="capLOB_ID" runat="server" Text="Product" EnableViewState="True"></asp:Label><span class="mandatory">*</span></br>
             <asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server" AutoPostBack="false" onchange="FillSubLOB();"></asp:dropdownlist>
             <br />
             <asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" 
											Display="Dynamic"></asp:requiredfieldvalidator>
            
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capSUBLOB_ID" Text="Line Of Business" runat="server"></asp:Label><span class="mandatory">*</span></br>
              <asp:dropdownlist id="cmbSUBLOB_ID" runat="server" AutoPostBack="false" onchange="setHidSubLob();"></asp:dropdownlist><br />
              <asp:requiredfieldvalidator id="rfvSUBLOB_ID" runat="server" ControlToValidate="cmbSUBLOB_ID"  
											Display="Dynamic"></asp:requiredfieldvalidator>
            </td>
            </tr>
            <tr>
            <td width="34%" class="midcolora">
                <asp:Label ID="capDISCOUNT_DESCRIPTION" runat="server" Text="Discount/Surcharge"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtDISCOUNT_DESCRIPTION" runat="server" Width="200px" onkeypress="MaxLength(this,40)" onpaste="MaxLength(this,40)"></asp:TextBox>
                <br />
                <asp:requiredfieldvalidator id="rfvDISCOUNT_DESCRIPTION" runat="server" ControlToValidate="txtDISCOUNT_DESCRIPTION" 
											Display="Dynamic"></asp:requiredfieldvalidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capPERCENTAGE" runat="server" Text="Percentage"></asp:Label><%--<span class="mandatory">*</span>--%></br>
               <asp:TextBox ID="txtPERCENTAGE" runat="server" Width="80px" MaxLength="7" CssClass="INPUTCURRENCY" onblur="this.value=formatAmount(this.value,2)"  onChange = "this.value=formatAmount(this.value,2)" ></asp:TextBox>
               <br />
               <%--<asp:requiredfieldvalidator id="rfvPERCENTAGE" runat="server" ControlToValidate="txtPERCENTAGE" 
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
				<asp:regularexpressionvalidator id="revPERCENTAGE" Runat="server" ControlToValidate="txtPERCENTAGE"
												Display="Dynamic"></asp:regularexpressionvalidator>
				<asp:customvalidator id="csvPERCENTAGE" Display="Dynamic" ControlToValidate="txtPERCENTAGE" ClientValidationFunction="Validate" Runat="server"></asp:customvalidator>
            </td>
            <td width="40%" class="midcolora">
                <asp:Label ID="capLEVEL" runat="server"></asp:Label><span class="mandatory">*</span>
               </br>
               <asp:Dropdownlist ID="cmbLEVEL" runat="server" ></asp:Dropdownlist><br />
               <asp:requiredfieldvalidator id="rfvLEVEL" runat="server" ControlToValidate="cmbLEVEL" 
											Display="Dynamic"></asp:requiredfieldvalidator>
        </td>
        </tr>
        </tr>
        <tr>
            <td width="40%" class="midcolora">
                <asp:Label ID="capEFFECTIVE_DATE" runat="server"></asp:Label><span class="mandatory">*</span>
                <br />
               <asp:TextBox runat="server" ID="txtEFFECTIVE_DATE" SIZE="15" MaxLength="10"></asp:TextBox>
               <asp:hyperlink id="hlkEFFECTIVE_FROM_DATE" runat="server" CssClass="HotSpot">
               <asp:Image runat="server" ID="imgEFFECTIVE_FROM_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
			   </asp:hyperlink>
               <br />
               <asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" 
											Display="Dynamic"></asp:requiredfieldvalidator>
			   <asp:regularexpressionvalidator id="revEFFECTIVE_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_DATE"
												Display="Dynamic"></asp:regularexpressionvalidator>
               <asp:comparevalidator id="cpvFINAL_DATE" ControlToValidate="txtFINAL_DATE" Display="Dynamic" Runat="server" ControlToCompare="txtEFFECTIVE_DATE" Type="Date"
						Operator="GreaterThanEqual"></asp:comparevalidator>	
            </td>
            <td width="40%" class="midcolora">
                <asp:Label ID="capFINAL_DATE" Text="Final Date" runat="server"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox runat="server" ID="txtFINAL_DATE" SIZE="15" MaxLength="10"></asp:TextBox>
                <asp:hyperlink id="hlkEFFECTIVE_TO_DATE" runat="server" CssClass="HotSpot">
                <asp:Image runat="server" ID="imgEFFECTIVE_TO_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
				</asp:hyperlink>
                <br />
                <asp:requiredfieldvalidator id="rfvFINAL_DATE" runat="server" ControlToValidate="txtFINAL_DATE" 
											Display="Dynamic"></asp:requiredfieldvalidator>
			    <asp:regularexpressionvalidator id="revFINAL_DATE" Runat="server" ControlToValidate="txtFINAL_DATE"
												Display="Dynamic" ></asp:regularexpressionvalidator>
				 <asp:CustomValidator ID="csvFINAL_DATE" runat="server" ControlToValidate="txtFINAL_DATE" ClientValidationFunction="allnumeric" Display="Dynamic"></asp:CustomValidator>					
            </td>
            <td width="20%" class="midcolora">
            </td>
        </tr>
        <tr>
            <td  class="midcolora" width="33%">
                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" 
                    CausesValidation="false" Text="ActivateDeactivate" 
                    onclick="btnActivateDeactivate_Click"/>
                </td><td  class="midcolora" width="33%"></td>
                <td  class="midcolora" width="33%">
                <table width="100%" class="tableWidthHeader">
                <tr>
                <td align="right" colspan="2" >
              
                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click"></cmsb:cmsbutton>
                        <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
                    causesvalidation="false" onclick="btnDelete_Click"></cmsb:cmsbutton>
                </td>
               
                </tr>
                </table>          
                </td>
				
           </tr>
        <input id="hidLOBXML" type="hidden" value="0" name="hidLOBXML" runat="server"/>
        <input id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server"/> 
        <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
        <input id="hidDiscountID" type="hidden" value="" name="hidDiscountID" runat="server"/>
        <input id="hidSUB_LOB" type="hidden" value="0" name="hidSUB_LOB" runat="server"/>
        <input id="hidOldSUB_LOB" type="hidden" value="0" name="hidOldSUB_LOB" runat="server"/>
        <input id="hidSave" type="hidden" value="0" name="hidSave" runat="server"/>
        <input id="hidSUB_GEN" type="hidden" value=0  runat="server"/>
        
        </table>
        </td>
        </tr>
    </table>
    </form>
     <script type="text/javascript" >
         if (document.getElementById('hidFormSaved').value == "1") {

             RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDiscountID').value);
         }
		</script>
</body>
</html>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDiscountSurcharge.aspx.cs" Inherits="Cms.Policies.aspx.AddDiscountSurcharge" culture="auto"  uiculture="auto" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>DisCounts_Surcharge</title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript" type="text/javascript">
		    function check() {
		        var alertmsg = '<%= alertmsg %>';
		        var Confirmmsg = '<%= Confirmmsg %>';
		        var frm = document.POL_DISCOUNTS_SURCHARGES;
		        var boolAllChecked;
		        boolAllChecked = false;
		        for (i = 0; i < frm.length; i++) {
		            e = frm.elements[i];
		            if (e.type == 'checkbox' && e.name.indexOf('chkDELETE') != -1)
		                if (e.checked == true) {

		                boolAllChecked = true;
		                break;
		            }
		        }
		        if (boolAllChecked == false) {
		            alert(alertmsg);
		            return false;
		        }
		        else {
		            var k = confirm(Confirmmsg);
		            return k;
		        }
		    }
		    function FormatAmountForSum(num) {
		        num = ReplaceAll(num, sGroupSep, '');
		        num = ReplaceAll(num, sDecimalSep, '.');
		        return num;
		    }
		    function Validate(objSource, objArgs) {
		       
		        var comm = document.getElementById(objSource.controltovalidate).value;
		        comm = FormatAmountForSum(comm);
		        if (parseFloat(comm) < 0 || parseFloat(comm) > 100) {
		            document.getElementById(objSource.controltovalidate).select();
		            objArgs.IsValid = false;
		        }
		        else
		            objArgs.IsValid = true;
		    }
		    function DisableValidator() {
		        document.getElementById("rfvMAIN_TYPE").setAttribute('enabled', false);
		        document.getElementById("rfvMAIN_TYPE").setAttribute('isValid', true);
		        document.getElementById("rfvMAIN_TYPE").style.display = "none";

		    }
		    function EnableValidator() {
		        document.getElementById("rfvMAIN_TYPE").setAttribute('enabled', true);
		        document.getElementById("rfvMAIN_TYPE").setAttribute('isValid', false);
		        document.getElementById("rfvMAIN_TYPE").style.display = "inline";
//		        if (typeof (Page_Validators) != 'undefined') {
//		            //For Gridview Validators off
//		            for (ctr = 0; ctr < Page_Validators.length; ctr++) {
//		                if (Page_Validators[ctr].id.substring(0, 13) == "grdDISCOUNTS_SURCHARGES") {
//		                    Page_Validators[ctr].setAttribute('enabled', false);
//		                }
//		            }
//		        }
		    }
		    function Init() {
		        ApplyColor();
		        ChangeColor();
		      DisableValidator();
		  }

		  function ShowAlertMessageForDelete(chkbox, isDelete) {
		      var alertmsg = '<%= alertmsg %>';
		      validatordisable();
		      var checked = false;
		     
		      for (var i = 0; i < document.POL_DISCOUNTS_SURCHARGES.length; i++) {
		          control = document.POL_DISCOUNTS_SURCHARGES.elements[i];
		        
		          if (control.type == 'checkbox' && control.name.indexOf(chkbox) != -1) {
		          
		              var splitid = control.id.split('_');
		              if (control.checked) {

		                 
		                  checked = true;
		                  var rfvPERCENT = splitid[0] + '_' + splitid[1] + '_' + splitid[2] +'_'+ 'rfvPERCENT';
		                  var revPERCENT = splitid[0] + '_' + splitid[1] + '_' + splitid[2] + '_' + 'revPERCENT';
		                  var txtPERCENT = splitid[0] + '_' + splitid[1] + '_' + splitid[2] + '_' + 'txtPERCENT';
		                  var csvPERCENT = splitid[0] + '_' + splitid[1] + '_' + splitid[2] + '_' + 'csvPERCENT';
		                  if (document.getElementById(txtPERCENT).value == '') {
		                      document.getElementById(rfvPERCENT).setAttribute('enabled', true);
		                      document.getElementById(rfvPERCENT).setAttribute('isValid', false);
		                      document.getElementById(rfvPERCENT).style.display = "inline";

		                  }
		                  else {
		                      document.getElementById(csvPERCENT).setAttribute('enabled', true);
		                      document.getElementById(csvPERCENT).setAttribute('isValid', true);
		                      document.getElementById(csvPERCENT).style.display = "inline";
		                      document.getElementById(revPERCENT).setAttribute('enabled', true);
		                      document.getElementById(revPERCENT).setAttribute('isValid', true);
		                      document.getElementById(revPERCENT).style.display = "inline";
		                     
		                  }
		                  Page_ClientValidate();
		               
		              }
		          }
		      }

		      if (checked == false) {
		          alert(alertmsg);
		          return checked;
		      }


		  }
		  function validatordisable() {
		      if (typeof (Page_Validators) == "undefined")
		          return;
		      var i, val;
		      for (i = 0; i < Page_Validators.length; i++) {
		          val = Page_Validators[i];
		          val.setAttribute('enabled', false);
		      }

		  }    
		</script>
    
</head>
<body oncontextmenu="return false;" leftMargin="0"  rightMargin="0" MS_POSITIONING="GridLayout" onload="Init();">
    <form id="POL_DISCOUNTS_SURCHARGES"  runat="server" name="POL_DISCOUNTS_SURCHARGES" onsubmit="" method="post">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" >
            <tbody>
             <tr>
                   <td  align="right" colspan="2">
	                 <asp:label id="lblFormLoadMessage" runat="server" Visible="False" 
                           CssClass="errmsg" ></asp:label>
	               </td>
	          </tr>
	          
	            <tr>							        
					<td class="headereffectCenter" colspan="2"><asp:label id="lblHeader" Runat="server"></asp:label>
					 </td>
				</tr>	
        
             <tr>
               <td class="pageHeader" align="left" colspan="2" >
		           <br />
		           <asp:label id="lblManHeader" Runat="server" 
                       ></asp:label>
		       </td>
	        </tr>
    	
	    <tr>
		    <td class="midcolorc" colspan="2"><asp:label id="lblMessage" runat="server" 
                    CssClass="errmsg" ></asp:label>
		    </td>
	    </tr>
	     <tr>
		    <td class="midcolora" colspan="2"><asp:label id="capTYPE" runat="server" ></asp:label><span class="mandatory">*</span><br />
		    <asp:DropDownList runat="server" ID="cmbMAIN_TYPE" >
		    <asp:ListItem Text="" Value="" ></asp:ListItem>
		    </asp:DropDownList>		   
		     <cmsb:CmsButton runat="server" CausesValidation="true"  CssClass="clsButton" ID="btnAdd" OnClientClick="EnableValidator()" onclick="btnadd_Click"/>
 
		     <br />
		     <asp:RequiredFieldValidator runat="server" ID="rfvMAIN_TYPE" ErrorMessage="" Display="Dynamic"  ControlToValidate="cmbMAIN_TYPE"></asp:RequiredFieldValidator>
		    </td>
	    </tr>
	      <tr>
		    <td class="midcolora" colspan="2" width="5px"></td>
	        <tr>
	        <td class="midcolora" colspan="2">
	            <asp:GridView runat="server" ID="grdDISCOUNTS_SURCHARGES" 
                    AutoGenerateColumns="False" Width="50%" 
                    onrowdatabound="grdDISCOUNTS_SURCHARGES_RowDataBound" 
                   >
	            <HeaderStyle CssClass="headereffectWebGrid" ></HeaderStyle>
	            <RowStyle CssClass="midcolora" ></RowStyle>            	
	            <Columns>
	            
	            <asp:TemplateField >
	            <ItemStyle Width="5%" />
	            <ItemTemplate>
	            <asp:CheckBox runat="server" ID="chkDELETE" />
	            </ItemTemplate>
	            </asp:TemplateField>
	            
	            <asp:TemplateField Visible="False" >              
	            <ItemTemplate>
	            <asp:label runat="server" ID="DISCOUNT_ID"  Text='<%# Eval("DISCOUNT_ID") %>' ></asp:label>	            
	            </ItemTemplate>
	            </asp:TemplateField>	            
	            
	            <asp:TemplateField Visible="False" >     	  
	            <ItemTemplate>              
	            <asp:Label runat="server" ID="DISCOUNT_ROW_ID"  Text='<%# Eval("DISCOUNT_ROW_ID") %>'></asp:Label>	            
	            </ItemTemplate>
	            </asp:TemplateField>      
            	
	            <asp:TemplateField >
	            <ItemStyle Width="20%" />
	            <HeaderTemplate>
	            <asp:Label runat="server" ID="capHEADER_TYPE" ></asp:Label>
	            </HeaderTemplate>
	            <ItemTemplate>
	            <asp:Label Width="100%" runat="server" ID="DISCOUNT_DESCRIPTION" Text='<%# Eval("DISCOUNT_DESCRIPTION") %>' >
	            </asp:Label>
	            </ItemTemplate>

	            </asp:TemplateField>
            	
	            <asp:TemplateField >
	            <ItemStyle Width="25%" />
	            <HeaderTemplate>
	            <asp:Label runat="server" ID="capHEADER_PERCENT"></asp:Label>
	            </HeaderTemplate>
	            <ItemTemplate>
	            <asp:TextBox runat="server" Width="150px" AutoCompleteType="Disabled"  
	            CssClass="INPUTCURRENCY" ID="txtPERCENT" MaxLength="7"  Text='<%# Eval("PERCENTAGE") %>'> </asp:TextBox>
	            <br />
	            <asp:RequiredFieldValidator runat="server" ID="rfvPERCENT"  ErrorMessage="" Display="Dynamic" ControlToValidate="txtPERCENT"></asp:RequiredFieldValidator>
	            <asp:RegularExpressionValidator runat="server" ID="revPERCENT" ErrorMessage="" Display="Dynamic" ControlToValidate="txtPERCENT"></asp:RegularExpressionValidator>
                <asp:customvalidator id="csvPERCENT" Display="Dynamic" ControlToValidate="txtPERCENT" ClientValidationFunction="Validate" Runat="server"></asp:customvalidator>
               </ItemTemplate>

<ItemStyle Width="25%"></ItemStyle>
	            </asp:TemplateField>
	            
	            </Columns>            	
	            </asp:GridView>
	        </td>
	    </tr>
    	
	        <tr>
	            <td class="midcolora" width="70%" align="left">
	               
	                <cmsb:CmsButton runat="server" CssClass="clsButton" ID="btnDelete" 
                          CausesValidation="False"  OnClick="btnDelete_Click" OnClientClick="return check()" />
	            </td>
	            <td class="midcolora" width="30%" style="text-align:right">
	                <cmsb:CmsButton runat="server" CssClass="clsButton" ID="btnSave"  OnClick="btnSave_Click" />
	            </td>
	         </tr>
    	     <tr>
    	     <td colspan="2">
    	     <input type="hidden" id="hidAction" name="hidAction" runat="server" />
    	         <input id="hidCustomer_Id" type="hidden" value="0" runat="server"/>
    	         <input id="hidPolicy_Id" type="hidden" value="0" runat="server"/>
    	         <input id="hidPolicy_Version_Id" type="hidden" value="0"  runat="server"/>
    	         <input id="hidRisk_Id" type="hidden" value="0"  runat="server"/>    	        
    	         <input id="hidCO_APPLICANT_ID" type="hidden" value="0"  runat="server"/>    	        
    	         
    	     </td>
    	     
    	     </tr>
	         </tbody>
	         </table>
	
    <%--<div>
    <center><b><font size="5"><asp:label ID="capgrid" Text="GRID FOR DISCOUNT  AND SURCHARGE CALCULATION" runat="Server"></asp:label></font></b></center>
        <%--<asp:Repeater ID="Repeater1" runat="server" >
        <HeaderTemplate>
        <table width="100%" border="2">
            <tr>
              <td width="25%" class="midcolora"><b><asp:Label ID="capSELECT" Text="SELECT" runat="server"></asp:Label></B></th>
              <TD width="25%" class="midcolora"><B><asp:Label ID="capTYPE_ID" Text="TYPE_ID" runat="server"></asp:Label></B></TD>
              <TD width="25%" class="midcolora"><B><asp:Label ID="capDISCOUNT_DESCRIPTION" Text="DISCOUNT DESCRIPTION" runat="server"></asp:Label></B></TD>
              <TD width="25%" class="midcolora"><B><asp:Label ID="capPERCENTAGE" Text="PERCENTAGE" runat="server"></asp:Label></B></TD>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td  class="midcolora"  width="25%">
              <asp:CheckBox AutoPostBack="true" ID="chkSelect" runat="server"  Enabled="false" />
            </td>
            <td  class="midcolora"  width="25%">
                <asp:DropDownList AutoPostBack="true" ID="CmbTYPE_ID" runat="server"  Width="150px"  Enabled="false">
                    <asp:ListItem Text="Select" Value="" Selected="True" />
                    <asp:ListItem Text="Discount" Value="Discount" />
                    <asp:ListItem Text="Surcharges" Value="Surcharges"/>
                </asp:DropDownList>
             </td>
             <td  class="midcolora"  width="25%">
                <asp:DropDownList ID="cmbDISCOUNT_DESCRIPTION" runat="server" Enabled="false" Width="200px">
                    <asp:ListItem Text="Discount/Surcharges" Value="" />
                </asp:DropDownList>
             </td>
            <td  class="midcolora"  width="25%">
              <asp:TextBox runat="server" ID="txtPERCENTAGE" Enabled="false" onblur="fun(this.id)" size="30" />
            </td>
          </tr>
        </ItemTemplate>
        
        <FooterTemplate>
            <tr>
            <td colspan="4" align="right">
              <asp:Button ID="Btn_Save" runat="server" Text="SAVE" /> 
              <asp:Button ID="Btn_Reset" runat="server" Text="RESET" />
                         
		</td>
		</tr>
          </table>
        </FooterTemplate>
      </asp:Repeater>
   </div>--%>
    </form>
</body>
</html>

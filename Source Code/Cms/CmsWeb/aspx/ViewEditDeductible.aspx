<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditDeductible.aspx.cs" Inherits="Cms.CmsWeb.aspx.ViewEditDeductible"  validateRequest="false" enableEventValidation="false" viewStateEncryptionMode ="Never"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Deductible Text</title>
    <link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
	<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	<script type="text/javascript" language="javascript">
	    function SaveDeductibleText() {
	        if (document.getElementById("txtDEDUCTIBLE_TEXT").value.length > 250) {
	            DEDUCTIBLE_TEXT_VALIDATE();
	            return false;
	        }
	        var id = document.getElementById("hidParentDeductId").value;
	        var myOpener = window.dialogArguments;
	        var Alert = document.getElementById('hidAlert').value
	        window.opener.document.getElementById(id).value = document.getElementById("txtDEDUCTIBLE_TEXT").value;
	       
	      
	        //myOpener.document.getElementById(id).value = document.getElementById("txtDEDUCTIBLE_TEXT").value;
	        if (document.getElementById("txtDEDUCTIBLE_TEXT").value == "") {
	            document.getElementById("rfvDEDUCTIBLE_TEXT").setAttribute("enabled", true);
	            document.getElementById("rfvDEDUCTIBLE_TEXT").style.display = "inline";
	            return false;
	        }
	        else {
	            alert(Alert); //("Deductible text updated");
	            window.close();
	            //return false;
	        }
	    }
	    function showMessage() {
	        var id = document.getElementById("hidParentDeductId").value;
	        document.getElementById('txtDEDUCTIBLE_TEXT').value = window.opener.document.getElementById(id).value;
	       // alert("Under Constraction");
	        //window.close();
	       // return false;
	    }
	    //Validates the maximum length for Deductible Text
	    function DEDUCTIBLE_TEXT_VALIDATE(source, arguments) {
	        var txtArea = arguments.Value;
	        if (txtArea.length > 250) {
	            arguments.IsValid = false;
	            document.getElementById("btnUpdate").setAttribute("disabled", true);
	            return false;
	        }
	        else {
	            document.getElementById("btnUpdate").setAttribute("disabled", false);
	        }
	    }
	</script>
</head>
<body oncontextmenu="return false;" onload="showMessage();">
    <form id="DEDUCTIBLE_TEXT" runat="server">
    <div>
     <table width="100%">
            <tr>							        
                <td class="headereffectCenter">
                    <asp:label id="lblHeader" Runat="server">View/Edit Deductible</asp:label>
                </td>
            </tr>	
            <tr>
                <td class="midcolorc">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr >
                <td class='midcolora' >
                    <asp:Label ID="capCOV_DESC" runat="server">Coverage:</asp:Label><span class="mandatory">*</span>
                    <br />
                    <asp:TextBox ID="txtCOV_DESC" width="450px" TextMode="MultiLine" runat="server"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="rfvCOV_DESC" runat="server" Display="Dynamic" ControlToValidate="txtCOV_DESC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class='midcolora'>
                    <asp:Label ID="capDEDUCTIBLE_TEXT" runat="server">Deductible Text:</asp:Label><span class="mandatory">*</span>
                    <br />
                    <asp:TextBox ID="txtDEDUCTIBLE_TEXT" width="450px" runat="server" TextMode="MultiLine" Rows="10" Columns="20" MaxLength="250"></asp:TextBox>                   
                    <asp:RequiredFieldValidator ID="rfvDEDUCTIBLE_TEXT" runat="server" Display="Dynamic" ControlToValidate="txtDEDUCTIBLE_TEXT" ErrorMessage=""></asp:RequiredFieldValidator>
                    <asp:customvalidator id="csvDEDUCTIBLE_TEXTS" ControlToValidate="txtDEDUCTIBLE_TEXT" Display="Dynamic" Runat="server"
										ClientValidationFunction="DEDUCTIBLE_TEXT_VALIDATE" ErrorMessage=""></asp:customvalidator><%--Deductible Text Length can't be greater than 250--%>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="midcolorr">                   
				    <cmsb:cmsbutton class="clsButton" id="btnUpdate" runat="server" Text="" 
                        ></cmsb:cmsbutton>					                
                </td>
            </tr>
                        <tr>
                <td>
                    <input id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"/>		    						
				    <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"/>								
				    <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server"/>
				    <input id="hidPOL_DEDUCT_ID" type="hidden" name="hidPOL_DEDUCT_ID" runat="server"/>
				    <input id="hidDEDUCT_ID" type="hidden" name="hidDEDUCT_ID" runat="server"/>
				    <input id="hidCOV_ID" type="hidden" name="hidCOV_ID" runat="server"/>
				    <input id="hidRISK_ID" type="hidden" name="hidRISK_ID" runat="server"/>
				    <input id="hidCOV_DESC" type="hidden" name="hidCOV_DESC" runat="server"/>
    			     <input id="hidPageTitle" type="hidden" name="hidPageTitle" runat="server" />
				     <input id="hidParentDeductId" type="hidden" name="hidParentDeductId" runat="server" />
				     <input id="hidAlert" type="hidden" name="hidAlert" runat="server"/>
				</td>            
            </tr>
        </table>   
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        try {
            document.title = document.getElementById('hidPageTitle').value;
        }
        catch (err) { }
    
    </script>
</body>
</html>

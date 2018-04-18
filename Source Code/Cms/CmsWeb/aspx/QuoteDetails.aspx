<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteDetails.aspx.cs" Inherits="Cms.CmsWeb.aspx.QuoteDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Quote Details</title>
     <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script> 
	
	<script language="javascript" type="text/javascript">

	    function AddData() {

	        //document.forms[0].reset();
	        document.getElementById('hid_ID').value = 'New';
	        //	        document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = -1;
	        //	       
	        //	        document.getElementById('txtCUSTOMER_FIRST_NAME').value = '';
	        //	        document.getElementById('txtCUSTOMER_MIDDLE_NAME').value = '';
	        //	        document.getElementById('txtCUSTOMER_LAST_NAME').value = '';       
	        //	        
	        //	        document.getElementById('txtDATE_OF_BIRTH').value = '';	        
	        //	        document.getElementById('cmbGENDER').options.selectedIndex = -1;
	        //	        document.getElementById('cmbAPPLICANT_OCCU').options.selectedIndex = 0;

	        //DisableValidators();

	        //Changing the color of mandatory controls
	        //ChangeColor();
	        //OnCustomerTypeChange();
	        //SSN_change();

	    }

	    function Initialize() {
	        // setTimeout("GetColorAgency()", 300);
	        HideShowClaims();
	        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
	            var tempXML = document.getElementById('hidOldXML').value;
	            if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
	                setMenu();

	                //Storing the XML in hidCUSTOMER_ID hidden fields
	                if (tempXML != undefined) {//Done for Itrack Issue 5454 on 20 April 09
	                    document.getElementById('hidOldData').value = tempXML;
	                }
	               
	            }
	            else {
	              
	                AddData();
	                //document.getElementById('hidCUSTOMER_ID').value = 'New';
	            }
	        }
	        
	    }
	        
	        function initPage() {
	         //alert();
	        Initialize();

	        }

	    function setValues() {
//debugger
////	        document.getElementById('hidPOLICY_LOB').value = document.getElementById('cmbMAKE').value;
//                alert(document.getElementById('cmbMAKE').value);
//               alert(document.getElementById('cmbMODEL').value);
//               alert(document.getElementById('cmbVEHICLE_TYPE').value);
                
	        if (document.getElementById('cmbMAKE').value == "") {
	            document.getElementById('cmbMODEL').innerHTML = "";
	            document.getElementById('cmbVEHICLE_TYPE').innerHTML = "";

	        }
	        if (document.getElementById('cmbMAKE').value != document.getElementById('hidMAKE').value) {
	            GetValues(document.getElementById('cmbMAKE').value);
	        }
	        else {
	            if (document.getElementById('cmbMODEL').value == "" || document.getElementById('cmbVEHICLE_TYPE').value == "") {
	                GetValues(document.getElementById('cmbMAKE').value);
	            }
	            else {
	                SetModel();
	                SetModelType();
	            }
	            //GetValues(document.getElementById('cmbMAKE').value);	           
	        }

	        //document.getElementById('txtAPP_EFFECTIVE_DATE').value = "";
	        //document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
	    }


	    function GetValues(MakeID) {
	        document.getElementById('hidMAKE').value = document.getElementById('cmbMAKE').value
	        if (MakeID != "" && MakeID != "0") {
	            var result = QuoteDetails.AjaxFetchVehicleModelType(MakeID);

	            //fillDTCombo(result.value, document.getElementById('cmbPOLICY_SUBLOB'), 'SUB_LOB_ID', 'SUB_LOB_DESC', 0);
	            fillDTCombo(result.value, document.getElementById('cmbMODEL'), 'ID', 'MODEL', 0);
	            fillDTCombo(result.value, document.getElementById('cmbVEHICLE_TYPE'), 'ID', 'MODEL_TYPE', 1);
	        }
	    }

	    function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
	        combo.innerHTML = "";
	        if (objDT != null) {

	            for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

	                if (i == 0) {
	                    oOption = document.createElement("option");
	                    oOption.value = "";
	                    oOption.text = "";
	                    combo.add(oOption);
	                }
	                oOption = document.createElement("option");
	                oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
	                oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
	                combo.add(oOption);
	            }
	        }
	    }

	    function SetModel() {	
            
	        if (document.getElementById('cmbMODEL').value != "") {
	            document.getElementById('hidMODEL').value = document.getElementById('cmbMODEL').value;
	            //alert(document.getElementById('cmbMODEL').value);
	            //document.getElementById("rfvMODEL").style.display = "none";
	        }
//////	        else {
//////	            document.getElementById("rfvMODEL").style.display = "inline";
//////	        }
	    }

	    function SetModelType() {	            
	        if (document.getElementById('cmbVEHICLE_TYPE').value != "") {
	            document.getElementById('hidMODEL_TYPE').value = document.getElementById('cmbVEHICLE_TYPE').value;
	        }
	    }

	    function ResetForm() {
	        //	        temp = 1;
	        document.QQ_QUOTE_DETAILS.reset();
	        //	        DisplayPreviousYearDesc();
	        //	        populateXML();
	        //	        BillType();
	        //	        DisableValidators();
	        //	        ChangeColor();


	        return false;
	    }

	    function HideShowClaims() {
	        
	        var isClaim = document.getElementById('cmbANY_CLAIM').options[document.getElementById('cmbANY_CLAIM').options.selectedIndex].value;

	        if (isClaim == 10963) {
	            //alert(isClaim);
	            document.getElementById("trClaim").style.display = "inline";
//	            document.getElementById("txtNO_OF_CLAIMS").value = "";
//	            document.getElementById("txtTOTAL_CLAIM_AMT").value = "";
	        }
	        else {
	            document.getElementById("trClaim").style.display = "none";
	            document.getElementById("txtNO_OF_CLAIMS").value = "0";
	            document.getElementById("txtTOTAL_CLAIM_AMT").value = "0";
	        }
	    }

	    function checkNum(data) {      // checks if all characters 
	        var valid = "0123456789.";     // are valid numbers or a "."
	        var ok = 1; var checktemp;
	        for (var i = 0; i < data.length; i++) {
	            checktemp = "" + data.substring(i, i + 1);
	            if (valid.indexOf(checktemp) == "-1") return 0;
	        }
	        return 1;
	    }
	    
	    function CurrencyFormator(claimamt){
	        //document.getElementById("txtTOTAL_CLAIM_AMT").value = formatCurrency(document.getElementById('txtTOTAL_CLAIM_AMT').value);
	        Num = claimamt;
	        dec = Num.indexOf(".");
	        end = ((dec > -1) ? "" + Num.substring(dec, Num.length) : ".00");
	        Num = "" + parseInt(Num);
	        var temp1 = "";
	        var temp2 = "";
	        if (checkNum(Num) == 0) {
	            //alert("This does not appear to be a valid number.");
	        }
	        else {
	            if (end.length == 2) end += "0";
	            if (end.length == 1) end += "00";
	            if (end == "") end += ".00";
	            var count = 0;
	            for (var k = Num.length - 1; k >= 0; k--) {
	                var oneChar = Num.charAt(k);
	                if (count == 3) {
	                    temp1 += ",";
	                    temp1 += oneChar;
	                    count = 1;
	                    continue;
	                }
	                else {
	                    temp1 += oneChar;
	                    count++;
	                }
	            }
	            for (var k = temp1.length - 1; k >= 0; k--) {
	                var oneChar = temp1.charAt(k);
	                temp2 += oneChar;
	            }
	            temp2 = temp2 + end;
	            //eval("document." + form + "." + field + ".value = '" + temp2 + "';");
	            document.getElementById("txtTOTAL_CLAIM_AMT").value = temp2;
	        }
	    }

	
	</script>
</head>
<body onload="initPage();">
    <form id="QQ_QUOTE_DETAILS" runat="server">
        <table align="center" width="99%">   
        
             <tr>
				<td class="midcolorc" align="right" colSpan="4">
				    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
				</td>
			</tr>         
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label2" runat="server" Text="Fields having * are mandatory fields"></asp:Label>
                </td>
            </tr>  
            
             <tr>
                <td class="midcolora" width="20%" >                  
                </td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >                    
                </td>
                <td class="midcolora" width="30%" ></td>
            </tr>
                     
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="capPOP" runat="server" Text="Particulars of Vehicle"></asp:Label>
                </td>  
            </tr>            
           
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capYEAR_OF_REG" runat="server" Text="Year of Registration"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbYEAR_OF_REG" runat="server"></asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfvYEAR_OF_REG" Runat="server" 
                    ControlToValidate="cmbYEAR_OF_REG" Display="Dynamic" 
                    ErrorMessage="Please select Year of Registration."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capMAKE" runat="server" Text="Make of Vehicle"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbMAKE" runat="server" onchange="setValues();"></asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfvMAKE" Runat="server" 
                    ControlToValidate="cmbMAKE" Display="Dynamic" 
                    ErrorMessage="Please select Vehicle Make."></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capMODEL" runat="server" Text="Model of Vehicle"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbMODEL" runat="server" Width="150px" onfocus="SelectComboIndex('cmbMODEL')" onchange="javascript:SetModel();"></asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfvMODEL" Runat="server" 
                    ControlToValidate="cmbMODEL" Display="Dynamic" 
                    ErrorMessage="Please select Vehicle Model."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >                    
                    <asp:Label ID="capVEHICLE_TYPE" runat="server" Text="Vehicle Type" ></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbVEHICLE_TYPE" runat="server" Width="150px" onchange="javascript:SetModelType();"></asp:DropDownList>
                    <br />
                    <asp:requiredfieldvalidator id="rfvVEHICLE_TYPE" Runat="server" 
                    ControlToValidate="cmbVEHICLE_TYPE" Display="Dynamic" 
                    ErrorMessage="Please select Vehicle Type."></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capENG_CAPACITY" runat="server" Text="Engine Capacity"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtENG_CAPACITY" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                    </br>
                    <asp:requiredfieldvalidator id="rfvENG_CAPACITY" Runat="server" 
                    ControlToValidate="txtENG_CAPACITY" Display="Dynamic" 
                    ErrorMessage="Please enter vehicle capacity."></asp:requiredfieldvalidator>
                    <asp:regularexpressionvalidator id="revENG_CAPACITY" runat="server" 
                    ControlToValidate="txtENG_CAPACITY" ErrorMessage="Please enter numeric value."
					Display="Dynamic" ValidationExpression="^\d{1,4}$"></asp:regularexpressionvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capNO_OF_DRIVERS" runat="server" Text="No. of named drivers"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbNO_OF_DRIVERS" runat="server"></asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfvNO_OF_DRIVERS" Runat="server" 
                    ControlToValidate="cmbNO_OF_DRIVERS" Display="Dynamic" 
                    ErrorMessage="Please select Number of named Driver."></asp:requiredfieldvalidator>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="20%" >&nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td id = "td1" class="midcolora" colspan="2">
                   <asp:Label ID="Label1" runat="server" Text="(For young and inexperienced drivers, we reserve our rights to vary the terms)"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label3" runat="server" Text="Claim History"></asp:Label>
                </td>  
            </tr> 
            
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capANY_CLAIM" runat="server" Text="Any claims in the last 3 years?"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbANY_CLAIM" runat="server" Height="16px" onchange="javascript:HideShowClaims();">
                    </asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfvANY_CLAIM" Runat="server" 
                    ControlToValidate="cmbANY_CLAIM" Display="Dynamic" 
                    ErrorMessage="Please select Any Claim."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >&nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            <tr id="trClaim" runat="server">
               <td class="midcolora" width="20%" >  
                    <asp:Label ID="capNO_OF_CLAIMS" runat="server" Text="No. of Claims"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                     <asp:TextBox ID="txtNO_OF_CLAIMS" runat="server" Width="100px"></asp:TextBox>
                     </br>
                    <asp:requiredfieldvalidator id="rfvNO_OF_CLAIMS" Runat="server" 
                    ControlToValidate="txtNO_OF_CLAIMS" Display="Dynamic" 
                    ErrorMessage="Please enter number of claims."></asp:requiredfieldvalidator>
                    <asp:regularexpressionvalidator id="revNO_OF_CLAIMS" runat="server" 
                    ControlToValidate="txtNO_OF_CLAIMS" ErrorMessage="Please enter numeric value."
					Display="Dynamic" ValidationExpression="^\d{1,2}$"></asp:regularexpressionvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capTOTAL_CLAIM_AMT" runat="server" Text="Total Claim Amount"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="Label4" runat="server" Text="S$"></asp:Label>
                    <asp:TextBox ID="txtTOTAL_CLAIM_AMT" CssClass="INPUTCURRENCY" runat="server" Width="100px" onblur="CurrencyFormator(this.value);"></asp:TextBox>
                    </br>
                    <asp:requiredfieldvalidator id="rfvTOTAL_CLAIM_AMT" Runat="server" 
                    ControlToValidate="txtTOTAL_CLAIM_AMT" Display="Dynamic" 
                    ErrorMessage="Please enter total claim amount."></asp:requiredfieldvalidator>
                     <asp:regularexpressionvalidator id="revTOTAL_CLAIM_AMT" runat="server" 
                    ControlToValidate="txtTOTAL_CLAIM_AMT" ErrorMessage="Please enter numeric value."
					Display="Dynamic" ValidationExpression="^-?(\.\d{0,9})?(\d{1,3},?(\d{3},?)*\d{3}(\.\d{0,9})?|\d{1,3}(\.\d{0,9})?)([kmKM])?$"></asp:regularexpressionvalidator>
                </td>
            </tr>
            
             <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label5" runat="server" Text="Cover Required"></asp:Label>
                </td>  
            </tr>            
            
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capCOVERAGE_TYPE" runat="server" Text="Type"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbCOVERAGE_TYPE" runat="server" Height="16px" Width="200px">
                    </asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfvCOVERAGE_TYPE" Runat="server" 
                    ControlToValidate="cmbCOVERAGE_TYPE" Display="Dynamic" 
                    ErrorMessage="Please select Coverage Type."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capNO_CLAIM_DISCOUNT" runat="server" Text="No Claim Discount Protection"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbNO_CLAIM_DISCOUNT" runat="server" Height="16px" Width="75px">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label6" runat="server" Text="Period of Insurance"></asp:Label>
                </td>  
            </tr>  
            
            <tr>
                 <td class="midcolora" width="20%" >
                    <asp:Label ID="capFROM" runat="server" Text="From(dd/mm/yyyy)"></asp:Label>                    
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtFROM_DAY" runat="server" Height="17px" Width="25px" ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtFROM_MONTH" runat="server" Height="17px" Width="25px" ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtFROM_YEAR" runat="server" Height="17px" Width="32px" ReadOnly="true"></asp:TextBox>
                </td>
                 <td class="midcolora" width="20%" >
                    <asp:Label ID="capTO" runat="server" Text="To(dd/mm/yyyy)"></asp:Label>                   
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtTO_DAY" runat="server" Height="17px" Width="25px" ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtTO_MONTH" runat="server" Height="17px" Width="25px" ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtTO_YEAR" runat="server" Height="17px" Width="32px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            
            
             <tr>
                <td class="midcolora" colspan="2">
                    <%--<cmsb:CmsButton class="clsButton" ID="btnBack" runat="server" Text="Back"></cmsb:CmsButton>--%>
                    <%--<cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>--%>
                    <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>
                </td>
                <td class="midcolora"  colspan="2" align="right"> 
                <table width="100%">
                        <tr>
                            <td align="right" width="100%">
                                <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                    Text="Save" OnClick="btnSave_Click"></cmsb:CmsButton>
                    <cmsb:CmsButton class="clsButton" ID="btnGetQuote" CausesValidation="true" runat="server"
                    Text="Get Quote" OnClick="btnGetQuote_Click"></cmsb:CmsButton>
                   <%-- <cmsb:CmsButton class="clsButton" ID="btnPrint" CausesValidation="true" runat="server"
                    Text="Print"></cmsb:CmsButton>--%>
                            </td>
                        </tr>
                </table> 
                   
                </td>
            </tr>
            
            <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>
			<input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>
			<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>			
			<input id="hid_ID" type="hidden" value="0" name="hid_ID" runat="server"/> 
			<input id="hidQQ_ID" type="hidden" value="0" name="hidQQ_ID" runat="server"/> 
			<input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server"/>
			<input id="hidOldData" type="hidden" name="Hidden1" runat="server"/> 
			<input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server"/>			
			<input id="hidOldXML" type="hidden" name="hidOldXML" runat="server"/>
			<input id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server"/>
			<input id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server"/> 
			<input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server"/>
			<input id="hidMsg" type="hidden" name="hidMsg" runat="server"/>
			<input id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server"/> 
			<input id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication" runat="server"/>
			<input id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server"/>	
			<input id="hidMODEL" type="hidden" name="hidMODEL" runat="server"/>
			<input id="hidMAKE" type="hidden" name="hidMAKE" runat="server"/>
			<input id="hidMODEL_TYPE" type="hidden" name="hidMODEL_TYPE" runat="server"/>
			
           
        </table>
    </form>
</body>
</html>


<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceDetailQQ.aspx.cs" Inherits="Cms.CmsWeb.aspx.InvoiceDetailQQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Personal Particulars</title>  
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script> 
    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
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
	        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
	            var tempXML = document.getElementById('hidOldXML').value;
	            if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
	                setMenu();

	                //Storing the XML in hidCUSTOMER_ID hidden fields 
	                if (tempXML != undefined)//Done for Itrack Issue 5454 on 20 April 09
	                    document.getElementById('hidOldData').value = tempXML;




	            }
	            else {
	                AddData();
	                //document.getElementById('hidCUSTOMER_ID').value = 'New';
	            }
	        }

	        //setTab();
	        //	        Check();
	        //This function will enable or disable the middle and last name depending upon
	        //the type of customer
	        //OnCustomerTypeChange();

	        //setEncryptFields();

	    }

	    function initPage() {
	        // alert();
	        Initialize();
	        //	        InsuranceScoreChange();
	        //	        ApplyColor();
	        //	        showButtons();
	        //	        LoadTitles();
	        //	        chkScore_clicked();
	        //	        RefreshClientTop();
	    }

	    function ResetForm() {
	        //    temp = 1;
	        DisableValidators();
	        document.MNT_VESSEL_MASTER.reset();
	        //    DisplayPreviousYearDesc();
	        populateXML();
	        //    BillType();

	        ChangeColor();


	        return false;
	    }
	    function FormatAmountForSum(num) {
	        num = ReplaceAll(num, ',', '.');
	        return num;
	    }

	    function fnformatCommission(value) {
	        var DecimalSep = ".";
	        if ($("#cmbPOLICY_CURRENCY option:selected").val() == "2")
	            DecimalSep = ",";
	        if (DecimalSep != 'undefined')
	            value = ReplaceAll(value, DecimalSep, ".");

	        if (isNaN(value)) return value;

	        if (value != "")
	            value = parseFloat(value).toFixed(2);
	        value = ReplaceAll(value, ".", DecimalSep)

	        return value;
	    }
	    function Validate(objSource, objArgs) {

	        var comm = parseFloat(FormatAmountForSum(document.getElementById(objSource.controltovalidate).value));

	      
	            if (comm < 0 || comm > 100) {
	                document.getElementById(objSource.controltovalidate).select();
	                objArgs.IsValid = false;
	            }
	            else
	                objArgs.IsValid = true;

	       

	    } 
	</script>
	
</head>
<BODY  oncontextmenu="return true;" leftMargin="0" topMargin="0" onload="initPage();">
    <form id="QQ_INVOICE_PARTICULAR_MARINE" runat="server">
        <table align="center" width="99%">
            <tr>
                <td colspan="4" align="center">           
                <asp:Label ID="capMessage" runat="server" 
                        Text="Marine Cargo Insurance Quotation" style="font-weight: 700; text-align:center"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td class="pageHeader" colspan="3">
                    <asp:Label ID="lblManHeader" runat="server" Style="display: none">Please note that all fields marked with * are mandatory</asp:Label>
                </td>
            </tr>

			<tr>
				<td class="midcolorc" align="right" colSpan="4">
				    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
				</td>
			</tr>
			<tr>
				<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are mandatory</TD>
			</tr>
            <tr>
                <td colspan="4">
                    <table width = "100%">
                        <tr>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="capDOQ" runat="server" Text="Date of Quotation"></asp:Label>
                            </td>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="lblDOQ" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="capAGENT" runat="server" Text="Agent/Broker"></asp:Label>
                            </td>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="lblAGENT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                        </tr>                       
                    </table>
                </td>
            </tr>
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="capPOP" runat="server" Text="Particulars of Proposer"></asp:Label>
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
            <tr id="trClientCode" runat="server" visible="false">
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capCLIENT_CODE" runat="server" Text="eProfessional Client Code"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtCUSTOMER_CODE" runat="server"></asp:TextBox>
                </td>
                <td class="midcolora" width="20%" ></td>
                <td class="midcolora" width="30%" ></td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capSTATUS" runat="server" Text="Type"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" > 
                    <asp:DropDownList ID="cmbCUSTOMER_TYPE" runat="server">
                    </asp:DropDownList><br />
                    <asp:requiredfieldvalidator id="rfvCUSTOMER_TYPE" Runat="server" ControlToValidate="cmbCUSTOMER_TYPE" Display="Dynamic" ErrorMessage="Please select Customer Type."></asp:requiredfieldvalidator>
                </td>
               <td class="midcolora" width="20%" >  
                    <asp:Label ID="capOPEN_COVER_NO" runat="server" Text="Open Cover No."></asp:Label>
                </td>
                <td class="midcolora" width="30%" > 
                    <asp:TextBox ID="txtOPEN_COVER_NO" runat="server"></asp:TextBox>
                    
                </td>
            </tr>

            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capCUSTOMER_FIRST_NAME" runat="server" Text="Company Name"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" > 
                    <asp:textbox id="txtCUSTOMER_FIRST_NAME" runat="server" size="25" maxlength="75"></asp:textbox><br />
								<asp:requiredfieldvalidator id="rfvCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
									Display="Dynamic" ErrorMessage="Please enter Company Name."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                   <asp:Label ID="capOCCUPATION_LIST" runat="server" Text="Business Type"></asp:Label>
                    <SPAN class="mandatory" id="SPAN5">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbAPPLICANT_OCCU" runat="server" Height="16px" Width="200px">
                    </asp:DropDownList></br>
                    <asp:requiredfieldvalidator id="rfvAPPLICANT_OCCU" runat="server" ControlToValidate="cmbAPPLICANT_OCCU" Display="Dynamic"	ErrorMessage="Please select Occupation."></asp:requiredfieldvalidator>
                </td>
            </tr>
             <tr>
                
                <td class="midcolora" width="20%" >
                <asp:Label ID="capINVOICE_TYPE" runat="server" Text="Invoice Type"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                 <asp:textbox id="txtINVOICE_TYPE" runat="server" size="25" maxlength="20"></asp:textbox><br />
								<asp:requiredfieldvalidator id="rfvINVOICE_TYPE" Runat="server" ControlToValidate="txtINVOICE_TYPE"
									Display="Dynamic" ErrorMessage="Please enter Invoice Type."></asp:requiredfieldvalidator>
                </td>
                 <td class="midcolora" width="20%" >
                <asp:Label ID="capINVOICE_AMOUNT" runat="server" Text="Invoice Amount"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                 <asp:textbox id="txtINVOICE_AMOUNT" runat="server" size="25" maxlength="75" CssClass="INPUTCURRENCY"></asp:textbox><br />
								<asp:requiredfieldvalidator id="rfvINVOICE_AMOUNT" Runat="server" ControlToValidate="txtINVOICE_AMOUNT"
									Display="Dynamic" ErrorMessage="Please enter Invoice Amount."></asp:requiredfieldvalidator>
                                    <asp:regularexpressionvalidator id="revINVOICE_AMOUNT" Runat="server" ControlToValidate="txtINVOICE_AMOUNT" Display="Dynamic"></asp:regularexpressionvalidator>
                </td>
            </tr>
             <tr>
                
                <td class="midcolora" width="20%" >
                <asp:Label ID="capCURRENCY_TYPE" runat="server" Text="Currency Type"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                  <asp:DropDownList ID="cmbCURRENCY_TYPE" runat="server">
                    </asp:DropDownList><br /><br />
								<asp:requiredfieldvalidator id="rfvCURRENCY_TYPE" Runat="server" ControlToValidate="cmbCURRENCY_TYPE"
									Display="Dynamic" ErrorMessage="Please enter Currency Type."></asp:requiredfieldvalidator>
                </td>
                 <td class="midcolora" width="20%" >
                <asp:Label ID="capBILLING_CURRENCY" runat="server" Text="Billing Currency"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                  <asp:DropDownList ID="cmbBILLING_CURRENCY" runat="server">
                    </asp:DropDownList><br /></asp:textbox><br />
								<asp:requiredfieldvalidator id="rfvBILLING_CURRENCY" Runat="server" ControlToValidate="cmbBILLING_CURRENCY"
									Display="Dynamic" ErrorMessage="Please enter Billing Currency."></asp:requiredfieldvalidator>
                                    
                </td>
            </tr>
             <tr>
                
                <td class="midcolora" width="20%" >
                <asp:Label ID="capMARK_UP_RATE_PERC" runat="server" Text="Mark Up Rate %"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                 <asp:textbox id="txtMARK_UP_RATE_PERC" runat="server" size="25" maxlength="75" onChange = "this.value=fnformatCommission(this.value)" onblur = "this.value=fnformatCommission(this.value)"></asp:textbox><br />
								<asp:requiredfieldvalidator id="rfvMARK_UP_RATE_PERC" Runat="server" ControlToValidate="txtMARK_UP_RATE_PERC"
									Display="Dynamic" ErrorMessage="Please enter Mark Up Rate %."></asp:requiredfieldvalidator>
                                     <asp:RegularExpressionValidator ID="revMARK_UP_RATE_PER" ControlToValidate="txtMARK_UP_RATE_PERC"
                                                Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="csvMARK_UP_RATE_PERC" Display="Dynamic" ControlToValidate="txtMARK_UP_RATE_PERC"
                                                ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>       
                </td>
                 <td class="midcolora" width="20%" >
               
                </td>
                <td class="midcolora" width="30%" >
                
                </td>
            </tr>
             <tr>
                <td class="midcolora" colspan="2">
                    <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>
                </td>
                <td class="midcolora" colspan="1">
                </td>
                <td class="midcolora" colspan="1">       
                    <table width="100%">
                        <tr>
                            <td align="right" width="100%">
                              <%-- <cmsb:CmsButton ID="btnSave" CssClass="clsbutton" Text="Save" runat="server" CausesValidation="true"
                            OnClick="btnSave_Click" />--%>
                            <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                    Text="Save" OnClick="btnSave_Click"></cmsb:CmsButton>
                            
                            </td>
                        </tr>
                    </table>              
                    
                </td>
            </tr>
            <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>
			<input id="hid_ID" type="hidden" value="0" name="hid_ID" runat="server"/> 
			<input id="hidCUST_PART_ID" type="hidden" value="0" name="hidCUST_PART_ID" runat="server"/> 
			<input id="hid_QUOTEID" type="hidden" value="0" name="hid_QUOTEID" runat="server"/> 
			<input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server"/>
			<input id="hidOldData" type="hidden" name="Hidden1" runat="server"/> 
			<input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server"/>
			<input id="Hidden1" type="hidden" name="Hidden1" runat="server"/> 
			<INPUT id="hidOldXML" type="hidden" name="hidOldXML" runat="server"/>
			<INPUT id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server"/>
			<INPUT id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server"/> 
			<input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server"/>
			<INPUT id="hidMsg" type="hidden" name="hidMsg" runat="server"/>
			<INPUT id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server"/> 
			<INPUT id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication" runat="server"/>
			<INPUT id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server"/>		
           
        </table>
    </form>
</body>
</html>

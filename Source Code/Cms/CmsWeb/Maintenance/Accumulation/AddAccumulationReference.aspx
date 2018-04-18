<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAccumulationReference.aspx.cs" Inherits="CmsWeb.Maintenance.AddAccumulationReference" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_ACCUMULATION_REFERENCE</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
        <script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language='javascript'>
		    var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		    function AddData() {
		        ChangeColor();
		        DisableValidators();
		        document.getElementById('hidDETAIL_TYPE_ID').value = 'New';
		      
		        if (document.getElementById('btnActivateDeactivate'))
		            document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
		       
		    }




		    function populateXML() {
		        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {


		            var tempXML;
		            if (document.getElementById('hidOldData') != null) {
		                tempXML = document.getElementById('hidOldData').value;
		               							 							
		                if (tempXML != "" && tempXML != 0) {
		                    populateFormData(tempXML, MNT_ACCUMULATION_REFERENCE);
		                    
		                }
		                else {
		                   
		                    if (document.getElementById('btnActivateDeactivate') != null)
		                        if (document.getElementById("btnActivateDeactivate"))
		                            document.getElementById('btnActivateDeactivate').style.display = "none";
		                    AddData();
		                }
		            }
		        }
		        if (document.getElementById('hidFormSaved').value == '3') {
		            //			if(document.getElementById('cmbTRANSACTION_CODE').options.selectedIndex!=-1)
		            //			{
		            //				document.getElementById('cmbTRANSACTION_CODE').value = document.getElementById('hidTRAN_CODE').value;
		            //			}


		        }



		    }

		    function SelectItem() {
		        //	if (document.getElementById("lstAssignedCrAcct").options[0])
		        //		document.getElementById("lstAssignedCrAcct").options[0].selected=true;
		        //	
		        //	if (document.getElementById("lstAssignedDrAcct").options[0])
		        //		document.getElementById("lstAssignedDrAcct").options[0].selected=true;

		        return false;
		    }




		    function ShowTcode() {
		        //	if (document.getElementById('hidTYPE_ID').value == 8)  //for Claim Transaction Code
		        //	{
		        //		document.getElementById('trTcode').style.display = "inline";
		        //	}
		        //	else
		        //	{
		        //		document.getElementById('trTcode').style.display = "none";
		        //		document.getElementById('rfvTRANSACTION_CODE').style.display = "none";
		        //		document.getElementById('rfvTRANSACTION_CODE').setAttribute("enabled",false);
		        //		document.getElementById('rfvTRANSACTION_CODE').setAttribute("isValid",false);
		        //	}

		    }


		    function GetValues() {

//		        var result = AddDefaultValueClaims.AjaxFetchExtraCoverages(document.getElementById('cmbLOSS_DEPARTMENT').value);

//		        fillDTCombo(result.value, document.getElementById('cmbLOSS_EXTRA_COVER'), 'COV_ID', 'COV_DES', 0);

		    }

		    function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {

//		        combo.innerHTML = "";
//		        if (objDT != null) {

//		            for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

//		                if (i == 0) {
//		                    oOption = document.createElement("option");
//		                    oOption.value = "";
//		                    oOption.text = "";
//		                    combo.add(oOption);
//		                }
//		                oOption = document.createElement("option");
//		                oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
//		                oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
//		                combo.add(oOption);
//		            }
//		        }
		    }

		    function SetExtraCover() {
//		        debugger;

//		        if (document.getElementById('cmbLOSS_EXTRA_COVER').value != "") {

//		            //var e = document.getElementById("cmbLOSS_EXTRA_COVER"); // select element
//		            //var strValue = e.options[e.selectedIndex].text;
//		            //alert(strValue);

//		            document.getElementById('hidExtraCover').value = document.getElementById("cmbLOSS_EXTRA_COVER").value;

//		        }
		    }

		    function ResetForm() {
		        //    temp = 1;
		        document.MNT_ACCUMULATION_REFERENCE.reset();
		        //    DisplayPreviousYearDesc();
		        populateXML();
		        //    BillType();
		        DisableValidators();
		        ChangeColor();


		        return false;
		    }

		    function setValues() {
		

		        if (document.getElementById('cmbLOB_ID').value == "") {
		        
		            document.getElementById('cmbCRITERIA_DESC').innerHTML = "";

		        }
		        if (document.getElementById('cmbLOB_ID').value != document.getElementById('hidLOB_ID').value) {
		        
		            GetValues(document.getElementById('cmbLOB_ID').value);
		        }
		        else {
		         
		            if (document.getElementById('cmbCRITERIA_DESC').value == "") {

		                GetValues(document.getElementById('cmbLOB_ID').value);
                        }
                        else{
		           
		                SetCriteria();

		            }

		        }

		    }

		    function GetValues(LOB_ID) {
		        document.getElementById('hidLOB_ID').value = document.getElementById('cmbLOB_ID').value
		        if (LOB_ID != "" && LOB_ID != "0") {
		            var result =AddAccumulationReference.AjaxFetchCriteria(LOB_ID)

		           
		            fillDTCombo(result.value, document.getElementById('cmbCRITERIA_DESC'), 'CRITERIA_ID', 'CRITERIA_DESC', 0);
		       
		        }
		    }

            //Added by Ruchika Chauhan on 2-March-2012 for TFS Bug # 3635
		    function SetAccRefNo() {
		        if (document.getElementById('hidLOB_ID').value != "") {
		            var result = AddAccumulationReference.AjaxGenerateAccumulationReferenceCode(document.getElementById('cmbLOB_ID').value);
		            document.getElementById('txtACC_REF_NO').value = result.value;
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

		    function SetCriteria() {
		  
		        if (document.getElementById('cmbCRITERIA_DESC').value != "") {
		            document.getElementById('hidCRITERIA_DESC').value = document.getElementById('cmbCRITERIA_DESC').value;
		            
		        }
		       
		    }

		    function CompareEffDateWithExpDate(objSource, objArgs) {
		   
		        var effdate = document.MNT_ACCUMULATION_REFERENCE.txtEFFECTIVE_DATE.value;
		        var expdate = document.MNT_ACCUMULATION_REFERENCE.txtEXPIRATION_DATE.value;
		        if (document.MNT_ACCUMULATION_REFERENCE.txtEFFECTIVE_DATE != null && document.MNT_ACCUMULATION_REFERENCE.txtEXPIRATION_DATE != null && expdate != "" && effdate != "") {
		            objArgs.IsValid = CompareTwoDate(expdate, effdate, jsaAppDtFormat);
		        }
		    }

		    function CompareTwoDate(DateFirst, DateSec, FormatOfComparision) {
		        var saperator = '/';
		        var firstDate, secDate;
		        var strDateFirst = DateFirst.split("/");
		        var strDateSec = DateSec.split("/");
		        if (FormatOfComparision.toLowerCase() == "dd/mm/yyyy") {
		            firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
		            secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0]) + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
		        }
		        if (FormatOfComparision.toLowerCase() == "mm/dd/yyyy") {
		            firstDate = DateFirst
		            secDate = DateSec;
		        }
		        firstDate = new Date(firstDate);
		        secDate = new Date(secDate);
		        firstSpan = Date.parse(firstDate);
		        secSpan = Date.parse(secDate);
		        if (firstSpan > secSpan)
		            return true; // first is greater
		        else
		            return false; // secound is greater
		    }

		    function CompareExpDateWithEffDate(objSource, objArgs) {
		        var effdate = document.MNT_ACCUMULATION_REFERENCE.txtEFFECTIVE_DATE.value;
		        var expdate = document.MNT_ACCUMULATION_REFERENCE.txtEXPIRATION_DATE.value;
		        if (document.MNT_ACCUMULATION_REFERENCE.txtEFFECTIVE_DATE != null && document.MNT_ACCUMULATION_REFERENCE.txtEXPIRATION_DATE != null && expdate != "" && effdate != "") {
		            objArgs.IsValid = CompareTwoDate(expdate, effdate, jsaAppDtFormat);
		        }
		    }	

		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();ShowTcode();'>
		<FORM id='MNT_ACCUMULATION_REFERENCE' method='post' runat='server'>

        <!-- Added by Agniswar to remove table structure -->

        <div class="midcolora"  style="width: 100%; display: block; border: 0px solid #000;">

			    
		</div>

        <!-- Till Here -->	
        
        
        <TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr id="trMessages" runat="server">
								<TD id="tdMessages" runat="server" class="pageHeader" colSpan="4">
                                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label>
                                </TD>
							</tr>
							<tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="4">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></asp:label>
                                </td>
							</tr>
                              <tr id="trLOB_ID" runat="server">
								<TD id="td1" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capLOB_ID" runat="server">Product</asp:Label>
                                    <span id="spnLOB_ID" runat="server" class="mandatory">*</span>
									<asp:DropDownList id="cmbLOB_ID" runat="server"  onchange="setValues();SetAccRefNo();" onblur="setValues();SetAccRefNo();" ></asp:DropDownList>
					                <asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

							<tr id="trACC_REF_NO" runat="server">
								<TD id="tdACC_REF_NO" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capACC_REF_NO" runat="server">Accumulation Reference Code</asp:Label>
                                    <span id="spnACC_REF_NO" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtACC_REF_NO' runat='server' size='20' maxlength='0'></asp:textbox><br>
				                    <asp:requiredfieldvalidator id="rfvACC_REF_NO" runat="server" 
                                    ControlToValidate="txtACC_REF_NO" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

                            <tr id="trCRITERIA_DESC" runat="server">
								<TD id="tdCRITERIA_DESC" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCRITERIA_DESC" runat="server">Criteria</asp:Label>
                                    <span id="spnCRITERIA_DESC" runat="server" class="mandatory">*</span>
									<asp:DropDownList id='cmbCRITERIA_DESC' runat='server' onchange="SetCriteria();" onblur="SetCriteria();" ></asp:DropDownList>
                                    <br />
					                <asp:requiredfieldvalidator id="rfvCRITERIA_DESC" runat="server" ControlToValidate="cmbCRITERIA_DESC" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

							<tr id="trCRITERIA_VALUE" runat="server">
								<TD id="tdCRITERIA_VALUE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCRITERIA_VALUE" runat="server">Criteria Value</asp:Label> 
                                    <span id="spnCRITERIA_VALUE" runat="server" class="mandatory">*</span>                               
									 <asp:textBox ID="txtCRITERIA_VALUE" runat="server"></asp:textBox>
                                     <asp:requiredfieldvalidator id="rfvCRITERIA_VALUE" runat="server" ControlToValidate="txtCRITERIA_VALUE" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>								
							</tr>
                            <tr id="trTREATY_CAPACITY_LIMIT" runat="server">
								<TD id="tdTREATY_CAPACITY_LIMIT" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capTREATY_CAPACITY_LIMIT" runat="server">Treaty Capacity Limit</asp:Label> 
                                    <span id="spnTREATY_CAPACITY_LIMIT" runat="server" class="mandatory">*</span>                               
									 <asp:textBox ID="txtTREATY_CAPACITY_LIMIT" runat="server" onblur="javascript:this.value=formatAmount(this.value,2)"></asp:textBox>
                                     <asp:requiredfieldvalidator id="rfvTREATY_CAPACITY_LIMIT" runat="server" ControlToValidate="txtTREATY_CAPACITY_LIMIT" Display="Dynamic"></asp:requiredfieldvalidator>
                                     <%--<asp:RegularExpressionValidator ID="revTREATY_CAPACITY_LIMIT" runat="server" ControlToValidate="txtTREATY_CAPACITY_LIMIT" Display="Dynamic" ValidationExpression="<%=RegExpDoublePositiveNonZeroNotLessThanOne %>"></asp:RegularExpressionValidator>--%>
								</TD>								
							</tr>
                             <tr id="trUSED_LIMIT" runat="server">
								<TD id="tdUSED_LIMIT" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capUSED_LIMIT" runat="server">Used Limit</asp:Label> 
                                   <%-- <span id="spnUSED_LIMIT" runat="server" class="mandatory">*</span>               --%>                
									 <asp:textBox ID="txtUSED_LIMIT" runat="server"  onblur="javascript:this.value=formatAmount(this.value,2)"></asp:textBox>
                                     <%--<asp:requiredfieldvalidator id="rfvUSED_LIMIT" runat="server" ControlToValidate="txtUSED_LIMIT" Display="Dynamic"></asp:requiredfieldvalidator>--%>
								</TD>								
							</tr>
                            <tr id="trEFFECTIVE_DATE" runat="server">
								<TD id="tdEFFECTIVE_DATE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capEFFECTIVE_DATE" runat="server">Effective Date</asp:Label> 
                                     <span id="spnEFFECTIVE_DATE" runat="server" class="mandatory">*</span>                            
									 <asp:TextBox ID="txtEFFECTIVE_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox>
                              
                                <asp:HyperLink ID="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot"> 
                                <asp:Image ID="imgEFFECTIVE_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                              
                                    </asp:Image>

                                </asp:HyperLink>
                                <br />
                                 <asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" Display="Dynamic" ErrorMessage="Please enter Effective Date"></asp:requiredfieldvalidator>
                                  <asp:customvalidator id="csvEFFECTIVE_DATE" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATE" Runat="server"
										ClientValidationFunction="CompareEffDateWithExpDate"></asp:customvalidator>
								</TD>								
							</tr>
                             <tr id="trEXPIRATION_DATE" runat="server">
								<TD id="tdEXPIRATION_DATE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capEXPIRATION_DATE" runat="server">Expiration Date</asp:Label> 
                                    <span id="spnEXPIRATION_DATE" runat="server" class="mandatory">*</span>  
									 <asp:TextBox ID="txtEXPIRATION_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox>
                                <asp:HyperLink ID="hlkEXPIRATION_DATE" runat="server" CssClass="HotSpot">
                                    <asp:Image ID="imgEXPIRATION_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">

                                    </asp:Image>
                                </asp:HyperLink>
                                <br />
                                 <asp:requiredfieldvalidator id="rfvEXPIRATION_DATE" runat="server" ControlToValidate="txtEXPIRATION_DATE" Display="Dynamic" ErrorMessage="Please enter Expiration Date."></asp:requiredfieldvalidator>
                                <asp:customvalidator id="csvEXPIRATION_DATE" Display="Dynamic" ControlToValidate="txtEXPIRATION_DATE"
									Runat="server" ClientValidationFunction="CompareExpDateWithEffDate"></asp:customvalidator>
                                    
								</TD>								
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									  <cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
					                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
						            Text="Deactivate" CausesValidation="False"   ></cmsb:cmsbutton>  
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>	
            <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
            <INPUT id="hidCRITERIA_DESC" type="hidden" value="0" name="hidCRITERIA_DESC" runat="server">	
		</FORM>
		<script>
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDETAIL_TYPE_ID').value);	
		</script>
	</BODY>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAccumulationCriteria.aspx.cs" Inherits="CmsWeb.Maintenance.AddAccumulationCriteria"  EnableViewState="true"%>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_ACCUMULATION_CRITERIA_MASTER</title>
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
		<script language='javascript'>
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
		                    populateFormData(tempXML, MNT_ACCUMULATION_CRITERIA_MASTER);
		                    
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
		        document.MNT_ACCUMULATION_CRITERIA_MASTER.reset();
		        //    DisplayPreviousYearDesc();
		        populateXML();
		        //    BillType();
		        DisableValidators();
		        ChangeColor();


		        return false;
		    }
		    
		    
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='ApplyColor();populateXML();'>
		<FORM id='MNT_ACCUMULATION_CRITERIA_MASTER' method='post' runat='server'>

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
							<tr id="trCRITERIA_CODE" runat="server">
								<TD id="tdCRITERIA_CODE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCRITERIA_CODE" runat="server">Criteria Code</asp:Label>
                                    <span id="spnCRITERIA_CODE" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtCRITERIA_CODE' runat='server' size='20' maxlength='0'></asp:textbox><br>
				                    <asp:requiredfieldvalidator id="rfvCRITERIA_CODE" runat="server" 
                                    ControlToValidate="txtCRITERIA_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

                            <tr id="trCRITERIA_DESC" runat="server">
								<TD id="tdCRITERIA_DESC" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCRITERIA_DESC" runat="server">Criteria Description</asp:Label>
                                    <span id="spnCRITERIA_DESC" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtCRITERIA_DESC' runat='server' size='50' maxlength='0'></asp:textbox><br>
					                <asp:requiredfieldvalidator id="rfvCRITERIA_DESC" runat="server" ControlToValidate="txtCRITERIA_DESC" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

							<tr id="trLOB_ID" runat="server">
								<TD id="tdLOB_ID" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capLOB_ID" runat="server">LOB_ID</asp:Label> 
                                    <span id="spnLOB_ID" runat="server" class="mandatory">*</span>                               
									 <asp:DropDownList ID="cmbLOB_ID" runat="server"></asp:DropDownList>
                                     <br />
                                     <asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" Display="Dynamic"></asp:requiredfieldvalidator>
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
                            <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>		
		</FORM>
		<script>
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDETAIL_TYPE_ID').value);	
		</script>
	</BODY>
</HTML>

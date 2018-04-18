<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="CurrencyMasterDetails.aspx.cs"
    Inherits="CmsWeb.Maintenance.CurrencyMasterDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
         <title>MNT_CURRENCY_MASTER</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
		<script language='javascript'>
		    function addRecord() {
		        ChangeColor();
		        DisableValidators();
		        document.getElementById('hidDETAIL_TYPE_ID').value = 'New';		      
		        if (document.getElementById('btnActivateDeactivate'))
		            document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
		    }
		    function AddData() {
		        addRecord();
		    }
		    function ResetTheForm() {
		        document.MNT_CURRENCY_MASTER.reset();		       
		        populateXML();		       
		        DisableValidators();
		        ChangeColor();
		        return false;
		    }
		    function populateXML() 
            {
		        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
		            var tempXML;
		            if (document.getElementById('hidOldData') != null) {
		                tempXML = document.getElementById('hidOldData').value;
		                if (tempXML != "" && tempXML != 0) {
		                    populateFormData(tempXML, MNT_CURRENCY_MASTER);
		                }
		                else {
		                    if (document.getElementById('btnActivateDeactivate') != null)
		                        if (document.getElementById("btnActivateDeactivate"))
		                            document.getElementById('btnActivateDeactivate').style.display = "none";
		                    addRecord();
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

            </script>
</head>
	<body leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<form id="MNT_CURRENCY_MASTER" method="post" runat="server">        
        <div class="midcolora"  style="width: 100%; display: block; border: 0px solid #000;">			    
		</div>      
        <TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='98%' border='0' align='center'>
							<tr id="trMessages" runat="server">
								<TD id="tdMessages" runat="server" class="pageHeader" colSpan="4">
                                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label>
                                </TD>
							</tr>
							<tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="4">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                </td>
							</tr>                            
							<tr id="trCURR_CODE" runat="server">
								<td id="tdCURR_CODE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCURR_CODE" runat="server">Currency Code</asp:Label>
                                    <span id="spnCURR_CODE" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtCURR_CODE' runat='server' size='20' maxlength='0' 
                                        style="margin-left: 44px" Width="131px"></asp:textbox><br>
				                    <asp:requiredfieldvalidator id="rfvCURR_CODE" runat="server" 
                                    ControlToValidate="txtCURR_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
								</td>								
							</tr>
                            <tr id="trCURR_DESC" runat="server">
								<td id="tdCURR_DESC" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCURR_DESC" runat="server">Currency Description</asp:Label> 
                                    <span id="spnCURR_DESC" runat="server" class="mandatory">*</span>									 
                                     <asp:textbox id='txtCURR_DESC' runat='server' size='50' maxlength='0' 
                                        style="margin-left: 10px" Width="133px"></asp:textbox><br>
                                     <asp:requiredfieldvalidator id="rfvCURR_DESC" runat="server" 
                                        ControlToValidate="txtCURR_DESC" Display="Dynamic"></asp:requiredfieldvalidator>
								</td>		
							</tr>	                         
                            <tr id="trCURR_SYMBOL" runat="server">
								<td id="tdCURR_SYMBOL" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capCURR_SYMBOL" runat="server">Currency Symbol</asp:Label>
                                    <span id="spnCURR_SYMBOL" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtCURR_SYMBOL' runat='server' size='50' maxlength='0' 
                                        style="margin-left: 32px" Width="132px"></asp:textbox><br>
					                <asp:requiredfieldvalidator id="rfvCURR_SYMBOL" runat="server" ControlToValidate="txtCURR_SYMBOL" Display="Dynamic"></asp:requiredfieldvalidator>
								</td>								
							</tr>                            					
							<tr>	
                            <td class='midcolora' colspan="2">
									  <cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>                                     
					                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text=""></cmsb:cmsbutton>														
								<td class='midcolorr' colspan="1">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="1" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="New" name="hidDETAIL_TYPE_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
                            <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>		
		</form>
		<script>
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDETAIL_TYPE_ID').value);	
		</script>
	</body>
</html>
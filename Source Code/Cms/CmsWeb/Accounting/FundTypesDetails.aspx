<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<%@ Page language="c#" Codebehind="FundTypesDetails.aspx.cs" AutoEventWireup="false" 
Inherits="Cms.CmsWeb.Maintenance.Accounting.FundTypesDetails" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_FUND_TYPES</title>
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
function AddData()
{
	ChangeColor();
	DisableValidators();
	document.getElementById('hidDETAIL_TYPE_ID').value	=	'New';
	//document.getElementById('txtDETAIL_TYPE_DESCRIPTION').focus();
	//document.getElementById('chkIS_SYSTEM_GENERATED').checked = false;
	if (document.getElementById('btnActivateDeactivate'))
	    document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
	//document.getElementById('txtDETAIL_TYPE_DESCRIPTION').value  = '';
	//$("#txtDETAIL_TYPE_DESCRIPTION").focus(); 
}




function populateXML()
{
	if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
		{

		   
			var tempXML;	 
			if(document.getElementById('hidOldData')!=null)
			{
				tempXML=document.getElementById('hidOldData').value;
				//alert(tempXML);									 							
				if(tempXML!="" && tempXML!=0)
				{	
					populateFormData(tempXML,MNT_FUND_TYPES);																
//					if(document.getElementById('chkIS_SYSTEM_GENERATED').checked==false)
//					{ 
//					  //Added by Sibin on 21 Oct 08 to eliminate null object
//					if(document.getElementById('btnActivateDeactivate')!=null)	
//						document.getElementById('btnActivateDeactivate').style.display = "inline";
//						
//					//Added by Sibin on 21 Oct 08 to eliminate null object
//					if(document.getElementById('btnSave')!=null)		
//						document.getElementById('btnSave').style.display = "inline";
//					}
//					else
//					{
//					  //Added by Sibin on 21 Oct 08 to eliminate null object
//					if(document.getElementById('btnActivateDeactivate')!=null)		
//						document.getElementById('btnActivateDeactivate').style.display = "none";
//						
//					//Added by Sibin on 21 Oct 08 to eliminate null object
//					if(document.getElementById('btnSave')!=null)		
//						document.getElementById('btnSave').style.display = "none";
//					}
				}
				else
				{     
				//Added by Sibin on 21 Oct 08 to eliminate null object
					if(document.getElementById('btnActivateDeactivate')!=null)	
						if(document.getElementById("btnActivateDeactivate"))
						   document.getElementById('btnActivateDeactivate').style.display = "none";
						AddData();
				}
			}
		}
		if(document.getElementById('hidFormSaved').value == '3')
		{
//			if(document.getElementById('cmbTRANSACTION_CODE').options.selectedIndex!=-1)
//			{
//				document.getElementById('cmbTRANSACTION_CODE').value = document.getElementById('hidTRAN_CODE').value;
//			}


}


		 
}

function SelectItem()
{
//	if (document.getElementById("lstAssignedCrAcct").options[0])
//		document.getElementById("lstAssignedCrAcct").options[0].selected=true;
//	
//	if (document.getElementById("lstAssignedDrAcct").options[0])
//		document.getElementById("lstAssignedDrAcct").options[0].selected=true;
		
	return false;
}




function ShowTcode()
{
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
    
    var result = AddDefaultValueClaims.AjaxFetchExtraCoverages(document.getElementById('cmbLOSS_DEPARTMENT').value);

        fillDTCombo(result.value, document.getElementById('cmbLOSS_EXTRA_COVER'), 'COV_ID', 'COV_DES', 0);
        
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

function SetExtraCover() {
    debugger;

    if (document.getElementById('cmbLOSS_EXTRA_COVER').value != "") {

        //var e = document.getElementById("cmbLOSS_EXTRA_COVER"); // select element
        //var strValue = e.options[e.selectedIndex].text;
        //alert(strValue);

        document.getElementById('hidExtraCover').value = document.getElementById("cmbLOSS_EXTRA_COVER").value;

    }
}

function ResetForm() {
//    temp = 1;
    document.MNT_FUND_TYPES.reset();
//    DisplayPreviousYearDesc();
    populateXML();
//    BillType();
    DisableValidators();
    ChangeColor();


    return false;
}
		</script>
	</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();ShowTcode();'>
		<FORM id='MNT_FUND_TYPES' method='post' runat='server'>

        <!-- Added by Agniswar to remove table structure -->

        <div class="midcolora"  style="width: 100%; display: block; border: 0px solid #000;">

			    <%--<div class="pageHeader" style="border-left: 0px solid #000; border-top: 0px solid #000; text-align:left">
				    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label>
			    </div>--%>
                <%--<div  class="midcolorc">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                </div>
                <br />--%>

                <%--<div id="divFUND_TYPE_CODE_CAP" runat="server" class="midcolora" style="width: 20%; height:auto; POSITION:relative;padding: 0.2em; float:left">--%>
                <%--<div id="divFUND_TYPE_CODE_CAP" runat="server">
                     <asp:Label id="capFUND_TYPE_CODE" runat="server">Fund Type Code</asp:Label>
                     <span id="spnFUND_TYPE_CODE" runat="server" class="mandatory">*</span>
                </div>--%>

                <%--<div id="divFUND_TYPE_CODE_TEXT" runat="server" class="midcolora" style="width: 80%; height:auto; POSITION:relative;padding: 0.2em; float:left">--%>
                <%--<div id="divFUND_TYPE_CODE_TEXT" runat="server">
                    <asp:textbox id='txtFUND_TYPE_CODE' runat='server' size='20' maxlength='0'></asp:textbox><br>
				    <asp:requiredfieldvalidator id="rfvFUND_TYPE_CODE" runat="server" ControlToValidate="txtFUND_TYPE_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
                </div>--%>

                <%--<div id="divFUND_TYPE_NAME_CAP" runat="server" class="midcolora" style="width: 20%; height:auto; POSITION:relative;padding: 0.2em; float:left">--%>
                <%--<div id="divFUND_TYPE_NAME_CAP" runat="server">
                     <asp:Label id="capFUND_TYPE_NAME" runat="server">Fund Type Name</asp:Label>
                     <span id="spnFUND_TYPE_NAME" runat="server" class="mandatory">*</span>
                </div>--%>
            
			
			    <%--<div id="divFUND_TYPE_NAME_TEXT" runat="server" class="midcolora" style="width: 80%; height:auto; POSITION:relative;padding: 0.2em; float:left">--%>
               <%-- <div id="divFUND_TYPE_NAME_TEXT" runat="server">
                        <asp:textbox id='txtFUND_TYPE_NAME' runat='server' size='50' maxlength='0'></asp:textbox><br>
					    <asp:requiredfieldvalidator id="rfvFUND_TYPE_NAME" runat="server" ControlToValidate="txtFUND_TYPE_NAME" Display="Dynamic"></asp:requiredfieldvalidator>               
			    </div>--%>

                <%--<div id="divFUND_TYPE_SOURCE_CAP" runat="server" class="midcolora" style="width: 20%; height:auto; POSITION:relative;padding: 0.2em; float:left">
               <%-- <div id="divFUND_TYPE_SOURCE_CAP" runat="server">
                    <asp:Label id="capFUND_TYPE_SOURCE" runat="server">Fund Type Source</asp:Label>
                        <%--<span id="spnFUND_TYPE_SOURCE" runat="server" class="mandatory">*</span>              
                </div>--%>

                <%--<div id="divFUND_TYPE_SOURCE_TEXT" runat="server" class="midcolora" style="width: 80%; height:auto; POSITION:relative;padding: 0.2em; float:left">--%>
                <%--<div id="divFUND_TYPE_SOURCE_TEXT" runat="server">
                         <asp:CheckBoxList class="midcolora" ID="chkFUND_TYPE_SOURCE" runat="server" Width="300px">
                            <asp:ListItem>DIRECT</asp:ListItem>
                            <asp:ListItem>DIRECT OTHERS</asp:ListItem>
                            <asp:ListItem>R/I INWARD OTHER THAN ASEAN</asp:ListItem>
                            <asp:ListItem>R/I INWARD ASEAN</asp:ListItem>
                        </asp:CheckBoxList>  
                </div>--%>

                <%--<div class="midcolora" style="width: 50%; height:auto; POSITION:relative;padding: 0.2em; float:left">
                       <cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
					    <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
						Text="Deactivate" CausesValidation="False"   ></cmsb:cmsbutton>  
                </div>--%>

                <%-- <div class="midcolora" style="width: 50%; height:auto; POSITION:relative;padding: 0.2em; float:left; text-align:right">
                      <cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>   
                </div>--%>
            
			    <%--<div style="border-left: 1px solid #000; border-bottom: 0px solid #000; border-top: none; clear: left;">
				    <span style="CLEAR:both;"></span>
			    </div>--%>

               <%-- <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
				<INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				<INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">--%>
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
							<tr id="trFUND_TYPE_CODE" runat="server">
								<TD id="tdFUND_TYPE_CODE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capFUND_TYPE_CODE" runat="server">Fund Type Code</asp:Label>
                                    <span id="spnFUND_TYPE_CODE" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtFUND_TYPE_CODE' runat='server' size='20' maxlength='0'></asp:textbox><br>
				                    <asp:requiredfieldvalidator id="rfvFUND_TYPE_CODE" runat="server" 
                                    ControlToValidate="txtFUND_TYPE_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

                            <tr id="trFUND_TYPE_NAME" runat="server">
								<TD id="tdFUND_TYPE_NAME" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capFUND_TYPE_NAME" runat="server">Fund Type Name</asp:Label>
                                    <span id="spnFUND_TYPE_NAME" runat="server" class="mandatory">*</span>
									<asp:textbox id='txtFUND_TYPE_NAME' runat='server' size='50' maxlength='0'></asp:textbox><br>
					                <asp:requiredfieldvalidator id="rfvFUND_TYPE_NAME" runat="server" ControlToValidate="txtFUND_TYPE_NAME" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>

							<tr id="trFUND_TYPE_SOURCE" runat="server">
								<TD id="tdFUND_TYPE_SOURCE" runat="server" class='midcolora' colspan="4">
									<asp:Label id="capFUND_TYPE_SOURCE" runat="server">Fund Type Source</asp:Label>                                
									 <asp:CheckBoxList class="midcolora" ID="chkFUND_TYPE_SOURCE" runat="server" Width="500px" valign="Top">
                                        <asp:ListItem>DIRECT</asp:ListItem>
                                        <asp:ListItem>DIRECT OTHERS</asp:ListItem>
                                        <asp:ListItem>R/I INWARD OTHER THAN ASEAN</asp:ListItem>
                                        <asp:ListItem>R/I INWARD ASEAN</asp:ListItem>
                                    </asp:CheckBoxList> 
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
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDETAIL_TYPE_ID').value);	
		</script>
	</BODY>
</HTML>

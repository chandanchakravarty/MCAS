
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUnderWritingAuthority.aspx.cs" Inherits="CmsWeb.Maintenance.AddUnderWritingAuthority" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<head>
		<title>MNT_UNDERWRITER_AUTHORITY</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
         <script language="javascript" type="text/javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
		<script language='javascript'>
		    function setURL() {
		        window.parent.location.href = "../addreinsurer.aspx?USERID=" + document.getElementById("hidCompany_ID").value;
		        //return false;
		    }
			
		    function AddData() {
		        ChangeColor();
		        DisableValidators();
		        document.getElementById('hidASSIGN_ID').value = 'New';

		        if (document.getElementById('btnActivateDeactivate'))
		            document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);

		    }




		    function populateXML() {
		        
		        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {


		            var tempXML;
		            if (document.getElementById('hidOldData') != null) {
		                tempXML = document.getElementById('hidOldData').value;

		                if (tempXML != "" && tempXML != 0) {
		                    populateFormData(tempXML, MNT_UNDERWRITER_AUTHORITY);

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
		       
               //Added by Ruchika Chauhan on 6-Feb-2012 for TFS # 3322
		        document.getElementById("txtPREMIUM_APPROVAL_LIMIT").value = formatAmount(document.getElementById("txtPREMIUM_APPROVAL_LIMIT").value, 2);
		        document.getElementById("txtPML_LIMIT").value = formatAmount(document.getElementById("txtPML_LIMIT").value, 2);

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
		        var result = AddUnderWritingAuthority.AjaxFetchLobByCountryId(document.getElementById('cmbCOUNTRTY_CODE').value);
		        fillDTCombo(result.value, document.getElementById('cmbLOB_ID'), 'LOB_ID', 'LOB_DESC', 0);

		    }


		    //Added by RC for TFS # 3322


		    function setTab() {
		        //alert(document.getElementById('hidFormSaved').value);
		        if (document.getElementById('cmbLOB_ID').value != "") {
		            Url = "UnderWritingAuthorityIndex.aspx?UserId=" + document.getElementById('hidUSER_ID').value + "&" + "CalledFrom=UnderWriterAuth" + "&" + "Lobid=" + document.getElementById('cmbLOB_ID').value+"&";
		            DrawTab(2, parent, 'View History', Url);
		        }

			}


            //Added till here


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
		        document.MNT_UNDERWRITER_AUTHORITY.reset();
		        //    DisplayPreviousYearDesc();
		        populateXML();
		        //    BillType();
		        DisableValidators();
		        ChangeColor();


		        return false;
		    }
		</script>
</head>
	<body leftMargin="0" topMargin="0" onload="GetValues();populateXML();ApplyColor();setTab();">
		<form id='MNT_UNDERWRITER_AUTHORITY' method='post' runat='server'>

        <!-- Added by Agniswar to remove table structure -->

     <%--   <div  style="width: 100%; display: block; border: 0px solid #000;">

			    
		</div>--%>

        <!-- Till Here -->	
        
        
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
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
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                </td>
							</tr>
							<tr id="trCOUNTRY_CODE" runat="server">
								<td id="tdCOUNTRY_CODE" runat="server" class='midcolora'>
									<asp:Label id="capCOUNTRY_CODE" runat="server">Country</asp:Label>
                                    <span id="spnCOUNTRY_CODE" runat="server" class="mandatory">*</span>
                                       </td>
                                    <td runat="server" class='midcolora'>
									 <asp:DropDownList ID="cmbCOUNTRTY_CODE" onchange='javascript:GetValues();' runat="server"></asp:DropDownList><br />
                                     <asp:requiredfieldvalidator id="rfvCOUNTRTY_CODE" runat="server" ControlToValidate="cmbCOUNTRTY_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
								</td>
                                <td id="tdLOB_ID" runat="server" class='midcolora'>
									<asp:Label id="capLOB_ID" runat="server">Product</asp:Label>
                                    <span id="spnLOB_ID" runat="server" class="mandatory">*</span>
                                    </td>
                                    <td id="Td1" runat="server" class='midcolora'>
									<asp:DropDownList ID="cmbLOB_ID" runat="server" onfocus="SelectComboIndex('cmbLOB_ID')"></asp:DropDownList>
                                    <br/>
					                <asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" Display="Dynamic"></asp:requiredfieldvalidator>
								</td>
								
							</tr>
                            
                           

							<tr id="trPML_LIMIT" runat="server">
								<TD id="tdPML_LIMIT" runat="server" class='midcolora'>
									<asp:Label id="capPML_LIMIT" runat="server">PML Limit(Treaty Currency)</asp:Label> 
                                  <%--  <span id="spnPML_LIMIT" runat="server" class="mandatory">*</span>--%>    
                                 </td>
                                 <td runat="server" class='midcolora'>                           
									 <asp:textbox id="txtPML_LIMIT" runat='server' size='10'  Columns="25" maxlength='0' onblur="javascript:this.value=formatAmount(this.value,2)"></asp:textbox> <br />
                                     <asp:requiredfieldvalidator id="rfvPML_LIMIT" runat="server" ControlToValidate="txtPML_LIMIT" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>	
                                <TD id="tdPREMIUM_APPROVAL_LIMIT" runat="server" class='midcolora'>
									<asp:Label id="capPREMIUM_APPROVAL_LIMIT" runat="server">Premium Approval Limit(Treaty Currency)</asp:Label> 
                                    </td>
                                    <td id="Td2"  runat="server" class='midcolora'>
                                  <%--  <span id="spnPREMIUM_APPROVAL_LIMIT" runat="server" class="mandatory">*</span> --%>                              
									 <asp:textbox id="txtPREMIUM_APPROVAL_LIMIT" runat='server' size='10' Columns="25" maxlength='0' onblur="javascript:this.value=formatAmount(this.value,2)"></asp:textbox><br />
                                     <asp:requiredfieldvalidator id="rfvPREMIUM_APPROVAL_LIMIT" runat="server" ControlToValidate="txtPREMIUM_APPROVAL_LIMIT" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>							
							</tr>
                            <%--Added by Ruchika on 3-Feb-2012 for TFS Bug # 3322--%>
                      <tr>
                            <td id="tdEffectiveDate"  class='midcolora' runat="server">
                            <asp:Label ID="capEffectiveDate" runat="server" >Effective Date</asp:Label>
                            <span id="spnEffectiveDate" class="mandatory" runat="server">*</span>                            
                            </td>
                            <td class='midcolora'>
                          <asp:TextBox ID="txtEffectiveDate" runat="server" ></asp:TextBox>
                          <asp:HyperLink ID="hlkEffectiveDate" runat="server" CssClass="HotSpot" Display="Dynamic" >
                                                <asp:Image ID="imgEffectiveDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>   <br /> 
                          <asp:RequiredFieldValidator ID="rfvEffectiveDate" Display="Dynamic" runat="server" ControlToValidate="txtEffectiveDate"></asp:RequiredFieldValidator>
                            </td>
                            <td id="tdEndDate" runat="server" class='midcolora'>
                             <asp:Label ID="capEndDate" runat="server" >End Date</asp:Label>
                            <span id="spnEndDate" class="mandatory" runat="server">*</span>   
                            </td>
                            <td class='midcolora'>
                             <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                          <asp:HyperLink ID="hlkEndDate" runat="server" CssClass="HotSpot" Display="Dynamic" >
                                                <asp:Image ID="imgEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink> <br /> 
                          <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ></asp:RequiredFieldValidator>
                          <br />
                          <asp:CompareValidator ID="cpvEndDate" runat="server" ControlToValidate ="txtEndDate" ControlToCompare="txtEffectiveDate" Operator="GreaterThan" Type="Date" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            </tr>
                           <%--Added till here--%>
                         <tr id="trCLAIM_RESERVE_LIMIT" runat="server">
								<TD id="tdCLAIM_RESERVE_LIMIT" runat="server" class='midcolora'>
									<asp:Label id="capCLAIM_RESERVE_LIMIT" runat="server">Claim Reserve(Base Currency)</asp:Label> 
                                    <span id="spnCLAIM_RESERVE_LIMIT" runat="server" class="mandatory">*</span>     
                                    </td>                          
                                    <td runat="server" class='midcolora'>
									 <asp:textbox id="txtCLAIM_RESERVE_LIMIT" runat='server' size='10' onblur="javascript:this.value=formatAmount(this.value,2)" maxlength='0'></asp:textbox>
                                     <asp:requiredfieldvalidator id="rfvCLAIM_RESERVE_LIMIT" runat="server" ControlToValidate="txtCLAIM_RESERVE_LIMIT" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>	
                                <TD id="tdCLAIM_REOPEN" runat="server" class='midcolora'>
									<asp:Label id="capCLAIM_REOPEN" runat="server">Claim Reopen</asp:Label>    
									 <span id="spnCLAIM_REOPEN" runat="server" class="mandatory">*</span>
                                </td>
                                <td runat="server" class='midcolora'>
									<asp:DropDownList ID="cmbCLAIM_REOPEN" runat="server"></asp:DropDownList>
                                    <%-- <asp:requiredfieldvalidator id="rfvCLAIM_REOPEN" runat="server" ControlToValidate="cmbCLAIM_REOPEN" Display="Dynamic"></asp:requiredfieldvalidator>--%>
                                    <br/>
								</TD>	
                            </tr>
                                							
                              <tr id="trCLAIM_SETTLMENT_LIMIT" runat="server">
								<TD id="tdCLAIM_SETTLMENT_LIMIT" runat="server" class='midcolora'>
									<asp:Label id="capCLAIM_SETTLMENT_LIMIT" runat="server">Claim Settlement Limit(Base Currency)</asp:Label>    
									 <span id="spnCLAIM_SETTLMENT_LIMIT" runat="server" class="mandatory">*</span>
                                 </td>
                                 <td runat="server" class='midcolora'>
									 <asp:textbox id="txtCLAIM_SETTLMENT_LIMIT" runat='server' size='10' maxlength='0'  onblur="javascript:this.value=formatAmount(this.value,2)"></asp:textbox>
                                     <asp:requiredfieldvalidator id="rfvCLAIM_SETTLMENT_LIMIT" runat="server" ControlToValidate="txtCLAIM_SETTLMENT_LIMIT" Display="Dynamic">
                                     </asp:requiredfieldvalidator>

                                    <br/>
								</TD>	
                                <td runat="server" class='midcolora' colspan="2">
                                </td>							
							</tr>

							<tr>
								<td class='midcolora' colspan='2'>
									  <cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
					                <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
						            Text="Deactivate" CausesValidation="False" Enabled ="false"></cmsb:cmsbutton>  
								</td>
								<td class='midcolorr' colspan="2">
                                <%--<cmsb:CmsButton ID='btnViewHistory'  CausesValidation="false" runat="server" Text = "View History" class ="clsButton"></cmsb:CmsButton>--%>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidASSIGN_ID" type="hidden" value="0" name="hidASSIGN_ID" runat="server">
                            <INPUT id="hidUSER_ID" type="hidden" value="0" name="hidUSER_ID" runat="server">
				            <%--<INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">--%>
						</TABLE>
					</TD>
				</TR>
			</TABLE>		
		</FORM>
         <script >
             try 
             {
                 if (document.getElementById('hidFormSaved').value == "1") {

                     RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidASSIGN_ID').value);
                 }
             }
             catch (err) {
             }      
		</script>
		<%--<script type="text/javascript">
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidASSIGN_ID').value);	
		</script>--%>
	</BODY>

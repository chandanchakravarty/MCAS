<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="QuoteDriverInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.QuoteDriverInformation" validateRequest = "false" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Quote Driver Information</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		
        <script src="/cms/cmsweb/scripts/common.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
        <script src="/cms/cmsweb/scripts/calendar.js"></script>
        <script language="javascript">

            var jsaAppDtFormat = "<%=aAppDtFormat  %>";

            function ChkDOB(objSource, objArgs) 
            {
                var expdate = document.APP_Q_Driver_Info.txtDriverDOB.value;
                objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>", expdate, jsaAppDtFormat);
            }

            function EnableDisableDescByDriver(txtDesc, rfvDesc, spnDesc, flag) 
            {

                if (flag == false) {

                    if (rfvDesc != null) {
                        rfvDesc.setAttribute('enabled', false);
                        rfvDesc.setAttribute('isValid', false);
                        rfvDesc.style.display = "none";
                        spnDesc.style.display = "none";                      
                        txtDesc.className = "";
                    }                   			
                }
                else {
                    if (rfvDesc != null) {
                        rfvDesc.setAttribute('enabled', true);
                        rfvDesc.setAttribute('isValid', true);                       
                        spnDesc.style.display = "inline";                     						
                        txtDesc.className = "MandatoryControl";                       								
                    }                  								
                }

            }

            function CheckForDriverType(calledfor) 
            {
                var SelectedValue;

                if (document.getElementById('cmbDriverType').selectedIndex != '-1') 
                {
                    SelectedValue = document.getElementById('cmbDriverType').options[document.getElementById('cmbDriverType').selectedIndex].value;

                    if (SelectedValue == '11942')  // Does not operate cycle
                    {                       
                        EnableDisableDescByDriver(document.getElementById('txtDriverLicenseNo'), document.getElementById('rfvDriverLicenseNo'), document.getElementById('spnDriverLicenseNo'), false);                       
                        EnableDisableDescByDriver(document.getElementById('txtDriverDateLicensed'), document.getElementById('rfvDriverDateLicensed'), document.getElementById('spnDriverDateLicensed'), false);
                    }
                    else if (SelectedValue == '11941') // Operates cycle
                    {

                        EnableDisableDescByDriver(document.getElementById('txtDriverLicenseNo'), document.getElementById('rfvDriverLicenseNo'), document.getElementById('spnDriverLicenseNo'), true);
                        EnableDisableDescByDriver(document.getElementById('txtDriverDateLicensed'), document.getElementById('rfvDriverDateLicensed'), document.getElementById('spnDriverDateLicensed'), true);                       
                    }
                    else 
                    {
                        EnableDisableDescByDriver(document.getElementById('txtDriverLicenseNo'), document.getElementById('rfvDriverLicenseNo'), document.getElementById('spnDriverLicenseNo'), false);
                        EnableDisableDescByDriver(document.getElementById('txtDriverDateLicensed'), document.getElementById('rfvDriverDateLicensed'), document.getElementById('spnDriverDateLicensed'), false);                        
                    }
                }
                else 
                {
                    EnableDisableDescByDriver(document.getElementById('txtDriverLicenseNo'), document.getElementById('rfvDriverLicenseNo'), document.getElementById('spnDriverLicenseNo'), false);
                    EnableDisableDescByDriver(document.getElementById('txtDriverDateLicensed'), document.getElementById('rfvDriverDateLicensed'), document.getElementById('spnDriverDateLicensed'), false);                    
                }                
                ApplyColor();
                ChangeColor();
                return false;
            }

//            function test1() {

//                alert(Session["TotalDrivers"].toString());
//            }
				
		</script>
	</HEAD>
	<body onload="ApplyColor();ChangeColor();" MS_POSITIONING="GridLayout">
		<form id="APP_Q_Driver_Info" method="post" runat="server">			
			<table id="tblMVR" width="100%" align="center">
				<tr>
					<td class="midcolora" align="left"><h1 align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg">Please enter Quote Driver Information</asp:label></h1></td>
				</tr>
				<tr>
					</tr>
			</table>
			<table width="100%" align="center">
			
				<tr>
                <td class="midcolora" align="left"><asp:Label ID="capDriverName" runat="server"></asp:Label><span class="mandatory">*</span></td>
                <td class="midcolora" align="left"><asp:TextBox ID="txtDriverName" runat="server"></asp:TextBox>
                <asp:requiredfieldvalidator id="rfvDriverName" runat="server" ControlToValidate="txtDriverName" Display="Dynamic"></asp:requiredfieldvalidator></td>
					
				</tr>
				<tr>
                <td class="midcolora" align="left"><asp:Label ID="capDriverCode" runat="server"></asp:Label><span class="mandatory">*</span></td>
                <td class="midcolora" align="left"><asp:TextBox ID="txtDriverCode" runat="server"></asp:TextBox>
                <asp:requiredfieldvalidator id="rfvDriverCode" runat="server" ControlToValidate="txtDriverCode" Display="Dynamic"></asp:requiredfieldvalidator></td>
					
				</tr>
				<tr>
               <TD class="midcolora" align="left"><asp:label id="capDriverGender" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" align="left"><asp:dropdownlist id="cmbDriverGender" onfocus="SelectComboIndex('cmbDriverGender')" runat="server">
											<asp:ListItem Value='M'>Male</asp:ListItem>
											<asp:ListItem Value='F'>Female</asp:ListItem>
										</asp:dropdownlist>
               
				</tr>
				<tr>
                <td class="midcolora" align="left">
                <asp:Label ID="capDriverDOB" runat="server"></asp:Label><span class="mandatory">*</span></td>
                <td class="midcolora" align="left">
                <asp:TextBox ID="txtDriverDOB" runat="server"></asp:TextBox>
                <asp:hyperlink id="hlkDriverDOB" runat="server" CssClass="HotSpot">
				<asp:image id="imgDriverDOB" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:image>
				</asp:hyperlink>
                <br>
				<asp:regularexpressionvalidator id="revDriverDOB" runat="server" Display="Dynamic"
					ControlToValidate="txtDriverDOB"></asp:regularexpressionvalidator>
				<asp:requiredfieldvalidator id="rfvDriverDOB" runat="server" Display="Dynamic"
					ControlToValidate="txtDriverDOB"></asp:requiredfieldvalidator>
				<asp:customvalidator id="csvDriverDOB" Display="Dynamic" ControlToValidate="txtDriverDOB" Runat="server"
					ClientValidationFunction="ChkDOB"></asp:customvalidator>
				</td>									
                </tr>  
                <tr>
                <TD class="midcolora" align="left"><asp:Label id="capDriverType" runat="server"></asp:Label></TD>
                <TD class="midcolora" align="left"><asp:DropDownList id="cmbDriverType" runat="server"  OnFocus="SelectComboIndex('cmbDriverType')" onchange="CheckForDriverType(0);"></asp:DropDownList></TD></tr>           
				<tr>
                <td class="midcolora" align="left"><asp:Label ID="capDriverLicenseNo" runat="server"></asp:Label>
                <span class="mandatory" id="spnDriverLicenseNo" runat="server">*</span></td>
                
                <td class="midcolora" align="left"><asp:TextBox ID="txtDriverLicenseNo" runat="server"></asp:TextBox>
                  <asp:requiredfieldvalidator id="rfvDriverLicenseNo" runat="server" ControlToValidate="txtDriverLicenseNo" Display="Dynamic"></asp:requiredfieldvalidator></td>
                </tr>

                <tr>
                 <td class="midcolora" align="left"><asp:Label ID="capDriverDateLicensed" runat="server"></asp:Label><span class="mandatory" id="spnDriverDateLicensed" runat="server">*</span></td>
                 <td class="midcolora" align="left"><asp:TextBox ID="txtDriverDateLicensed" runat="server"></asp:TextBox>
                <asp:hyperlink id="hlkDriverDateLicensed" runat="server" CssClass="HotSpot" >
				<asp:image id="imgDriverDateLicensed" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:image>
				</asp:hyperlink>
                <br />
                <asp:regularexpressionvalidator id="revDriverDateLicensed" runat="server" Display="Dynamic"
					ControlToValidate="txtDriverDateLicensed"></asp:regularexpressionvalidator>
				<asp:requiredfieldvalidator id="rfvDriverDateLicensed" runat="server" Display="Dynamic" 
					ControlToValidate="txtDriverDateLicensed"></asp:requiredfieldvalidator>
				<asp:customvalidator id="csvDriverDateLicensed" Display="Dynamic" ControlToValidate="txtDriverDateLicensed" Runat="server"
					ClientValidationFunction="ChkDOB"></asp:customvalidator>
                </td>
                </tr>
                <tr>
                <TD class="midcolora" align="left"><asp:label id="capDriverDrugViolation" runat="server"></asp:label><span class="mandatory" id="spnDriverDrugViolation"</span></TD>
                <TD class="midcolora" align="left"><asp:dropdownlist id="cmbDriverDrugViolation" onfocus="SelectComboIndex('cmbDriverDrugViolation')" runat="server">
                         
                </asp:dropdownlist><BR>									
			    <asp:requiredfieldvalidator id="rfvDriverDrugViolation" runat="server" ControlToValidate="cmbDriverDrugViolation" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
                </tr>
               <tr>
                
              <td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" 
                      CausesValidation ="false" id="btnReset" runat="server" Text="Reset" 
                      onclick="btnReset_Click"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnSave" 
                      runat="server" Text="Delete" causesvalidation="false" onclick="btnSave_Click"></cmsb:cmsbutton>
									</td>
               </tr>
			</table>
			<INPUT id="hidKeyValues" type="hidden" name="hidKeyValues" runat="server">
            <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
            <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
            <input id="hidQUOTE_ID" type="hidden" value="0" name="hidQUOTE_ID" runat="server">
            <input id="hidDRIVER_TYPE" type="hidden" name="hidDRIVER_TYPE" runat="server">
            <input id="hidDRIVER_DRUG_VIOLATION" type="hidden" name="hidDRIVER_DRUG_VIOLATION" runat="server">
		</form>
		<script>
		</script>
	</body>
</HTML>

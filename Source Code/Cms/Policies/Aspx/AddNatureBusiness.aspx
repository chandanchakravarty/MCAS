<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNatureBusiness.aspx.cs" Inherits="Cms.Policies.Aspx.AddNatureBusiness" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL_NATURE_OF_BUSINESS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		 <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >  
             
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
        <script src="/cms/cmsweb/scripts/Calendar.js"></script>
       

        <script type="text/javascript" language="javascript">
            function DateComparer(DateFirst, DateSec) {
               
//                var saperator = '/';
//                var firstDate, secDate;               

//                var strDateFirst = DateFirst.split("/");
//                var strDateSec = DateSec.split("/");
//                                   
//                firstDate = DateFirst
//                secDate = DateSec;
//                firstDate = new Date(firstDate);
//                secDate = new Date(secDate);

//                firstSpan = Date.parse(firstDate);
//                secSpan = Date.parse(secDate);
//              

//                // var effdate = document.getElementById("txtBUSINESS_START_DATE").value;
//                if (firstSpan > secSpan) {

//                    
//                    return true; // first is greater
//                }
//                else {

//                    //alert("Invalid Valid bsuiness" + firstSpan);
//                    document.getElementById('csvBusiness_Date').innerText = "Business date can not be future date"
//                    document.getElementById('csvBusiness_Date').style.display = 'inline';
//                    return false;

//                }   // secound is greater

            }
            function ChkBackDate(objSource, objArgs) {
                //debugger;
                
                if (document.getElementById("revBusiness_Date").isvalid == true) {
                    
                    var busnessdate = document.getElementById("txtBUSINESS_START_DATE").value;
                    //objArgs.IsValid = DateComparer("<%=DateTime.Now%>", busnessdate);
                }
                else
                    objArgs.IsValid = true;
            }
            function ResetForm() {

                document.POL_NATURE_OF_BUSINESS.reset();                
                //populateXML();
                //    BillType();
               // populateXML();
                //    BillType();
               // DisableValidators();
                //ChangeColor();            
                
                return false;
            }

            function allnumeric(objSource, objArgs) {//debugger
//                var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
//                var inputxt = document.getElementById('txtBUSINESS_START_DATE').value;
//                if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
//                    document.getElementById('revBusiness_Date').setAttribute('enabled', true);

//                    objArgs.IsValid = true;
//                }
//                else {
//                    document.getElementById('revBusiness_Date').setAttribute('enabled', true);
//                    document.getElementById('revBusiness_Date').style.display = 'none';
//                    document.getElementById('csvBusiness_Date').setAttribute('enabled', true);
//                    document.getElementById('csvBusiness_Date').style.display = 'inline';
//                    objArgs.IsValid = false;
//                }
//                if (objArgs.IsValid = true) {
//                    ChkFutureDate();
//                }
            }

            function ChkOccurenceDate_YEAR_BUILT(objSource, objArgs) {

                var effdate = document.getElementById('txtBUSINESS_START_DATE').value;
                var date = '<%=DateTime.Now.Year%>';

               
                if (effdate.length < 4 || effdate.length != 4 || effdate == 0 || effdate < 1900 || effdate > 2100) {
                    objArgs.IsValid = false;


                }
                else {
                    if (effdate > date)
                        objArgs.IsValid = false;
                    else
                        objArgs.IsValid = true;
                }
               
            }
        </script>
</head>

<%--<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();ShowTcode();'>--%>
<BODY leftMargin='0' topMargin='0' onload='ApplyColor();' >
		<FORM id='POL_NATURE_OF_BUSINESS' method='post' runat='server'>

        <!-- Added by Agniswar to remove table structure -->

      <%--  <div class="midcolora"  style="width: 100%; display: block; border: 0px solid #000;">

			    
		</div>--%>

        <!-- Till Here -->	
        
        
    
    

   <table width="100%" border="0" cellpadding="0" cellspacing="0">
				<TR>
					<TD>
        <TABLE width='100%' border='0' align='center'>

                <tr>
                    <td class="headereffectCenter" colspan="4">
                        <asp:Label ID="lblHeader" runat="server">Nature of Business</asp:Label>
                    </td>
                </tr>

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
                           
                <tr id="trBusines_Nature" runat="server">
                    <TD id="tdFUND_TYPE_CODE" runat="server" class='midcolora' width="33%">
                        <asp:Label id="capBusines_Nature" runat="server">Nature of Business</asp:Label><span class="mandatory">*</span>  
                        <br />					
                        <asp:DropDownList id='cmbBusines_Nature' runat='server' Height="16px" Width="145px"></asp:DropDownList><br />
                        <asp:requiredfieldvalidator id="rfvBusines_Nature" runat="server" ControlToValidate="cmbBusines_Nature" ErrorMessage="Please select nature of business."
				Display="Dynamic" ></asp:requiredfieldvalidator>
                    </TD>
                    <TD id="tdRetail_Store" runat="server" class='midcolora' width="33%">
                        <asp:Label id="capRetail_Stores" runat="server">Retail Stores or Service Operation % of Total Sales</asp:Label> 
                        <br />	
                        <asp:textbox id='txtRetail_Stores' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox><br />
                        <asp:RegularExpressionValidator ID="revRetail_Stores" runat="server" 
                        ControlToValidate="txtRetail_Stores" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvRetail_Stores" runat="server" ControlToValidate="txtRetail_Stores" display="Dynamic"
                        MaximumValue="100" MinimumValue="0" Type="Currency"></asp:RangeValidator>
                    </TD>
                <TD id="tdPrimary_Operation" runat="server" class='midcolora'  width="33%">
                    <asp:Label id="capPrimary_Operation" runat="server">Description of All Operations</asp:Label>                                    
                    <br />					
									
                    <asp:textbox id='txtPrimary_Operation' runat='server' size='50' maxlength='0' 
                    Width="200px"></asp:textbox>
                </TD>
                </tr>
								
            <tr id="trPrimary_Operation" runat="server">
								
                <TD id="tdInstallation_Service" runat="server" class='midcolora'  width="33%">
                    <asp:Label id="capInstallation_Service" runat="server">Installation, Service or Repair Work %</asp:Label>                                    
                    <br />					
									  
                    <asp:textbox id='txtInstallation_Service' runat='server' size='50' maxlength='0' 
                    Width="200px"></asp:textbox><br />
                    <asp:RegularExpressionValidator ID="revInstallation_Service" runat="server" 
                    ControlToValidate="txtInstallation_Service" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="rvInstallation_Service" runat="server" ControlToValidate="txtInstallation_Service" display="Dynamic"
                    MaximumValue="100" MinimumValue="0" Type="Currency"></asp:RangeValidator>
                </TD>
                    <TD id="td1" runat="server" class='midcolora' width="33%">
                    <asp:Label id="capBusiness_Date1" runat="server">Year Business Started</asp:Label><span class="mandatory">*</span>                                    
                    <br />					
                    <%--<asp:textbox id='txtBUSINESS_START_DATE' runat='server' size='50' maxlength='0' onblur="FormatDate();ValidatorOnChange();"
                    Width="178px"></asp:textbox>--%>
                    <asp:textbox id='txtBUSINESS_START_DATE' runat='server' size='50' maxlength='0'
                    Width="178px"></asp:textbox>
                    <%--<asp:HyperLink ID="hlkBusiness_Date" runat="server" CssClass="HotSpot"> 
                    <asp:Image ID="imgBusiness_Date" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>                                 
                    </asp:HyperLink>--%>
                    <br />
                    <asp:requiredfieldvalidator id="rfvBusiness_Date" runat="server" ControlToValidate="txtBUSINESS_START_DATE" ErrorMessage="Please enter Business Date"
													Display="Dynamic"></asp:requiredfieldvalidator>
                  <%--  <asp:RegularExpressionValidator ID="revBusiness_Date" runat="server" Display="Dynamic"
                    ControlToValidate="txtBUSINESS_START_DATE" ></asp:RegularExpressionValidator> --%>
                     <asp:customvalidator id="csvBUSINESS_START_DATE" Runat="server" ControlToValidate="txtBUSINESS_START_DATE" ErrorMessage="Please enter Valid Year" ClientValidationFunction="ChkOccurenceDate_YEAR_BUILT" Display="Dynamic"></asp:customvalidator>
                  
				                   
                </TD>
                <TD id="td2" runat="server" class='midcolora' width="33%" >
                    <asp:Label id="capPremise_Installation" runat="server">Off  Premises Installation, Service or Repair Work %</asp:Label>                                    
                    <br />					
								  
                    <asp:textbox id='txtPremise_Installation' runat='server' size='50' maxlength='0' 
                    Width="200px"></asp:textbox><br />
                    <asp:RegularExpressionValidator ID="revPremise_Installation" runat="server" 
                    ControlToValidate="txtPremise_Installation" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="rvPremise_Installation" runat="server" ControlToValidate="txtPremise_Installation" display="Dynamic"
                    MaximumValue="100" MinimumValue="0" Type="Currency"></asp:RangeValidator>
                </TD>
            </tr>
        <%--    <tr id="trDate_Business" runat="server">
								
								
        </tr>--%>


        <tr id="trOther_Operation" runat="server">
        <TD id="tdOther_Operation" runat="server" class='midcolora' width="33%">

        <asp:Label id="capOther_Operation" runat="server">Notes to Underwriters</asp:Label>                                    
        <br />					
                                   						
        <asp:textbox id='txtOther_Operation' runat='server' size='50' maxlength='0' 
        Width="200px"></asp:textbox>
                                                                     
                                    				                   
        </TD>
        <td class='midcolora' width="33%"></td>
        <td class='midcolora' width="33%"></td>
								
        </tr>
                           

        <tr>
        <td class='midcolora' valign="bottom">
        <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
        </td>
        <td class='midcolora'> </td> 	
                                   
        <td class="midcolorr">							
        <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></cmsb:cmsbutton>                            
       
                                       
        </td>
                                
        </tr>

							

							
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
                            <INPUT id="hidBusiness_ID" type="hidden" value="0" name="hidBusiness_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>		
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCoinsuranceDetails.aspx.cs" Inherits="Cms.Claims.Aspx.AddCoinsuranceDetails" %>
<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CoinsuranceDetails</title>
    
       <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		
	<script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script> 

	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 


    <script type="text/javascript" language="javascript">
       var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
         function initPage() {

             ApplyColor();


         }
         function ResetTheForm() {
             document.CoinsuranceDetails.reset();
             return false;
         }

         function ValidateCLAIM_REGISTRATION_DATE(objSource, objArgs) {


           var IsLegalClaim = document.getElementById('hidLitigation').value;

           var RegDate = document.getElementById(objSource.controltovalidate).value;
           var PolicyEffDate = document.getElementById('hidEffDate').value;
           var PolicyExpDate = document.getElementById('hidExpDate').value;
           // DiseaseDate = new Date(DiseaseDate);
           var TodayDate = '<%=DateTime.Now.ToShortDateString()%>';
           //   TodayDate = new Date(TodayDate);

           var aDateArr;
           var strDay;
           var strMonth;
           var strYear;
           var sDiseaseDate;
           var sTodayDate;
           var sPolicyEffDate;
           var sPolicyExpDate;

           //Added for Multilingual Support
           if (sCultureDateFormat == 'DD/MM/YYYY') 
           {
               aDateArr = RegDate.split('/');

               strDay = aDateArr[0];
               strMonth = aDateArr[1];
               strYear = aDateArr[2];

               sDiseaseDate = strMonth + '/' + strDay + '/' + strYear

               aDateArr = TodayDate.split('/');
               strDay = aDateArr[0];
               strMonth = aDateArr[1];
               strYear = aDateArr[2];
               sTodayDate = strMonth + '/' + strDay + '/' + strYear

               aDateArr = PolicyEffDate.split('/');
               strDay = aDateArr[0];
               strMonth = aDateArr[1];
               strYear = aDateArr[2];
               sPolicyEffDate = strMonth + '/' + strDay + '/' + strYear

               aDateArr = PolicyExpDate.split('/');
               strDay = aDateArr[0];
               strMonth = aDateArr[1];
               strYear = aDateArr[2];
               sPolicyExpDate = strMonth + '/' + strDay + '/' + strYear

               PolicyEffDate = new Date(sPolicyEffDate);
               PolicyExpDate = new Date(sPolicyExpDate);
               TodayDate = new Date(sTodayDate);
               RegDate = new Date(sDiseaseDate);
           }
           else 
           {
               PolicyEffDate = new Date(PolicyEffDate);
               PolicyExpDate = new Date(PolicyExpDate);
               TodayDate = new Date(TodayDate);
               RegDate = new Date(RegDate);
           }
           // IF ENTERED DATE IS FUTURE DATE
           if (RegDate > TodayDate)
            {
               objArgs.IsValid = false;
               objSource.innerHTML = document.getElementById('hidFutureDateMsg').value;

           }
           // DATE SHOULD BE BETWEEN POLICY EFFECTIVE AND EXPIRY DATE
           else if (IsLegalClaim == "Y" && RegDate > PolicyExpDate)
            {
               objArgs.IsValid = false;
               objSource.innerHTML = document.getElementById('hidPolicyExpiryDateMsg').value;
           }
           else if (IsLegalClaim == "N" && ( RegDate< PolicyEffDate || RegDate > PolicyExpDate) )
           {
               objArgs.IsValid = false;
               objSource.innerHTML = document.getElementById('hidPolicyExpiryDateMsg').value;
           }
           // NO ERROR
           else 
           {
               objArgs.IsValid = true;
             
           }
         } 
         
         
    </script>
</head>
<body leftMargin="0" topMargin="0"  onload="initPage();">
    <form id="CoinsuranceDetails" runat="server" oncontextmenu="return false;" >
    			<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="3">
                    <asp:label id="lblRequiredFieldsInformation" runat="server" 
                                        ></asp:label>
                                        </TD>
									</tr>
									<tr>
										<td class="midcolorc" align="center" colSpan="3">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                        </td>
									</tr>
									<tr>
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capLEADER_SUSEP_CODE" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtLEADER_SUSEP_CODE" runat="server" size="32" maxlength="40"></asp:textbox>
                                                <br />
										<asp:requiredfieldvalidator id="rfvLEADER_SUSEP_CODE" runat="server" ControlToValidate="txtLEADER_SUSEP_CODE"
											Display="Dynamic"></asp:requiredfieldvalidator>
                                                <br />
                                                </TD>
										<TD  width="33%" class="midcolora" valign="top">
                                                <asp:label id="capLEADER_POLICY_NUMBER" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtLEADER_POLICY_NUMBER" runat="server" size="32" 
                                                    maxlength="40"></asp:textbox>
                                                <br />
										<asp:requiredfieldvalidator id="rfvLEADER_POLICY_NUMBER" runat="server" ControlToValidate="txtLEADER_POLICY_NUMBER"
											Display="Dynamic"></asp:requiredfieldvalidator>
                                                <br />
                                                <TD class="midcolora" valign="top">
                                                <asp:label id="capLEADER_ENDORSEMENT_NUMBER" runat="server"></asp:label>
                                                    <br />
                                                <asp:textbox id="txtLEADER_ENDORSEMENT_NUMBER" 
                        runat="server" size="32" maxlength="40" Width="152px"></asp:textbox>
                                                    <br />
                                            </TD>
									</tr>
								
									<tr>
										<TD class="midcolora" valign="top">
                                                <asp:label id="capLEADER_CLAIM_NUMBER" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtLEADER_CLAIM_NUMBER" runat="server" 
                        size="32" maxlength="40"></asp:textbox>
                                                </TD>
										<TD valign="top" class="midcolora">
                                                <asp:label id="capCLAIM_REGISTRATION_DATE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtCLAIM_REGISTRATION_DATE" runat="server" 
                        size="32" maxlength="10" Width="104px"></asp:textbox>
                                                <asp:HyperLink ID="hlkCLAIM_REGISTRATION_DATE" runat="server" 
                                                    CssClass="HotSpot">
                                             <asp:Image ID="imgCLAIM_REGISTRATION_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>     
                                           </asp:HyperLink>
                                                <br />
                                                <asp:RegularExpressionValidator ID="revCLAIM_REGISTRATION_DATE" runat="server" 
                                                    ControlToValidate="txtCLAIM_REGISTRATION_DATE" Display="Dynamic"></asp:RegularExpressionValidator> 
                                                    <asp:CustomValidator ID="csvCLAIM_REGISTRATION_DATE" ErrorMessage="" runat="server" ControlToValidate="txtCLAIM_REGISTRATION_DATE" ClientValidationFunction="ValidateCLAIM_REGISTRATION_DATE" ></asp:CustomValidator></br>
                                                </TD>
                                                <TD class="midcolora" valign="top">
												
                                                <asp:label id="capLITIGATION_FILE" runat="server"></asp:label>
                                                    <br />
                                                <asp:dropdownlist id="cmbLITIGATION_FILE" onfocus="SelectComboIndex('cmbLITIGATION_FILE')" runat="server">
											</asp:dropdownlist>
												
                                                </TD>
									</tr>
									<tr>
										<TD class="midcolora" colspan="3">&nbsp;</TD>
									</tr>
									<tr>
										<TD colspan="3" class="midcolora">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="50%">
                                                <cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnReset" runat="server" Text="Reset" 
                                                     OnClientClick="javascript:return ResetTheForm();" />
					                        </td>
                            <td class="midcolorr">
                                                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                                                   OnClick="btnSave_Click"  ></cmsb:cmsbutton>
                                            </td>
                        </tr>
                    </table>
                    </TD>
                    </tr>
                    </TBODY>
                    </TABLE>
                    
               
                <INPUT id="hidCOINSURANCE_ID" type="hidden" value="0" name="hidCOINSURANCE_ID" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			    <INPUT id="hidEffDate" type="hidden" value="0" name="hidEffDate" runat="server" />
                <INPUT id="hidExpDate" type="hidden" value="0" name="hidExpDate" runat="server" />
                <input id="hidLitigation" type="hidden" value="0" name="hidLitigation" runat ="server" />    
                <input id="hidFutureDateMsg" type="hidden" value="0" name="hidFutureDateMsg" runat ="server" />  
                <input id="hidPolicyExpiryDateMsg" type="hidden" value="0" name="hidPolicyExpiryDateMsg" runat ="server" />                  
    </form>
</body>
</html>

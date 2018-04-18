<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFeeLimit.aspx.cs" Inherits=" Cms.CmsWeb.Maintenance.AddFeeLimit" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
        <meta content="False" name="vs_showGrid">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
         <STYLE>
            .hide { OVERFLOW: hidden; TOP: 5px }
            .show { OVERFLOW: hidden; TOP: 5px }
            #tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">

		    function initPage() {

		        ApplyColor();
		    }

		    function ResetTheForm() {
		        document.MNT_INTEREST_RATES.reset();
		    }

		    //function TriggerOnBlurFunction() { //Commented by Aditya  ug # 1761

//		        $('#txtMAXIMUM_LIMIT').blur();
//		        $('#txtFEES_PERCENTAGE').blur();

//		        return false;
//		    }

		</script>

        <script language="javascript">

            function FormatAmountForSum(num) {  //Added by aditya for bug # 1761

                num = ReplaceAll(num, sBaseDecimalSep, '.');
                return num;
            }

            function formatCurrencyRate(num) { 
                
                num = isNaN(num) || num === '' || num === null ? 0.00 : num;
                return parseFloat(num).toFixed(2);
            }

            function formatRateTextValue(val, num) {               
                if (val == "100" || val == "100.0" || val == "100,0") {
                    if (num == 1) {
                        val = "100.00";
                    }
                    else {
                        val = "100,00";
                    }

                }
                else {
                    if (num == 1) {
                        if (val.indexOf(".") > -1 || val.indexOf(",") > -1) {
                            val = val.replace(",", ".");                           
                        }
                    }
                    else {
                        if (val.indexOf(".") > -1 || val.indexOf(",") > -1) {
                            val = val.replace(".", ",");                           
                        }
                    }

                }

                return val;                //Added till here

            }

            function validateLimit(objSource, objArgs) {
                if (document.getElementById('revMAXIMUM_LIMIT').isvalid == false || document.getElementById('revFEES_PERCENTAGE').isvalid == false)
                return

                var Limt = document.getElementById(objSource.controltovalidate).value;

                Limt = FormatAmountForSum(Limt);
                if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
                    objArgs.IsValid = true;
                else
                    objArgs.IsValid = false;
            }
            </script>
</head>
<body leftMargin="0" topMargin="0" oncontextmenu = "return false;" leftmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
    <form id="form1" runat="server">
    <div>
    <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
					<td align="center" colSpan="4" class="headereffectCenter"><asp:label id="lblDelete" runat="server" class="headereffectCenter" Visible="true">Fee Limit</asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							
							<tr>
                        <td class="pageHeader" colspan="4">
                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>		                   
                        <tr>
							<TD class="midcolora" width="18%">
								 <asp:Label ID="capMAXIMUM_LIMIT" runat="server">Maximum Limit</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtMAXIMUM_LIMIT" class="INPUTCURRENCY" runat="server" size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>                                            
                                            <br />
                                            <asp:requiredfieldvalidator id="rfvMAXIMUM_LIMIT" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAXIMUM_LIMIT"></asp:requiredfieldvalidator>
                                            <asp:RegularExpressionValidator
                                    ID="revMAXIMUM_LIMIT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtMAXIMUM_LIMIT"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="csvMAXIMUM_LIMIT" runat="server" ControlToValidate="txtMAXIMUM_LIMIT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
								 </TD>
								<td class="midcolora" width="18%">
							 <asp:Label ID="capFEES_PERCENTAGE" runat="server" text="Fees Percentage">Fees Percentage</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:TextBox ID="txtFEES_PERCENTAGE" runat="server" class="INPUTCURRENCY"  size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>   
                                 <br />       
                                  <asp:requiredfieldvalidator id="rfvFEES_PERCENTAGE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtFEES_PERCENTAGE"></asp:requiredfieldvalidator>
                                            <asp:RegularExpressionValidator
                                    ID="revFEES_PERCENTAGE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtFEES_PERCENTAGE"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="csvFEES_PERCENTAGE" runat="server" ControlToValidate="txtFEES_PERCENTAGE" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>                       
							 </td>
							</tr>							                           
							
				<tr>
					<td class="midcolora">
					<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
						
                                        
                                     </td>
					<td class="midcolorr" colSpan="3">
					
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave"  runat="server" Text="Save" ></cmsb:cmsbutton>
                            
                            </td>
				</tr>
						
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
				 <input id="hidOldData" type="hidden" name="hidOldData" runat="server">                
                 <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>  
                  <input id="hidInterestRateID" type="hidden" value="0" name="hidInterestRateID" runat="server"/>
			</TABLE>
    </div>
    </form>
</body>
</html>

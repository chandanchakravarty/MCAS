<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddIOFDetails.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddIOFDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
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
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript">

		    function ResetTheForm() {
		        document.MNT_LOB_MASTER.reset();
		    }

//		    function TriggerOnBlurFunction() { // Commented by aditya for bug # 1761

//		        $('#txtIOF_PERCENTAGE').blur();

//		        return false;
//		    }

		</script>
        <script language="javascript">
        function FormatAmountForSum(num) {

            num = ReplaceAll(num, sBaseDecimalSep, '.');
            return num;
        }

        function formatCurrencyRate(num) {  //Added by aditya for bug # 1761

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

            return val;

        } //Added till here

        function validateLimit(objSource, objArgs) {

            if (document.getElementById('revIOF_PERCENTAGE').isvalid == false)
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
<body>
    <form id="form1" runat="server">
    <div>
    <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
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
								 <asp:Label ID="capLOB_DESC" runat="server">Products</asp:Label>   
								</TD>	
                                <td class='midcolora' width='32%'>
										<asp:Label ID="lblLOB_DESC" Runat="server"></asp:Label>		
                                        </td>					
								<td class="midcolora" width="18%">
							 <asp:Label ID="capIOF_PERCENTAGE" runat="server" text="IOF Percentage"></asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtIOF_PERCENTAGE" runat="server" class="INPUTCURRENCY" maxlength="50" size="52"></asp:textbox><BR>
                                 <asp:requiredfieldvalidator id="rfvIOF_PERCENTAGE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtIOF_PERCENTAGE"></asp:requiredfieldvalidator>
                                 <asp:RegularExpressionValidator
                                    ID="revIOF_PERCENTAGE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtIOF_PERCENTAGE"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="csvIOF_PERCENTAGE" runat="server" ControlToValidate="txtIOF_PERCENTAGE" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
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
                  <input id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server"/>
			</TABLE>
    </div>
    </form>
     <script type="text/javascript" > 
         if (document.getElementById('hidFormSaved').value == "1") {

             RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidLOBID').value);
         }
		</script>
</body>
</html>

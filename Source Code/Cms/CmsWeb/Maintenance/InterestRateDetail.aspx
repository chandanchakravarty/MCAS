<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="InterestRateDetail.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.InterestRateDetail" %>
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
		        document.MNT_INTEREST_RATES.reset();
		    }

//		    function TriggerOnBlurFunction() {

//		        $('#txtINTEREST_RATE').blur();		       

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
//            if (document.getElementById('revINTEREST_RATE').isvalid == false)
//                return

            var Limt = document.getElementById(objSource.controltovalidate).value;

            Limt = FormatAmountForSum(Limt);
            if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
                objArgs.IsValid = true;
            else
                objArgs.IsValid = false;
        }

        function allnumeric(objSource, objArgs) {
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtRATE_EFFECTIVE_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revRATE_EFFECTIVE_DATE').setAttribute('enabled', true);
                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revRATE_EFFECTIVE_DATE').setAttribute('enabled', false);
                document.getElementById('revRATE_EFFECTIVE_DATE').style.display = 'none';
                document.getElementById('csvRATE_EFFECTIVE_DATE').setAttribute('enabled', true);
                document.getElementById('csvRATE_EFFECTIVE_DATE').style.display = 'inline';
                objArgs.IsValid = false;
            }
        } 
            </script>
</head>
<body leftMargin="0" topMargin="0">
    <form id="MNT_INTEREST_RATES" runat="server" method="post">
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
								 <asp:Label ID="capDate" runat="server">Date</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtRATE_EFFECTIVE_DATE" runat="server" size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkRATE_EFFECTIVE_DATE" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgRATE_EFFECTIVE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvRATE_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                                ErrorMessage="Date can't be blank." ControlToValidate="txtRATE_EFFECTIVE_DATE"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revRATE_EFFECTIVE_DATE" runat="server" 
                                                    ControlToValidate="txtRATE_EFFECTIVE_DATE" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvRATE_EFFECTIVE_DATE" runat="server" ControlToValidate="txtRATE_EFFECTIVE_DATE" Display="Dynamic" ClientValidationFunction="allnumeric"></asp:CustomValidator>
								 </TD>
								<td class="midcolora" width="18%">
							 <asp:Label ID="capNO_OF_INSTALLMENTS" runat="server" text="Number of Installments"></asp:Label><%--<span class="mandatory">*</span>--%>   
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:DropDownList ID="cmbNO_OF_INSTALLMENTS" runat="server">
                                  </asp:DropDownList><br />
                                  <asp:RequiredFieldValidator ID="rfvNO_OF_INSTALLMENTS" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbNO_OF_INSTALLMENTS"></asp:RequiredFieldValidator>
							 </td>
							</tr>
							
							<tr >
							
                             <td class="midcolora" width="18%">
							 <asp:Label ID="capINTEREST_TYPE" runat="server" Text ="Interest Type"></asp:Label><%--<span class="mandatory">*</span>--%>   
								</TD>
								<TD class="midcolora" width="32%">
								  <asp:DropDownList ID="cmbINTEREST_TYPE" runat="server"   
                                  Width="240px"></asp:DropDownList><br />
                                 
							 </td>
                             <TD class="midcolora" width="18%"><asp:label id="capINTEREST_RATE" runat="server" Text = "Interest Rate">Interest Rate</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtINTEREST_RATE" runat="server" class="INPUTCURRENCY" maxlength="50" size="52"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvINTEREST_RATE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtINTEREST_RATE"></asp:requiredfieldvalidator>
                                            <asp:RegularExpressionValidator
                                    ID="revINTEREST_RATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtINTEREST_RATE"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="csvINTEREST_RATE" runat="server" ControlToValidate="txtINTEREST_RATE" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator></TD>
							 </tr>
                            
							
				<tr>
					<td class="midcolora">
					<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
						
                                        <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server"  causesvalidation="false" align="right" onclick="btnDelete_Click"></cmsb:cmsbutton>
                                     </td>
					<td class="midcolorr" colSpan="3">
					
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave"  runat="server" Text="Save"></cmsb:cmsbutton>
                            
                            </td>
				</tr>
						
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
				 <input id="hidOldData" type="hidden" name="hidOldData" runat="server">      
                 <input id="hidInterestRateID" type="hidden" value="0" name="hidInterestRateID" runat="server"/>
			</TABLE>
    </div>

    </form>
     <script type="text/javascript" >
         if (document.getElementById('hidFormSaved').value == "1") {

             RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidInterestRateID').value);
         }
		</script>
</body>
</html>

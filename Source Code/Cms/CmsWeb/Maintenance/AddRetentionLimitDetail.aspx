<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRetentionLimitDetail.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddRetentionLimitDetail" %>



<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Retention Limit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
        <script type="text/javascript" type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		<script language="javascript">


    </script>
    <script language="javascript" type="text/javascript"> 

    </script>
     <script language="javascript">
           
        function FormatAmountForSum(num) {

            num = ReplaceAll(num, sBaseDecimalSep, '.');
            return num;
        }

        function validateAmount(objSource, objArgs) {
            if (document.getElementById('revRETENTION_LIMIT').isvalid == false || document.getElementById('revRETENTION_LIMIT').isvalid == false)
                return

            var Limt = document.getElementById(objSource.controltovalidate).value;

            Limt = FormatAmountForSum(Limt);
            if (parseFloat(Limt) > 0) {
                objArgs.IsValid = true;
            }
            else
                objArgs.IsValid = false;
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
         </script>
   
	</HEAD>
	<body >
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
                       <TR>

                          <TD class="midcolora" width="18%">
							<asp:label id="capREF_SUSEP_LOB_ID" runat="server">Susep Product Description</asp:label><span runat="server" class="mandatory">*</span></TD>
						    <TD class="midcolora" width="32%">
							  
                              <asp:DropDownList ID="cmbREF_SUSEP_LOB_ID" runat="server" Width="100%"></asp:DropDownList><br />
                                                 <asp:RequiredFieldValidator runat="server" ID="rfvREF_SUSEP_LOB_ID" ControlToValidate="cmbREF_SUSEP_LOB_ID"></asp:RequiredFieldValidator>
                                </TD>
						   <TD class="midcolora" width="18%">
							   <asp:label id="capRETENTION_LIMIT" runat="server">Retention Limit</asp:label><span id="Span1" runat="server" class="mandatory">*</span></TD>
						    <TD class="midcolora" width="32%">
							<asp:TextBox id="txtRETENTION_LIMIT" runat="server" size="12" MaxLength="10"></asp:TextBox><br>
                                <asp:RequiredFieldValidator runat="server" ID="rfvRETENTION_LIMIT" ControlToValidate="txtRETENTION_LIMIT"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="revRETENTION_LIMIT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtRETENTION_LIMIT"></asp:RegularExpressionValidator>
                                     <asp:CustomValidator ID="csvRETENTION_LIMIT" Display="Dynamic" ControlToValidate="txtRETENTION_LIMIT"
                                                         ClientValidationFunction="validateAmount" runat="server"></asp:CustomValidator>
                              </TD>                         
                            </TR>		

				    <tr>
					<td class="midcolora" >
					<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesValidation="false" ></cmsb:cmsbutton>
                       
						                                    
                                     </td>
					<td class="midcolorr" colSpan="3">
				
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave"  runat="server" Text="Save" ></cmsb:cmsbutton>
                           
                            </td>
				</tr>
						
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
                 <input id="hidRETENTION_LIMIT_ID" type="hidden" value="0" name="hidRETENTION_LIMIT_ID" runat="server"/>
			</TABLE>
    </div>
    </form>
         <script type="text/javascript" >
             if (document.getElementById('hidFormSaved').value == "1") {

                 RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidRETENTION_LIMIT_ID').value);
             }
		</script>
	</body>
</HTML>



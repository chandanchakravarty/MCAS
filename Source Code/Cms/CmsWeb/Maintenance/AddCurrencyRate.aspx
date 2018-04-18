<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCurrencyRate.aspx.cs" Inherits="CmsWeb.Maintenance.AddCurrencyRate" %>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html  >
<head runat="server">
    <title>Currency Rate</title>
        <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">

		    function ResetTheForm() {
		        document.MNT_CURRENCY_RATE_MASTER.reset();
		    }

		    function formatCurrencyRate(num) {

		        //strRate = document.getElementById("txtRATE").value;
		        num = isNaN(num) || num === '' || num === null ? 0.00 : num;
		        return parseFloat(num).toFixed(2);


		    }
            
		    function formatRateTextValue(val,num) {
            //debugger
		        //var str = val.replace(",",".");
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
		                    val = val.substring(0, val.indexOf(".") + 3);
		                }
		            }
		            else {
		                if (val.indexOf(".") > -1 || val.indexOf(",") > -1) {
		                    val = val.replace(".", ",");
		                    val = val.substring(0, val.indexOf(",") + 3);
		                }
		            }

		        }
		        //alert(val);
//		        var str2 = val.substring(str.indexOf(".")+1);
//		        alert(str2);

//		        if (str2.length > 2) {
//		            str2 = str2.substring(0, 2);
//		        }
//		        alert(str2);
		        return val;
		        //return parseFloat(val).toFixed(2);

		    }
            
            function allnumeric(objSource, objArgs) {//debugger
    var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
    var inputxt = document.getElementById('txtRATE_EFFETIVE_FROM').value;
    if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
        document.getElementById('revRATE_EFFETIVE_FROM').setAttribute('enabled', true);

        objArgs.IsValid = true;
    }
    else {
        document.getElementById('revRATE_EFFETIVE_FROM').setAttribute('enabled', false);
        document.getElementById('revRATE_EFFETIVE_FROM').style.display = 'none';
        document.getElementById('csvRATE_EFFETIVE_FROM').setAttribute('enabled', true);
        document.getElementById('csvRATE_EFFETIVE_FROM').style.display = 'inline';
        objArgs.IsValid = false;
    }
}

function allnumeric1(objSource, objArgs) {//debugger
    var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
    var inputxt = document.getElementById('txtRATE_EFFETIVE_TO').value;
    if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
        document.getElementById('revRATE_EFFETIVE_TO').setAttribute('enabled', true);
       

        objArgs.IsValid = true;
    }
    else {
        document.getElementById('revRATE_EFFETIVE_TO').setAttribute('enabled', false);
        document.getElementById('revRATE_EFFETIVE_TO').style.display = 'none';
        document.getElementById('csvRATE_EFFETIVE_TO').setAttribute('enabled', true);
        document.getElementById('csvRATE_EFFETIVE_TO').style.display = 'inline';
        document.getElementById('cpvRATE_EFFETIVE_TO').setAttribute('enabled', false);
        objArgs.IsValid = false;
    }
}

function validate() {
    
  
    if (document.getElementById('revRATE').isvalid == false) {
        document.getElementById('rvRate').setAttribute('enabled', false);
        document.getElementById('rvRate').style.display = 'none';
    }
    else
        document.getElementById('rvRate').setAttribute('enabled', true);
}


function validate1() {


    if (document.getElementById('revRATE_EFFETIVE_TO').isvalid == false) {
       document.getElementById('cpvRATE_EFFETIVE_TO').setAttribute('enabled',false);
     
    }
    else
        document.getElementById('cpvRATE_EFFETIVE_TO').setAttribute('enabled', true);
    }


		</script>
</head>
<BODY leftMargin="0" topMargin="0" onload="">
		<FORM id="MNT_CURRENCY_RATE_MASTER" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<br />
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
								 <asp:Label ID="capCURRENCY_ID" runat="server">Currency</asp:Label><span class="mandatory">*</span>    
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:DropDownList runat="server" ID="cmbCURRENCY_ID"  Height="17px">
                        </asp:DropDownList><br />
                       <asp:RequiredFieldValidator ID="rfvCURRENCY_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbCURRENCY_ID"></asp:RequiredFieldValidator>
								 </TD>
								<td class="midcolora" width="18%">
								<asp:Label ID="capRATE" runat="server">Rate</asp:Label><span class="mandatory">*</span>   
								</td>
								<td class="midcolora" width="32%">
                                <!-- Max Length of txtRate is increased to 5 for Bug # 899 by Agniswar on 12/09/2011 -->
								  <asp:TextBox ID="txtRATE" runat="server"  CssClass="INPUTCURRENCY" size="12" onpaste="MaxLength(this,7)" MaxLength="5" ></asp:TextBox><br />
								   <asp:RequiredFieldValidator ID="rfvRATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtRATE"></asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="revRATE" runat="server" 
                    ControlToValidate="txtRATE" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="rvRate" runat="server" ControlToValidate="txtRate" display="Dynamic"
                                        MaximumValue="100" MinimumValue="0" Type="Currency"></asp:RangeValidator>
                                 
                                                
								</td>
							</tr>
							
								
							<tr>
							<TD class="midcolora" width="18%">
								 <asp:Label ID="capRATE_EFFETIVE_FROM" runat="server">Effective From</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtRATE_EFFETIVE_FROM" runat="server" size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkRATE_EFFETIVE_FROM" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgRATE_EFFETIVE_FROM" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvRATE_EFFETIVE_FROM" runat="server" Display="Dynamic"
                                                ControlToValidate="txtRATE_EFFETIVE_FROM"></asp:RequiredFieldValidator>
                                          <asp:RegularExpressionValidator ID="revRATE_EFFETIVE_FROM" runat="server" Display="Dynamic"
                                                ControlToValidate="txtRATE_EFFETIVE_FROM"></asp:RegularExpressionValidator> 
                                            
                                        <asp:CustomValidator ID="csvRATE_EFFETIVE_FROM"	runat="server" Display="Dynamic" ClientValidationFunction="allnumeric" ControlToValidate="txtRATE_EFFETIVE_FROM"></asp:CustomValidator>
                                     </TD>
								<td class="midcolora" width="18%">
								 <asp:Label ID="capRATE_EFFETIVE_TO" runat="server">Effective To</asp:Label><span
                                                class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
								  <asp:TextBox ID="txtRATE_EFFETIVE_TO" runat="server"  size="12" MaxLength="10"  Display="Dynamic" onchange="validate1();"></asp:TextBox>
                                            <asp:HyperLink ID="hlkRATE_EFFETIVE_TO" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgRATE_EFFETIVE_TO" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvRATE_EFFETIVE_TO" runat="server" Display="Dynamic"
                                                ControlToValidate="txtRATE_EFFETIVE_TO"></asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="revRATE_EFFETIVE_TO" runat="server" Display="Dynamic"
                                                ControlToValidate="txtRATE_EFFETIVE_TO"></asp:RegularExpressionValidator>
                                        <asp:CustomValidator ID="csvRATE_EFFETIVE_TO"	runat="server" Display="Dynamic" ClientValidationFunction="allnumeric1" ControlToValidate="txtRATE_EFFETIVE_TO" ></asp:CustomValidator>     	
								        <asp:comparevalidator id="cpvRATE_EFFETIVE_TO" ControlToValidate="txtRATE_EFFETIVE_TO" Display="Dynamic" Runat="server" ControlToCompare="txtRATE_EFFETIVE_FROM" Type="Date"
										Operator="GreaterThanEqual"></asp:comparevalidator>
                                </td>
							</tr>
							
							<tr >
							<td colspan="2">
							
							 </td>
							 </tr>
							
							
							
				<tr>
					<td class="midcolora" colSpan="2">
					<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
						<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text=""  onclick="btnActivateDeactivate_Click"
							CausesValidation="false"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2">
					
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
                            
                            </td>
				</tr>
							
								
						
							
							
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
				  <input id="hidcurrencyrateId" type="hidden" value="0" name="hidcurrencyrateId" runat="server"/>
				<input id="hidOldData" type="hidden"  name="hidOldData" runat="server"/>
			</TABLE>
			
			
		</FORM>
		<script type="text/javascript" >
		    if (document.getElementById('hidFormSaved').value == "1") {

		        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidcurrencyrateId').value);
		    }
		</script>
	</BODY>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremiumModPlan.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.PremiumModPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body leftMargin='0' topMargin='0' onload='ApplyColor();'>
    <form id="form1" runat="server"  method='post'>
   <table width="100%" border="0" cellpadding="0" cellspacing="0">
				<TR>
					<TD>
    <TABLE width='100%' border='0' align='center'>

                            <tr>
                                <td class="headereffectCenter" colspan="3">
                                    <asp:Label ID="lblHeader" runat="server">Individual Risk Premium modification Plan</asp:Label>
                                </td>
                            </tr>

							<tr id="trMessages" runat="server">
								<TD id="tdMessages" runat="server" class="pageHeader" colSpan="3">
                                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label>
                                </TD>
							</tr>
							<tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="3">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                </td>
							</tr>
                            	<tr>
								<TD id="tdFUND_TYPE_CODE" runat="server" class="midcolora" width="35%">
									<asp:Label id="capBusines_Nature" runat="server">Management Debit/Credit</asp:Label> 
                                   	<br />					
                                   <asp:textbox id='Textbox2' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
				                   
								</TD>
                                <TD id="tdRetail_Store" runat="server" class="midcolora" width="35%">
									<asp:Label id="capRetail_Stores" runat="server">Employees Debit/Credit</asp:Label> 
                                   	<br />	
                                  <asp:textbox id='txtRetail_Stores' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
                               
				                   
								</TD>
                                 <TD id="td1" runat="server" class="midcolora" width="35%">
									<asp:Label id="Label1" runat="server">Location Debit/Credit</asp:Label> 
                                   	<br />	
                                  <asp:textbox id='Textbox1' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
                               
				                   
								</TD>
                                </tr>
                                <tr>
								<TD id="td2" runat="server" class="midcolora" width="35%">
									<asp:Label id="Label2" runat="server">Protection Debit/Credit</asp:Label> 
                                   	<br />					
                                   <asp:textbox id='Textbox3' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
				                   
								</TD>
                                <TD id="td3" runat="server" class="midcolora" width="35%">
									<asp:Label id="Label3" runat="server">Building Features Debit/Credit</asp:Label> 
                                   	<br />	
                                  <asp:textbox id='Textbox4' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
                               
				                   
								</TD>
                                 <TD id="td4" runat="server" class="midcolora" width="35%">
									<asp:Label id="Label4" runat="server">Other</asp:Label> 
                                   	<br />	
                                  <asp:textbox id='Textbox5' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
                               
				                   
								</TD>
                                </tr>
                                <tr>
								<TD id="td5" runat="server" class="midcolora" width="35%">
									<asp:Label id="Label5" runat="server">Premise and Equipment Debit/Credit</asp:Label> 
                                   	<br />					
                                   <asp:textbox id='Textbox6' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
				                   
								</TD>
                                <TD id="td6" runat="server" class="midcolora"  colspan="2">
									<asp:Label id="Label6" runat="server">Reason for Modification</asp:Label> 
                                   	<br />	
                                  <asp:textbox id='Textbox7' runat='server' size='50' maxlength='0' 
                                            Width="200px" Height="70px" TextMode="MultiLine"></asp:textbox>		                               				                   								    
				                   
								</TD>
                                </tr>
                               
                                <tr >
                               
                                <td id="td7" runat="server" colspan="3" class="midcolora">
                                <asp:Label id="Label7" runat="server">TOTAL : DEBIT/CREDIT</asp:Label> 
                                   	<br />	
                                  <asp:textbox id='Textbox8' runat='server' size='50' maxlength='0' 
                                            Width="200px"></asp:textbox>
                                </td>
                                </tr>
                              <%--   <tr>
                    <td class="midcolora" colspan="1" valign="bottom" style="width:190px">
                        <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
                        <cmsb:CmsButton runat="server" CausesValidation="false"  ID="btndelete"  
                        CssClass="clsButton" onclick="btndelete_Click"></cmsb:CmsButton>
                        <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
                    </td>         
                </tr> --%>
            </TABLE>
                           
                            </TD>
                            </TR>
          </table>
    </form>
</body>
</html>

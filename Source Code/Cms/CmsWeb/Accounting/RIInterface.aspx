<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RIInterface.aspx.cs" Inherits="Cms.CmsWeb.Accounting.RIInterface" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >

<html >
<head >
    <title>RI Interface</title>
       <meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
        <script src="/cms/cmsweb/scripts/Calendar.js"></script>
</head>
<script language="javascript">
function validate1() {


    if (document.getElementById('revRATE_EFFETIVE_TO').isvalid == false) {
       document.getElementById('cpvRATE_EFFETIVE_TO').setAttribute('enabled',false);
     
    }
    else
        document.getElementById('cpvRATE_EFFETIVE_TO').setAttribute('enabled', true);
}


    </script>

<body oncontextmenu = "return false;" onload="setfirstTime();" >
	<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
   
    <FORM id="form1" method="post" runat="server">
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
								 <asp:Label ID="capFILE_NAME" runat="server">File Name</asp:Label><span class="mandatory">*</span>    
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:DropDownList runat="server" ID="cmbFILE_NAME"  Height="17px">
                        </asp:DropDownList><br />
                      <%-- <asp:RequiredFieldValidator ID="rfvCURRENCY_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbCURRENCY_ID"></asp:RequiredFieldValidator>--%>
								 </TD>
                                 <TD class="midcolora" width="18%">
								 <asp:Label ID="capFILE_TYPE" runat="server">File Type</asp:Label><span class="mandatory">*</span>    
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:DropDownList runat="server" ID="cmbFILE_TYPE"  Height="17px">
                        </asp:DropDownList><br />
                      <%-- <asp:RequiredFieldValidator ID="rfvCURRENCY_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbCURRENCY_ID"></asp:RequiredFieldValidator>--%>
								 </TD>
								
								
							</tr>
							
								
							<tr>
							<TD class="midcolora" width="18%">
								 <asp:Label ID="capDATE_EFFETIVE_FROM" runat="server">Effective From</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtDATE_EFFETIVE_FROM" runat="server" size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkDATE_EFFETIVE_FROM" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgDATE_EFFETIVE_FROM" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvDATE_EFFETIVE_FROM" runat="server" Display="Dynamic"
                                                ControlToValidate="txtDATE_EFFETIVE_FROM"></asp:RequiredFieldValidator>
                                          <asp:RegularExpressionValidator ID="revDATE_EFFETIVE_FROM" runat="server" Display="Dynamic"
                                                ControlToValidate="txtDATE_EFFETIVE_FROM"></asp:RegularExpressionValidator> 
                                            
                                       <%-- <asp:CustomValidator ID="csvDATE_EFFETIVE_FROM"	runat="server" Display="Dynamic" ClientValidationFunction="allnumeric" ControlToValidate="txtDATE_EFFETIVE_FROM"></asp:CustomValidator>--%>
                                     </TD>
								<td class="midcolora" width="18%">
								 <asp:Label ID="capDATE_EFFETIVE_TO" runat="server">Effective To</asp:Label><span
                                                class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
								  <asp:TextBox ID="txtDATE_EFFETIVE_TO" runat="server"  size="12" MaxLength="10"  Display="Dynamic" onchange="validate1();"></asp:TextBox>
                                            <asp:HyperLink ID="hlkDATE_EFFETIVE_TO" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgDATE_EFFETIVE_TO" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvDATE_EFFETIVE_TO" runat="server" Display="Dynamic"
                                                ControlToValidate="txtDATE_EFFETIVE_TO"></asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="revDATE_EFFETIVE_TO" runat="server" Display="Dynamic"
                                                ControlToValidate="txtDATE_EFFETIVE_TO"></asp:RegularExpressionValidator>
                                      <%--  <asp:CustomValidator ID="csvDATE_EFFETIVE_TO"	runat="server" Display="Dynamic" ClientValidationFunction="allnumeric1" ControlToValidate="txtDATE_EFFETIVE_TO" ></asp:CustomValidator>     	--%>
								        <asp:comparevalidator id="cpvDATE_EFFETIVE_TO" ControlToValidate="txtDATE_EFFETIVE_TO" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_EFFETIVE_FROM" Type="Date"
										Operator="GreaterThanEqual"></asp:comparevalidator>
                                </td>
							</tr>
							
							
							
							
							
				<tr>
					<td class="midcolora" colSpan="2">
					</td>
					<td class="midcolorr" colSpan="2">
					
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
                            
                            </td>
				</tr>
							
								
						
							
							
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
				 
				<input id="hidOldData" type="hidden"  name="hidOldData" runat="server"/>
			</TABLE>
			
			
		</FORM>
   

    
</body>

</html>

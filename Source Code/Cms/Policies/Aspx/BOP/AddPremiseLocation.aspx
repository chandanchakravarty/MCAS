<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPremiseLocation.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddPremiseLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POL_BOP_PREMISESLOCATIONS</title>
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
              function FormatValue() {

                  document.getElementById('txtANN_REVENUE').value = formatRateBase("127722");
              }

              function ResetForm() {

                  //alert("Caled Rs");
                  //document.getElementById('cmbINTEREST').options.selectedIndex = -1;
               
                  document.POL_BOP_PREMISESLOCATIONS.reset();
                  document.getElementById("POL_BOP_PREMISESLOCATIONS").reset();

                  // document.getElementById('txtLOCATION_ID').value = "";
                  document.getElementById('txtFL_TM_EMP').value = '';
                  document.getElementById('txtPT_TM_EMP').value = '';
                  
                  document.getElementById('txtANN_REVENUE').value = '';

                  document.getElementById('txtOPEN_AREA').value = '';
                  document.getElementById('txtOCC_AREA').value = '';
                  document.getElementById('txtTOT_AREA').value = '';
                  document.getElementById('txtCITY').value = '';
                  document.getElementById('txtZIP').value = '';
                  document.getElementById('txtSTREET_ADDR').value = '';
                  document.getElementById('txtCOUNTY').value = '';
                  document.getElementById('txtBLDNG_ID').value = '';
                  
                  
                  document.getElementById('cmbAREA_LEASED').value = '';
                  document.getElementById('cmbINTEREST').value = '';
                  document.getElementById('cmbSTATE').value = '';

                  
                  //var cmbINT= document.getElementById("cmbINTEREST");
                  //cmbINT.options[0].text = ""
                                 

               

                 


                  //populateXML();
                  //    BillType();
                  // populateXML();
                  //    BillType();
                  // DisableValidators();
                  //ChangeColor();             


                  return false;
              }
        
        
        </script>
</head>
<body leftMargin='0' topMargin='0' onload='ApplyColor();'>
    <form  id='POL_BOP_PREMISESLOCATIONS' method='post' runat='server'>
     
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <TR>
		<TD>
            <TABLE width='100%' border='0' align='center'>
              <tr style="background-color:Orange">
                    <td class="headereffectCenter" colspan="3" align="center">
                        <asp:Label ID="lblHeader" runat="server" >Policy Premises Location</asp:Label>
                    </td>
             </tr>
              <tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="3">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></asp:label>
                                </td>
							</tr>		
          <tr>
			    <TD   class='midcolora' width="33%">
				<asp:Label id="capUNITS" runat="server">Location #</asp:Label> 
                <br />
                <asp:textbox id='txtLOCATION_ID' runat='server' size='50' maxlength='0' 
                        Width="50px"></asp:textbox>
                </TD>
                <TD   class='midcolora' width="33%">
				<asp:Label id="capBLDNG_ID" runat="server">Building #</asp:Label><br />
                <asp:textbox id='txtBLDNG_ID' runat='server' size='50' maxlength='0' 
                        Width="80px"></asp:textbox> <br />
                        <asp:RegularExpressionValidator ID="revBLDNG_ID" runat="server" ControlToValidate= "txtBLDNG_ID" Display="Dynamic"></asp:RegularExpressionValidator> 
                </TD> 
                <TD   class='midcolora' width="33%">&nbsp;</TD> 
                
              
          </tr>
          <tr>
            
                <TD   class='midcolora' width="99%" colspan="3">
				<asp:Label id="capSTREET_ADDR" runat="server">Street(911 Address)</asp:Label> <br />

                <asp:textbox id='txtSTREET_ADDR' runat='server' size='50' maxlength='0' 
                        Width="550px"></asp:textbox>
                </TD> 
                <%--<TD   class='midcolora' width="33%">&nbsp;</TD> --%>
               <%--TD   class='midcolora' width="33%">&nbsp;</TD>--%> 
                
             </tr>
             <tr>
			<TD   class='midcolora' width="33%">
				<asp:Label id="capCITY" runat="server">City</asp:Label> <br />
                <asp:textbox id='txtCITY' runat='server' size='50' maxlength='0' 
                        Width="150px"></asp:textbox></TD>
              						
            <TD   class='midcolora' width="33%">
				<asp:Label id="capSTATE" runat="server">State</asp:Label>
                <br />
                 <asp:DropDownList id='cmbSTATE' runat='server' Height="16px" 
                        Width="1px"></asp:DropDownList> </TD>  
                        
            <TD   class='midcolora' width="33%"><asp:Label id="capZIP" runat="server">Zip</asp:Label> <br />
              <asp:textbox id='txtZIP' runat='server' size='50' maxlength='0' 
                        Width="150px"></asp:textbox><br />
                        <asp:RegularExpressionValidator ID="revZIP" runat="server" ControlToValidate="txtZIP"
                    Display="Dynamic"></asp:RegularExpressionValidator></TD>	                      
                      
             </tr>
             <tr>
	
            <TD   class='midcolora' width="33%">
				<asp:Label id="capCOUNTY" runat="server">County</asp:Label> <br />
                  <asp:textbox id='txtCOUNTY' runat='server' size='50' maxlength='0' 
                        Width="150px"></asp:textbox>
				                   
                
                </TD>    
                <TD   class='midcolora' width="33%">&nbsp;</TD> 
                <TD   class='midcolora' width="33%">&nbsp;</TD>               
 
             </tr>
             <tr>
			
            <TD   class='midcolora' width="33%">
				<asp:Label id="capINTEREST" runat="server">Interest</asp:Label> <br />
            <asp:DropDownList id='cmbINTEREST' runat='server' Height="17px" 
                        Width="100px"></asp:DropDownList>
                </TD>	

             <td class='midcolora' width="33%">
                <asp:Label id="capCLASSIFICATION_CODE" runat="server">Classification</asp:Label><br />
                <asp:textbox id='txtCLASSIFICATION_CODE' runat='server' size='50' maxlength='0'  
                        Width="100px" ReadOnly="true"></asp:textbox>
                <asp:hyperlink id="hlkClassCodeLookup" runat="server" CssClass="HotSpot">
					<asp:image id="imgClassCodeLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
				</asp:hyperlink>
             </td> 
             <TD   class='midcolora' width="33%">&nbsp;</TD>   
			                            
			<%-- <TD class="midcolora" width="18%"><asp:Label id="capDESC_OPERTN" runat="server">Description of Operations</asp:Label> 
				</TD>
                <TD class='midcolora' width="32%">                                                                      
									
                <asp:textbox id='txtDESC_OPERTN' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>--%>
             </tr>
             <tr>
                <TD class="midcolora" width="33%">
				<asp:Label id="capPT_TM_EMP" runat="server"># of Part Time Employees</asp:Label> <br />
                <asp:textbox id='txtPT_TM_EMP' runat='server' size='50' maxlength='0' 
                        Width="80px"></asp:textbox><br />
                        <asp:RegularExpressionValidator ID="revPT_TM_EMP" runat="server" ControlToValidate= "txtPT_TM_EMP" Display="Dynamic"></asp:RegularExpressionValidator>   

                </TD> 

                <TD class="midcolora" width="33%">
				<asp:Label id="capFL_TM_EMP" runat="server"># of Full Time Employees</asp:Label>
                <br />
                  <asp:textbox id='txtFL_TM_EMP' runat='server' size='50' maxlength='0' 
                        Width="80px"></asp:textbox><br />
                        <asp:RegularExpressionValidator ID="revFL_TM_EMP" runat="server" ControlToValidate= "txtFL_TM_EMP" Display="Dynamic"></asp:RegularExpressionValidator> 
                </TD>

                <TD class="midcolora" width="33%">
				<asp:Label id="capANN_REVENUE" runat="server">Annual Revenue</asp:Label> <br />
 <asp:textbox id='txtANN_REVENUE' runat='server' size='50' maxlength='0' 
                        Width="200px" CssClass="inputcurrency"></asp:textbox><br />
                         <asp:RegularExpressionValidator ID="revANN_REVENUE" runat="server" ControlToValidate="txtANN_REVENUE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
                </TD>
             </tr>

             <tr>

              <TD class="midcolora" width="33%">
				<asp:Label id="capOCC_AREA" runat="server">Occupied Area</asp:Label> <br />
            <asp:textbox id='txtOCC_AREA' runat='server' size='50' maxlength='0' 
                        Width="100px"></asp:textbox><br />
                         <asp:RegularExpressionValidator ID="revOCC_AREA" runat="server" ControlToValidate="txtOCC_AREA"
                    Display="Dynamic"></asp:RegularExpressionValidator>

                </TD> 
             
                        <TD class="midcolora" width="33%">
				<asp:Label id="capOPEN_AREA" runat="server">Open to Public Area</asp:Label> <br />
                <asp:textbox id='txtOPEN_AREA' runat='server' size='50' maxlength='0' 
                        Width="100px"></asp:textbox><br />
                          <asp:RegularExpressionValidator ID="revOPEN_AREA" runat="server" ControlToValidate="txtOPEN_AREA"
                    Display="Dynamic"></asp:RegularExpressionValidator>

                </TD>
                               
            <TD class="midcolora" width="33%">
				<asp:Label id="capTOT_AREA" runat="server">Total Building Area</asp:Label><br />
                <asp:textbox id='txtTOT_AREA' runat='server' size='50' maxlength='0'  
                        Width="100px"></asp:textbox><br />
                        <asp:RegularExpressionValidator ID="revTOT_AREA" runat="server" ControlToValidate="txtTOT_AREA"
                    Display="Dynamic"></asp:RegularExpressionValidator>

                 
                </TD> 
             
             </tr>

             <tr>
             <TD class="midcolora" width="33%">
				<asp:Label id="capAREA_LEASED" runat="server">Any Area Leased to Others?</asp:Label> <br />
                
                <asp:DropDownList id='cmbAREA_LEASED' runat='server' Height="16px" 
                            Width="58px"></asp:DropDownList>
                </TD> 
                <TD   class='midcolora' width="33%">&nbsp;</TD> 
                <TD   class='midcolora' width="33%">&nbsp;</TD>   
             </tr>
            
          <%-- <tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="2">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></asp:label>
                                </td>
							</tr>		--%>	
            
               


             <%--Below Butoons--%>  
             <tr>
                <td class='midcolora' colspan="1" valign="bottom" style="width:190px">
                    <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Save"></cmsb:cmsbutton>
                    <cmsb:CmsButton runat="server" CausesValidation="false"  ID="btndelete"  
                                            CssClass="clsButton" onclick="btndelete_Click"></cmsb:CmsButton>
                                           		</td>	
                                                <TD class="midcolora" width="33%"></TD>				
                <td class='midcolorr'>
                   <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Reset"></cmsb:cmsbutton>                              
                </td>
               
               <%-- <td class='midcolora' colspan="1">
                    <table width="100%">
                        <tr>
                            <td class="midcolorr" colSpan="2" align="center">							
						        <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Reset"></cmsb:cmsbutton>                            
                            </td> 	
                        </tr>
                    </table>
                </td>--%>
                                
              </tr>           
             
            </TABLE>

        <%--the main outer table--%>
        </TD>
		</TR>
                              <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
                            <INPUT id="hidPremLoc_ID" type="hidden" value="0" name="hidPremLoc_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
        </table>
    </form>
</body>
</html>

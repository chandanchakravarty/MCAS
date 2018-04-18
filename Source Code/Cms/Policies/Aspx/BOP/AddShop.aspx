<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShop.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddShop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL_SUP_FORM_SHOP</title>
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

             function ResetForm() {

                 document.POL_SUP_FORM_SHOP.reset();
                 //populateXML();
                 //    BillType();
                 // populateXML();
                 //    BillType();
                 // DisableValidators();
                 //ChangeColor();   
                 //var updFrom = document.getElementById('cmbUpdatedFrom').options[document.getElementById('cmbUpdatedFrom').selectedIndex].value; 
                 //document.getElementById('cmbACCOUNT_ID').options.selectedIndex = -1;


                 document.getElementById('txtBBQ_PIT_DIST').value = '';
                 document.getElementById('txtUNITS').value = '';
                 document.getElementById('txtPERCENT_OCUP').value = '';
                 document.getElementById('txtPERCENT_SALES').value = '';
                 
                 document.getElementById('cmbIS_INSURED').value = '';
                 document.getElementById('cmbTENANT_LIABILITY').value = '';
                 document.getElementById('cmbRESTURANT_OCUP').value = '';
                 document.getElementById('cmbFLAME_COOKING').value = '';
                 document.getElementById('cmbSEPARATE_BAR').value = '';
                 document.getElementById('cmbNUM_FRYERS').value = '';
                 document.getElementById('cmbNUM_GRILLS').value = '';
                 document.getElementById('cmbBBQ_PIT').value = '';
                 document.getElementById('cmbDUCT_SYS').value = '';
                 document.getElementById('cmbDUCT_CLND_PST_SIX_MONTHS').value = '';
                 document.getElementById('cmbSUPPR_SYS').value = '';
                 document.getElementById('cmbBLDG_TYPE_COOKNG').value =
                 document.getElementById('cmbIS_ENTERTNMT').value = '';

                 return false;                                  
                                    
                
             }
        
        
        </script>
    <style type="text/css">
        .style3
        {
            width: 27%;
        }
        .style4
        {
            width: 28%;
        }
        .style5
        {
            width: 20%;
        }
    </style>
</head>
<body leftMargin='0' topMargin='0' onload='ApplyColor();'>
    <form  id='POL_SUP_FORM_SHOP' method='post' runat='server'>
         <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <TR>
		<TD>
            <TABLE width='100%' border='0' align='center'>
              <tr style="background-color:Orange">
                    <td class="headereffectCenter" colspan="3" align="center">
                        <asp:Label ID="lblHeader" runat="server" >Shopping/Strip Center</asp:Label>
                    </td>
             </tr>
              <tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="3">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></asp:label>
                                </td>
							</tr>	
             
           <tr>
			<TD   class="midcolora">
				<asp:Label id="capUNITS" runat="server">% of Units</asp:Label> 
                <br />									
                                    
                <asp:textbox id='txtUNITS' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>                                    
				                   
			
            <TD class="midcolora"  width="33%">
            <asp:Label id="capPERCENT_OCUP" runat="server">% of Occupancy</asp:Label> 
                <br /> 									
                                    
                <asp:textbox id='txtPERCENT_OCUP' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				
				                   
			</TD>
           <TD class="midcolora"  width="33%">
           <asp:Label id="capIS_INSURED" runat="server">Is property owner named additional insured</asp:Label> 
                <br />                                                                      
									
           <asp:DropDownList id='cmbIS_INSURED' runat='server' Height="16px" Width="145px"></asp:DropDownList>
           
           </TD>
             </tr>
             <tr>
			 <TD class="midcolora"  width="33%">
             <asp:Label id="capTENANT_LIABILITY" runat="server">Are tenants required by lease to carry liability insurnce with equal limits or better?</asp:Label> 
                <br />                                                                      
									
                        <asp:DropDownList id='cmbTENANT_LIABILITY' runat='server' Height="16px" 
                            Width="145px"></asp:DropDownList>
				</TD>                                    
				                   
			
            <TD class="midcolora"  width="33%">
            <asp:Label id="capRESTURANT_OCUP" runat="server">Are there any restaurant occupancies?</asp:Label> 
                <br />									
                                    
                        <asp:DropDownList id='cmbRESTURANT_OCUP' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList>
				
				                   
			</TD>
            <TD class="midcolora"  width="33%">
            <asp:Label id="capPERCENT_SALES" runat="server">What is the percentage of sales that is liquor, beer or wine?</asp:Label> 
                <br />
                <asp:textbox id='txtPERCENT_SALES' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
            </TD>
             </tr>
           
             <tr>
			<TD   class="midcolora">
				<asp:Label id="capFLAME_COOKING" runat="server">Is there frying,gilling or open flame cooking?</asp:Label> 
                <br />									
                                    
                        <asp:DropDownList id='cmbFLAME_COOKING' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList>
				                   
								</TD>                                    
				                   
			
            <TD class="midcolora"  width="33%">
				<asp:Label id="capSEPARATE_BAR" runat="server">Is there a separate bar?</asp:Label> 
                <br />                                                                    
									
                        <asp:DropDownList id='cmbSEPARATE_BAR' runat='server' Height="16px" 
                            Width="145px"></asp:DropDownList>
				                   
			</TD>
            <TD class="midcolora"  width="33%"><asp:Label id="capNUM_FRYERS" runat="server">Number of Deep Fryers</asp:Label><br />
                                    
                        <asp:DropDownList id='cmbNUM_FRYERS' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList></TD>
             </tr>
             <tr>
			  <TD class="midcolora"  width="33%">
				<asp:Label id="capNUM_GRILLS" runat="server">Number of Grills</asp:Label><br />
                                    
                        <asp:DropDownList id='cmbNUM_GRILLS' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList>
				                   
								</TD>                                    
				                   
			
            <TD class="midcolora"  width="33%">
				<asp:Label id="capIS_ENTERTNMT" runat="server">What type of live entertainment?</asp:Label><br />        
									
                        <asp:DropDownList id='cmbIS_ENTERTNMT' runat='server' Height="16px" 
                            Width="145px"></asp:DropDownList>
				                   
			 </TD>
             <TD class="midcolora"  width="33%"><asp:Label id="capDUCT_SYS" runat="server">Is there a hood and duct system?</asp:Label> 
                <br />								
                                    
                        <asp:DropDownList id='cmbDUCT_SYS' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList></TD>
             </tr>
             <tr>
			 <TD class="midcolora"  width="33%">
				
            <asp:Label id="capBBQ_PIT" runat="server">Does the restaurant have a BBQ pit?</asp:Label> 
                <br />                                                                     
									
                        <asp:DropDownList id='cmbBBQ_PIT' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				
				                   
								</TD>                                    
				                   
			
            <TD class="midcolora"  width="33%">
				<asp:Label id="capBBQ_PIT_DIST" runat="server">What is the distance of the BBQ pit from the main bulding?</asp:Label> 
				<br />                                                                  									
                       
               <asp:textbox id='txtBBQ_PIT_DIST' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
			 </TD>
             <TD class="midcolora"  width="33%">
             <asp:Label id="capSUPPR_SYS" runat="server">Is there  a working automatic(Ansul) dry chemical fire suppressant system?</asp:Label> 
               <br />								
                                    
            <asp:DropDownList id='cmbSUPPR_SYS' runat='server' Height="16px" 
            Width="145px"></asp:DropDownList></TD>
             </tr>
             <tr>
			 <TD class="midcolora"  width="33%">
				<asp:Label id="capBLDG_TYPE_COOKNG" runat="server">What is the type of construction of the building for outside cooking?</asp:Label> <br />
									
            <asp:DropDownList id='cmbBLDG_TYPE_COOKNG' runat='server' Height="16px" 
                Width="145px"></asp:DropDownList>                   
			</TD>
             <TD class="midcolora"  width="33%">
				<asp:Label id="capDUCT_CLND_PST_SIX_MONTHS" runat="server">Are the ducts cleaned at least every 6 months by contract with outside firm?</asp:Label> 
                <br />								
                                    
                        <asp:DropDownList id='cmbDUCT_CLND_PST_SIX_MONTHS' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList>                   
			</TD>					                   			                             				                   
			
            <TD class="midcolora"  width="33%">
				                   
			</TD>
             </tr>
           
		
             <tr>
                <td class='midcolora' colspan="1" valign="bottom">
                    <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
                    <cmsb:CmsButton runat="server" CausesValidation="false"  ID="btndelete"  
                                            CssClass="clsButton" onclick="btndelete_Click"></cmsb:CmsButton>
                </td>
                <td class='midcolora' colspan="2">
                    <table width="100%">
                        <tr>
                            <td class="midcolorr" colSpan="2" >							
						        <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>                            
                            </td> 	
                        </tr>
                    </table>
                </td>
                                
              </tr>           
             
            </TABLE>

        <%--the main outer table--%>
        </TD>
		</TR>
                            <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
                            <INPUT id="hidSHOP_ID" type="hidden" value="0" name="hidSHOP_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
        </table>
    </form>
</body>
</html>

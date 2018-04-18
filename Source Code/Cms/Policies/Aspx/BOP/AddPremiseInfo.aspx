<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPremiseInfo.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddPremiseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL_BOP_PREMISES_LOC_DETAILS</title>
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

                  document.POL_BOP_PREMISES_LOC_DETAILS.reset();
                  //populateXML();
                  //    BillType();
                  // populateXML();
                  //    BillType();
                  // DisableValidators();
                  //ChangeColor();   
                  //var updFrom = document.getElementById('cmbUpdatedFrom').options[document.getElementById('cmbUpdatedFrom').selectedIndex].value; 
                  //document.getElementById('cmbACCOUNT_ID').options.selectedIndex = -1; 
                  document.getElementById('txtOTH_PROTECTION').value = '';

                 document.getElementById('txtSAFE_VAULT_CLASS').value = '';
                 document.getElementById('txtSAFE_VAULT_MANUFAC').value = '';
                 document.getElementById('txtRIGHT_EXP_DESC').value = '';
                 document.getElementById('txtREAR_EXP_DIST').value = '';

                 document.getElementById('txtFRONT_EXP_DESC').value = '';
                 document.getElementById('txtRIGHT_EXP_DIST').value = '';
                 document.getElementById('txtFRONT_EXP_DIST').value = '';
                 document.getElementById('txtLEFT_EXP_DESC').value = '';
                 document.getElementById('txtLEFT_EXP_DIST').value = '';


                 document.getElementById('txtMAX_CASH_PREM').value = '';
                 document.getElementById('txtMAX_CASH_MSG').value = '';
                 document.getElementById('txtMONEY_OVER_NIGHT').value = '';
                 document.getElementById('txtFREQUENCY_DEPOSIT').value = '';
                 document.getElementById('txtSAFE_DOOR_CONST').value = '';
                 document.getElementById('txtGRADE').value = '';
                 document.getElementById('txtREAR_EXP_DESC').value = '';
                 
               

                 
                
                 document.getElementById('cmbSAFE_VAULT_LBL').value = '';
                 document.getElementById('cmbALARM_DESC').value = '';
                 document.getElementById('cmbCYL_DOOR_LOCK').value = '';
                 document.getElementById('cmbPREMISE_ALARM').value = '';
                 document.getElementById('cmbALARM_TYPE').value = '';
                 document.getElementById('cmbMED_EQUIP').value = ''
                 document.getElementById('cmbIS_RES_SPACE').value = '';
                 document.getElementById('cmbIS_ALM_USED').value = ''
                 //rat
                
                 document.getElementById('cmbBOILER').value = '';
                 document.getElementById('cmbRES_SPACE_SMK_DET').value = '';
                 document.getElementById('cmbFIRE_STATION_DIST').value = '';
                 document.getElementById('cmbFIRE_HYDRANT_DIST').value = ''; 
                                                                                                               
                  

                  return false;
              }
        
        
        </script>
</head>
<body leftMargin='0' topMargin='0' onload='ApplyColor();'>
    <form  id='POL_BOP_PREMISES_LOC_DETAILS' method='post' runat='server'>
    <%--  <div class="midcolora"  style="width: 100%; display: block; border: 0px solid #000;">

			    
		</div>--%>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
		<TD>
            <TABLE width='100%' border='0' align='center'>
              <tr style="background-color:Orange">
                    <td class="headereffectCenter" colspan="4" align="center">
                        <asp:Label ID="lblHeader" runat="server" >Premises Information</asp:Label>
                    </td>
             </tr>
              <tr id="trErrorMsgs" runat="server">
								<td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="3">
                                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                </td>
							</tr>		
              <tr  class='midcolora'>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td width="10%"><asp:Label ID="capLOCATION_ID" runat="server" >Location #</asp:Label></td>
                                <td width="25%"><asp:Label ID="lblLOCATION_ID" runat="server" ></asp:Label></td>
                                <td width="10%"><asp:Label ID="capBUILDING_ID" runat="server" >Building #</asp:Label></td>
                                <td width="25%"><asp:Label ID="lblBUILDING_ID" runat="server" ></asp:Label></td>
                                <td width="10%"><asp:Label ID="capCLASS_CODE" runat="server" >Class Code</asp:Label></td>
                                <td width="20%"><asp:Label ID="lblCLASS_CODE" runat="server" ></asp:Label></td>

                            </tr>
                        </table>
                    </td>
             </tr>        

             <tr>
             <td colspan="3">&nbsp;</td>
             </tr>
             
             <tr>
                <td class='midcolora'width="33%"><asp:Label id="capBLANKET" runat="server">Blanket Rate</asp:Label>
                 <br />
                 <asp:DropDownList ID="cmbBLANKET" runat="server" CausesValidation="true"></asp:DropDownList>
                 </td>
			    <TD   class='midcolora'width="33%">
				<asp:Label id="capFIRE_HYDRANT_DIST" runat="server">Distance to Fire Hydrant</asp:Label><span class="mandatory">*</span> 
               <br />									
                                    
                 <asp:DropDownList ID="cmbFIRE_HYDRANT_DIST" runat="server" CausesValidation="true"></asp:DropDownList><br />
                  <asp:requiredfieldvalidator id="rfvFIRE_HYDRANT_DIST" runat="server" ControlToValidate="cmbFIRE_HYDRANT_DIST" ErrorMessage="Please select distance from fire hydrant."
				Display="Dynamic" InitialValue=" "></asp:requiredfieldvalidator></TD> 
                <TD class="midcolora">
				<asp:Label id="capFIRE_STATION_DIST" runat="server">Distance to Fire Station:</asp:Label><span class="mandatory">*</span> <br />         
									
              
                        <asp:DropDownList ID="cmbFIRE_STATION_DIST" runat="server" CausesValidation="true"></asp:DropDownList><br />
                      
                       <asp:requiredfieldvalidator id="rfvFIRE_STATION_DIST" runat="server" ControlToValidate="cmbFIRE_STATION_DIST" ErrorMessage="Please select distance from fire station."
													Display="Dynamic" InitialValue=" "></asp:requiredfieldvalidator>
                             
				                   
								</TD>
             </tr>
        
             <tr>
			    <TD   class='midcolora'  width="33%">
			<asp:Label id="capLST_ALL_OCCUP" runat="server">List all occupancies at this premise</asp:Label> 
            <br />								
                                    
            <asp:textbox id='txtLST_ALL_OCCUP' runat='server' size='50' maxlength='0' 
                    Width="200px"></asp:textbox>
				                   
							</TD>                                    
                <TD class="midcolora" width="33%">
			<asp:Label id="capFIRE_DIST_NAME" runat="server"> Fire District Name:</asp:Label><br />                 
									
            <asp:textbox id='txtFIRE_DIST_NAME' runat='server' size='50' maxlength='0' 
                    Width="200px"></asp:textbox><%--<br />
			<asp:RegularExpressionValidator ID="revFIRE_DIST_NAME" runat="server" 
                ControlToValidate="txtFIRE_DIST_NAME" Display="Dynamic"></asp:RegularExpressionValidator>     --%>              
							</TD>
                <TD   class='midcolora'>
				<asp:Label id="capANN_SALES" runat="server">Annual Sales/Receipts:</asp:Label> 
              <br />									
                                    
                <asp:textbox id='txtANN_SALES' runat='server' size='50' maxlength='0' 
                        Width="200px" CssClass="inputcurrency"></asp:textbox>
				                   
								</TD>
             </tr>
             <tr>
                <TD class="midcolora">
				<asp:Label id="capFIRE_DIST_CODE" runat="server"> Fire District Code:</asp:Label> <br />                
									
                <asp:textbox id='txtFIRE_DIST_CODE' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>
                <TD   class='midcolora'>
				<asp:Label id="capTOT_PAYROLL" runat="server">Total Payroll:</asp:Label><span class="mandatory">*</span> 
              <br />									
                                    
                <asp:textbox id='txtTOT_PAYROLL' runat='server' size='50' maxlength='0' 
                        Width="200px" CssClass="inputcurrency"></asp:textbox><br />
                        <asp:requiredfieldvalidator id="rfvTOT_PAYROLL" runat="server" ControlToValidate="txtTOT_PAYROLL" ErrorMessage="Please enter total payroll."
													Display="Dynamic"></asp:requiredfieldvalidator>
                        <asp:RegularExpressionValidator ID="revTOT_PAYROLL" runat="server" 
                    ControlToValidate="txtTOT_PAYROLL" Display="Dynamic"></asp:RegularExpressionValidator>
				                   
								</TD>                                    
                <TD class="midcolora">
				<asp:Label id="capBCEGS" runat="server">Building Code Grade-BCEGS</asp:Label><br />                
									
                <%--<asp:textbox id='txtBCEGS' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>--%>
                        <asp:Label id="Label8" runat="server"></asp:Label>
				                   
								</TD>
             </tr>
          
           <tr>
	   	  	   <TD   class='midcolora'>
				<asp:Label id="capRATE_NUM" runat="server">Rate Number:</asp:Label> 
              <br />									
                                    
               <asp:Label id="Label5" runat="server"></asp:Label> </TD>                                    
               <TD class="midcolora">
				<asp:Label id="capCITY_LMT" runat="server">Inside City Limits?</asp:Label> 
                <br />                                                               
									
                        <asp:DropDownList id='cmbCITY_LMT' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				                   
								</TD>
               <TD   class='midcolora'>
				<asp:Label id="capRATE_GRP" runat="server">Rate Group:</asp:Label> 
              <br />									
                                    
               <asp:Label id="Label14" runat="server"></asp:Label> </TD>
             </tr>
           <tr style="height:60px">
             <TD class="midcolora">
				<asp:Label id="capSWIMMING_POOL" runat="server">Is there a swimming pool on premises?</asp:Label> 
               <br />                                                                  
									
                        <asp:DropDownList id='cmbSWIMMING_POOL' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				                   
								</TD>
             <TD   class='midcolora'>
				<asp:Label id="capRATE_TER_NUM" runat="server">Rate Territory Number:</asp:Label> 
              <br />								
                                    
               <asp:Label id="Label15" runat="server"></asp:Label> </TD>                                    
             <TD class="midcolora">
				<asp:Label id="capPLAY_GROUND" runat="server">Is there a playground on premise?</asp:Label> 
               <br />                                                                    
									
                        <asp:DropDownList id='cmbPLAY_GROUND' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				                   
								</TD>
             </tr>
           
           <tr style="height:70px">
			 <TD   class='midcolora'>
				<asp:Label id="capPROT_CLS" runat="server">Protection Class:</asp:Label><span class="mandatory">*</span>  
               <br />									
                                    
                <asp:textbox id='txtPROT_CLS' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox><br />
                        <asp:requiredfieldvalidator id="rfvPROT_CLS" runat="server" ControlToValidate="txtPROT_CLS" ErrorMessage="Please enter protection class"
													Display="Dynamic"></asp:requiredfieldvalidator>
                          <asp:RegularExpressionValidator ID="revPROT_CLS" runat="server" 
                    ControlToValidate="txtPROT_CLS" Display="Dynamic"></asp:RegularExpressionValidator>
                        
                        </TD>                                    
             <TD class="midcolora">
				<asp:Label id="capBUILD_UNDER_CON" runat="server">Is the building under construction?</asp:Label> 
               <br />                                                                    
									
                        <asp:DropDownList id='cmbBUILD_UNDER_CON' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				                   
								</TD>
             <TD   class='midcolora'>
				<asp:Label id="capIS_ALM_USED" runat="server">Is aluminium wiring used?</asp:Label> 
               <br />									
                                    
               
                         <asp:DropDownList id='cmbIS_ALM_USED' runat='server' Height="16px" Width="145px"></asp:DropDownList>
                        </TD>
           </tr>
             <tr>
              <TD class="midcolora">
				<asp:Label id="capBUILD_SHPNG_CENT" runat="server">Is the building a shopping/strip center?</asp:Label> 
                <br />                                                                   
									
                        <asp:DropDownList id='cmbBUILD_SHPNG_CENT' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				                   
								</TD>
              <TD   class='midcolora'>
				<asp:Label id="capIS_RES_SPACE" runat="server">Is there any residential space?</asp:Label> 
               <br />								
                                    
                <asp:DropDownList id='cmbIS_RES_SPACE' runat='server' Height="16px" Width="145px"></asp:DropDownList></TD>                                    
              <TD class="midcolora">
				<asp:Label id="capBOILER" runat="server">Does applicant have boiler for heating or processing?</asp:Label> 
               <br />                                                                   
									
                        <asp:DropDownList id='cmbBOILER' runat='server' Height="16px" Width="145px"></asp:DropDownList>
				                   
								</TD>
             </tr>
             <tr>
		  	  <TD   class='midcolora'>
				<asp:Label id="capRES_SPACE_SMK_DET" runat="server">Does the residential space have smoke detectors?</asp:Label> 
               <br />							
                                    
               <asp:DropDownList id='cmbRES_SPACE_SMK_DET' runat='server' Height="16px" Width="145px">
               <asp:ListItem Text="None" Value="-1"></asp:ListItem>
               <asp:ListItem Text="1" Value="1"></asp:ListItem>
               <asp:ListItem Text="2" Value="2"></asp:ListItem>
               <asp:ListItem Text="3" Value="3"></asp:ListItem>
               <asp:ListItem Text="4" Value="4"></asp:ListItem>
               
               </asp:DropDownList></TD>                                    
              <TD class="midcolora">
				<asp:Label id="capMED_EQUIP" runat="server">Any specialized equipment,such as medical equipment or other valued at over $100,000?</asp:Label> 
               <br />                                                                   
									
                        <asp:DropDownList id='cmbMED_EQUIP' runat='server' Height="16px" Width="145px"></asp:DropDownList></TD>				                  
			  <TD   class='midcolora'>
				<asp:Label id="capRES_OCC" runat="server">Residential Occupancy</asp:Label> 
               <br />								
                                    
               <asp:DropDownList id='cmbRES_OCC' runat='server' Height="16px" Width="145px"></asp:DropDownList></TD>					
             </tr>
                          
             <tr>
			                                    
				                   
			<td class='midcolora'></td>
                	                			                               			                  
								
             </tr>
             <tr>
              <td class="headerEffectSystemParams">
                        <asp:Label ID="Label1" runat="server" >Security / Crime</asp:Label>
                    </td>
             </tr>
           <tr>
			 <TD   class='midcolora'>
				<asp:Label id="capALARM_TYPE" runat="server">Alarm Type:</asp:Label> 
               <br /><asp:DropDownList id='cmbALARM_TYPE' runat='server' 
                        Height="16px" Width="145px"></asp:DropDownList>									
                                    
               </TD>                                    
              
              	<TD   class='midcolora'>
				<asp:Label id="capALARM_DESC" runat="server">Alarm Description:</asp:Label> 
                <br /><asp:DropDownList id='cmbALARM_DESC' runat='server' 
                        Height="16px" Width="145px"></asp:DropDownList>									
                                    
               </TD>	     
               
               <td class="midcolora" width = "33%"></td>                                                            
								                                        				                   
								
             </tr>
             <tr>
               <TD class="midcolora">
				<asp:Label id="capMAX_CASH_PREM" runat="server">Maximum Cash on Premise:</asp:Label> 
              <br />
                <asp:textbox id='txtMAX_CASH_PREM' runat='server' 
                        size='50' maxlength='0' 
                        Width="200px" CssClass="inputcurrency"></asp:textbox></TD>

              <TD class="midcolora">
				<asp:Label id="capMAX_CASH_MSG" runat="server">Maximum Cash with Messenger:</asp:Label> 
                <br />
                <asp:textbox id='txtMAX_CASH_MSG' runat='server' 
                        size='50' maxlength='0' 
                        Width="200px" CssClass="inputcurrency"></asp:textbox></TD>

                        <td class="midcolora" width="33%"></td>
					                                        				                   
             </tr>
             <tr>
                <TD   class='midcolora' colspan="3">
				<asp:Label id="Label7" runat="server">Extent of Protection:</asp:Label><br />
                <%--  <asp:Label id="Label8" runat="server">Safe or Valut:</asp:Label>--%>
                </TD>
             </tr>

             <tr>
                			   <TD   class='midcolora'>
             <asp:Label id="Label6" runat="server">Safe or Vault:</asp:Label> <br />                                               
									
                        <asp:DropDownList id='cmbSAFE_VAULT' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList></TD>  
                        
                 <TD   class='midcolora'>
             <asp:Label id="capPREMISE_ALARM" runat="server">Premises Alarm:</asp:Label> <br />          
									
                        <asp:DropDownList id='cmbPREMISE_ALARM' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList></TD>  
                        
                         							
               

                                  <td class="midcolora" width="33%"></td>
             </tr>
           
             <tr>
			
              <TD class="midcolora">
				<asp:Label id="capMONEY_OVER_NIGHT" runat="server">Money on Premise Over night:</asp:Label> 
               <br />
                <asp:textbox id='txtMONEY_OVER_NIGHT' 
                        runat='server' size='50' maxlength='0' 
                        Width="200px" CssClass="inputcurrency"></asp:textbox></TD>  

                 <TD class="midcolora">
			 <asp:Label id="capFREQUENCY_DEPOSIT" runat="server">Frequency of Deposits:</asp:Label>
               <br />                                                                    
									
                <asp:textbox id='txtFREQUENCY_DEPOSIT' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>  
                        
                         <td class="midcolora" width="33%"></td>                                                                  
								                                        				                   
								
             </tr>
 
             <tr>
               <TD class="midcolora">
			 <asp:Label id="capSAFE_DOOR_CONST" runat="server">Safe Door Construction:</asp:Label>
               <br />                                                                  
									
                <asp:textbox id='txtSAFE_DOOR_CONST' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>		
			<TD   class='midcolora'>
             <asp:Label id="capCYL_DOOR_LOCK" runat="server">Deadbolt Cyclinder Door Locks:</asp:Label><br />
									
                        <asp:DropDownList id='cmbCYL_DOOR_LOCK' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList></TD>   							
            <TD class="midcolora">
			 <asp:Label id="capGRADE" runat="server">Grade</asp:Label>
             <br />                                                                    
									
                <asp:textbox id='txtGRADE' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>                                                                    
			
				 <br /> 				                                        				                   
								
             </tr>

            <tr>
            <TD   class='midcolora'>
				<asp:Label id="Label13" runat="server">Safe/Vault/Receptacle:</asp:Label>
             </TD>
             <TD class="midcolora">
				<asp:Label id="capOTH_PROTECTION" runat="server">Other Protection (Fences, lightning & Etc):</asp:Label> 
               <br /><asp:textbox id='txtOTH_PROTECTION' 
                        runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>                                                                    
			 <TD   class='midcolora' >
				<asp:Label id="capSAFE_VAULT_LBL" runat="server">Label</asp:Label>
           
                 
              <br />                                                                    
									
                        <asp:DropDownList id='cmbSAFE_VAULT_LBL' runat='server' Height="16px" 
                        Width="145px"></asp:DropDownList></TD>   							
             	                                        				                   
            </tr>

             <tr>
             <TD   class='midcolora'>
				<asp:Label id="capSAFE_VAULT_CLASS" runat="server">Class</asp:Label>
          
                 
              <br />
                    <asp:textbox id='txtSAFE_VAULT_CLASS' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD> 	
			<TD   class='midcolora'>
				<asp:Label id="capSAFE_VAULT_MANUFAC" runat="server">Manufacturers Name</asp:Label>
            
                 
              <br />
                    <asp:textbox id='txtSAFE_VAULT_MANUFAC' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>   							
                                                                                 				                   			
            <TD class="midcolora">
			</TD>                                                                    
					                    				                   
								
             </tr>
             <tr>
                    <td class='headerEffectSystemParams' colspan="3">
                        <asp:Label ID="Label2" runat="server" >Surrounding Exposure & Other Occupancies</asp:Label>
                    </td>
             </tr>
             <tr ><td id="tr001" runat="server"  colSpan="3">
             <table width='100%' border='0' align='center'>
              <tr>
                    <td class="headereffectCenter"width="33%" colspan="1" align="left">
                        <asp:Label ID="Label3" runat="server" >Right Exposure </asp:Label>
                    </td>
                    <td class="headereffectCenter"width="33%"  colspan="1" align="left">
                        <asp:Label ID="Label4" runat="server" >Front Exposure</asp:Label>
                    </td>
             </tr>

             
              <tr>
			<TD   class='midcolora'width="33%" >
				<asp:Label id="capRIGHT_EXP_DESC" runat="server">Description</asp:Label> 
              <br />									
                                    
                <asp:textbox id='txtRIGHT_EXP_DESC' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>                                    
				                   
			
            <TD class="midcolora"width="33%" >
				<asp:Label id="capFRONT_EXP_DESC" runat="server">Description</asp:Label> 
              <br />                                                                    
									
                <asp:textbox id='txtFRONT_EXP_DESC' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>
             </tr>
              <tr>
			<TD   class='midcolora'width="33%" >
				<asp:Label id="capRIGHT_EXP_DIST" runat="server">Distance</asp:Label> 
              <br />								
                                    
                <asp:textbox id='txtRIGHT_EXP_DIST' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>                                    
				                   
			
            <TD class="midcolora"width="33%" >
				<asp:Label id="capFRONT_EXP_DIST" runat="server">Distance</asp:Label> 
               <br />                                                                 
									
                <asp:textbox id='txtFRONT_EXP_DIST' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>
             </tr>
             <tr >
                    <td class='headereffectCenter' colspan="1" align="Left">
                        <asp:Label ID="Label11" runat="server" >Left Exposure </asp:Label>
                    </td>
                    <td class="headereffectCenter" colspan="1" align="left">
                        <asp:Label ID="Label12" runat="server" >Rear Exposure</asp:Label>
                    </td>
             </tr>
             <tr>
			    <TD   class='midcolora'width="33%" >
				<asp:Label id="capLEFT_EXP_DESC" runat="server">Description</asp:Label> 
               <br />								
                                    
                <asp:textbox id='txtLEFT_EXP_DESC' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>                                    
				                   
			
            <TD class="midcolora"width="33%" >
				<asp:Label id="capREAR_EXP_DESC" runat="server">Description</asp:Label> 
               <br />                                                                   
									
                <asp:textbox id='txtREAR_EXP_DESC' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>
             </tr>
              <tr>
			<TD   class='midcolora' width="50%" >
				<asp:Label id="capLEFT_EXP_DIST" runat="server">Distance</asp:Label> 
               <br />									
                                    
                <asp:textbox id='txtLEFT_EXP_DIST' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox></TD>                                    
				                   
			
            <TD class="midcolora" width="50%" >
				<asp:Label id="capREAR_EXP_DIST" runat="server">Distance</asp:Label> 
               <br />                                                                     
									
                <asp:textbox id='txtREAR_EXP_DIST' runat='server' size='50' maxlength='0' 
                        Width="200px"></asp:textbox>
				                   
								</TD>
             </tr>
             


             <%--Below Butoons--%>  
              <tr>
                <td class='midcolora' colspan="1" valign="bottom" style="width:190px">
                    <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Save"></cmsb:cmsbutton>
                    <cmsb:CmsButton runat="server" CausesValidation="false"  ID="btndelete"  
                                            CssClass="clsButton" onclick="btndelete_Click"></cmsb:CmsButton>
                      
                </td>
               
                <td class='midcolora' colspan="1">
                    <table width="100%">
                        <tr>
                            <td class="midcolorr" colSpan="2">							
						        <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Reset"></cmsb:cmsbutton>                            
                            </td> 	
                        </tr>
                    </table>
                </td>
                                
              </tr> 
              </table>
             </td></tr>
            </TABLE>

        <%--the main outer table--%>
        </TD>
		</tr>
                            <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				            <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				            <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
                            <INPUT id="hidLOC_DETAILS_ID" type="hidden" value="0" name="hidLOC_DETAILS_ID" runat="server">
				            <INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
				            <INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
        </table>
    </form>
</body>
</html>

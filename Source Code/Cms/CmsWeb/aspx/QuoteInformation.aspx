<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteInformation.aspx.cs" Inherits="Cms.CmsWeb.aspx.QuoteInformation" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Private Vehicle proposal Form</title>
    <style type="text/css">
        .style2
        {
            font-size: small;
            font-style: italic;
        }
    </style>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script> 
	<script>
	  function Print() {
          document.getElementById("btnPrint").style.display = "none";
          
          window.print()
          document.getElementById("btnPrint").style.display = "inline";
          return false;
      }
      function GetVehicleNo() {
          if (document.getElementById("txtREG_NO").value != "")
          {
            document.getElementById("txtVehicleNo").value = document.getElementById("txtREG_NO").value; 
          }
      }
      function EnableDisable() {
          
          if (document.getElementById("rbDEMERIT_YES").checked == true) {
              document.getElementById("txtDemerit_Desc").disabled = false;
              document.getElementById("txtDemerit_Desc").focus();
          }
          else {
              document.getElementById("txtDemerit_Desc").disabled = true;
              //document.getElementById("txtDemerit_Desc").value = "";
          }

          if (document.getElementById("rbREJECT_YES").checked == true) {
              document.getElementById("txtReject_Desc").disabled = false;
              document.getElementById("txtReject_Desc").focus();
          }
          else {
              document.getElementById("txtReject_Desc").disabled = true;
              //document.getElementById("txtReject_Desc").value = "";
          }

          if (document.getElementById("rbDISEASE_YES").checked == true) {
              document.getElementById("txtDisease_Desc").disabled = false;
              document.getElementById("txtDisease_Desc").focus();
          }
          else {
              document.getElementById("txtDisease_Desc").disabled = true;
              //document.getElementById("txtDisease_Desc").value = "";
          }
      }

      //Added by Ruchika Chauhan on 13-Dec-2011 for TFS #1000
      function openDriverIndexPage() {

          var url = window.open('/cms/cmsweb/Aspx/QuoteDriverInformation.aspx', '', 'resizeable=no,menubar=no,toolbar=no,width=600,height=400');

      }

      function openWindow(path) {
          var myWin = window.open(path, 'MOTOR COI', 'width=400,height=200');
      }
      
	</script>
</head>
<body onload="EnableDisable();">
    <form id="QUOTE_INFO" runat="server">
    <table align="center" width="99%">   
        
             <tr>
				<td class="midcolorc" align="right" colSpan="4">
				    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
				</td>
			</tr>         
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label2" runat="server" Text="Fields having * are mandatory fields"></asp:Label>
                </td>
            </tr> 
            
            <tr class="midcolora" align="center">          
              <td align="center" colspan="4">
                <b>Proposal Form for Private Vehicle</b>
              </td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label9" runat="server" Text="IMPORTANT NOTICE TO THE PROPOSER"></asp:Label>
                </td>  
            </tr> 
            
            <tr  class="midcolora">
                <td colspan="4">1. Under Section 25(5) of the Insurance Act (Cap.142), or any subsequent amendments thereof, you must disclose
                in this Proposal Form, fully and faithfully, all the facts which you know or ought to know in respect of the risk
                proposed; otherwise the policy issued hereunder may be void.</td>            
            </tr>
            <tr class="midcolora">
                <td colspan="4">2. No insurance is in force until this Proposal has been accepted by the Company.</td>            
            </tr>
            <tr class="midcolora">
                <td colspan="4">3. The policy will carry a Premium Warrenty Clause which requires the premium to be paid in full
                within 60 days from the inception of the cover, failing which there would be no liability under the policy.</td>            
            </tr> 
            
            <tr class="midcolora">
                <td colspan="4">4. An additional excess of $2,000 will be imposed on top of the policy excess if the vehicle is driven by any authorized person (other than
                the Insured or Named Drivers) who is under the age of 22 and/or who has held a full driving licence for less than 2 years.</td>
            </tr>
            <tr class="midcolora"><td  colspan="4">&nbsp;</td></tr>
            <tr class="midcolora">
                <td>Date of Quotation</td>
                <td>&nbsp;</td>
                <td> 
                    <asp:Label ID="lblDOQ" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr class="midcolora">
                <td>Reference No.</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblQQ_NO" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr class="midcolora">
                <td>Agent/Broker</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblAGENT" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr class="midcolora">
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            
            <tr class="midcolora">
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label8" runat="server" Text="1. Premium Details"></asp:Label>
                </td>  
            </tr>            
            
            <tr class="midcolora">
                <td colspan="2">Total Premium (Inclusive of GST)</td>                 
                <td>
                    <asp:Label ID="Label33" runat="server" Text="S$"></asp:Label>
                    <asp:Label ID="lblTOTAL_PREMIUM" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>            
            </tr>
            <tr class="midcolora">
                <td colspan="2">Excess: Insured and Named Drivers(s)</td> 
                <td>
                    <asp:Label ID="Label32" runat="server" Text="S$"></asp:Label>
                    <asp:Label ID="lblEXCESS_NAMED" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>            
            </tr>
            <tr class="midcolora">
                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Unnamed Driver(s)</td> 
                <td>
                    <asp:Label ID="Label34" runat="server" Text="S$"></asp:Label>
                    <asp:Label ID="lblEXCESS_UNNAMED" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>            
            </tr> 
            
            <tr class="midcolora">
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            
            <tr class="midcolora">
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label7" runat="server" Text="2. Particulars of Proposer"></asp:Label>
                </td>  
            </tr>           
            
            
            <tr class="midcolora">
                <td width="20%" >  
                    <asp:Label ID="capSTATUS" runat="server" Text="Type"></asp:Label>
                </td>
                <td width="30%" > 
                    <%--<asp:DropDownList ID="cmbCUSTOMER_TYPE" runat="server">
                    </asp:DropDownList>--%>
                    <%--<asp:requiredfieldvalidator id="rfvCUSTOMER_TYPE" Runat="server" ControlToValidate="cmbCUSTOMER_TYPE" Display="Dynamic" ErrorMessage="Please select Customer Type."></asp:requiredfieldvalidator>--%>
                </td>
                <td width="20%" >
                    <asp:Label ID="lblStatusType" runat="server"></asp:Label>
                </td>
                <td width="30%" ></td>
            </tr>
            <tr class="midcolora">
                <td width="20%">
					<asp:Label ID="Label10" runat="server" Text="Name"></asp:Label>
				</td>
				<td colSpan="3">
				    <asp:Label ID="lblName" runat="server" Width="215px"></asp:Label>
				</td>
            </tr>
            <tr class="midcolora">
                <td width="20%">
                    <asp:Label ID="capAddress1" runat="server" Text="Address 1"></asp:Label>
                </td>
                <td width="30%">
                     <asp:textbox id="txtAddress1" runat="server" size="12" Width="215px" 
                         Height="18px"></asp:textbox>
                </td>
                <td width="20%">
                    <asp:Label ID="capAddress2" runat="server" Text="Address 2"></asp:Label>
                </td>
                <td width="30%">
                    <asp:textbox id="txtAddress2" runat="server" size="12" Width="215px" 
                        Height="18px"></asp:textbox>
                </td>
            </tr>
            <tr class="midcolora" runat="server" visible = "false">
                <td width="20%">
                    <asp:Label ID="capCity" runat="server" Text="City"></asp:Label>
                </td>
                <td width="30%">
                    <asp:textbox id="txtCity" runat="server" size="12" Width="150px" Height="18px"></asp:textbox>
                </td>
                <td width="20%">
                    <asp:Label ID="capState" runat="server" Text="State"></asp:Label>
                </td>
                <td width="30%">
                    <asp:textbox id="txtState" runat="server" size="12" Width="150px" Height="18px"></asp:textbox>
                </td>
            </tr>
            <tr class="midcolora">
                <td width="20%">
                    <asp:Label ID="capZIP" runat="server" Text="Postal Code"></asp:Label>
                </td>
                <td width="30%">
                    <asp:textbox id="txtZIP" runat="server" size="12" Width="150px" Height="18px"></asp:textbox>
                </td>
                <td width="20%">
                    <asp:Label ID="capContactNum" runat="server" Text="Contact Number"></asp:Label>
                </td>
                <td width="30%">
                    <asp:textbox id="txtContactNum" runat="server" size="12" Width="150px" 
                        Height="18px"></asp:textbox>
                </td>
            </tr>
            <tr class="midcolora">
                <td width="20%">
                    <asp:Label ID="capNationality" runat="server" Text="Nationality"></asp:Label>
                </td>
                <td width="30%">
                    <asp:Label ID="lblNationality" runat="server"></asp:Label>
                </td>
                <td width="20%">
                    <asp:Label ID="capPassport" runat="server" Text="NRIC No./Passport"></asp:Label>
                </td>
                <td width="30%">
                    <asp:textbox id="txtPassport" runat="server" size="12" maxlength="10" 
                        Width="150px" Height="18px"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capGENDER" runat="server" Text="Sex"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="lblGENDER" runat="server"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capDOB" runat="server" Text="Date of Birth (dd/mm/yyyy)"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblDOB" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
               <td class="midcolora" width="20%" >  
                    <asp:Label ID="capMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbMaritalStatus" runat="server" Height="16px" Width="130px">
                    </asp:DropDownList>                    
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capOCCUPATION" runat="server" Text="Occupation"></asp:Label>
                </td>
                <td class="midcolora" width="30%" > 
                    <asp:Label ID="lblOCCUPATION" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
               <td id=lbMerit class="midcolora" width="20%" >
                    <asp:Label ID="capDemerit" runat="server" Text="Demerit Points Free Discount"></asp:Label>
                </td>
                <td id="tdMerit" class="midcolora" width="30%" >
                     <asp:Label ID="lblDemerit" runat="server"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label16" runat="server" Text="Driving Experience(years)"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblDRIVE_EXP" runat="server"></asp:Label>
                    <asp:Label ID="Label31" runat="server" Text="Year(s)"></asp:Label>
                </td>
            </tr>
           
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
                     
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="capPOP" runat="server" Text="3. Particulars of Vehicle"></asp:Label>
                </td>  
            </tr>  
            
            <tr class="midcolora">
                <td width="20%">
                    <asp:Label ID="capNewVehicle" runat="server" Text="Is this a Brand New Vehicle?"></asp:Label>
                </td>
                <td colspan="3">
                    
                    <asp:RadioButton ID="rbIS_NEW_YES" runat="server" Text="Yes" GroupName="rbIS_NEW" />
                    <asp:RadioButton ID="rbIS_NEW_NO" runat="server" Text="No" GroupName="rbIS_NEW" />
                    
                </td>
            </tr>  
            
            <tr class="midcolora">
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capLTAREGISTRATION" runat="server" Text="Date of LTA Registration"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtREG_DD" runat="server" Height="18px" Width="25px"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtREG_MM" runat="server" Height="18px" Width="25px"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtREG_YYYY" runat="server" Height="18px" Width="32px"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label11" runat="server" Text="(dd/mm/yyyy)"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label12" runat="server" Text="Cover Note No."></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:textbox id="txtNoteNum" runat="server" size="12" maxlength="10" 
                        Width="150px" Height="18px"></asp:textbox>
                </td>
            </tr>  
            
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capREG_NO" runat="server" Text="Registration No"></asp:Label>
                    <SPAN class="mandatory" id="spnREG_NO">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:textbox id="txtREG_NO" runat="server" size="12" maxlength="10" 
                        Width="150px" Height="18px"></asp:textbox> <br />
                        <asp:requiredfieldvalidator id="rfvREG_NO" Runat="server" ControlToValidate="txtREG_NO"
									Display="Dynamic" ErrorMessage="Please enter Registration No." ></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capYEAR_OF_REG" runat="server" Text="Year of Registration"></asp:Label>                    
                </td>
                <td class="midcolora" width="30%" >
                     <asp:Label ID="lblYEAR_OF_REG" runat="server"></asp:Label>
                     &nbsp;
                    <asp:Label ID="Label1" runat="server" Text="(YYYY)"></asp:Label>
                </td>
            </tr>      
           
            <tr>
                <td class="midcolora" width="20%" > 
                    <asp:Label ID="capMAKE" runat="server" Text="Make of Vehicle"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                   <asp:Label ID="lblMAKE" runat="server"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capMODEL" runat="server" Text="Model of Vehicle"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblMODEL" runat="server"></asp:Label>            
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                     <asp:Label ID="capVEHICLE_TYPE" runat="server" Text="Vehicle Type" ></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblVEHICLE_TYPE" runat="server"></asp:Label>
                </td>
                <td class="midcolora" width="20%" > 
                    <asp:Label ID="capENG_CAPACITY" runat="server" Text="Engine Capacity"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblENG_CAPACITY" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capEngineNo" runat="server" Text="Engine No."></asp:Label>
                </td>
                <td class="midcolora" width="30%" >                   
                    <asp:textbox id="txtENGINE_NO" runat="server" size="12" maxlength="10" 
                        Width="150px" Height="18px"></asp:textbox>                   
                </td>
                <td class="midcolora" width="20%" >
                     <asp:Label ID="capChassisNo" runat="server" Text="Chassis No."></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:textbox id="txtChassisNo" runat="server" size="12" maxlength="10" 
                        Width="150px" Height="18px"></asp:textbox>  
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capHIRE_PURCHASE" runat="server" Text="Is this vehicle under any hire Purchase?"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                
                    <asp:RadioButton ID="rbHIRE_YES" runat="server" Text="Yes" GroupName="rbIS_HIRE" />
                    <asp:RadioButton ID="rbHIRE_NO" runat="server" Text="No" GroupName="rbIS_HIRE" />
                
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capNAME_FIN_COMP" runat="server" Text="Name of Finance Company"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:textbox id="txtNAME_FIN_COMP" runat="server" size="12" maxlength="80" 
                        Width="215px" Height="18px"></asp:textbox>  
                
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
                     
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label13" runat="server" Text="4. NCD Entitlement/Confirmation"></asp:Label>
                </td>  
            </tr>  
            
           <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capEXISTING_NCD" runat="server" Text="Existing NCD"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblEXISTING_NCD" runat="server"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capEXISTING_INSURER" runat="server" Text="Existing Insurer"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbEXISTING_INSURER" runat="server" Height="16px" 
                        Width="215px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capEXIST_POL_NUM" runat="server" Text="Existing Policy Number"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtEXIST_POL_NUM" runat="server" Width="205px" Height="18px"></asp:TextBox>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capEXP_DATE" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtEXP_DD" runat="server" Height="18px" Width="25px"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtEXP_MM" runat="server" Height="18px" Width="25px"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtEXP_YY" runat="server" Height="18px" Width="32px"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label14" runat="server" Text="(dd/mm/yyyy)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capVehicleNo" runat="server" Text="Vehicle No"></asp:Label>                
                </td>
                <td class="midcolora" width="30%" >
                     <asp:TextBox ID="txtVehicleNo" runat="server" Width="205px" Height="18px"></asp:TextBox>
                </td>
                <td class="midcolora" width="20%" colspan="2" style="width: 50%" >
                    <cmsb:CmsButton class="clsButton" ID="btnFetch" CausesValidation="true" runat="server" Text="Same as Registration No"></cmsb:CmsButton>
                </td>
            </tr>
            <tr class="midcolora">
                <td colspan="4" >
                    <asp:Label ID="Label15" runat="server"><b>Note:</b></asp:Label> 
                    <br />
                    <span><i>Premium is calculated based on declared NCD. If confirmation from your existing insuer states otherwise, we will recover the discount accorded to you.</i></span>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label3" runat="server" Text="5. Cover Required"></asp:Label>
                </td>  
            </tr> 
            
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capCOV_TYPE" runat="server" Text="Type"></asp:Label> 
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblType" runat="server"></asp:Label>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label17" runat="server" Text="No Claim Discount Protection"></asp:Label> 
                </td>
                <td class="midcolora" width="30%" >
                    <asp:Label ID="lblCLAIM_DISC" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label6" runat="server" Text="6. Period of Insurance"></asp:Label>
                </td>  
            </tr>  
            
            <tr>
                 <td class="midcolora" width="20%" >
                    <asp:Label ID="capFROM" runat="server" Text="From Date"></asp:Label>                    
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtFROM_DAY" runat="server" Height="18px" Width="25px" 
                        ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtFROM_MONTH" runat="server" Height="18px" Width="25px" 
                        ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtFROM_YEAR" runat="server" Height="18px" Width="32px" 
                        ReadOnly="true"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label18" runat="server" Text="(dd/mm/yyyy)"></asp:Label>                    
                </td>
                 <td class="midcolora" width="20%" >
                    <asp:Label ID="capTO" runat="server" Text="To Date"></asp:Label>                   
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtTO_DAY" runat="server" Height="18px" Width="25px" 
                        ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtTO_MONTH" runat="server" Height="18px" Width="25px" 
                        ReadOnly="true"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtTO_YEAR" runat="server" Height="18px" Width="32px" 
                        ReadOnly="true"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label19" runat="server" Text="(dd/mm/yyyy)"></asp:Label>   
                </td>
            </tr>
            
             <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label20" runat="server" Text="7. Additional Drivers (if any)"></asp:Label>
                </td>  
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label4" runat="server" Text="Please give details in respect of any person including yourself who to your knowledge, will drive the vehicle."></asp:Label>
                </td>
            </tr>
            
            <tr>
            <%--Modified by Ruchika Chauhan on 12-Dec-2011 for TFS #1000--%>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="lblDriverDetails" runat="server" Text="Please click here "></asp:Label>
                    <%--<span id="spnDriverDetails" runat="server" style="cursor: hand" onclick=""></span> --%>
                    <asp:HyperLink ID="hlkDriverDetails" runat="server" style="cursor:hand" onclick="openDriverIndexPage();"  Target="_blank">
                    <asp:Image ID="imgDriverDetails" runat="server" ImageUrl="/cms/cmsweb/Images/PolicyStatus.gif"></asp:Image> 
                    </asp:HyperLink>
                    <asp:Label ID="Label35" runat="server" Text="to enter Driver(s) details."></asp:Label>
                </td>
            </tr>
            
             <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label21" runat="server" Text="8. Claim History of Proposer & Named/Authorized Driver(s) (Last 3 Years)"></asp:Label>
                </td>  
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label23" runat="server" Text="Please give details of Claims History of Proposer & Named/Authorized Driver(s) for (Last 3 years)"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label24" runat="server" Text="Please click here"></asp:Label>
                    <asp:HyperLink ID="hlkClaimsDetails" runat="server" NavigateUrl="" >
                    <asp:image id="imgClaimsDetails" runat="server" imageurl="/cms/cmsweb/Images/PolicyStatus.gif"></asp:image>
                    </asp:HyperLink>
                    <asp:Label ID="Label_24" runat="server" Text ="to enter Claim(s) details."></asp:Label>
                </td>
            </tr>
            
             <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label22" runat="server" Text="9. General Questions"></asp:Label>
                </td>  
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label25" runat="server" Text="1. Have you or your named driver(s) been given/accumulated demerit points during the last 24 months?"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label26" runat="server" Text="If Yes, please specify the driver and the number of demerit points accumulated"></asp:Label>
                    
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="3">
                 
                    <asp:TextBox ID="txtDemerit_Desc" runat="server" Height="30px" Width="500px" 
                        TextMode="MultiLine"></asp:TextBox>
                 
                 </td>
                <td class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbDEMERIT_YES" runat="server" Text="Yes" GroupName="rbDEMERIT" onclick="EnableDisable();" />
                    <asp:RadioButton ID="rbDEMERIT_NO" runat="server" Text="No" GroupName="rbDEMERIT" onclick="EnableDisable();" />
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label27" runat="server" Text="2. Have you or your named driver(s) had any motor insurance proposal declined, cancelled or renewal rejected by any insurance company?"></asp:Label>
                
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label28" runat="server" Text="If Yes, please give Details"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="3">
                    <asp:TextBox ID="txtReject_Desc" runat="server" Height="30px" Width="500px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbREJECT_YES" runat="server" Text="Yes" GroupName="rbREJECT" onclick="EnableDisable();"   />
                    <asp:RadioButton ID="rbREJECT_NO" runat="server" Text="No" GroupName="rbREJECT" onclick="EnableDisable();"  />
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label29" runat="server" Text="3. Have you or any of your named driver(s) suffered any disease or infirmity that could impair the ability to drive?"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label30" runat="server" Text="If Yes, please give Details"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="3">
                    <asp:TextBox ID="txtDisease_Desc" runat="server" Height="30px" Width="500px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbDISEASE_YES" runat="server" Text="Yes" GroupName="rbDISEASE" onclick="EnableDisable();" />
                    <asp:RadioButton ID="rbDISEASE_NO" runat="server" Text="No"  GroupName="rbDISEASE"  onclick="EnableDisable();"   />
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            
             <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="Label5" runat="server" Text="Declaration"></asp:Label>
                </td>  
            </tr>            
            
            <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="capDeclaration1" runat="server">(1) I/We have declared to the best of my/our knowledge and belief that all the answers given in this Proposal are true and correct and I/we have not withheld any information likely to affect acceptance of this proposal.</asp:Label>
                    <br />
                    <asp:Label ID="capDeclaration2" runat="server">(2) I/We agree that this Proposal shall be the basis of the Contract between me/us and the Company and I/we further agree to accept the Company's policy subject to the terms exclusions and conditions expressed therein, endorsed thereon or attached thereto.</asp:Label>
                    <br />
                    <asp:Label ID="capDeclaration3" runat="server">(3) I/We undertake the vehicle to be insured is and will be kept in a good condition,and will not be driven by any person who to my/our knowledge has been refused motor insurance or continuance therefore.</asp:Label>
                    <br />
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
            
            
            
             <tr>
                <td class="midcolora" colspan="2">
                    <cmsb:CmsButton class="clsButton" ID="btnPrint" CausesValidation="true" runat="server" Text="Print"></cmsb:CmsButton>
                    <cmsb:CmsButton class="clsButton" ID="btnGenerate" CausesValidation="true" runat="server" Text="Generate COI" OnClick="btnGenerate_Click"></cmsb:CmsButton>
                </td>
                <td class="midcolora"  colspan="2" align="right"> 
                <table width="100%">
                        <tr>
                            <td align="right" width="100%">
                    <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                    Text="Save" OnClick="btnSave_Click"></cmsb:CmsButton>
                            </td>
                        </tr>
                </table> 
                   
                </td>
            </tr>
            
            <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>
			<input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>
			<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>			
			<input id="hid_ID" type="hidden" value="0" name="hid_ID" runat="server"/> 
			<input id="hidQQ_ID" type="hidden" value="0" name="hidQQ_ID" runat="server"/> 
			<input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server"/>
			<input id="hidOldData" type="hidden" name="Hidden1" runat="server"/> 
			<input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server"/>			
			<input id="hidOldXML" type="hidden" name="hidOldXML" runat="server"/>
			<input id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server"/>
			<input id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server"/> 
			<input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server"/>
			<input id="hidMsg" type="hidden" name="hidMsg" runat="server"/>
			<input id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server"/> 
			<input id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication" runat="server"/>
			<input id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server"/>	
			<input id="hidMODEL" type="hidden" name="hidMODEL" runat="server"/>
			<input id="hidMAKE" type="hidden" name="hidMAKE" runat="server"/>
			<input id="hidMODEL_TYPE" type="hidden" name="hidMODEL_TYPE" runat="server"/>
			
           
            
            
             <tr>
                <td class="midcolora" colspan="2">
                    &nbsp;</td>
                <td class="midcolora"  colspan="2" align="right"> 
                    &nbsp;</td>
            </tr>
            
                       
        </table>
    </form>
</body>
</html>

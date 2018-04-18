<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProtectiveDevicesInfo.aspx.cs" Inherits="Cms.Policies.Aspx.ProtectiveDevicesInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>ProtectiveDevicesInfo</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" language="javascript">

        function ResetTheForm() {
            document.PROTECTIVE_DEVICE_ID.reset();
        }  
        </script>
</head>
<body>
<form id="PROTECTIVE_DEVICE_ID" runat="server">
		<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0" border="0" align="center">
		<tr>
		<td colspan="4" class="midcolora" ></td>
		</tr>
		</td>
		<tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>				
       <tr>
        <td class="pageHeader" colspan="3">
            <asp:Label ID="lblMandatory" runat="server" ></asp:Label>
        </td>
        </tr>
    <tr>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capFIRE_EXTINGUISHER" runat="server" Text="FIRE EXTINGUISHER"></asp:Label></br>
                    <asp:CheckBox ID="chkFIRE_EXTINGUISHER" runat="server" />
                </td>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capSPL_FIRE_EXTINGUISHER_UNIT" Text="SPECIAL FIRE EXTINGUISHER" runat="server"></asp:Label></br>
                    <asp:CheckBox ID="chkSPL_FIRE_EXTINGUISHER_UNIT" runat="server" />
                </td>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capFOAM" runat="server" Text="FOAM"></asp:Label>
                    </br>
                    <asp:CheckBox ID="chkFOAM" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capMANUAL_FOAM_SYSTEM" runat="server" Text="MANUAL FOAM SYSTEM"></asp:Label></br>
                    <asp:CheckBox ID="chkMANUAL_FOAM_SYSTEM" runat="server" />
                </td>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capINERT_GAS_SYSTEM" Text="INERT GAS SYSTEM" runat="server"></asp:Label></br>
                    <asp:CheckBox ID="chkINERT_GAS_SYSTEM" runat="server" />
                </td>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capMANUAL_INERT_GAS_SYSTEM" runat="server" Text="MANUAL INERT GAS SYSTEM"></asp:Label>
                    </br>
                    <asp:CheckBox ID="chkMANUAL_INERT_GAS_SYSTEM" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capCOMBAT_CARS" runat="server" Text="COMBAT CARS"></asp:Label></br>
                    <asp:CheckBox ID="chkCOMBAT_CARS" runat="server" />
                </td>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capCORRAL_SYSTEM" Text="CORRAL SYSTEM" runat="server"></asp:Label></br>
                    <asp:CheckBox ID="chkCORRAL_SYSTEM" runat="server" />
                </td>
                <td width="33%" class="midcolora">
                    <asp:Label ID="capALARM_SYSTEM" runat="server" Text="ALARM SYSTEM"></asp:Label>
                    </br>
                    <asp:CheckBox ID="chkALARM_SYSTEM" runat="server" />
                </td>
            </tr>
            <tr>
            <td width="33%" class="midcolora">
                <asp:Label ID="capFREE_HYDRANT" runat="server" Text="FREE HYDRANTS"></asp:Label></br>
                <asp:CheckBox ID="chkFREE_HYDRANT" runat="server" />
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capSPRINKLERS" Text="SPRINKLERS" runat="server"></asp:Label></br>
                <asp:CheckBox ID="chkSPRINKLERS" runat="server" />
            </td>
            <td width="33%" class="midcolora">
            </td>
        </tr>
        <tr>
            <td width="33%" class="midcolora">
                <asp:Label ID="capSPRINKLERS_CLASSIFICATION" runat="server" Text="SPRINKLER CLASSIFICATION"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtSPRINKLERS_CLASSIFICATION" MaxLength="65"></asp:TextBox>
   
                <asp:RegularExpressionValidator ID="revSPRINKLERS_CLASSIFICATION" runat="server" 
                    ControlToValidate="txtSPRINKLERS_CLASSIFICATION" Display="Dynamic"></asp:RegularExpressionValidator>
   
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capFIRE_FIGHTER" Text="FIRE FIGHTERS" runat="server"></asp:Label></br>
                <asp:TextBox runat="server" ID="txtFIRE_FIGHTER" MaxLength="65" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="revFIRE_FIGHTER" runat="server" 
                    ControlToValidate="txtFIRE_FIGHTER" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capQUESTIION_POINTS" runat="server" Text="QUESTION POINTS"></asp:Label>
                </br>
                <asp:TextBox runat="server" ID="txtQUESTIION_POINTS" CssClass="INPUTCURRENCY" MaxLength="10" onblur="this.value=formatAmount(this.value,4)"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revQUESTIION_POINTS" runat="server" 
                    ControlToValidate="txtQUESTIION_POINTS" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td   class="midcolora">
                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" 
                    CausesValidation="False"></cmsb:cmsbutton>
				<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" 
                    Text="Activate/Deactivate" CausesValidation="false" 
                    onclick="btnActivateDeactivate_Click" Visible="False" />
            </td>
            <td  class="midcolora" Width="33%" >
            </td>
            <td    class="midcolorr">
               <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                    ></cmsb:cmsbutton>
               <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
                    causesvalidation="false" onclick="btnDelete_Click" Visible="False"></cmsb:cmsbutton>
                
               <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
               <input id="hidRiskId" type="hidden" value="0" name="hidRiskId" runat="server"/>
               <input id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server"/>
               <input id="hidLocationId" type="hidden" value="0" name="hidLocationId" runat="server"/>
		</td>
        </tr>
        </table>
 </form>     
      
</body>
</html>

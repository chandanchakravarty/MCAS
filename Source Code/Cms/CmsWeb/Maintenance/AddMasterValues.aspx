<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMasterValues.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddMasterValues" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<HTML>
	<HEAD runat="server">
		<title>MASTER VALUE DETAIL</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript" type="text/javascript">

		    function ResetTheForm()
            {
		        document.getElementById('txtCODE').value = '';
		        document.getElementById('txtDESCRIPTION').value = '';
		       
		    }
        </SCRIPT>

</HEAD>
<body>
    <form id="MNT_MASTER_VALUE" runat="server">
   <TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
		<tr>
			<td>
                <TABLE width='100%' border='0' align='center' border="1">
                   <tr id="trMessages" runat="server">
                      <td id="tdMessages" runat="server" class="pageHeader">
                        <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:Label><br />
                      </td>
                    </tr>
                    <tr id="trErrorMsgs" runat="server">
                        <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="4">
                            <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label><br />
                        </td>
                    </tr>
                    <tr id="trCODE" runat="server">
                      <td id="tdCODE" runat="server" class='midcolora' colspan="4">
                        <asp:Label ID="capCODE" runat="server"></asp:Label><span id="spnCODE" runat="server"></span> <br />
                        <asp:TextBox ID="txtCODE" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvCODE" runat="server" ControlToValidate="txtCODE" Display="Dynamic"></asp:RequiredFieldValidator>
                      </td>
                    </tr>
                    <tr id="trDESCRIPTION" runat="server">
                      <td id="tdDESCRIPTION" runat="server" class='midcolora' colspan="5">
                       <asp:Label ID="capDESCRIPTION" runat="server"></asp:Label><span id="spnDESCRIPTION" runat="server"></span><br />
                       <asp:TextBox ID="txtDESCRIPTION" runat="server"></asp:TextBox><br />
                       <asp:RequiredFieldValidator ID="rfvDESCRIPTION" runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic"></asp:RequiredFieldValidator>
                      </td>
                    </tr>
                    <tr id="trRECOVERY_TYPE" runat="server">
                     <td id="tdRECOVERY_TYPE" runat="server" class='midcolora' colspan="5">
                      <asp:Label ID="capRECOVERY_TYPE" runat="server"></asp:Label><span id="spnRECOVERY_TYPE" runat="server"></span><br />
                      <asp:DropDownList ID="cmbRECOVERY_TYPE" runat="server"></asp:DropDownList>
                      <asp:RequiredFieldValidator ID="rfvRECOVERY_TYPE" runat="server" ControlToValidate="cmbRECOVERY_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>
                     </td>
                    </tr>
                    <tr>
						<td class='midcolora' width='100%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
							Text="Deactivate" CausesValidation="False" onclick="btnActivateDeactivate_Click"   ></cmsb:cmsbutton>
						</td>
                        <td class='midcolorr'><cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' onclick="btnSave_Click" ></cmsb:cmsbutton></td>
				    </tr>
                 </TABLE>

                 
            </td>
         </tr>
            
     </TABLE>
         <input id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server" />
         <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
         <INPUT id="hidTYPE_UNIQUE_ID" type="hidden" value="NEW" name="hidTYPE_UNIQUE_ID" runat="server">
         <INPUT id="hidCountry" type="hidden"  name="hidCountry" runat="server">
         <INPUT id="hidOldData" type="hidden"  name="hidOldData" runat="server">
    </form>

        <script type="text/javascript">

            try {
                if (document.getElementById('hidFormSaved').value == "1") {

                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidTYPE_UNIQUE_ID').value);
                }
            }
            catch (err) {
            }      
		</script>
</body>
</html>

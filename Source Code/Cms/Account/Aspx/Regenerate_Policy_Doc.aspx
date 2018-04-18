<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regenerate_Policy_Doc.aspx.cs" Inherits="Cms.Account.Aspx.Regenerate_Policy_Doc" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>PostAgencyStatements</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
             <script type="text/javascript" type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		<script language="javascript" type="text/javascript">
		    function HideShowTransactionInProgress() {
		        DisableButton(document.getElementById('btnPost'));
		    }
        function validateRange(objSource, objArgs) {
            debugger;
           var e=  document.getElementById('CmbACTION_TYPE');
           var ActionType= e.options[e.selectedIndex].value;

     
            var Range = document.getElementById(objSource.controltovalidate).value;
            var len = Range.length;
            //Range = FormatAmountForSum(Range);
            if (ActionType == 1) {
                if (parseFloat(len) == 21)
                    objArgs.IsValid = true;
                else
                    objArgs.IsValid = false;
            }

        }

//        modified by naveen , itrack 631
        $(document).ready(function () {
           
            $("#CmbACTION_TYPE").change(function () {
                $("#lblMsg").hide();
            });

        });
		</script>

</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
			<col width="34%" />
			<col width="33%" />
			<col width="33%" />
				<tr>
					<td class="headereffectcenter" colspan="3">
						<asp:Label id="capheaders" runat="server"></asp:Label></asp:Label>
					</td>
				</tr>
		
				<tr>
					<td class="midcolorc" colspan="3">
                        <asp:Label ID="lblMsg" Visible="false"   runat="server" CssClass="errmsg"></asp:Label>
					</td>
				</tr>
		
				<tr>
					<td class="midcolora"><asp:Label id="capACTION_TYPE" runat="server"></asp:Label>
					</td>
					<td class="midcolora">
						<asp:DropDownList ID="CmbACTION_TYPE" runat="server" AutoPostBack="true" 
                            onselectedindexchanged="CmbACTION_TYPE_SelectedIndexChanged">
                    </asp:DropDownList> 
					</td>	
					<td class="midcolora">
					    &nbsp;</td>				
				</tr>
		
				<tr>
					<td class="midcolora"><asp:Label id="capPOLICY_NUMBER" runat="server"></asp:Label><span class="mandatory" id="spn_mandatory">*</span>
					</td>
					<td class="midcolora">
						<asp:TextBox ID="txtPOLICY_NUMBER" runat="server"></asp:TextBox>&nbsp&nbsp
						<asp:Button ID="btnClick" runat="server" CssClass="clsButton" 
                       Enabled="true" onclick="btnClick_Click" />
					</td>	
					<td class="midcolora">
					<asp:RequiredFieldValidator ID="rfvProcess" runat="server" ControlToValidate="txtPOLICY_NUMBER"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:CustomValidator ID="csvProcess" runat="server" ControlToValidate="txtPOLICY_NUMBER"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
					</td>				
				</tr>
				<tr>
					<td class="midcolora">
					<asp:Label id="capDISPLAY_VER_NO" runat="server"></asp:Label>
					</td>
					<td class="midcolora">
					
                    
                        <asp:DropDownList ID="CmbDISPLAY_VERSION_NO" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="CmbDISPLAY_VERSION_NO_SelectedIndexChanged">
                    </asp:DropDownList> 
					
                    
                    </td>
                    <td class="midcolora">
                    
					</td>					
				</tr>
				<tr><td class="midcolora" >
                    &nbsp;</td>
                    <td class="midcolora" >&nbsp;</td>
                    <td class="midcolora" >&nbsp;</td>
                    </tr>
				<tr><td class="midcolora" >
                   </td>
                    <td class="midcolora" >
                 
                        <asp:Button ID="btnProcess" runat="server" CssClass="clsButton" Enabled="true" 
                            onclick="btnProcess_Click" Width="160px" />
                            
                    </td>
                    <td class="midcolora" >
                        &nbsp;</td>
                    </tr>
				<tr>
				<td class="midcolora">
				
                    </td>
                    <td class="midcolora">
                 
                        &nbsp;</td>
				<td class="midcolora" align="right">
				    &nbsp;</td>
					
				</tr>
			</table>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidCLAIM_NUMBER" type="hidden" value="" name="hidCLAIM_NUMBER" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" value="" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidACTIVITY_ID" type="hidden" value="" name="hidACTIVITY_ID" runat="server">
			
			
		</form>
	</body>
</HTML>

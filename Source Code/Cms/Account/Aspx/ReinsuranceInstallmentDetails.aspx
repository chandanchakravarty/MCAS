<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReinsuranceInstallmentDetails.aspx.cs" Inherits="Cms.Account.Aspx.ReinsuranceInstallmentDetails" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reinsurance Installment Detail</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript" type="text/javascript">
   
        function initPage() {

            ApplyColor();


        }

        $(document).ready(function () {
            $("#btnReport").click(function () {

                $(".errmsg").hide();
            });
        });

    </script>

     
</head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();ChangeColor(); top.topframe.main1.mousein = false;findMouseIn();showScroll();">
	<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
    <form id="ReinsuranceInstallment" runat="server" method = "post" >

        <asp:Panel ID="Panel1" runat="server">
        <table class="tableWidth" cellspacing="1" cellpadding="0" width="100%" align="center"
            border="0">
          
            <tr>
                <td class="headereffectCenter" colspan="2">
                    <asp:Label runat="server" ID="capheader_field"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="midcolora"  colspan="2">
                </td>
            </tr>
            <tr>
                <td class="midcolora"  colspan="2">
                </td>
            </tr>
             <tr>
                <td class="midcolora">
                    <asp:Label ID="capPolNumber" runat="server">Policy Number</asp:Label><span id="Span1"  runat="server" class="mandatory">*</span>
                </td>
                <td class="midcolora">
                    <asp:TextBox ID="txtPolNumber" runat="server" size="25" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPOL_NUMBER" runat="server" ControlToValidate="txtPolNumber" Display="Dynamic"></asp:RequiredFieldValidator>
                                     
                </td>
            </tr>
            <tr>
                <td class="midcolora">
                    <cmsb:CmsButton class="clsbutton" ID="btnReport" CausesValidation="true" 
                        runat = "server" onclick="btnReport_Click" 
                        ></cmsb:CmsButton>
                </td>
                <td class="midcolora">
                </td>
            </tr>
             <tr>
                <td class="midcolora" colspan="2" align="center">
                    <asp:Label ID="lblErr" runat="server" CssClass="errmsg" Visible="false"></asp:Label>
                </td>
            </tr>
          </table> 
        

     <div id = "grdre" runat = "server" >
            <asp:GridView ID="Gv_General" runat="server" AutoGenerateColumns="false">
                <Columns>
                </Columns>
            </asp:GridView>
        </div>
         </asp:Panel> 
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PremiumRefundCheckCreate.aspx.cs" Inherits="Cms.Account.Aspx.PremiumRefundCheckCreate" 
ValidateRequest="false"%>

<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register Namespace="Ebix.WebControls" TagPrefix="Ebix" Assembly="Ebix.WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Premium Refund Check</title>
    <link id="lnkCSS" rel="Stylesheet" runat="server" />
    <style type="text/css">
        #frmDetail
        {
            height: 505px;
        }
    </style>
    <script type="text/javascript">
        function findMouseIn() 
        {
            if (!top.topframe.main1.mousein) {
                //createActiveMenu();
                top.topframe.main1.mousein = true;
            }
            setTimeout('findMouseIn()', 5000);
        }

    
    </script>
</head>
<body onload="top.topframe.main1.mousein = false;findMouseIn();">
    <form id="form1" runat="server">
    <webcontrol:menu id="bottomMenu" runat="Server"></webcontrol:menu>
    <div class="pageContent" id="bodyHeight">
        <table class="tableWidth" cellpadding="0" cellspacing="0" border="0" align="center">
             <tr>
                <td>
                    <table class="tableWidth" cellspacing="0" cellpadding="0" border="0" align="center">
                        <tr> 
                            <td>
                                <webcontrol:GridSpacer id="Grid1" runat="Server"></webcontrol:GridSpacer>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <Ebix:AdvGrid runat="server" ID="RefundChkLister" AllowPaging="true" BackColor="White" PageSize="10" DataKeyNames="C1" ShowSearch="true"
                                HeaderText="Premium Refund Checks" SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button"
                                width="100%" HeaderTextCSS="headereffectCenter" HeaderStyle-CssClass="headereffectWebGrid" RowStyle-CssClass="DataRow" 
                                AlternatingRowStyle-CssClass="AlternateDataRow" BorderStyle="None" GridLines="None" PagerStyle-CssClass="DataRow" AutoGenerateCheckBoxColumn="true">
                                </Ebix:AdvGrid>
                                <asp:Button ID="btnCreate" runat="server" Text="Create Check" OnClick="btnCreate_Click" class="clsButton" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

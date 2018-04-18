<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetentionLimitIndex.aspx.cs"
    Inherits="Cms.CmsWeb.Maintenance.RetentionLimitIndex" %>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register Namespace="Ebix.WebControls" TagPrefix="Ebix" Assembly="Ebix.WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retention Limit</title>
    <link id="lnkCSS" rel="Stylesheet" runat="server" />
    <style type="text/css">
        #frmDetail
        {
            height: 505px;
        }
    </style>
    <script>
        function findMouseIn() {
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
    <webcontrol:menu id="bottomMenu" runat="server">
    </webcontrol:menu>
    <div class="pageContent" id="bodyHeight">
        <table class="tableWidth" cellspacing="0" cellpadding="0" border="0" align="center">
            <tr>
                <td>
                    <table class="tableWidth" cellspacing="0" cellpadding="0" border="0" align="center">
                        <tr>
                            <td>
                                <webcontrol:GridSpacer id="Gridspacer1" runat="server">
                                </webcontrol:GridSpacer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <Ebix:AdvGrid runat="server" ID="advLister" AllowPaging="True" BackColor="White"
                                    PageSize="4" DataKeyNames="C1" ShowSearch="true" HeaderText="SUSEP Retention Limits"
                                    SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button"
                                    Width="100%" HeaderTextCSS="headereffectCenter" HeaderStyle-CssClass="headereffectWebGrid"
                                    RowStyle-CssClass="DataRow" AlternatingRowStyle-CssClass="AlternateDataRow" BorderStyle="None"
                                    GridLines="None" PagerStyle-CssClass="DataRow">
                                </Ebix:AdvGrid>
                                <asp:Button ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" class="clsButton" />
                                <br />
                                <iframe id="frmDetail" runat="server" frameborder="0" width="100%"></iframe>
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

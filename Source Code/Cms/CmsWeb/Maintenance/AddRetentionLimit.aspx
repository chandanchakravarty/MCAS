<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRetentionLimit.aspx.cs"
    Inherits="Cms.CmsWeb.Maintenance.AddRetentionLimit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="lnkCSS" rel="Stylesheet" runat="server" />
</head>
<body onload="top.topframe.main1.mousein = false;" class="contentsBody">
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="midcolorc">
                    <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0" >
                        <tr>
                            <td>
                                <asp:Panel ID="pnlDetails" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlControls" runat="server">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="clsButton"  />
                                </asp:Panel>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Susep_Reports.aspx.cs"
    Inherits="Cms.Reports.Aspx.Susep_Reports" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Susep Reports</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/calendar.js"></script>
    <script type="text/javascript" type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
    <script language="javascript">


    </script>
    <script language="javascript" type="text/javascript">
        function ResetForm() {

            document.SusepReport.reset();
            return false;
        }
        $(document).ready(function () {
            $("#btnReport").click(function () {

                $(".errmsg").hide();
            });
        });
        //applied the below function to handle validation on page,tfs 792
        function RunReport() {

            if (Page_ClientValidate()) {

                document.getElementById("btnReport").disabled = true
                __doPostBack("btnReport", "Yes_clicked");
            }

        }
        function validate1() {

            if (document.getElementById('revExpirationStartDate').isvalid == false) {
                document.getElementById('cpvEND_Date').setAttribute('enabled', false);

            }
            else
                document.getElementById('cpvEND_Date').setAttribute('enabled', true);
        }
        
    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $("#cmbReportList").change(function () {
                document.getElementById("btnReport").disabled = false;
            });
        });
    </script>
</head>
<body oncontextmenu="return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();"
    ms_positioning="GridLayout">
    <form id="SusepReport" method="post" runat="server">
    <div>
        <webcontrol:Menu id="bottomMenu" runat="server">
        </webcontrol:Menu></div>
    <!-- To add bottom menu -->
    <!-- To add bottom menu ends here -->
    <asp:Panel ID="Panel1" runat="server">
        <table class="tableWidth" cellspacing="1" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td class="pageHeader" id="tdClientTop" colspan="4">
                    <webcontrol:GridSpacer id="grdSpacer" runat="server">
                    </webcontrol:GridSpacer>
                </td>
            </tr>
            <tr>
                <td class="headereffectCenter" colspan="4">
                    <asp:Label runat="server" ID="lblheader_field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" colspan="4">
                </td>
            </tr>
            <tr>
                <td class="midcolorc" colspan="4">
                </td>
            </tr>
            <tr>
                <td class="midcolora">
                    <asp:Label ID="lblRI" runat="server"></asp:Label>
                </td>
                <td class="midcolora">
                    <asp:DropDownList ID="cmbReportList" runat="server" ValidationGroup="Reptgrp">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvcmbReoportlist" ControlToValidate="cmbReportList"
                        ValidationGroup="rpt"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolora">
                </td>
                <td class="midcolora">
                </td>
            </tr>
            <tr id="trDate">
                <td class="midcolora">
                    <asp:Label ID="lblinitialDate" runat="server">Date</asp:Label>
                </td>
                <td class="midcolora">
                    <asp:TextBox ID="txtinitialDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:HyperLink
                        ID="hlkinitialDate" runat="server" CssClass="HotSpot">
                        <asp:Image ID="imginitialDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                        </asp:Image>
                    </asp:HyperLink><br>
                    <asp:RequiredFieldValidator runat="server" ID="rfvinitialdate" ControlToValidate="txtinitialDate"
                        ValidationGroup="rpt"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revinitialDate" runat="server" Display="Dynamic"
                        ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." ControlToValidate="txtinitialDate"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora">
                    <asp:Label ID="lblExpirationStartDate" runat="server">Date</asp:Label>
                </td>
                <td class="midcolora">
                    <asp:TextBox ID="txtExpirationStartDate" runat="server" size="12" MaxLength="10" onblur="validate1();"></asp:TextBox><asp:HyperLink
                        ID="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
                        <asp:Image ID="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                        </asp:Image>
                    </asp:HyperLink><br>
                    <%-- applied validator for date range,itrack 779--%>
                    <asp:RegularExpressionValidator ID="revExpirationStartDate" runat="server" Display="Dynamic"
                        ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." ControlToValidate="txtExpirationStartDate"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="cpvEND_Date" ControlToValidate="txtExpirationStartDate"
                        Display="Dynamic" runat="server" ControlToCompare="txtinitialDate" Type="Date"
                        Operator="GreaterThanEqual"></asp:CompareValidator>
                    <asp:RequiredFieldValidator runat="server" ID="rfvExpirationStartDate" ControlToValidate="txtExpirationStartDate"
                        ValidationGroup="rpt"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" colspan="4" width="79%">
                    <br>
                </td>
            </tr>
            <tr>
                <td class="midcolora">
                    <cmsb:CmsButton class="clsbutton" ID="btnresetreport" runat="server" OnClick="btnresetreport_Click">
                    </cmsb:CmsButton>
                </td>
                <td class="midcolora">
                    <cmsb:CmsButton class="clsbutton" ID="btnReport" OnClientClick="RunReport()" CausesValidation="true"
                        ValidationGroup="rpt" runat="server" OnClick="btnReport_Click"></cmsb:CmsButton>
                </td>
                <%-- <td><asp:Button runat="server" ID="btnrefresh" Visible="false"    OnClick="btnrefresh_Click" /></td>--%>
                <td class="midcolora">
                </td>
                <td class="midcolora">
                </td>
            </tr>
            <tr>
                <td class="midcolora" colspan="4" align="center">
                    <asp:Label ID="lblErr" runat="server" CssClass="errmsg" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="Gv_General" runat="server" AutoGenerateColumns="false">
                <Columns>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    </form>
</body>
</html>

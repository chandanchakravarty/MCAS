<%--<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx" %>--%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%--<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>--%>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page Language="c#" CodeBehind="CustomerManagerSearch.aspx.cs" ValidateRequest="false" AutoEventWireup="false" Inherits="Cms.Client.Aspx.CustomerManagerSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Customer Manager Search</title>
    <%--<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
    <link rel="stylesheet" type="text/css" href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />

    <script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>

    <style type="text/css">
        .hide
        {
            overflow: hidden;
            top: 5px;
        }
        .show
        {
            overflow: hidden;
            top: 5px;
        }
        #tabContent
        {
            position: absolute;
            top: 160px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function onRowClicked(num, msDg) {

            //if(parseInt(num)==0)
            //	strXML="";
            rowNum = num;
            populateXML(num, msDg);
            //changeTab(0, 0);
            //document.location.href = "CustomerTabIndex.aspx?" + locQueryStr;
        }
        function addNew() {
            document.location.href = "CustomerTabIndex.aspx?";
        }

        function addCustomer() {
            document.location.href = "CustomerTabIndex.aspx?";
        }

        function addApplication() {
            document.location.href = "/cms/policies/aspx/policytab.aspx?CALLEDFROM=CLT";
        }

        function addQuote() {
            document.location.href = "/cms/Policies/Aspx/QuickQuote.aspx?CALLEDFROM=QAPP";
        }
        function CreateMenu() {
            if ('<%=strCalledFor%>' != 'Claim')
                return;

            if (top.topframe.main1.menuXmlReady == false)
                setTimeout("setMenu();", 1000);

            top.topframe.main1.activeMenuBar = '1'; //Customer Manager Search
            top.topframe.createActiveMenu();
           

        }
        //Change is only for singapore to enable quick quote menu
        function setMenu() {
           
            if ('<%=GetSystemId().ToUpper() %>' == "S001" || '<%=GetSystemId().ToUpper() %>' == "SUAT")
                top.topframe.enableMenu("1,1,2");
            else
                top.topframe.disableMenu("1,1,2");
        }
    </script>
</head>

<body id="htmlBody" runat="server" oncontextmenu="javascript:return false;" ms_positioning="GridLayout">
    <!-- To add bottom menu -->
    <webcontrol:Menu id="bottomMenu" runat="server">
    </webcontrol:Menu>
    <!-- To add bottom menu ends here -->
    <div id="bodyHeight" class="pageContent">
        <form id="indexForm" method="post" runat="server">
        <table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="tableWidthHeader" cellspacing="0" cellpadding="0" border="0" align="center">  
                        <tr>
                            <td>
                                <span id="singleRec"></span>
                                <p align="center">
                                    <asp:Label ID="capMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                                </p>
                            </td>
                        </tr>                      
                        <tr>
                            <td id="tdGridHolder">
                                <webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
                                <asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tableWidthHeader" cellspacing="0" cellpadding="0" border="0" align="center">
                                    <tr>
                                        <td>
                                            <iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no"
                                                frameborder="0" width="100%"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="hidTemplateID" />
                    <input type="hidden" name="hidRowID" />
                    <input id="hide" type="hidden" name="ConVar" />
                    <!--//added on 27/04/2005-->
                    <input type="hidden" name="hidlocQueryStr" />
                    <input type="hidden" name="hidMode" />
                    <!--//added on 27/04/2005-->
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>

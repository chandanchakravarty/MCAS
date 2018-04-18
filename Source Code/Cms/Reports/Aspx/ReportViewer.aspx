<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="Reports.Aspx.ReportViewer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EbixAdvantage Reports</title>
    <style type="text/css">
    .fullheight{margin-bottom:50px;}
    </style>

    <script language="JavaScript" type="text/JavaScript">
        
       
        window.onresize= function Resize()
        { 
            var viewer = document.getElementById("<%=rptVW.ClientID %>");
            var htmlheight = document.documentElement.clientHeight; 
            viewer.style.height = (htmlheight - 50) + "px";
        } 
        

        window.onload=function SetSize()
        { 
            var viewer = document.getElementById("<%=rptVW.ClientID %>");
            var htmlheight = document.documentElement.clientHeight; 
            viewer.style.height = (htmlheight - 50) + "px";
        } 
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="overflow:visible; height: 100%;">

        </div>
        <rsweb:ReportViewer ID="rptVW" runat="server" Height="96px" 
            Width="1165px">
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>
</body>
</html>

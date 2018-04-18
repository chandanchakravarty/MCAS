<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cms.Policies.Aspx.Default" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function Load() {
            var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
            var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
            var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
            var LOB_ID = document.getElementById('hidLOB_ID').value;
            var LOB_String = document.getElementById('hidLOB_String').value;
            var QueryStr = "CALLED_FROM=POL&CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&LOB_ID=" + LOB_ID + "&Lob_String=" + LOB_String;

            this.location = '/Cms/Policies/Aspx/WebForm1.aspx?' + QueryStr;
         }
    </script>
</head>
<body onload="javascript:setfirstTime();top.topframe.main1.mousein = false;findMouseIn();Load();" ms_positioning="GridLayout">
<webcontrol:Menu id="bottomMenu" runat="server">
    </webcontrol:Menu>
<div style="overflow:auto">

</div>
<input type="hidden" runat="server" id="hidCUSTOMER_ID" />
<input type="hidden" runat="server" id="hidPOLICY_ID" />
<input type="hidden" runat="server" id="hidPOLICY_VERSION_ID" />
<input type="hidden" runat="server" id="hidLOB_ID" />
<input type="hidden" runat="server" id="hidLOB_String" />
</body>
</html>

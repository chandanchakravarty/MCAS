<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeclarationPage.aspx.cs" Inherits="Cms.application.Aspx.DeclarationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title></title>
 <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">
   <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
    
<script language="javascript" type="text/javascript">
     
    
    function ShowPdfFile(strFileName) {
        window.open(strFilePath, "PopUp", "top=100,left=200,scrollbars=yes,resizable=1,height=400,width=500");

    }
   
    function Init() {
        ApplyColor();
        ChangeColor();
    }
    function openAll() {
        var frm = document.POL_DECLARATION;
        var e = frm.elements;
        for (i = 0; i < e.length - 1; i++) {
            if (e[i].type == "checkbox" && e[i].checked == true) {
                var chkId = e[i].id.split('_');
                var lnkID = chkId[0] + '_lnkFilePATH';
                var obj = document.getElementById(lnkID);
                window.open(obj.href, "");
            }
        }
    }
</script>

</head>


<body onload="Init();">
    <form id="POL_DECLARATION"  runat="server" >   
    <div>     
    <asp:panel ID="Panel1" runat="server">
        <asp:Label ID="lblMessage" runat="server" Visible="false" Text="vh" CssClass="errmsg"></asp:Label>
    
    </asp:panel>
    <input class="clsButton" runat="server"  name="btnOPEN_ALL" id="btnOPEN_ALL" value ="Open Selected"  onclick = "javascript:openAll()"
  type="button" />

  <input id="hidCLM_RECEIPT" type="hidden"  name="hidCLM_RECEIPT" runat="server"/>  
   <input id="hidCLAIM_ID" type="hidden"  name="hidCLAIM_ID" runat="server"/>  
    <input id="hidACTIVITY_ID" type="hidden"  name="hidACTIVITY_ID" runat="server"/>  
     <input id="hidCLAIM_DOC_TYPE" type="hidden"  name="hidCLAIM_DOC_TYPE" runat="server"/>  
    
    </div>
    
    
    </form>
    
</body>
</html>


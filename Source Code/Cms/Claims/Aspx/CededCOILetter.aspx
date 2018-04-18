<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CededCOILetter.aspx.cs" Inherits="Cms.Claims.Aspx.CededCOILetter" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript">
         function Print() {
             document.getElementById("PrintFNOL").style.display = "none";

             window.print()
             document.getElementById("PrintFNOL").style.display = "inline";
             return false;
         }
      
     
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panelshow" runat="server"></asp:Panel>
    </div>
    <div>
        <asp:Button ID="PrintFNOL"  OnClientClick="javascript:return Print()"  runat="server" Visible="false" Text="Print" Width="60px"   />
    </div>
    <input type="hidden" id="hidClaimId" runat="server" />
    <input type="hidden" id="hidActivityId" runat="server" />
    <input type="hidden" id="hidProcessType" runat="server" />  
     <input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
	<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> 
    </form>
</body>
</html>

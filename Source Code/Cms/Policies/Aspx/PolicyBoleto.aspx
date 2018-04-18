<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyBoleto.aspx.cs" Inherits="Cms.Policies.Aspx.PolicyBoleto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>Boleto</title>
   <link href="/Cms/CmsWeb/Css/BoletoStyleSheet.css" rel="stylesheet" type="text/css" />
   <link href="/Cms/CmsWeb/Css/BoletoNet.css" rel="stylesheet" type="text/css" />
  
  <script language="javascript" type="text/javascript">
      function Print() {
          document.getElementById("PrintBoleto").style.display = "none";
          
          window.print()
          document.getElementById("PrintBoleto").style.display = "inline";
          return false;
       }
      
     
    </script>
  </head>
<body>
    <form id="form1" runat="server">
    <div>   
    <asp:Button ID="PrintBoleto"  OnClientClick="javascript:return Print()"  runat="server" Visible="false" 
            Text="Print" Width="60px"   />
    
             <table width=100%>
      <tr><td align="center">
                <asp:Label ID="LblMsg"  runat="server" Text="Message" Font-Bold="True" 
            ForeColor="Red" Width="100%" Visible="False" Font-Names="Times New Roman" 
                    Font-Size="Small"></asp:Label>
                
                </td>
            </tr>
            </table>
            
    <div>
          <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
    </div>
    </form>
</body>
</html>

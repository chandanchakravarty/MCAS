<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UWRuleTestBed.aspx.cs" Inherits="Cms.Account.Aspx.UWRuleTestBed" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UWRuleTestBed</title>
        <META content="Microsoft Visual Studio 7.0" name="GENERATOR">
        <META content="C#" name="CODE_LANGUAGE">
        <META content="JavaScript" name="vs_defaultClientScript">
        <META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
        <script src="/cms/cmsweb/scripts/common.js"></script>
        <script src="/cms/cmsweb/scripts/form.js"></script>
        <LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
        <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
        <LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />          
        <STYLE>
            .hide { OVERFLOW: hidden; TOP: 5px }
            .show { OVERFLOW: hidden; TOP: 5px }
            #tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
        <script type="text/javascript" language="javascript">
            function ShowDialogBox() 
            {
                var strCustomerid = document.getElementById('hidCustomerid').value;
                var strPolicyId = document.getElementById('hidPolicyId').value;
                var strPolicyVerId = document.getElementById('hidPolicyVerId').value;
                var url = "/Cms/application/Aspx/ShowPolicyRules.aspx?Customerid=" + strCustomerid + "&PolicyId=" + strPolicyId + "&PolicyVerId=" + strPolicyVerId;
                ShowPopup(url, 'ShowDialog', 900, 600);
                return false;
            }
            function ShowPopup(url, winname, width, height)
            {
                var MyURL = url;
                var MyWindowName = winname;
                var MyWidth = width;
                var MyHeight = height;
                var MyScrollBars = 'Yes';
                var MyResizable = 'Yes';
                var MyMenuBar = 'No';
                var MyToolBar = 'No';
                var MyStatusBar = 'No';

                if (document.all)
                    var xMax = screen.width, yMax = screen.height;
                else
                    if (document.layers)
                        var xMax = window.outerWidth, yMax = window.outerHeight;
                    else
                        var xMax = 640, yMax = 480;

                var xOffset = (xMax - MyWidth) / 2, yOffset = (yMax - MyHeight) / 2;

                MyWin = window.open(MyURL, MyWindowName, 'width=' + MyWidth + ',height=' + MyHeight + ',screenX= ' + xOffset + ',screenY=' + yOffset + ',top=' + yOffset + ',left=' + xOffset + ',scrollbars=' + MyScrollBars + ',resizable=' + MyResizable + ',menubar=' + MyMenuBar + ',toolbar=' + MyToolBar + ',status=' + MyStatusBar + '');
                MyWin.focus();

            }	
		
        </script>
</head>
<body oncontextmenu="return false;"  onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();"  MS_POSITIONING="GridLayout";>
    <!-- To add bottom menu -->
    <webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
    <!-- To add bottom menu ends here -->
    <div id="bodyHeight" class="pageContent">
     <form id="form1" runat="server">
      <table id="tblBody" width="100%" align="center" border="0" runat="server">
        <tr>
            <td class="headereffectcenter" colspan="2"><asp:Label ID="capHeader" runat="server" Text="UW Rule Test Bed"></asp:Label></td>
        </tr>
        <tr>
            <td class="pageHeader" colSpan="2"><asp:Label ID="capMandatoryNotes" runat="server"></asp:Label></td></tr>
        <tr>
            <td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
        </tr>
       <tr>
         <td class="midcolora" width="18%"><asp:Label ID="capRuleMethod" runat="server" Text="Rule Method"></asp:Label></td>
         <td class="midcolora" width="32%"><asp:DropDownList ID="cmbRuleMethod" runat="server"></asp:DropDownList></td>
       </tr>
       <tr>
         <td class="midcolora" width="18%"><asp:Label ID="capCustomerid" runat="server" Text="Customer id"></asp:Label></td>
         <td class="midcolora" width="32%"><asp:TextBox ID="txtCustomerid" runat="server"></asp:TextBox><br />
         <asp:regularexpressionvalidator id="revCustomerid" ControlToValidate="txtCustomerid" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
         </td>
       </tr>
       <tr>
         <td class="midcolora" width="18%"><asp:Label ID="capPolicyId" runat="server" Text="Policy Id"></asp:Label></td>
          <td class="midcolora" width="32%"> <asp:TextBox ID="txtPolicyId" runat="server"></asp:TextBox> <br />
            <asp:regularexpressionvalidator id="revPolicyId" ControlToValidate="txtPolicyId" Display="Dynamic"  Runat="server"></asp:regularexpressionvalidator>
          </td>
       </tr>
       <tr>
         <td class="midcolora" width="18%"><asp:Label ID="capPolicyversionId" runat="server" Text="Policy Version Id"></asp:Label></td>
         <td class="midcolora" width="32%"><asp:TextBox ID="txtPolicyversionId" runat="server"></asp:TextBox><br />
          <asp:regularexpressionvalidator id="revPolicyversionId" ControlToValidate="txtPolicyversionId" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
         </td>
       </tr>
       <tr> 
         <td class="midcolora" width="30%"><asp:Label ID="capInputXML" runat="server" Text="Input XML For Rule"></asp:Label> <br /><br />
                                           <asp:TextBox ID="txtInputXML" TextMode="MultiLine" Height="300px" Width="320px" runat="server"></asp:TextBox><br /><br />
                                           <input ID="txt_ATTACH_XML" runat="server" size="70" type="file" />
          </td>
         <td class="midcolora" width="32%"><asp:Label ID="capInputXMLPol" runat="server" Text="Input XML For Policy Data"></asp:Label><br /><br />
                                           <asp:TextBox ID="txtInputXMLPol" TextMode="MultiLine" Height="300px" Width="320px" runat="server"></asp:TextBox><br /><br />
                                           <input ID="txt_ATTACH_Policy" runat="server" size="70" type="file" />
         </td>
       </tr>
       <tr> 
         <td class="midcolora" width="18%">
         <asp:Button ID="btnBulkTest" class="clsButton" runat="server" Text="Bulk Test" onclick="btnBulkTest_Click" />
         </td>
         <td class="midcolora" width="32%"><asp:Button ID="btnPerformRule" class="clsButton" runat="server" Text="Perform Rule" onclick="btnPerformRule_Click" />
             <asp:Button ID="btnShowOutPut" class="clsButton" runat="server" Text="Show Output" onclick="btnShowOutPut_Click" /></td>
       </tr>
       <tr>
         <td class="midcolora" width="18%"><asp:Label ID="capOutputXML" runat="server" Text="Output XML from rule parser"></asp:Label> </td>
         <td class="midcolora" width="32%"><asp:TextBox ID="txtOutputXML" TextMode="MultiLine"  Height="300px" Width="320px" runat="server"></asp:TextBox></td>
       </tr>
      
     </table>
      <INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server" />
      <INPUT id="hidHtml" type="hidden" name="hidHtml" runat="server" />
      <input type="hidden" name="hidTemplateID"> 
      <input type="hidden" name="hidRowID">
	  <input type="hidden" runat="server" name="hidlocQueryStr" id="hidlocQueryStr"> 
      <input type="hidden" runat="server" id="hidCustomerid" />
      <input type="hidden" runat="server" id="hidPolicyId" />
      <input type="hidden" runat="server" id="hidPolicyVerId" />
    </form>
   </div>
</body>
</html>


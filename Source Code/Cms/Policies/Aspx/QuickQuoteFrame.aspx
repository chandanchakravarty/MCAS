<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuickQuoteFrame.aspx.cs" Inherits="Cms.Policies.Aspx.QuickQuoteFrame" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" />
    
    <script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
    <title>Quick Quote Frame</title>
    
    <script type="text/javascript" language="javascript">
        
        function initFrame() {
            setfirstTime();
            top.topframe.main1.mousein = false;
            findMouseIn();
            //document.getElementById('hidrisk_id').value = document.getElementById('cmbRisk').value;
        }

        function RiskChange() {
        
            if (document.getElementById('cmbRisk').value != '') {
                document.getElementById('hidrisk_id').value = document.getElementById('cmbRisk').value
                document.getElementById('tabLayer').setAttribute('src', document.getElementById('hidFrameUrl').value + document.getElementById('cmbRisk').value);
            }
        }
        function BindRisk() {
            var result = QuickAppFrame.AjaxFetchRisk();

            fillDTCombo(result.value, document.getElementById('cmbRisk'), 'id', 'value', 0);

        }

        function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
            combo.innerHTML = "";
            if (objDT != null) {

                for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

                    if (i == 0) {
                        oOption = document.createElement("option");
                        oOption.value = "";
                        oOption.text = "";
                        combo.add(oOption);
                    }
                    oOption = document.createElement("option");
                    oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
                    oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
                    combo.add(oOption);
                }
            }
        }
        function RedirectFrame(policyid, policyversionid, customerid) {

            document.getElementById('tabLayer').setAttribute('src', document.getElementById('hidFrameUrl').value + "NEW");
            return false;
           
        }
    </script>
</head>
<body oncontextmenu = "javascript:return false;" onload="javascript:initFrame();"  style="">
<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
<div id="bodyHeight" class="pageContent" style="height:100%;vertical-align=top">
    <form id="QuickAppFrameForm" runat="server">
    <table border="0" width="95%" align="center">  
        <tbody>
            <tr>
                <td colspan="2">
                    <asp:Menu ID="QuickAppMenu" runat="server"                           
                    DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#0089CF"
                    StaticSubMenuIndent="10px" BackColor="White" StaticDisplayLevels="5" Orientation="Horizontal" StaticEnableDefaultPopOutImage="true" OnMenuItemDataBound="QuickAppMenu_MenuItemDataBound" >
                     <StaticSelectedStyle ForeColor="Chocolate" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#fefefe"  />
                        <DynamicSelectedStyle BackColor="#0683c7" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />                
                    </asp:Menu>
                    <asp:XmlDataSource ID="XmlDataSourceMenu" runat="server" DataFile="~/Policies/Aspx/QuickQuote.xml"></asp:XmlDataSource>
                </td>
            </tr>
            <tr>
                <td style="width:50%" align="left">
                    <cmsb:CmsButton ID="btnPrevious" runat="server" Text="Previous" class="clsbutton" onclick="btnPrevious_Click" />
                </td>
                <td style="width:50%" align="right">
                    <cmsb:CmsButton ID="btnNext" runat="server" Text="Next" class="clsbutton" onclick="btnNext_Click" />
                </td>
            </tr>
            <tr id="trHeader" runat="server">
                <td class="headereffectCenter" colspan="2">
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trRisk" runat="server">
                <td colspan="2" class="midcolora">
                    <asp:Label ID="capRisk" runat="server" Text="Select Risk: "></asp:Label>
                    <asp:DropDownList ID="cmbRisk" runat="server" onchange="javascript:RiskChange();"  ></asp:DropDownList>
                    <cmsb:CmsButton ID="btnAdd" runat="server" Text="Add New"  OnClientClick="return RedirectFrame();" class="clsbutton" />
                </td>
            </tr>
            <tr id="tabCtlRow">
			    <td colspan="2">
				    <webcontrol:Tab ID="TabCtl" runat="server" TabURLs="" TabLength="150"></webcontrol:Tab>
			    </td>
		    </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>                
                <td colspan="2" valign="top">
                    <iframe class="iframsHeightLong" id="tabLayer" name="tabLayer" runat="server" src="" width="100%"  
                    frameborder="0" scrolling="no"></iframe>
                </td>               
            </tr>            
            <tr id="trHiddenFields" runat="server">
                <td colspan="2">
                    <input id="hidFrameUrl" runat="server" type="hidden" value="" />
                    <input id="hidNextUrl" runat="server" type="hidden" value="" />
                    <input id="hidPrevUrl" runat="server" type="hidden" value="" />
                    <input id="hidLevel" runat="server" type="hidden" value="0" />
                    <input id="hidrisk_id" runat="server" type="hidden" value="0" /> 
                    <input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr">    
                                    
                </td>
            </tr>
        </tbody>
    </table>
    </form>
    </div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepositMessageDetails.aspx.cs" Inherits="Cms.Account.Aspx.DepositMessageDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
   <title>Deposit Message Details</title>
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
        <script type="text/javascript" type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		<script type="text/javascript" language="javascript">

		    //itrack - 1049 - Validation message display
		    $(document).ready(function () {
		        $("#btnSubmitAnyway").click(function () {
		            window.returnValue = "SubmitAnyway"
		            window.close()
		            return false;
		        });

		        $("#btnClose").click(function () {
		            window.close();
		            return false;
		        });
		    });
		    
		</script>
</head>
<body  >
    <form id="form1" runat="server">
    <table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td>
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colspan="2">
                        <asp:Label ID="lblDepositLineException" runat="server" ></asp:Label></td>
				</tr>
				 
				<tr>
					<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="errmsg"><asp:Label ID="lblMessage" Runat="server"></asp:Label></td>
				</tr>
				<tr>
				<td id="tdRTLImportHistoryDetails" runat="server" colspan="2">
                 </td>
                
                
                 <td><input id="hidSubmit" type="hidden" runat="server" />
                 <input id="hid_close" type="hidden" runat="server" /></td>
                 </tr>
				<tr  >
                       
                <td height='24' class='midcolora'>
				<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server"></cmsb:cmsbutton>
                </td>
                <td height='24' class='midcolorr'>
                    <cmsb:cmsbutton class="clsButton" id="btnSubmitAnyway" runat="server" ></cmsb:cmsbutton>
                 </td>
                        
                </tr>
				
                
			</table>
    </form>
</body>
</html>

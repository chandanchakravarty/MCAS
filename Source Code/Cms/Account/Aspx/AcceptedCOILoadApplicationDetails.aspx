<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcceptedCOILoadApplicationDetails.aspx.cs" Inherits="Cms.Account.Aspx.AcceptedCOILoadApplicationDetails" %>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Accepted COI Load Details</title>
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/Calendar.js"></script>

	
	
</head>
<body  oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='ApplyColor();'>
    <form id="AcceptedCOILoadApplicationDetails" runat="server">
    <div>
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
     <tr>
       <td class="midcolorc" align="right" colSpan="4">&nbsp;</td>
        <tr id="trBody" runat="server">
          <td>
             <table width="100%" align="center" border="0">
                  <tr>
                    <td class="midcolorc" align="right">&nbsp;</td>
                  </tr>
                  <tr>
                   <td>
                     <asp:Panel ID="pnlErrorGridMessage" runat="server" Width="100%">
                         <asp:GridView ID="grdErrorDetails" runat="server" 
                                 AutoGenerateColumns="False"  
                                 Width="100%" onrowdatabound="grdErrorDetails_RowDataBound">
                             <HeaderStyle CssClass="headereffectWebGrid" />
                             <RowStyle CssClass="midcolora" />
                             <EmptyDataRowStyle CssClass="midcolora" />
                             <Columns>
                                 <asp:BoundField DataField="ERROR_DESC" HeaderText="ERROR_DESC" />
                                 <asp:BoundField DataField ="ERROR_COLUMN" HeaderText="ERROR_COLUMN" />
                                 <asp:BoundField DataField="ERROR_COLUMN_VALUE" HeaderText="ERROR_COLUMN_VALUE" />
                             </Columns>
                         </asp:GridView>
                     </asp:Panel>
                   </td>
                  </tr>
             </table>
            </td>
          </tr>
          
          <input type="hidden" name="hidIMPORT_REQUEST_ID" id="hidIMPORT_REQUEST_ID" runat="server" /> 
	      <input id="hidIMPORT_SERIAL_NO" type="hidden" name="hidIMPORT_SERIAL_NO" runat="server" /> 
	      <input id="hidMODE" type="hidden" runat="server" /> 
         </tr>
      </table>
           
    </div>
    <input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
		  <input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
    </form>
   
</body>
</html>

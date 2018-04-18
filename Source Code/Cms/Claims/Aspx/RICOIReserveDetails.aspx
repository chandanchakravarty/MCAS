<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RICOIReserveDetails.aspx.cs" Inherits="Cms.Claims.Aspx.RICOIReserveDetails" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reserva de Recuperação</title>
    
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>

  <script type="text/javascript" language="javascript">


      function Init() {


          ApplyColor();
          
        
      }
  </script>
</head>
<body  oncontextmenu="return false;" >
    <form id="RICOIReserveDetails" runat="server">
   
    <table cellpadding="0" cellspacing="0" style="width:100%" >
        <tr>
            <td class="headereffectCenter">
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="pageHeader">
                <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td  class="midcolorl">
                <asp:GridView ID="grdClaimCoverages" runat="server" AutoGenerateColumns="False" 
                   Width="100%" onrowdatabound="grdClaimCoverages_RowDataBound" >
                   <HeaderStyle CssClass="headereffectWebGrid" />
                   <RowStyle CssClass="midcolora" />
                   <EmptyDataRowStyle CssClass="midcolora" />
                  
                <Columns>
                
                    <asp:BoundField DataField="REIN_COMAPANY_NAME" HeaderText="REIN_COMAPANY_NAME"   />
                    <asp:BoundField DataField="COMP_TYPE" HeaderText="COMP_TYPE"   />
                    <asp:BoundField DataField="COMP_PERCENTAGE" HeaderText="COMP_PERCENTAGE"   />
                    <asp:BoundField DataField="RESERVE_AMT" HeaderText="RESERVE_AMT"/>
                    <asp:BoundField DataField="TRAN_RESERVE_AMT" HeaderText="TRAN_RESERVE_AMT"   />                  
                    <asp:BoundField DataField="PAYMENT_AMT" HeaderText="PAYMENT_AMT"   />          
                
                </Columns>
               
                
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" class="midcolora">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="midcolora">
                &nbsp;</td>
        </tr>
    </table>
                <INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
                 <INPUT id="hidRESERVE_ID" type="hidden" value="0" name="hidRESERVE_ID" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
    </form>
</body>
</html>

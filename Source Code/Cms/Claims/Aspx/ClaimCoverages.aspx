<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimCoverages.aspx.cs" Inherits="Cms.Claims.Aspx.ClaimCoverages" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ClaimCoverages</title>
    
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
 <style type="text/css">
        
    .hiddenColum
    {
        display:none;
    }
    .visColum
    {
        display:block;
    }
  </style>
        
		<script type="text/javascript">
		
		
     function ShowDetails(clickedCell,dataCell) {
         
       
var spDetail = document.getElementById('spDetails');
var DvShowDetails = document.getElementById('DvShowDetails');
        
         var Details = dataCell.innerText;
        
         if (Details != "" && Details.length>1) 
         {
             spDetail.innerText = Details;
             DvShowDetails.style.display = '';


             var xCord = event.clientX;
             var yCord = event.clientY;

            
             DvShowDetails.style.position = "absolute";

             DvShowDetails.style.left = (xCord - 300);
             DvShowDetails.style.top = (yCord +10);
             DvShowDetails.style.visibility = "visible";

            
         }

     }
     function HideDetails(){

         var DvShowDetails = document.getElementById('DvShowDetails');

         DvShowDetails.style.display = 'none';            
     }

		</script>

</head>
<body>
    <form id="ClaimCoverages" runat="server">
   
    <table cellpadding="0" cellspacing="0" style="width:100%" >
        <tr>
            <td class="headereffectCenter">
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="grdClaimCoverages" runat="server" AutoGenerateColumns="False" 
                   Width="100%" onrowdatabound="grdClaimCoverages_RowDataBound">
                   <HeaderStyle CssClass="headereffectWebGrid" />
                   <RowStyle CssClass="midcolora" />
                   <EmptyDataRowStyle CssClass="midcolora" />
                  
                <Columns>
                
                
                    <asp:BoundField DataField="COVERAGE_CODE_ID" HeaderText="Coverage Code ID"/>
                    <asp:BoundField DataField="IS_RISK_COVERAGE" HeaderText="Is Risk Coverages"   />
                  
                    <asp:BoundField DataField="COV_DES" HeaderText="Coverage"   />
                      <asp:BoundField DataField="RI_APPLIES" HeaderText="RI Applies"   />
                    <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status"   />
                    <asp:BoundField DataField="LIMIT_OVERRIDE" HeaderText="Limit Override"   />
                    <asp:BoundField DataField="LIMIT_1" HeaderText="LIMIT_1" ItemStyle-HorizontalAlign="Right"  />
                    
                      <asp:TemplateField HeaderText="Deductible"  ItemStyle-HorizontalAlign="Right" >
						<ItemTemplate>
						<table>
						<tr>
						
						 <td style="width:90%" align="right" valign="top">
						 <asp:Label ID="lblDEDUCTIBLE_1" CssClass="INPUTCURRENCY" runat="server" Text='<%# Eval("DEDUCTIBLE_1") %>' ></asp:Label>
						 </td>
						
						 <td>
						 	<asp:Image ID=imgDEDUCTIBLE_1 ImageUrl="../../cmsweb/images/Details.jpg" runat="server" style="cursor:pointer" />

						 </td>
						 
						 </tr>
						</table>
						
						
							
						</ItemTemplate>
					</asp:TemplateField>
					
                    <asp:BoundField DataField="POLICY_LIMIT" HeaderText="Policy Limt"  ItemStyle-HorizontalAlign="Right" />
                
                    <asp:BoundField DataField="DEDUCTIBLE1_AMOUNT_TEXT" HeaderText="DEDUCTIBLE1_AMOUNT_TEXT" ItemStyle-HorizontalAlign="Left"  />
                    
                </Columns>
               
                
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
    </table>
    
       <%-- DIV TO SHOW DETAILS IN POPUP--%>
                            <div ID="DvShowDetails" style="border: 1px solid #000000; display:none; background-color: #FFFFEA; width: 300px;">
                                                        <table cellspacing="0"  width="100%">
                                                            <tr>
                                                                <td align="left" 
                                                                    
                                                                    
                                                                    
                                                                    style="background-image: url('../../cmsweb/images/PopupTitleBar.gif'); background-repeat: repeat-x; padding-left: 10px; font-family: Verdana; font-size: 8pt;">
                                                                   
                                                                    <asp:Label ID="LblPopupDetails" runat="server" Font-Bold="True" 
                                                                        Text=""></asp:Label>
                                                                </td>
                                                                
                                                                <td align="right" style="background-image: url('../../cmsweb/images/PopupTitleBar.gif'); background-repeat: repeat-x"  width="30%">
                                                                     <asp:Image ID="ImgCloseDetails" runat="server" 
                                                                         onmouseover="this.style.cursor='pointer'" onclick="HideDetails();"
                                                                         ImageUrl="../../cmsweb/images/Close.jpg" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td  align="left" valign="top" colspan="2">
                                                                <div style="margin: 2px 5px 10px 5px" align="left">
                                                                    <span ID="spDetails" style="font-family: Tahoma; font-size: 12px"></span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
    </form>
</body>
</html>

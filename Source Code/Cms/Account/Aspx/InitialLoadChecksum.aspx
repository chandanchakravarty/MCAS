<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitialLoadChecksum.aspx.cs" Inherits="Cms.Account.Aspx.InitialLoadChecksum" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checksum Summary</title>
        <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>

	<script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>

     <script type="text/javascript" language="javascript">


		    function initPage() {

		        ApplyColor();


		    }


		    function onRowClicked(num, msDg) {
		        rowNum = num;
		        if (parseInt(num) == 0)
		            strXML = "";
		        populateXML(num, msDg);
		        changeTab(0, 0);
		    }

		    function findMouseIn() {
		        if (!top.topframe.main1.mousein) {
		            //createActiveMenu();
		            top.topframe.main1.mousein = true;
		        }
		        setTimeout('findMouseIn()', 5000);
		    }
		</script>
         <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 29px;
        }
    </style>
</head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();">
    <form id="form1" runat="server">
    <div id="gridid" style='height:7000px; overflow:scroll';> 
			<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">					
				<tr>
					<td>
						<table id="Table2" width="100%" align="center" border="0">
							<tbody>	
                            <tr>
                            <td>&nbsp;</td>
                            </tr>
							    <tr>							        
					                <td class="headereffectCenter" colspan="4"><asp:label id="lblHeader" Runat="server">Checksum Summary</asp:label>
					                </td>
				                </tr>	
                                 <tr>
                            <td>&nbsp;</td>
                            </tr>						
								<tr>
									<td class="midcolorc" align="right" colspan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>	
				    			<tr>
								    <td colspan="4" width="100%" class="midcolora" >
								          <asp:GridView ID="grdInitialLoadchecksum" runat="server" AllowPaging="true" 
                                                                AutoGenerateColumns="False" 
                                                                Width="100%">
                                                               <HeaderStyle CssClass="headereffectWebGrid" />
                                                                <RowStyle CssClass="midcolora" />
                                                                <EmptyDataRowStyle CssClass="midcolora" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="FILE_TYPE" HeaderText="FILE_TYPE" />
                                                                    <asp:BoundField DataField="DISPLAY_FILE_NAME" HeaderText="DISPLAY_FILE_NAME" />
                                                                    <asp:BoundField DataField="FAILED_RECORDS" HeaderText="FAILED_RECORDS" />
                                                                    <asp:BoundField DataField="SUCCESS_RECORDS" HeaderText="SUCCESS_RECORDS" />
                                                                </Columns>
                                                            </asp:GridView>
									</td>
								</tr>	

							</tbody>
						</table>
										
				<tr><td class="midcolorc">				
				<input id="hidIMPORT_REQUEST_ID" type="hidden" value="0" name="hidIMPORT_REQUEST_ID" runat="server"/>				
				<cmsb:CmsButton ID="btnStartImportProcess" runat="server" class="clsButton" 
                                                                            
                        CausesValidation="false" Text="Start Import Process" 
                        onclick="btnStartImportProcess_Click" />
                        <cmsb:CmsButton ID="btnExportReport" runat="server" class="clsButton" 
                                                                            
                        CausesValidation="false" Text="Export Report" 
                        onclick="btnExportReport_Click" />
                                                                       
				</td></tr>				
			</table>
			</div>
    </form>
</body>
</html>

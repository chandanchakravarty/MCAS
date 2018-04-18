<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddInitialLoad.aspx.cs" Inherits="Cms.Account.Aspx.AddInitialLoad" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Initial Load</title>
    
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

         function ViewChecksum() {
             var Import_Request_ID = document.getElementById('hidIMPORT_REQUEST_ID').value;
             var url = "InitialLoadChecksum.aspx?IMPORT_REQUEST_ID=" + Import_Request_ID;
             ShowPopup(url, 'DefineSubRanges', 900, 600);
             return false;
         }


         function ViewDetails() {

             var REQUEST_STATUS = document.getElementById('hidREQUEST_STATUS').value;
             var Import_Request_ID = document.getElementById('hidIMPORT_REQUEST_ID').value;
             var IMPORT_FILE_TYPE = document.getElementById('hidIMPORT_FILE_TYPE').value;
             var IMPORT_FILE_TYPE_NAME = document.getElementById('hidIMPORT_FILE_TYPE_NAME').value;
             var Lob_Id = document.getElementById('hidLobId').value;
             var url = "InitialLoadDetails.aspx?REQUEST_STATUS=" + REQUEST_STATUS + "&IMPORT_REQUEST_ID=" + Import_Request_ID + "&Import_File_Type=" + IMPORT_FILE_TYPE + "&Import_File_Type_Name=" + IMPORT_FILE_TYPE_NAME + "&lobId="+Lob_Id;
             ShowPopup(url, 'DefineSubRanges', 900, 600);
             return false;
         }
         function ViewCommitPolicyDetails() {
             
             var REQUEST_STATUS = document.getElementById('hidREQUEST_STATUS').value;
             var Import_Request_ID = document.getElementById('hidIMPORT_REQUEST_ID').value;
             var IMPORT_FILE_TYPE = document.getElementById('hidIMPORT_FILE_TYPE').value;
             var IMPORT_FILE_TYPE_NAME = document.getElementById('hidIMPORT_FILE_TYPE_NAME').value;
             var Lob_Id = document.getElementById('hidLobId').value;
             var url = "InitialLoadCommitedPolicyList.aspx?REQUEST_STATUS=" + REQUEST_STATUS + "&IMPORT_REQUEST_ID=" + Import_Request_ID + "&Import_File_Type=" + IMPORT_FILE_TYPE + "&Import_File_Type_Name=" + IMPORT_FILE_TYPE_NAME + "&lobId=" + Lob_Id;
             ShowPopup(url, 'DefineSubRanges', 900, 600);
             return false;
         }
         function ResetTheForm() {
             document.AcceptedCOILoad.reset();
             return false;
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
    <form id="AcceptedCOILoad" runat="server">
    
   
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader">
                    <asp:label id="lblRequiredFieldsInformation" runat="server" 
                                        ></asp:label>
                                        </TD>
									</tr>
									<tr>
										<td class="midcolorc" align="center">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                                  
                                        </td>
									</tr>
									<tr>
										<TD class="midcolora">
                                            <asp:Panel ID="PnlGrid" runat="server">
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grdImportRequestFiles" runat="server" 
                                                                AutoGenerateColumns="False" onrowcommand="grdImportRequestFiles_RowCommand" 
                                                                onrowdatabound="grdImportRequestFiles_RowDataBound" Width="100%">
                                                                <HeaderStyle CssClass="headereffectWebGrid" />
                                                                <RowStyle CssClass="midcolora" />
                                                                <EmptyDataRowStyle CssClass="midcolora" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="IMPORT_REQUEST_ID" HeaderText="Request ID" />
                                                                    <asp:BoundField DataField="IMPORT_REQUEST_FILE_ID" HeaderText="File ID" />
                                                                    <asp:BoundField DataField="DISPLAY_FILE_NAME" HeaderText="" />
                                                                     <asp:BoundField DataField="IMPORT_FILE_GROUP_TYPE" HeaderText="File Group" />
                                                                    <asp:BoundField DataField="IMPORT_FILE_TYPE" HeaderText="" />
                                                                    <asp:BoundField DataField="FILE_IMPORTED_DATE" 
                                                                        HeaderText="" />
                                                                    <asp:BoundField DataField="FILE_IMPORTED_BY" HeaderText="" />
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="BtnDeleteFile" runat="server" CausesValidation="False" 
                                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                                                                                CommandName="DeleteFile" ImageAlign="Middle" 
                                                                                ImageUrl="../../cmsweb/images/Delete.jpg" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="BtnViewDetails" runat="server" CausesValidation="False" 
                                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                                                                                CommandName="ViewDetails" ImageAlign="Middle" 
                                                                                ImageUrl="../../cmsweb/images/ViewReserveDetails.jpg" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="REQUEST_STATUS" />
                                                                    <asp:BoundField DataField="FILE_TYPE" />
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td width="50%">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%">
                                                                        <cmsb:CmsButton ID="btnAddNew" runat="server" class="clsButton" 
                                                                            onclick="btnAddNew_Click" Text="Adicionar Novo" />
                                                                        <cmsb:CmsButton ID="btnCommitPolicyList" runat="server"  OnClientClick="ViewCommitPolicyDetails();"
                                                                            CausesValidation="false" class="clsButton" Text="View Commit Detail" />
                                                                    </td>
                                                                    <td align="right">
                                                                        <cmsb:CmsButton ID="btnChecksumSummary" runat="server" class="clsButton" 
                                                                            CausesValidation="false" Text="Checksum Summary" />
                                                                       
                                                                        <cmsb:CmsButton ID="btnStartProcess" runat="server" CausesValidation="false"  
                                                                            class="clsButton" onclick="btnStartProcess_Click" Text="Início do processo" />
                                                                       
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                              
                                            </asp:Panel>
                                             <asp:Panel ID="PnlDetails" runat="server">
                                                                <table align="center" border="0" width="100%">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" class="midcolorr" colspan="2">
                                                                            <p align="left">
                                                                                &nbsp;</p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr ID="trPACKAGE_NAME" runat="server">
                                                                        <td class="midcolora">
                                                                            <asp:Label ID="lblREQUEST_DESC" runat="server">Nome do pacote</asp:Label><span id="Span1"  runat="server" class="mandatory">*</span>
                                                                        </td>
                                                                        <td class="midcolora">
                                                                            <asp:TextBox ID="txtREQUEST_DESC" runat="server" MaxLength="50" size="25"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvREQUEST_DESC" runat="server" ControlToValidate="txtREQUEST_DESC" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr ID="tr1" runat="server">
                                                                        <td class="midcolora" width="25%">
                                                                            <asp:Label ID="capGROUP_FILE_TYPE" runat="server">Group file Type</asp:Label><span id="Span3"  runat="server" class="mandatory">*</span>
                                                                        </td>
                                                                        <td class="midcolora">
                                                                            <asp:DropDownList ID="cmbGROUP_FILE_TYPE" runat="server" AutoPostBack="True" 
                                                                                onselectedindexchanged="cmbGROUP_FILE_TYPE_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvGROUP_FILE_TYPE" runat="server" ControlToValidate="cmbGROUP_FILE_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr ID="trIMPORT_FILE_TYPE" runat="server">
                                                                        <td class="midcolora" width="25%">
                                                                            <asp:Label ID="capIMPORT_FILE_TYPE" runat="server">Tipo de Arquivo</asp:Label><span id="spnORIGINAL_ISSUE"  runat="server" class="mandatory">*</span>
                                                                        </td>
                                                                        <td class="midcolora">
                                                                            <asp:DropDownList ID="cmbIMPORT_FILE_TYPE" runat="server">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvIMPORT_FILE_TYPE" runat="server" ControlToValidate="cmbIMPORT_FILE_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr ID="trATTACH_FILE_NAME" runat="server">
                                                                        <td class="midcolora">
                                                                            <asp:Label ID="capATTACH_FILE_NAME" runat="server">Nome do arquivo</asp:Label><span id="Span2"  runat="server" class="mandatory">*</span>
                                                                        </td>
                                                                        <td class="midcolora">
                                                                            <input ID="txtATTACH_FILE_NAME" runat="server" size="70" type="file" /><br />
                                                                            <asp:Label ID="lblATTACH_FILE_NAME" Runat="server"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="rfvATTACH_FILE_NAME" runat="server" 
                                                                                ControlToValidate="txtATTACH_FILE_NAME" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="midcolora" colspan="2">
                                                                            
                                                                            <br />
                                                                            <br />
                                                                            <br />
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="midcolora">
                                                                            <div>
                                                                                <cmsb:CmsButton ID="btnReset" runat="server" CausesValidation="false" 
                                                                                    class="clsButton" OnClientClick="javascript:return ResetTheForm();" 
                                                                                    Text="Reset" />
                                                                            </div>
                                                                        </td>
                                                                        <td class="midcolorr">
                                                                            <p align="right">
                                                                                <cmsb:CmsButton ID="btnSave" runat="server" class="clsButton" 
                                                                                    onclick="btnSave_Click" Text="Save" />
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                    </caption>
                                                                </table>
                                                            </asp:Panel>
                                            
                                          
                                        </TD>
									</tr>
									<tr>
										<TD class="midcolora">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="50%">
                                                &nbsp;</td>
                            <td class="midcolorr">
                                                                        &nbsp;</td>
                        </tr>
                    </table>
                    </TD>
                    </tr>
                    </TBODY>
                    </TABLE>
                    
                <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
                <INPUT id="hidIMPORT_REQUEST_ID" type="hidden" value="" name="hidIMPORT_REQUEST_ID" runat="server">
                <INPUT id="hidIMPORT_REQUEST_FILE_ID" type="hidden" value="0" name="hidIMPORT_REQUEST_FILE_ID" runat="server">	
                <INPUT id="hidREQUEST_STATUS" type="hidden" value="0" name="hidREQUEST_STATUS" runat="server">			 
			    <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
                <INPUT id="hidIMPORT_FILE_TYPE" type="hidden" value="" name="hidIMPORT_FILE_TYPE" runat="server">
                <INPUT id="hidIMPORT_FILE_TYPE_NAME" type="hidden" value="" name="hidIMPORT_FILE_TYPE_NAME" runat="server">
                <INPUT id="hidLobId" type="hidden" value="" name="hidLobId" runat="server">
                
                                     
    </form>
   

      <script type="text/javascript">

          try {
              if (document.getElementById('hidFormSaved').value == "1") {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidIMPORT_REQUEST_ID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

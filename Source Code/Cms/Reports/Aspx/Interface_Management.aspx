<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Interface_Management.aspx.cs"
    Inherits="Cms.Reports.Aspx.Interface_Management" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Search</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/calendar.js"></script>

    <style type="text/css">
        .grid_view
        {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function OnMouseOver(obj, RowIndex) {
            //var gridViewCtlId = '<%=dgrInterface.ClientID%>';
            var gridViewCtl = document.getElementById('dgrInterface');
            var selRow = null;
            
            if (null != gridViewCtl) {
               
                selRow = gridViewCtl.rows[RowIndex+1];
            }
            if (null != selRow) {              
                
                if (selRow.style.backgroundColor != '#1e90ff') {
                    
                    selRow.style.backgroundColor = 'lightsteelblue';
                }
                selRow.style.cursor = 'hand';
            }
        
//            if (obj.style.backgroundColor != '#1E90FF') {
//                obj.style.backgroundColor = 'lightsteelblue';
//            }
           
        }
        function OnSelect(obj, RowIndex) {
            //alert(obj.toString());
             var gridViewCtl = document.getElementById('dgrInterface');
            //var gridViewCtl = document.getElementById(obj);
            var selRow = null;
            if (null != gridViewCtl) {               
                 selRow = gridViewCtl.rows[RowIndex];
            }

//            if (curSelRow != null) {
//                curSelRow.style.backgroundColor = '#ffffff';
//            }

            if (null != selRow) { 
                if (selRow.style.backgroundColor == '#1e90ff') {
                   
                    selRow.style.backgroundColor = '#f3f3f3'; 
                }
            }

                        

            //document.getElementById('headerRecord_FollowUp').style.display = 'inline';
//            if (obj.style.backgroundColor == '#1e90ff') {
//                alert(obj);
//             obj.style.backgroundColor = '#f3f3f3';
//            obj.style.cursor = 'default';
//             }
          
//            var element = document.getElementsByTagName('tr')
//            
//            for (i = 0; i < element.length; i++) {
//                if (element[i].style.backgroundColor == '#1E90FF')
//                { element[i].style.backgroundColor = '#f3f3f3'; }
//            }

          

            //debugger;
            if (typeof (obj) != 'undefined' && obj.cells[0] != null)
                document.getElementById('hidFILE_ID').value = obj.cells[0].childNodes[0].innerText;
            //alert(document.getElementById('hidFILE_ID').value);
            //                document.getElementById('dgrShowRecords').style.display = 'inline'; 
            //                 else
            //                    document.getElementById('dgrShowRecords').style.display = 'none';
            var val = obj.cells[6].childNodes[0].innerText;
            __doPostBack('btnShowRecord', val + '^' + RowIndex); 
           // obj.style.backgroundColor = '#1E90FF';
            if (typeof (obj) != 'undefined' && obj.cells[6] != null) {

                if (obj.cells[6].childNodes[0].innerText == 'ERR' || obj.cells[6].childNodes[0].innerText == 'APR') {

                    document.getElementById('btnReProcess').style.display = 'inline';
                }
                else
                    document.getElementById('btnReProcess').style.display = 'none';
            }
//            if (obj.style.backgroundColor != '#1E90FF') {
//                obj.style.backgroundColor = 'lightsteelblue';
//            }
            obj.style.cursor = 'hand';
        }

        function OnMouseOut(obj, RowIndex) {
           /* if (obj.style.backgroundColor != '#1E90FF') {
                obj.style.backgroundColor = '#f3f3f3';
                //obj.style.cursor = 'default';
                }*/

            var gridViewCtl = document.getElementById('dgrInterface');
            var selRow = null;            
            if (null != gridViewCtl) {                
                selRow = gridViewCtl.rows[RowIndex+1];
            }
            if (null != selRow) {                
                if (selRow.style.backgroundColor != '#1e90ff') {
                    //alert(selRow.style.backgroundColor);
                    selRow.style.backgroundColor = '#f3f3f3';
                }
                selRow.style.cursor = 'default';
            }
           
        }
        function onDoubleClick(obj) {
            obj.style.backgroundColor = '#f3f3f3';
            obj.style.cursor = 'default';
            document.getElementById('btnReProcess').style.display = 'none';

        }
        function validate1() {

            if (document.getElementById('revInceptionEndDate').isvalid == false) {
                document.getElementById('cmpToDate').setAttribute('enabled', false);

            }
            else
                document.getElementById('cmpToDate').setAttribute('enabled', true);
        }

		   
    </script>

</head>
<body scroll="yes" oncontextmenu="return false;" ms_positioning="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();  ApplyColor();
            ChangeColor();" >
    <form id="Form1" runat="server">
    <webcontrol:menu id="bottomMenu" runat="server">
    </webcontrol:menu>
    <!-- To add bottom menu -->
    <!-- To add bottom menu ends here -->
     <input type="hidden" runat="server" id="hidFILE_ID" />
    <table cellspacing="1" cellpadding="0" width="100%" border="0">
    <col width=20% />
    <col width=7% />
    <col width=33% />
    <col width=7% />
    <col width=33% />
        <tr>
            <td class="pageHeader" id="tdClientTop" colspan="5">
                <webcontrol:GridSpacer id="grdSpacer" runat="server">
                </webcontrol:GridSpacer>
            </td>
        </tr>
        <tr>
            <td class="headereffectCenter" colspan="5">
                <asp:Label ID="lblSearch" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora" colspan="1">
                <asp:Label ID="capInterface_Type" runat="server"></asp:Label><span class="mandatory">*</span>
            </td>
            <td class="midcolora" colspan="4">
                <asp:DropDownList ID="cmbInterface_Type" AutoPostBack="true" runat="server" OnSelectedIndexChanged="SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvInterface_Type" runat="server" ControlToValidate="cmbInterface_Type"
                    Display="Dynamic" Enabled="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" colspan="1">
                <asp:Label ID="capInterface_File" runat="server"></asp:Label>
            </td>
            <td class="midcolora" colspan="4">
                <asp:DropDownList ID="cmbInterface_File" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td class="midcolora" colspan="1">
           <asp:Label ID="capFileRecord" runat="server"></asp:Label>
            </td>
            <td class="midcolora" colspan="4" >
                <asp:RadioButtonList ID="rblFileRecord" runat="server" CssClass="midcolora" 
                    RepeatDirection="Horizontal" AutoPostBack="true"
                    onselectedindexchanged="rblFileRecord_SelectedIndexChanged" >
                    <asp:ListItem Value="1" Selected="true">File</asp:ListItem>
                    <asp:ListItem Value="2">Record</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvFileRecord" runat="server" ControlToValidate="rblFileRecord">  
                </asp:RequiredFieldValidator>     
        </td>
        </tr>
        <tr id = "trStatus" runat="server" visible ="false" >
        <td class="midcolora" colspan="1">
                <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
            </td>
        <td class="midcolora" colspan="4">
            <asp:DropDownList ID="cmbStatus" runat="server">
                </asp:DropDownList>
        </td>
        </tr>
        <tr id ="trInputOutput" runat="server">
            <td class="midcolora" colspan="1">
                <asp:Label ID="capInputOutput" runat="server"></asp:Label>
            </td>
            <td class="midcolora" colspan="4">
                <asp:RadioButtonList ID="rblInputOutput" runat="server" CssClass="midcolora" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">Input</asp:ListItem>
                    <asp:ListItem Value="2">Output</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvInputOutput" runat="server" ControlToValidate="rblInputOutput">  
                </asp:RequiredFieldValidator>
                <%--               <asp:CustomValidator ID="cvYESNO" runat="server" ClientValidationFunction="ValidateChkList" ></asp:CustomValidator>
--%>
            </td>
        </tr>
        <tr>
            <td class="midcolora" colspan="1">
                <asp:Label ID="capFile_Date" runat="server"></asp:Label>
            </td>
            <td class="midcolora" colspan="1">
                <asp:Label ID="lblInceptionStartDate" runat="server"></asp:Label>
            </td>
            <td class="midcolora" colspan="1">
                <asp:TextBox ID="txtCREATE_DATE" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:HyperLink
                    ID="hlkCREATE_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgCREATE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br>
                <asp:RequiredFieldValidator ID="rfvCREATE_DATE" runat="server" ControlToValidate="txtCREATE_DATE"> </asp:RequiredFieldValidator>
                <asp:regularexpressionvalidator id="revCREATE_DATE" Runat="server" ControlToValidate="txtCREATE_DATE"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
            </td>
            <td class="midcolora" colspan="1">
                <asp:Label ID="lblInceptionEndDate" runat="server"></asp:Label>
            </td>
            <td class="midcolora" colspan="1">
                <asp:TextBox ID="txtInceptionEndDate" runat="server" size="12" MaxLength="10" onblur="validate1();"></asp:TextBox><asp:HyperLink
                    ID="hlkInceptionEndDate" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgInceptionEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
                <asp:RequiredFieldValidator ID="rfvInceptionEndDate" runat="server" ControlToValidate="txtInceptionEndDate"> </asp:RequiredFieldValidator><br />
                <asp:regularexpressionvalidator id="revInceptionEndDate" Runat="server" ControlToValidate="txtInceptionEndDate"
								Display="Dynamic"></asp:regularexpressionvalidator><br />
					 <asp:comparevalidator id="cmpToDate" Runat="server" ControlToValidate="txtInceptionEndDate"
								Display="Dynamic" ControlToCompare="txtCREATE_DATE" Type="Date" Operator="GreaterThanEqual"></asp:comparevalidator>		
            </td>
            
        </tr>
       
        <tr>
            <td class="midcolorc" colspan="2">
                <cmsb:CmsButton class="clsbutton" ID="btnCancel" runat="server"></cmsb:CmsButton>
            </td>
            <td class="midcolorc" colspan="3">
                <cmsb:CmsButton class="clsbutton" ID="btnSearch" runat="server" OnClick="btnSearch_Click"></cmsb:CmsButton>
            </td>
        </tr>
        <tr class="midcolora" id="headerSearch_List" runat="server">
            <td class="headereffectcenter" colspan="5">
                <asp:Label runat="server" ID="lblHeader"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolorc" colspan="5">
                <asp:Label ID="lblRecord_NotFound" runat="server" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        
        <tr>
        
            <td class="midcolorc" colspan="5">
                <asp:GridView ID="dgrInterface" AlternatingItemStyle-CssClass="AlternateDataRow2"
                    ItemStyle-CssClass="DataRow2" HeaderStyle-CssClass="headereffectWebGrid" AutoGenerateColumns="false"
                    runat="server" OnRowDataBound="dgrInterface_RowDataBound" Width="100%" 
                    AutoPostback="false">
                    <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle CssClass="midcolora"></RowStyle>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="grid_view" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capFILE_ID"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle CssClass="grid_view" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFILE_ID" Text='<%# Eval("FILE_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capFILE_TYPE" Text="File Type"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFILE_TYPE" Text='<%# Eval("FILE_TYPE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capFILE_NAMES" Text="File Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFILE_NAMES" Text='<%# Eval("FILE_NAMES") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capINPUT_OUTPUT" Text="Input/Output"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblINPUT_OUTPUT" Text='<%# Eval("INPUT_OUTPUT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCREATE_DATE" Text="File Generation Date"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCREATE_DATE" Text='<%# Eval("CREATE_DATE","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capNUMBER_OF_RECORDS" Text="Number of Records"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNUMBER_OF_RECORDS" Text='<%# Eval("NUMBER_OF_RECORDS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCURRENT_FILE_FOLDER" Text="Current File Folder"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCURRENT_FILE_FOLDER" Text='<%# Eval("CURRENT_FILE_FOLDER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
                
                <asp:GridView ID="gvMeleventos" AlternatingItemStyle-CssClass="AlternateDataRow2" ItemStyle-CssClass="DataRow2" HeaderStyle-CssClass="headereffectWebGrid" AutoGenerateColumns="False"
                    runat="server"  Width="100%"  AutoPostback="false" 
                    onrowdatabound="gvMeleventos_RowDataBound" OnRowCommand ="gvMeleventos_RowCommand">
                    <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle CssClass="midcolora"></RowStyle>
                     <Columns>                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capSERIAL_NO" Text="Serial #"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>                                                                
                                <asp:Label runat="server" ID="lblSERIAL_NO" Text='<%# Eval("SERIAL_NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                              
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capFILE_NAMES" Text="File Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>                                                                
                                <asp:Label runat="server" ID="lblFILE_NAMES" Text='<%# Eval("FILE_NAMES") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCreateDate" Text="Create Date"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCreateDate" Text='<%# Eval("START_DATETIME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                         <asp:ButtonField CommandName="select"  Text="Details" /> 
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="5">
                <cmsb:CmsButton class="clsbutton" ID="btnReProcess" runat="server" 
                    Text="ReProcess" onclick="btnReProcess_Click">
                </cmsb:CmsButton>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="5">
                <cmsb:CmsButton class="clsbutton" ID="btnShowRecord" runat="server" Text="Show Record"
                    OnClick="btnShowRecord_Click" Visible="false"></cmsb:CmsButton>
            </td>
        </tr>
        <tr>
            <td class="midcolorc" colspan="5">
                <br />
                <br />
            </td>
        </tr>
        <tr class="midcolora" id="headerRecord_FollowUp" runat="server">
            <td class="headereffectcenter" colspan="5">
                <asp:Label runat="server" ID="lblRecords_Followup"></asp:Label>
            </td>
        </tr>
        
        <tr>
        <td class="midcolorc" colspan="5">
         
        <asp:GridView ID="dgrShowRecords" AlternatingItemStyle-CssClass="AlternateDataRow2"
            ItemStyle-CssClass="DataRow2" HeaderStyle-CssClass="headereffectWebGrid" AutoGenerateColumns="false"
            runat="server" Width="100%" OnRowDataBound="dgrShowRecords_RowDataBound">
            <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
            <RowStyle CssClass="midcolora"></RowStyle>
            <Columns>
                <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capRECORD_ID" Text="SRL. #"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRECORD_ID" Text='<%# Eval("RECORD_ID") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capAPÓLICE" Text="POLICY NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAPÓLICE" Text='<%# Eval("APÓLICE_CONT") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYMENT_METHOD" Text="PAYMENT METHOD"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYMENT_METHOD" Text='<%# Eval("PAYMENT_METHOD") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYEE_ID_CPF_CNPJ" Text="PAYEE ID CPF CNPJ"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYEE_ID_CPF_CNPJ" Text='<%# Eval("PAYEE_ID_CPF_CNPJ") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYMENT_ID" Text="PAYMENT ID"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYMENT_ID" Text='<%# Eval("PAYMENT_ID") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCARRIER_POLICY_BRANCH_CODE" Text="CARRIER POLICY BRANCH CODE"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCARRIER_POLICY_BRANCH_CODE" Text='<%# Eval("CARRIER_POLICY_BRANCH_CODE") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capREFUND_AMOUNT" Text="AMOUNT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblREFUND_AMOUNT" Text='<%# Eval("REFUND_AMOUNT") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYMENT_DESCRIPTION" Text="PAYMENT DESCRIPTION"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYMENT_DESCRIPTION" Text='<%# Eval("REFUND_PAYMENT_DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capRETURN_STATUS" Text="RETURN STATUS"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRETURN_STATUS" Text='<%# Eval("RETURN_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capFILE_NAMES" Text="FILE NAME"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFILE_NAMES" Text='<%# Eval("FILE_NAMES") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign ="Center">
                            <HeaderTemplate >
                                <asp:Label runat="server" ID="capENDORSEMENT_NUMBER" Text="ENDORSEMENT NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblENDORSEMENT_NUMBER" Text='<%# Eval("ENDORSEMENT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign ="Center">
                            <HeaderTemplate >
                                <asp:Label runat="server" ID="capINSTALLMENT_NUMBER" Text="INSTALLMENT NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblINSTALLMENT_NUMBER" Text='<%# Eval("INSTALLMENT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCLAIM_NUMBER" Text="CLAIM NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCLAIM_NUMBER" Text='<%# Eval("CLAIM_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capBENEFICIARY_NAME" Text="BENEFICIARY NAME"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBENEFICIARY_NAME" Text='<%# Eval("BENEFICIARY_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                     
                     
            </Columns>
        </asp:GridView>
    
        </td>
         </tr>  
         
        <tr>
        <td class="midcolorc" colspan="5">
        <asp:GridView ID="gvRecordSearch" AlternatingItemStyle-CssClass="AlternateDataRow2"
            ItemStyle-CssClass="DataRow2" HeaderStyle-CssClass="headereffectWebGrid" AutoGenerateColumns="false"
            runat="server" Width="100%" OnRowDataBound="gvRecordSearch_RowDataBound">
            <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
            <RowStyle CssClass="midcolora"></RowStyle>
            <Columns>
                <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capRECORD_ID" Text="SRL. #"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRECORD_ID" Text='<%# Eval("RECORD_ID") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capAPÓLICE" Text="POLICY NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAPÓLICE" Text='<%# Eval("APÓLICE_CONT") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYMENT_METHOD" Text="PAYMENT METHOD"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYMENT_METHOD" Text='<%# Eval("PAYMENT_METHOD") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYEE_ID_CPF_CNPJ" Text="PAYEE ID CPF CNPJ"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYEE_ID_CPF_CNPJ" Text='<%# Eval("PAYEE_ID_CPF_CNPJ") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYMENT_ID" Text="PAYMENT ID"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYMENT_ID" Text='<%# Eval("PAYMENT_ID") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCARRIER_POLICY_BRANCH_CODE" Text="CARRIER POLICY BRANCH CODE"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCARRIER_POLICY_BRANCH_CODE" Text='<%# Eval("CARRIER_POLICY_BRANCH_CODE") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capREFUND_AMOUNT" Text="AMOUNT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblREFUND_AMOUNT" Text='<%# Eval("REFUND_AMOUNT") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPAYMENT_DESCRIPTION" Text="PAYMENT DESCRIPTION"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPAYMENT_DESCRIPTION" Text='<%# Eval("REFUND_PAYMENT_DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capRETURN_STATUS" Text="RETURN STATUS"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRETURN_STATUS" Text='<%# Eval("RETURN_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capFILE_NAMES" Text="FILE NAME"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFILE_NAMES" Text='<%# Eval("FILE_NAMES") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign ="Center">
                            <HeaderTemplate >
                                <asp:Label runat="server" ID="capENDORSEMENT_NUMBER" Text="ENDORSEMENT NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblENDORSEMENT_NUMBER" Text='<%# Eval("ENDORSEMENT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign ="Center">
                            <HeaderTemplate >
                                <asp:Label runat="server" ID="capINSTALLMENT_NUMBER" Text="INSTALLMENT NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblINSTALLMENT_NUMBER" Text='<%# Eval("INSTALLMENT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCLAIM_NUMBER" Text="CLAIM NUMBER"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCLAIM_NUMBER" Text='<%# Eval("CLAIM_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capBENEFICIARY_NAME" Text="BENEFICIARY NAME"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBENEFICIARY_NAME" Text='<%# Eval("BENEFICIARY_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                     
                     
            </Columns>
        </asp:GridView>
        </td>
        </tr>
      <%--  GRID MEL EVENTOS RECORD SEARCH --%>
        <tr>
        <td class="midcolorc" colspan="5">
        <asp:GridView ID="gvSunRecordSearch" AlternatingItemStyle-CssClass="AlternateDataRow2"
            ItemStyle-CssClass="DataRow2" HeaderStyle-CssClass="headereffectWebGrid" AutoGenerateColumns="false"
            runat="server" Width="100%" OnRowDataBound="gvSunRecordSearch_RowDataBound">
            <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
            <RowStyle CssClass="midcolora"></RowStyle>
            <Columns>
                <%--<asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capRECORD_ID" Text="SRL. #"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRECORD_ID" Text='<%# Eval("RECORD_ID") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>--%>
                <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capBatch_Code" Text="Batch Code"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBatch_Code" Text='<%# Eval("Batch_Code") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capEvent_Code" Text="Event Code"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEvent_Code" Text='<%# Eval("Event_Code") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capPosting_Date" Text="Posting Date"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPosting_Date" Text='<%# Eval("Posting_Date") %>'></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capAmount" Text="Amount"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAmount" Text='<%# Eval("Amount") %>'></asp:Label>                              
                            </ItemTemplate>
                     </asp:TemplateField>
                     
                     
                     
                     
            </Columns>
        </asp:GridView>
        </td>
        </tr>
        
          
        
        
    </table>  
   
    
    </form>
</body>
</html>

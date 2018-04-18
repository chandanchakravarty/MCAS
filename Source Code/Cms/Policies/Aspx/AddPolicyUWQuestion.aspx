<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="AddPolicyUWQuestion.aspx.cs" ValidateRequest="false" Inherits="Cms.Policies.Aspx.AddPolicyUWQuestion" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Liability Coverage</title>
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

</head>
<body oncontextmenu="return true;" leftmargin="0" rightmargin="1" onload="ApplyColor();ChangeColor();" ms_positioning="GridLayout">
    <form id="POL_UW_QUESTIONS" runat="server">
    <table cellspacing="2" cellpadding="2" width="100%">
        <tr>
            <td class="headereffectCenter" colspan="4">
                <asp:Label ID="lblTitle" runat="server">Underwriting Questions</asp:Label>
            </td>            
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMessage" runat="server" EnableViewState="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="headerEffectSystemParams" colspan="4">
                <asp:Label ID="lblPolicyCaption" runat="server">Commercial Underwriting Questions</asp:Label>
            </td>
        </tr>
        <tr id="trPOLICY_LEVEL_GRID" runat="server">
            <td class="midcolora">
                <asp:DataGrid ID="dgCommercialUW" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="PARENT_QUES_ID">
                    <AlternatingItemStyle></AlternatingItemStyle>
                    <ItemStyle></ItemStyle>
                    <%--<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>--%>
                    <Columns>
                        <asp:TemplateColumn ItemStyle-Width="2%" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPARENT_QUES_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENT_QUES_ID") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidPARENT_QUES_ID" name="hidPARENT_QUES_ID" runat="server" />

                                <%--<asp:Label ID="lblPARENT_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENT_ID") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidPARENT_ID" name="hidPARENT_ID" runat="server" />--%>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                         <asp:TemplateColumn ItemStyle-Width="38%" HeaderText="" Visible="true">
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPARENT_QUES_DESC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENT_QUES_DESC") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="10%" HeaderText="" Visible="true">
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                             <ItemTemplate>
                               <asp:DropDownList runat="server" ID="cmbYESNO" width="50px" Height="17px">
                                </asp:DropDownList>
                               <%-- <select id="cmbYESNO" style="width:50%" Runat="server">
											</select>--%>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="2%" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblQUES_LEVEL" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_LEVEL") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidQUES_LEVEL" name="hidQUES_LEVEL" runat="server" />

                              <%--  <asp:Label ID="lblCHILD_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CHILD_ID") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidCHILD_ID" name="hidCHILD_ID" runat="server" />--%>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="46%" HeaderText="" Visible="true">
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                             <ItemTemplate>
                                <asp:Label ID="lblCHILD_QUES_DESC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_ANS_TYPE") %>'>
                                </asp:Label> 
                                <asp:Label ID="lblCHILD_QUES_DESC2" runat="server">
                                </asp:Label>                                
                                <%--<asp:TextBox ID="txtCHILD_QUES_DESC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CHILD_QUES_DESC") %>'></asp:TextBox>--%>
                               <%-- <asp:TextBox ID="txtCHILD_QUES_DESC" runat="server"></asp:TextBox>
                                <asp:CheckBox ID="chkCHILD_QUES_DESC"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_ANS_TYPE") %>' />
                                <asp:DropDownList runat="server" ID="cmbCHILD_QUES_DESC" width="50px" Height="17px">
                                </asp:DropDownList>--%>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="2%" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCHILD_QUES_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_ANS_TYPE") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidCHILD_QUES_TYPE" name="hidCHILD_QUES_TYPE" runat="server" />

                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                
            </td>
        </tr>

        <tr>
            <td class="headerEffectSystemParams" colspan="4">
                <asp:Label ID="Label1" runat="server">BOP Underwriting Questions</asp:Label>
            </td>
        </tr>
        <tr id="trRISK_LEVEL_GRID" runat="server">
            <td class="midcolora">
                <asp:DataGrid ID="dgBOPUW" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="PARENT_QUES_ID">
                    <AlternatingItemStyle></AlternatingItemStyle>
                    <ItemStyle></ItemStyle>
                    <%--<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>--%>
                    <Columns>
                        <asp:TemplateColumn ItemStyle-Width="2%" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPARENT_QUES_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENT_QUES_ID") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidPARENT_QUES_ID" name="hidPARENT_QUES_ID" runat="server" />

                                <%--<asp:Label ID="lblPARENT_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENT_ID") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidPARENT_ID" name="hidPARENT_ID" runat="server" />--%>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                         <asp:TemplateColumn ItemStyle-Width="38%" HeaderText="" Visible="true">
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPARENT_QUES_DESC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENT_QUES_DESC") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="10%" HeaderText="" Visible="true">
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                             <ItemTemplate>
                               <asp:DropDownList runat="server" ID="cmbYESNO" width="50%" Height="17px">
                                </asp:DropDownList>
                               <%-- <select id="cmbYESNO" style="width:50%" Runat="server">
											</select>--%>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="2%" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblQUES_LEVEL" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_LEVEL") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidQUES_LEVEL" name="hidQUES_LEVEL" runat="server" />

                              <%--  <asp:Label ID="lblCHILD_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CHILD_ID") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidCHILD_ID" name="hidCHILD_ID" runat="server" />--%>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="46%" HeaderText="" Visible="true">
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                             <ItemTemplate>
                                <asp:Label ID="lblCHILD_QUES_DESC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_ANS_TYPE") %>'>
                                </asp:Label>     
                                 <asp:Label ID="lblCHILD_QUES_DESC2" runat="server">
                                </asp:Label>                               
                                <%--<asp:TextBox ID="txtCHILD_QUES_DESC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CHILD_QUES_DESC") %>'></asp:TextBox>--%>
                               <%-- <asp:TextBox ID="txtCHILD_QUES_DESC" runat="server"></asp:TextBox>
                                <asp:CheckBox ID="chkCHILD_QUES_DESC"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_ANS_TYPE") %>' />
                                <asp:DropDownList runat="server" ID="cmbCHILD_QUES_DESC" width="50px" Height="17px">
                                </asp:DropDownList>--%>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn ItemStyle-Width="2%" HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCHILD_QUES_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUES_ANS_TYPE") %>'>
                                </asp:Label>
                                <input type="hidden" id="hidCHILD_QUES_TYPE" name="hidCHILD_QUES_TYPE" runat="server" />

                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                
            </td>
        </tr>

               

    </table>
    </form>
</body>
</html>

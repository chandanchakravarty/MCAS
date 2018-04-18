<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultilingualSupport.aspx.cs" Inherits="CmsWeb.Aspx.MultilingualSupport" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>    
    <title>Suporte Para Idiomas</title>
		<%--<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>--%>
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet' />
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" language="javascript">
		   function disableAllRfvValidators()
		   {
		       if (document.getElementById('hidLANG_COUNT') == null || document.getElementById('hidLANG_COUNT').value == "" || document.getElementById('hidLANG_COUNT').value == "0")
		       {
		           return;
		       }
		       
		        for(var i = 0;  i<parseInt(document.getElementById('hidLANG_COUNT').value); i++)
		        {
		            var name = 'rfv' + (parseInt(i)+1);

		            if (document.getElementById(name))
                    {
		                document.getElementById(name).style.display = "none";
		                document.getElementById(name).setAttribute('enabled', false);			            
		            }
		        }
		    }		    
		</script>
</head>
<body>
    <form id="Multilingual_Support" runat="server">    
    <table width="100%">
    <tr>
        <td class="pageHeader"><asp:Label ID="lblPageHeader" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="midcolorc" align="right" colspan="4">
			<asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
		</td>
    </tr>
    <tr>
        <td class="midcolora">
            <asp:Panel ID="Multi_Lang_Panel" runat="server">
                <asp:GridView ID="gvCulLang" runat="server" AutoGenerateColumns="False" 
                    Width="100%" onrowdatabound="gvCulLang_RowDataBound">
                <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                <RowStyle CssClass="midcolora"></RowStyle>
                    <Columns>
                    <asp:TemplateField>
                            <ItemStyle Width="5%" CssClass="midcolorc" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCULTURE_LANG" Text=""></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="capCULTURE_LANG" Text='<%# Eval("LANG_NAME")%>'><span class="mandatory">*</span></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField>                                                       
                            <ItemStyle Width="5%" CssClass="midcolorc" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="capCULTURE_LANG_DESC" Text=""></asp:Label><span id="Span1" class="mandatory">*</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                 <asp:TextBox ID="txtCULTURE_LANG_DESC" runat="server" Width="100%" MaxLength="50" Text='<%# Eval("LOOKUP_VALUE_DESC") %>'></asp:TextBox>
                                 <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvCULTURE_LANG_DESC" ControlToValidate="txtCULTURE_LANG_DESC"></asp:RequiredFieldValidator>                                                           
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>            
        </td>
    </tr>
    <tr>
        <td class="midcolorr">     
            <cmsb:CmsButton ID="btnSave" class="clsButton"  runat="server" Text="Save" onclick="btnSave_Click" />
            <cmsb:CmsButton ID="btnClose" class="clsButton"  runat="server" Text="Close" onclick="btnClose_Click" />
        </td>
    </tr>
    <tr>
    <td>
        <input id="hidPRIMARY_COLUMN" type="hidden" runat="server" />
        <input id="hidPRIMARY_ID" type="hidden" runat="server" value="0" />
        <input id="hidMASTER_TABLE_NAME" type="hidden" runat="server" />
        <input id="hidCHILD_TABLE_NAME" type="hidden" runat="server" />
        <input id="hidLANG_COUNT" type="hidden" runat="server" />
        <input id="hidDESCRIPTION_COLUMN" type="hidden" runat="server" />
        <input id="hidPageTitle" type="hidden" runat="server" />
    </td>
    </tr>
    </table>    
    </form>
     <script type="text/javascript" language="javascript">
         try {
             document.title = document.getElementById('hidPageTitle').value;
         }
         catch (err) { }    
    </script>
</body>
</html>

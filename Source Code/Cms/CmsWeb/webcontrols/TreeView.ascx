<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeView.ascx.cs" Inherits="Cms.CmsWeb.WebControls.TreeView" %>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var height = screen.height;
        document.getElementById('divTreeview').style.height = height - 275; //"590px"; // "495px"// "Rightiframe2";
    });
</script>
<div style="width: 100%;height:436px;overflow:auto;" id="divTreeview">
    <table width="100%">
        <tr>
            <td align="left" valign="top">
                <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource" Width="150px"
                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px"  ForeColor="Black"
                    NodeWrap="false" 
                    LineImagesFolder="/Cms/CmsWeb/images/DemoImages/TreeLineImages"
                    ShowLines="True" ontreenodedatabound="TreeView1_TreeNodeDataBound">
                    <ParentNodeStyle Font-Bold="true" />
                    <RootNodeStyle Font-Bold="true" />
                    <HoverNodeStyle ForeColor="Red" />
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="NodeItem" NavigateUrlField="NavigateUrl"  ValueField="ValueF"  TextField="Text" />
                    </DataBindings>
                </asp:TreeView>
            </td>
        </tr>
    </table>
     <asp:XmlDataSource ID="XmlDataSource" runat="server" TransformFile="/Cms/CmsWeb/webcontrols/tree.xsl" XPath="/*/*">
    </asp:XmlDataSource>
</div>

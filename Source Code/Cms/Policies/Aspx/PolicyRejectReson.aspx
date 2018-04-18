<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyRejectReson.aspx.cs" Inherits="Cms.Policies.Aspx.PolicyRejectReson" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Reject Description</title>
    <link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
	<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	<script type="text/javascript" language="javascript">
	    function refreshParent() {
	        var url = window.opener.location.href;
	        var parent = 'PolicyInformation';
	        if (url != null && url != "" && url.indexOf(parent) != -1) {
	            window.opener.location.href = window.opener.location.href;
	        }
	    }
	</script>
</head>
<body onunload="javascript:refreshParent();" >
    <form id="POL_POLICY_REJECTION" runat="server" >    
        <table width="100%">
            <tr>							        
                <td class="headereffectCenter"  colspan="2">
                    <asp:label id="lblHeader" Runat="server"></asp:label>
                </td>
               
            </tr>	
            <tr>
                <td class="midcolorc"  colspan="2">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                </td>
                 
            </tr>
            
            <tr>
                <td class="midcolora"  colspan="2">
                    <asp:Label ID="capREASON_TYPE_ID" runat="server"></asp:Label><span class="mandatory">*</span>
                    <br />
                    <asp:DropDownList ID="cmbREASON_TYPE_ID" runat="server"></asp:DropDownList></br><asp:RequiredFieldValidator ID="rfvREASON_TYPE_ID" runat="server" Display="Dynamic" ControlToValidate="cmbREASON_TYPE_ID"></asp:RequiredFieldValidator>
                    </td >
                      
            </tr>
            
            <tr>
                <td class='midcolora' colspan="2" >
                    <asp:Label ID="capREASON_DESC" runat="server"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtREASON_DESC" Width="100%" runat="server" 
                        TextMode="MultiLine" Rows="15" MaxLength="4000"></asp:TextBox>                   
                    
                </td >
                  
            </tr>
            <tr>
                <td  class='midcolora'  colspan="2">&nbsp;</td>
                
            </tr>
            <tr >
             <td class='midcolora'  >
                    <cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close" ></cmsb:cmsbutton>	
              </td>
              <td class='midcolora' style="width:10%" align="right">
                    <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" onclick="btnSave_Click"></cmsb:cmsbutton>					                
              </td>
            </tr>
            <tr>
                <td>
                    <input id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"/>		    						
				    <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"/>								
				    <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server"/>
				     <input id="hidREJECT_REASON_ID" type="hidden" value="0" name="hidREJECT_REASON_ID" runat="server"/>
				    
				</td>            
            </tr>
        </table>   
    </form>
    
</body>
</html>

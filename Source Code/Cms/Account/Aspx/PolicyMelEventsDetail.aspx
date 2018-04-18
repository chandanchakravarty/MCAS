<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="PolicyMelEventsDetail.aspx.cs" Inherits="Cms.Account.Aspx.PolicyMelEventsDetail" %>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html  >
<head  runat="server">
    <title>Add Policy</title>
        <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		
		<script language="javascript">

	
		     function ResetTheForm() {
		         document.Policy_melevent.reset();
		     }

		     function init() {
		         if (document.getElementById('btnDelete') != null) {
		             if (document.getElementById('hidrowId').value == "NEW")
		                 document.getElementById('btnDelete').setAttribute("disabled", true);
		             else
		                 document.getElementById('btnDelete').setAttribute("disabled", false);
		         }
		         if (document.getElementById('txtDate').value == "") {
		             document.getElementById('txtDate').value = Date().getDate();
		         }
		     }
		    </script>
		    
		  <script language="javascript">  
		    function OpenPolicyLookup() {
		        var strPolicy = document.getElementById('hidPolicy').value;
		        var url = '<%=URL%>';
		        OpenLookupWithFunction(url, 'POLICY_APP_NUMBER', 'CUSTOMER_ID_NAME', 'hidPOLICYINFO', $("#txtPolicyNo").text().trim(), 'DBPolicy', strPolicy, '', 'splitPolicy()');
		    }
		    //This function splits the policyid and policy version id and put it in different controls
		    function splitPolicy() {

		        if (document.getElementById("hidPOLICYINFO").value.length > 0) {

		            var arr = document.getElementById("hidPOLICYINFO").value.split("~");
		            $("#hidPOLICY_ID").val(arr[0]);
		            $("#hidPOLICY_VERSION_ID").val(arr[1]);
		            $("#txtPolicyNo").val(arr[2]);
		            $("#hidCUSTOMER_ID").val(arr[6]);

		        }
		    }

		    function open_popup() {
		        var str;
		    }
		    
		    
		    </script>
		
</head>
<BODY leftMargin="0" topMargin="0" onload="init();">
		<FORM id="Policy_melevent" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">							
							<tr>
                        <td class="pageHeader" colspan="4">
                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    
                    
                    
							<tr>
							
							<TD class="midcolora" width="18%">
								 <asp:Label ID="capDate" runat="server">Date</asp:Label><span class="mandatory">*</span>    
								</TD>
								
						<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtDate" runat="server" size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkDate" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator  ID="rfvDate" runat="server" Display="Dynamic"
                                                ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                         
								 </TD>
								 
								
								
								<TD class="midcolora" width="18%">
								 <asp:Label ID="capPolicyNo" runat="server">Policy #</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtPolicyNo"  size="40"  runat="server"></asp:TextBox>
								    <IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"	runat="server"><br />
								  <asp:RequiredFieldValidator  ID="rfvPolicy_No" runat="server" Display="Dynamic"
                                                ControlToValidate="txtPolicyNo"></asp:RequiredFieldValidator>
                                  
								 </TD>
									
								
							</tr>						
								
							
							
							<tr >
							<td colspan="2">
							
							 </td>
							 </tr>
							
							
							
				<tr>
					<td class="midcolora" colSpan="2">
					<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
					<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
                    causesValidation="false" ></cmsb:cmsbutton>	
					
							</td>
					<td class="midcolorr" colSpan="2">
					
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave"  runat="server" Text="Save"  ></cmsb:cmsbutton>
                            
                            </td>
				</tr>						
						
							
							
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
				  <input id="hidrowId" type="hidden" value="0" name="hidrowId" runat="server"/>
				<input id="hidOldData" type="hidden"  name="hidOldData" runat="server"/>
			
				
				
				
				 <input id="hidHierarchySelected" type="hidden" name="hidHierarchySelected" runat="server"/>
			    <input id="hidPOLICYINFO" type="hidden" runat="server"/>
			    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/> 
			    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>
			    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>  
			    <input id="hidPOLICY_NUM" type="hidden" name="hidPOLICY_NUM" runat="server"/>  
			    <input id="hidPolicy" type="hidden" runat="server" />
				
			</TABLE>
			
			
		</FORM>
		<script type="text/javascript" >
		    if (document.getElementById('hidFormSaved').value == "1") {

		        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidrowId').value);
		    }
		</script>
	</BODY>
</html>


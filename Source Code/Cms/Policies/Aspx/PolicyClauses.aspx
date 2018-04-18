<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyClauses.aspx.cs" validateRequest="false" Inherits="Cms.Policies.Aspx.PolicyClauses" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>POL_CLAUSES</title>
    <%--<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
	<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
    <script type="text/javascript" language="javascript">
        function openViewEditClauseWin(POL_CLAUSE_ID,DEFIND, DEFIND_CHK) {
            if (DEFIND == '1') {

                var url = "../../cmsweb/aspx/ViewEditClauses.aspx" + "?POL_CLAUSE_ID=" + POL_CLAUSE_ID + "&CUSTOMER_ID=" +
                 document.getElementById('hidCustomerID').value + '&POLICY_ID=' + document.getElementById('hidPolicyID').value +
                 '&POLICY_VERSION_ID=' + document.getElementById('hidPolicyVersionID').value + '&DEFIND_ID=' + DEFIND + '&IS_CHECKED=' + DEFIND_CHK;
            }
            else
            {       
                var url = "../../cmsweb/aspx/ViewEditClauses.aspx" + "?POL_CLAUSE_ID=" + POL_CLAUSE_ID + "&CUSTOMER_ID=" +
                 document.getElementById('hidCustomerID').value + '&POLICY_ID=' + document.getElementById('hidPolicyID').value +
                 '&POLICY_VERSION_ID=' + document.getElementById('hidPolicyVersionID').value + '&DEFIND_ID=' + DEFIND;
             }
            
            var nuWin = window.open(url, '', 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=500,height=450');
        }

      
        function ShowAlertMessageForDelete(chkbox,isDelete) 
        {           
            var checked = false;
            for (var i = 0; i < document.POL_CLAUSES.length; i++) 
            {
                control = document.POL_CLAUSES.elements[i];
                if (control.type == 'checkbox' && control.name.indexOf(chkbox) != -1) 
                {
                    if (control.checked) 
                    {
                        checked = true;
                        break;
                    }
                }
            }

            if (checked == false) 
            {
                alert(document.getElementById('lblAlertCheck').value);
                return checked;
            }
            
            if(isDelete)
            {
                var r = confirm(document.getElementById('lblDelete').value);
                return r;
            }
        }     
       
    </script>
</head>
<body style="margin-left:0; margin-top:0" oncontextmenu="javascript:return false;" >
        <br />
		<form id="POL_CLAUSES" name="POL_CLAUSES" method="post" runat="server">
		<div id="gridid" style='height:7000px; overflow:scroll';> 
			<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">					
				<tr>
					<td>
						<table id="Table2" width="100%" align="center" border="0">
							<tbody>	
							    <tr>							        
					                <td class="headereffectCenter" colspan="4"><asp:label id="lblHeader" Runat="server">Clauses</asp:label>
					                </td>
				                </tr>							
								<tr>
									<td class="midcolorc" align="right" colspan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>	
								<tr>
								    <td>&nbsp;</td>
								</tr>							
								<tr>
									<td class="headerEffectSystemParams" colspan="4"><asp:Label ID="capHeading" Text="System Defined Clauses" runat="server"></asp:Label></td>
								</tr>
								<tr>
								    <td colspan="4" width="100%" class="midcolora" >
								        <asp:GridView  runat="server" ID="grdSystemDefinedClauses" AutoGenerateColumns="False" 
	                                         onrowdatabound="grdSystemDefinedClauses_RowDataBound" Width="100%"  >
	                                         <HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
	                                         <RowStyle CssClass="midcolorba" ></RowStyle>                                        
	                                        <Columns>	                                        		                                        				
	                                        <asp:TemplateField>		                                      
	                                        <ItemStyle Width="2%" />					
	                                        <ItemTemplate>
	                                        <asp:CheckBox runat="server"  ID="chkSELECT"></asp:CheckBox>						
	                                        </ItemTemplate>
	                                        </asp:TemplateField>
                                        	  
	                                        <asp:TemplateField Visible="false" >						
	                                        <ItemTemplate >
	                                        <asp:Label runat="server" ID="capCLAUSE_ID"  Text='<%# Eval("CLAUSE_ID") %>'></asp:Label>					
	                                        </ItemTemplate>
	                                        </asp:TemplateField>                                          
	                                       
	                                        
                                            <%--<asp:TemplateField Visible="false" >						
	                                        <ItemTemplate >
	                                        <asp:Label runat="server" ID="capPOL_SUSEP_LOB_ID"  Text='<%# Eval("SUSEP_LOB_ID") %>'></asp:Label>					
	                                        			
	                                        </ItemTemplate>
	                                        </asp:TemplateField>--%>
	                                    	<asp:TemplateField >
	                                        <ItemStyle Width="5%" />	
	                                        <HeaderTemplate >
	                                        <asp:Label runat="server" ID="capHEADER_CLAUSE_CODE" Text="Clause Code"></asp:Label> 
	                                        </HeaderTemplate>
	                                        <ItemTemplate>
	                                        <asp:Label runat="server" ID="capCLAUSE_CODE" Text='<%# Eval("CLAUSE_CODE") %>' ></asp:Label>						
	                                        </ItemTemplate> 
	                                        </asp:TemplateField>			
	                                        <asp:TemplateField >
	                                        <ItemStyle Width="50%" />	
	                                        <HeaderTemplate >
	                                        <asp:Label runat="server" ID="capHEADER_CLAUSE_TITLE" Text="Clause Title"></asp:Label> 
	                                        </HeaderTemplate>
	                                        <ItemTemplate>
	                                        <asp:Label runat="server" ID="capCLAUSE_TITLE" Text='<%# Eval("CLAUSE_TITLE") %>' ></asp:Label>						
	                                        </ItemTemplate> 
	                                        </asp:TemplateField>
	                                        
	                                        <asp:TemplateField Visible="false" >						
	                                        <ItemTemplate >
	                                        <asp:Label runat="server" ID="capCLAUSE_DESCRIPTION"  Text='<%# Eval("CLAUSE_DESCRIPTION") %>'></asp:Label>					
	                                        </ItemTemplate>
	                                        </asp:TemplateField>
                                        	
	                                        <asp:TemplateField  Visible="false">
	                                        <ItemStyle Width="25%" />	
	                                        <HeaderTemplate >
	                                        <asp:Label runat="server" ID="capHEADER_SUSEP_LOB" Text="SUSEP LOB" ></asp:Label>
	                                        </HeaderTemplate>
                                            <ItemTemplate>		
                                           
                                                <asp:TextBox ID="txtSUSEP_LOB" ReadOnly="true" Width="300px" Text='<%# Eval("SUSEP_LOB_DESC") %>' runat="server"> </asp:TextBox>							
	                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                         
	                                          <asp:TemplateField Visible="false" >						
	                                        <ItemTemplate >
	                                        <asp:Label runat="server" ID="capSUSEP_LOB_ID"  Text='<%# Eval("SUSEP_LOB_ID") %>'></asp:Label>					
	                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                       
	                                 <%-- <asp:TemplateField Visible="false" >						
	                                        <ItemTemplate >
	                                        <asp:Label runat="server" ID="capPOL_CLAUSE_ID"  Text='<%# Eval("POL_CLAUSE_ID") %>'></asp:Label>					
	                                        </ItemTemplate>
	                                        </asp:TemplateField>--%>
	                                    
	                                    <asp:TemplateField >
	                                    <ItemStyle Width="5%" />	    
                                        <ItemTemplate>		
                                         <a ID="hlkView" runat="server" href="" ><asp:Label ID="lblView" runat="server"></asp:Label></a>	
	                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    	    
	                                        </Columns>
	                                    </asp:GridView>
									</td>
								</tr>	
								<tr id="trNoRows" runat="server" visible="false">
								    <td class="midcolorc" colspan="2">
								        <asp:Label ID="lblNoRows" runat="server">No Records Found!</asp:Label>
								    </td>
								</tr>					   						
								<tr>					                
					                <td class="midcolora" align="left" style="width:40%">					                   
					                    <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
                                            causesValidation="false" onclick="btnDelete_Click"></cmsb:cmsbutton>									    
					                </td>
					                <td class="midcolorr" align="right" style="width:60%">
					                    <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                                            onclick="btnSave_Click"></cmsb:cmsbutton>										
					                </td>							                
				                </tr>
				                <tr>
				                <td>&nbsp;</td>
				                </tr>
				                <tr>
									<td class="headerEffectSystemParams" colspan="4"><asp:Label ID="capHeading1" Text="User Defined Clauses" runat="server"></asp:Label></td>
								</tr>
								<tr>
								    <td colspan="4" width="100%" class="midcolora">
								        <asp:GridView  runat="server" ID="grdUserDefinedClauses" AutoGenerateColumns="False" 
	                                     onrowdatabound="grdUserDefinedClauses_RowDataBound" Width="100%">
	                                     <HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
	                                     <RowStyle CssClass="midcolorba" ></RowStyle>	                                                          				 
	                                    <Columns>	
	                                    
	                                    <asp:TemplateField>	
                                        <ItemStyle Width="2%" />					
                                        <ItemTemplate>
                                        <asp:CheckBox runat="server"  ID="chkSELECT_UD"></asp:CheckBox>						
                                        </ItemTemplate>
                                        </asp:TemplateField>	
                                	      
	                                    <asp:TemplateField Visible="false" >						
	                                    <ItemTemplate >
	                                    <asp:Label runat="server" ID="capPOL_CLAUSE_ID"  Text='<%# Eval("POL_CLAUSE_ID") %>'></asp:Label>					
	                                    </ItemTemplate>
	                                    </asp:TemplateField>  
	                                    
	                                    <asp:TemplateField >
	                                        <ItemStyle Width="5%" />	
	                                        <HeaderTemplate >
	                                        <asp:Label runat="server" ID="capHEADER_CLAUSE_CODE" Text="Clause Code"></asp:Label> 
	                                        </HeaderTemplate>
	                                        <ItemTemplate>
	                                        <asp:Label runat="server" ID="capCLAUSE_CODE" Text='<%# Eval("CLAUSE_CODE") %>' ></asp:Label>						
	                                        </ItemTemplate> 
	                                        </asp:TemplateField>
	                                     
	                                    <asp:TemplateField Visible="false" >						
	                                    <ItemTemplate >
	                                    <asp:Label runat="server" ID="capPOL_SUSEP_LOB_ID"  Text='<%# Eval("SUSEP_LOB_ID") %>'></asp:Label>					
	                                    </ItemTemplate>
	                                    </asp:TemplateField> 
	                                    
	                                    <asp:TemplateField Visible="false" >						
	                                    <ItemTemplate >
	                                    <asp:Label runat="server" ID="capCLAUSE_ID_UD"  Text='<%# Eval("CLAUSE_ID") %>'></asp:Label>					
	                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    
	                                     <asp:TemplateField Visible="false" >						
	                                    <ItemTemplate >
	                                    <asp:Label runat="server" ID="capPREVIOUS_VERSION_ID"  Text='<%# Eval("PREVIOUS_VERSION_ID") %>'></asp:Label>					
	                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    							
	                                    <asp:TemplateField >
	                                    <ItemStyle Width="50%" />	
	                                    <HeaderTemplate >
	                                    <asp:Label runat="server" ID="capHEADER_CLAUSE_TITLE_UD" Text="Clause Title"></asp:Label> 
	                                    </HeaderTemplate>
	                                    <ItemTemplate>
	                                    <asp:Label runat="server" ID="capCLAUSE_TITLE_UD" Text='<%# Eval("CLAUSE_TITLE") %>' ></asp:Label>						
	                                    </ItemTemplate> 
	                                    </asp:TemplateField>
	                                    
	                                    <asp:TemplateField Visible="false" >						
	                                    <ItemTemplate >
	                                    <asp:Label runat="server" ID="capCLAUSE_DESCRIPTION_UD"  Text='<%# Eval("CLAUSE_DESCRIPTION") %>'></asp:Label>					
	                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    	
	                                    <asp:TemplateField  Visible="false">
	                                    <ItemStyle Width="25%" />	
	                                    <HeaderTemplate >
	                                    <asp:Label runat="server" ID="capHEADER_SUSEP_LOB_UD" Text="SUSEP LOB" ></asp:Label>
	                                    </HeaderTemplate>
                                        <ItemTemplate>		
                                         <asp:DropDownList ID="cmbSUSEP_LOB_ID" runat="server"></asp:DropDownList>				
	                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                	    
	                                    <asp:TemplateField >
	                                    <ItemStyle Width="5%" />	    
                                        <ItemTemplate>		
                                         <a ID="hlkView_Edit" runat="server" href="" ><asp:Label ID="lblView_Edit" runat="server"></asp:Label></a>	
	                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                	    
	                                    </Columns>
	                                    </asp:GridView>
								    </td>
								</tr>
								<tr id="trNoRows_UD" runat="server" visible="false">
								    <td class="midcolorc"  colspan="2">
								        <asp:Label ID="lblNoRows_UD" runat="server">No Records Found!</asp:Label>
								    </td>
								</tr>
								<tr>					                
					                <td class="midcolora" align="left" style="width:40%">
					                    <cmsb:CmsButton class="clsButton" id="btnAdd" runat="server" Text="Add User Defined Clauses"></cmsb:CmsButton>					                   
					                    <cmsb:cmsbutton class="clsButton" id="btnDelete_UD" runat="server" 
                                            Text="Delete" causesValidation="false" onclick="btnDelete_UD_Click"></cmsb:cmsbutton>						                    																		    
					                </td>
					                <td class="midcolorr" align="right" style="width:60%">
					                    <cmsb:cmsbutton class="clsButton" id="btnSave_UD" runat="server" Text="Save" 
                                            onclick="btnSave_UD_Click"></cmsb:cmsbutton>										
					                </td>							                
				                </tr>
							</tbody>
						</table>
					</td>
				</tr>								
				<tr><td>				
				<input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server"/>				
				<input id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"/>		
				<input id="hidLOBID" type="hidden" name="hidLOBID" runat="server"/> 
				<input id="hidSUB_LOB" type="hidden" name="hidSUB_LOB" runat="server"/>								
				<input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"/>								
				<input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server"/>	
				<input id="hidTransactLabel" type="hidden" name="hidTransactLabel" runat="server" />
				<input id="lblDelete" type="hidden" name="lblDelete" runat="server" />
				<input id="lblAlertCheck" type="hidden" runat="server" />													
				</td></tr>				
			</table>
			</div>
		</form>
	</body>
</html>
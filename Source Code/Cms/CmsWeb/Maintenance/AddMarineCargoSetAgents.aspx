<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMarineCargoSetAgents.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddMarineCargoSetAgents" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
         <title>MARINE_CARGO_SETTLING_AGENTS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
		<script language='javascript'>
		    function AddData() {
		        ChangeColor();
		        document.getElementById('hidDETAIL_TYPE_ID').value = 'New';
		        document.getElementById('hidFormSaved').value = '0';
		        if (document.getElementById('btnActivateDeactivate'))
		            document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
		    }
		    function Reset() {
		        document.MARINE_CARGO_SETTLING_AGENTS.reset();
		        populateXML();		       
		        ChangeColor();
		        return false;
		    }
		    function populateXML() {		     
		        if (document.getElementById('hidOldData') != null && document.getElementById('hidFormSaved').value == '1') {
		            var tempXML = '';
		            tempXML = document.getElementById('hidOldData').value;
		            if (tempXML != "" && tempXML != 0 && tempXML!="<NewDataSet />") {
		                    populateFormData(tempXML, MARINE_CARGO_SETTLING_AGENTS);
		                //if (document.getElementById('hidIS_ACTIVE').value == "Y")
		                    //document.getElementById('btnActivateDeactivate').value = 'Deactivate';
		                //else
		                    //document.getElementById('btnActivateDeactivate').value = 'Activate';

		            }
		            else {
		                AddData();
		            }
		        }
		        return false;

		    }

            </script>
</head>
	<body leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<form id="MARINE_CARGO_SETTLING_AGENTS" method="post" runat="server">        
       <div class="pageContent" id="bodyHeight">  
        <table cellspacing='0' cellpadding='0' width='100%' border='0'>
        <tr>
            <td>
                <table width='100%' border='0' align='center'>
                    <tbody>
                        <tr>
                            <td class="pageHeader" colspan="4">
                                <asp:Label ID="capMessages" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capAgentCode" runat="server">Settling Agent code</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtAgentCode" runat="server" MaxLength="10"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvAgentCode" ControlToValidate="txtAgentCode"
                                    runat="server" Display="Dynamic" ErrorMessage = "Please enter Agent Code"></asp:RequiredFieldValidator>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capAgentName" runat="server">Agent Name</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtAgentName" runat="server" MaxLength="40"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvAgentName" ControlToValidate="txtAgentName" runat="server"
                                    Display="Dynamic" ErrorMessage = "Please enter Agent Name" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capAddress1" runat="server">Agent Adderss1</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtAddress1" runat="server" MaxLength="40"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvAddress1" ControlToValidate="txtAddress1" runat="server"
                                    Display="Dynamic" ErrorMessage = "Please enter Agent Address1" ></asp:RequiredFieldValidator>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capAddress2" runat="server">Agent Adderss2</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtAddress2" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capCity" runat="server">City</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtCity" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capCountry" runat="server">Country</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:DropDownList ID="cmbCountry" runat="server"></asp:DropDownList>
                                     <asp:requiredfieldvalidator id="rfvCountry" runat="server" 
                                        ControlToValidate="cmbCountry" Display="Dynamic" ErrorMessage="Please Select Country."></asp:requiredfieldvalidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capSurveyCode" runat="server">Survey Code</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:textbox id="txtSurveyCode" runat="server" MaxLength="40"></asp:textbox>
                                <br />                                
                            </td>
                             <td class='midcolora' width='18%'>
                            </td>
                            <td class='midcolora' width='32%'>
                            </td>
                            </tr>
                        <tr>
                            <td class='midcolora' colspan='2'>
                                <cmsb:CmsButton class="clsButton" ID='btnReset' runat="server" Text='Reset'></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'
                                    CausesValidation="False" Visible="false"></cmsb:CmsButton>
                            </td>
                            <td class='midcolorr' colspan="2">
                                <cmsb:CmsButton class="clsButton" ID='btnSave' runat="server" Text='Save' ></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" id="btnDelete" runat="server" Text='Delete' Visible="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <input id="hidFormSaved" type="hidden" value="1" name="hidFormSaved" runat="server" />
				            <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server" />
				            <input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server" />
                            <input id="hidAgentId" type="hidden" name="hidAgentId" runat="server" />
                            <input id="hidTAB_TITLES" type="hidden" name="hidTAB_TITLES" runat="server" />
                            <input id="hidDETAIL_TYPE_ID" type="hidden" value="New" name="hidDETAIL_TYPE_ID" runat="server" /> 
                            <input id="hidLookUp_ID" type="hidden" value="0" name="hidLookUp_ID" runat="server" />
                            <input id="hidLOOKUP_UNIQUE_ID" type="hidden" value="0" name="hidLOOKUP_UNIQUE_ID"
                                    runat="server" />
                                <script language ="javascript" type ="text/javascript">
                                    if (document.getElementById("hidFormSaved").value == "5") {
                                        /*Record deleted*/
                                        /*Refreshing the grid and coverting the form into add mode*/
                                        /*Using the javascript*/
                                        RefreshWebGrid("1", "1");
                                        document.getElementById("hidFormSaved").value = "0";
                                        AddData();
                                    }

				                </script>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>	
		</form>
       </div>
        <script>
            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDETAIL_TYPE_ID').value);	
		</script>
	</body>
</html>
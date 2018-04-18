<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVesselMaster.aspx.cs"
    Inherits="Cms.CmsWeb.Maintenance.AddVesselMaster" ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register Assembly="CmsButton" Namespace="Cms.CmsWeb.Controls" TagPrefix="cmsb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>MNT_LOOKUP_VALUES</title>
    <%--		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR' />
		<meta content='C#' name='CODE_LANGUAGE' />
		<meta content='JavaScript' name='vs_defaultClientScript' />
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema' />--%>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet' />
    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
    <script type="text/javascript" language='javascript'>

        function AddData() {
            ChangeColor();
            DisableValidators();
            document.getElementById('hidLOOKUP_UNIQUE_ID').value = 'New';
            document.getElementById('hidFormSaved').value = '0';
            if (document.getElementById('btnActivateDeactivate'))
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
        }
        function populateXML() {
            if (document.getElementById('hidOldData') != null && document.getElementById('hidFormSaved').value == '1') {
                var tempXML = '';
                tempXML = document.getElementById('hidOldData').value;
                if (tempXML != "" && tempXML != 0) {

                    populateFormData(tempXML, MNT_VESSEL_MASTER);
                    if(document.getElementById('hidIS_ACTIVE').value == "Y")
                        document.getElementById('btnActivateDeactivate').value = 'Deactivate';
                        else
                            document.getElementById('btnActivateDeactivate').value = 'Activate';

                }
                else  {
                    AddData();
                }
            }
            return false;

        }
        function Validate() {
           
            if (document.getElementById('cmbClass_Type').value == 1) {
                    ValidatorEnable(document.getElementById('rfvClass'), true);
                document.getElementById('spnclass').style.display = "inline";
            }
            else {
                ValidatorEnable(document.getElementById('rfvClass'), false);
                document.getElementById('spnclass').style.display = "none";
            }
        }
        function ResetForm() {
            //    temp = 1;
            DisableValidators();
            document.MNT_VESSEL_MASTER.reset();
            //    DisplayPreviousYearDesc();
            populateXML();
            //    BillType();

            ChangeColor();


            return false;
        }
    </script>
</head>
<body oncontextmenu="javascript:return false;" style="margin-left: 0; margin-top: 0" onload="javascript:populateXML();ApplyColor();">
    <form id='MNT_VESSEL_MASTER' method='post' runat='server'>
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
                                <asp:Label ID="capVESSEL_NAME" runat="server">Vessel Name</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtVESSEL_NAME" runat="server" MaxLength="40"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvVESSEL_NAME" ControlToValidate="txtVESSEL_NAME"
                                    runat="server" Display="Dynamic" ErrorMessage = "Please enter Vessel Name" ></asp:RequiredFieldValidator>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capGRT" runat="server">GRT</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtGRT" runat="server" MaxLength="10"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvGRT" ControlToValidate="txtGRT" runat="server"
                                    Display="Dynamic" ErrorMessage = "Please enter GRT" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capIMO" runat="server">IMO</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtIMO" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capNRT" runat="server">NRT</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtNRT" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capFLAG" runat="server">FLAG</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtFLAG" runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capClassification" runat="server">Classification</asp:Label>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtClassification" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capYear_Built" runat="server">Year Built</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:DropDownList ID="cmbYear_Built" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvYear_Built" ControlToValidate="cmbYear_Built"
                                    runat="server" Display="Dynamic" ErrorMessage = "Please select Year Built" ></asp:RequiredFieldValidator>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capLiner" runat="server">Liner</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:DropDownList ID="cmbLiner" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLiner" ControlToValidate="cmbLiner" runat="server"
                                    Display="Dynamic" ErrorMessage = "Please select Liner"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capType_of_Vessel" runat="server">Type of Vessel</asp:Label><span
                                    class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:DropDownList ID="cmbType_of_Vessel" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvType_of_Vessel" ControlToValidate="cmbType_of_Vessel"
                                    runat="server" Display="Dynamic" ErrorMessage = "Please select Type of Vessel"></asp:RequiredFieldValidator>
                            </td>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capClass_Type" runat="server">Class Type</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:DropDownList ID="cmbClass_Type" runat="server" onchange = "Validate();">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvClass_Type" ControlToValidate="cmbClass_Type"
                                    runat="server" Display="Dynamic" ErrorMessage = "Please select Class Type"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capClass" runat="server">Class</asp:Label><span id = "spnclass" class="mandatory" style ="display:none">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtClass" runat="server" MaxLength ="5"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvClass" ControlToValidate="txtClass" runat="server"
                                    Display="Dynamic" ErrorMessage = "Please enter Class" Enabled ="false"></asp:RequiredFieldValidator>
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
                                    CausesValidation="False"></cmsb:CmsButton>
                            </td>
                            <td class='midcolorr' colspan="2">
                                <cmsb:CmsButton class="clsButton" ID='btnSave' runat="server" Text='Save' OnClientClick ="Validate();" ></cmsb:CmsButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="hidFormSaved" type="hidden" value="1" name="hidFormSaved" runat="server" />
                                <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server" />
                                <input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server" />
                                <input id="hidLOOKUP_UNIQUE_ID" type="hidden" value="0" name="hidLOOKUP_UNIQUE_ID"
                                    runat="server" />
                                <input id="hidLookUp_ID" type="hidden" value="0" name="hidLookUp_ID" runat="server" />
                                <input id="hidLookUp_Name" type="hidden" value="0" name="hidLookUp_Name" runat="server" />
                                <input id="hidLookUp_Desc" type="hidden" value="0" name="hidLookUp_Desc" runat="server" />
                                <input id="hidLookUpValue_Desc" type="hidden" value="0" name="hidLookUpValue_Desc"
                                    runat="server" />
                                <input id="hidCalledForEnter" type="hidden" value="0" name="hidCalledForEnter" runat="server" />
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
    <script type="text/javascript" language="javascript">
        RefreshWindowsGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidLOOKUP_UNIQUE_ID').value);
    </script>
</body>
</html>

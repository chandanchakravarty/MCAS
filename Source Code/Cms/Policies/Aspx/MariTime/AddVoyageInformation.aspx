<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVoyageInformation.aspx.cs" Inherits="Cms.Policies.Aspx.MariTime.AddVoyageInformation" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL_MARINECARGO_VOYAGE_INFORMATION</title>
     <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script> 
    <script language="javascript" type="text/javascript">
        function AddData() {            
            document.getElementById('hid_ID').value = 'New';          
        }

        function Initialize() {

            //if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0')) {
                var tempXML = document.getElementById('hidOldXML').value;                
                if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
                    //setMenu();

                    //Storing the XML in hidCUSTOMER_ID hidden fields
                    if (tempXML != undefined) {
                        document.getElementById('hidOldData').value = tempXML;
                    }

                }
                else {

                    AddData();                    
                }
               
            }

        }



        function initPage() {           
            Initialize();
        }

        function ResetForm() {
            document.MarineCargo_Voyage_Information.reset();           
            return false;
        }
        
    </script>
</head>
<body onload="ApplyColor();initPage();">
    <form id="MarineCargo_Voyage_Information" runat="server">
   <table align="center" border="0" width="100%">
   <tr>
				<td class="midcolorc" align="right" colSpan="4">
				    <asp:label id="Label1" runat="server" Visible="False" CssClass="errmsg"></asp:label>
				</td>
			</tr> 
    <tr>
				<td class="midcolorc" align="right" colSpan="4">
				    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
				</td>
			</tr>
             <tr>
                <td class="midcolora" colspan="4" >
                    <asp:Label ID="Label2" runat="server" Text="Fields having * are mandatory fields"></asp:Label>
                </td>
            </tr>
             <tr>
                <td class="midcolora" width="20%" >                  
                </td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >                    
                </td>
                <td class="midcolora" width="30%" ></td>
            </tr>
             <tr>
				<td class="midcolora" width="25%">
				    <asp:label id="lblVoyageFrom" runat="server" Visible="True" Text="Voyage From" ></asp:label>
                     <span class="mandatory" id="spnVoyageFrom" runat="server">*</span>
				</td>
			         
          
                <td class="midcolora" width="25%">                    
                    <asp:DropDownList ID="cmbVoyageFrom" runat="server"></asp:DropDownList>
                     </br>
                    <asp:RequiredFieldValidator ErrorMessage="Please select Voyage From" ID="rfvVoyageFrom" runat="server" ControlToValidate="cmbVoyageFrom"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
         
				<td class="midcolora" width="25%">
				    <asp:label id="lblVoyageTo" runat="server" Visible="True" Text="Voyage To"></asp:label>
                    <span class="mandatory" id="spnVoyageTo" runat="server">*</span>
				</td>
			        
           
                <td class="midcolora" width="25%">                    
                    <asp:DropDownList ID="cmbVoyageTo" runat="server"></asp:DropDownList>
                     </br>
                     <asp:RequiredFieldValidator ErrorMessage="Please select Voyage To" ID="rfvVoyageTo" runat="server" ControlToValidate="cmbVoyageTo"
                     Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
          
            </tr> 
            <tr>
				<td class="midcolora" width="25%" >
				    <asp:label id="lblThenceTo" runat="server" Visible="True" Text="Thence To"></asp:label>
                    <span class="mandatory" id="spnThenceTo" runat="server">*</span>
				</td>
			
                <td class="midcolora" width="25%">                    
                    <asp:DropDownList ID="cmbThenceTo" runat="server"></asp:DropDownList>
                     </br>
                     <asp:RequiredFieldValidator ErrorMessage="Please select Thence To" ID="rfvThenceTo" runat="server" ControlToValidate="cmbThenceTo"
                     Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolora" width="25%" align="left">
				    <asp:label id="lblQtyAndDesc" runat="server" Text="Quantity And Description"  style="margin-left:0" ReadOnly="true"></asp:label>
                    <span class="mandatory" id="spnQtyAndDesc" runat="server">*</span></br>
				</td>
			
                <td class="midcolora" width="25%"  >                                                         
                    <asp:TextBox ID="txtQtyAndDesc" runat="server" TextMode="MultiLine"></asp:TextBox> </br>
                     <asp:RequiredFieldValidator ErrorMessage="Please enter Quantity and Description" ID="rfvQtyAndDesc" runat="server" ControlToValidate="txtQtyAndDesc"
                     Display="Dynamic"></asp:RequiredFieldValidator>                   
                </td>
</tr>
 <tr>
            <td align="left">  <cmsb:CmsButton class="clsButton" ID="btnReset" CausesValidation="false" runat="server"
                    Text="Reset"></cmsb:CmsButton>
            </td>
          
            <td></td>
            <td></td>
                            <td align="right">
                              
                    <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                    Text="Save"  ></cmsb:CmsButton>
                 
                            </td>
                        </tr>

  <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>
			<input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>
			<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>			
			<input id="hid_ID" type="hidden" value="0" name="hid_ID" runat="server"/> 
			<input id="hidRISK_ID" type="hidden" value="0" name="hidRISK_ID" runat="server"/> 			
			<input id="hidOldData" type="hidden" name="Hidden1" runat="server"/> 
			<input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server"/>			
			<input id="hidOldXML" type="hidden" name="hidOldXML" runat="server"/>	
            <input id="hidVOYAGE_INFO_ID" type="hidden" name="hidVOYAGE_INFO_ID" runat="server"/>							
   </table>
    </form>
</body>
</html>

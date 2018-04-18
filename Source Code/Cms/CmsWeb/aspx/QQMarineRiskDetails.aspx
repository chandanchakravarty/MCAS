<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QQMarineRiskDetails.aspx.cs" Inherits="Cms.CmsWeb.aspx.QQMarineRiskDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MNT_PORT_MASTER</title>
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

            //document.forms[0].reset();
            document.getElementById('hid_ID').value = 'New';
           

        }

        function Initialize() {
          
            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) 
            {
                var tempXML = document.getElementById('hidOldXML').value;
                //alert('tempxml= '+tempXML);
                if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") 
                {
                    setMenu();

                    //Storing the XML in hidCUSTOMER_ID hidden fields
                    if (tempXML != undefined) 
                    {//Done for Itrack Issue 5454 on 20 April 09
                        document.getElementById('hidOldData').value = tempXML;
                    }

                }
                else if(document.getElementById('hid_ID').value =="0") 
                {

                    AddData();
                  
                }
                document.getElementById('txtAircraftNo').style.display = 'none';
                document.getElementById('txtLandTransport').style.display = 'none';
            }

        }



        function initPage() {
           
            Initialize();

        }

        function ResetForm() {

            document.QQMarineRiskDetails.reset();         
            return false;
        }


        function HideShowTxtAircraftNo() {            
            if (document.getElementById('chkAircraftNo').checked) {                
                document.getElementById('txtAircraftNo').style.display = 'inline';
            }
            else {
                document.getElementById('txtAircraftNo').style.display = 'none';
            }
        }

//        function validatorForInsuranceConditions() {debugger;
//        var flag=0;
//        alert('Hi' + document.getElementById('rdlInsuranceCondition1').value);
//        var radioList = document.getElementById('rdlInsuranceCondition1');
//          
//        for (var j = 0; j < radioList.length; j++)
//        {
//                if (radioList[j].checked)
//                    {
//                        flag++;
//                        alert(radioList[j].value);
//                    }
//                    else
//                        flag--;
//                }
//        if((flag==0)&&(!document.getElementById('chkInsuranceCondition2').checked)&&(!document.getElementById('chkInsuranceCondition3').checked))
//        {
//            document.getElementById('spnAPP_EXPIRATION_DATE').innerHTML = "Expiry date should be greater than the Inception date.";
//        }

//         }
//            if ((!document.getElementById('chkInsuranceCondition2').checked) && (!document.getElementById('chkInsuranceCondition3').checked) && (document.getElementById('rdlInsuranceCondition1').selectedItem.value))
//        {
//        }
        

        function HideShowTxtLandTransport() {
            if (document.getElementById('chkLandTransport').checked) {
                document.getElementById('txtLandTransport').style.display = 'inline';
            }
            else {
                document.getElementById('txtLandTransport').style.display = 'none';
            }
        }
        
    </script>
</head>

    <body onload="ApplyColor();initPage();">
    <form id="QQMarineRiskDetails" runat="server">
        <table align="center" border="0" width="100%">   
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
                <td class="midcolora" width="25%" >                  
                </td>
                <td class="midcolora" width="25%" >&nbsp;</td>
                <td class="midcolora" width="25%" >                    
                </td>
                <td class="midcolora" width="25%" ></td>
            </tr>
             <tr>
				<td class="midcolora" width="25%">
				    <asp:label id="lblVoyageFrom" runat="server" Visible="True" Text="Voyage From" ></asp:label>
                     <span class="mandatory" id="spnVoyageFrom" runat="server">*</span>
				</td>
			         
          
                <td class="midcolora" width="25%">                    
                    <asp:DropDownList ID="cmbVoyageFrom" runat="server"></asp:DropDownList>
                     </br>
                    <asp:RequiredFieldValidator ID="rfvVoyageFrom" runat="server" ErrorMessage="Please select Voyage From" ControlToValidate="cmbVoyageFrom"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
         
				<td class="midcolora" width="25%">
				    <asp:label id="lblVoyageTo" runat="server" Visible="True" Text="Voyage To"></asp:label>
                    <span class="mandatory" id="spnVoyageTo" runat="server">*</span>
				</td>
			        
           
                <td class="midcolora" width="25%">                    
                    <asp:DropDownList ID="cmbVoyageTo" runat="server"></asp:DropDownList>
                     </br>
                     <asp:RequiredFieldValidator ID="rfvVoyageTo" ErrorMessage="Please select Voyage To" runat="server" ControlToValidate="cmbVoyageTo"
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
          
				<td class="midcolora" width="25%" >
				    <asp:label id="lblVessel" runat="server" Visible="True" Text="Vessel"></asp:label>
                    <span class="mandatory" id="spnVessel" runat="server">*</span>
				</td>
			
                <td class="midcolora" width="25%">                    
                    <asp:DropDownList ID="cmbVessel" runat="server"></asp:DropDownList>
                     </br>
                    <asp:RequiredFieldValidator ID="rfvVessel" ErrorMessage="Please select Vessel" runat="server" ControlToValidate="cmbVessel"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr>
				<td class="midcolora" width="25%" >
				    <asp:label id="lblAircraftNo" runat="server" Visible="True" Text="Aircraft (Flight Number)"></asp:label></br>
				                
                   
                    <asp:CheckBox ID="chkAircraftNo" runat="server" onclick="HideShowTxtAircraftNo();" ></asp:CheckBox>
                    <asp:TextBox ID="txtAircraftNo" runat="server"></asp:TextBox>

                </td>
          
				<td class="midcolora" width="25%" >
				    <asp:label id="lblLandTransport" runat="server" Visible="True" Text="Land Transport"></asp:label></br>
			          
                   
                    <asp:CheckBox ID="chkLandTransport" runat="server" onclick="HideShowTxtLandTransport();"></asp:CheckBox>
                    <asp:TextBox ID="txtLandTransport" runat="server"></asp:TextBox>

                </td>
        
				<td class="midcolora" width="25%" >
				    <asp:label id="lblVoyageFromDate" runat="server" ReadOnly="True" Text="Voyage From Date"></asp:label>
                    <span class="mandatory" id="spnVoyageFromDate" runat="server">*</span></br>
				                                                   
                    

                </td>
                <td class="midcolora"  width="25%" >
                <asp:TextBox ID="txtVoyageFromDate" runat="server" size="27"></asp:TextBox>
                     <asp:HyperLink ID="hlkVoyageFromDate" runat="server" CssClass="HotSpot" Display="Dynamic" >
                     <asp:Image ID="imgVoyageFromDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                     valign="middle"></asp:Image>
                     </asp:HyperLink> 
                     </br>
                     <asp:RequiredFieldValidator ErrorMessage="Please select Voyage From Date" ID="rfvVoyageFromDate" runat="server" ControlToValidate="txtVoyageFromDate"
                     Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>

            <tr>
				<td class="midcolora" width="25%" align="left">
				    <asp:label id="lblQtyAndDesc" runat="server" Text="Quantity And Description"  style="margin-left:0" ReadOnly="true"></asp:label>
                    <span class="mandatory" id="spnQtyAndDesc" runat="server">*</span></br>
				</td>
			
                <td class="midcolora" width="25%"  >                                                         
                    <asp:TextBox ID="txtQtyAndDesc" runat="server" TextMode="MultiLine"></asp:TextBox> </br>
                     <asp:RequiredFieldValidator ID="rfvQtyAndDesc" ErrorMessage="Please enter Quantity and Description" runat="server" ControlToValidate="txtQtyAndDesc"
                     Display="Dynamic"></asp:RequiredFieldValidator>                   
                </td>
        <td class="midcolora" width="25%"></td>
        <td class="midcolora" width="25%"></td>
				
            </tr>
            <tr>
            <td class="midcolora" width="25%" >
				    <asp:label id="lblInsuranceConditions" runat="server" Text="Insurance Conditions"></asp:label>
                    <span class="mandatory" id="spnInsuranceConditions" runat="server">*</span>
				</td>
			
                <td class="midcolora" width="50%" colspan=3 >                                                                            
                    <asp:RadioButtonList ID="rdlInsuranceCondition1" runat="server"  Font-Size="Smaller" RepeatDirection="Horizontal" >
                       <asp:ListItem Text="ICC(Air)" Value="0.33" Selected="True"></asp:ListItem>
                       <asp:ListItem Text="ICC(B)" Value="0.34"></asp:ListItem>
                       <asp:ListItem Text="ICC(C)" Value="0.35"></asp:ListItem>
                       <asp:ListItem Text="Inland Transport" Value="0.36"></asp:ListItem>
                    </asp:RadioButtonList>                                                                
               </td>
            </tr>
            <tr>
             <td class="midcolora"></td>
              <td class="midcolora" colspan="3">
              <asp:CheckBox ID="chkInsuranceCondition2" runat="server" Text="War, Strikes, Riots & Civil Commotion"></asp:CheckBox>
              </td>
              
            </tr>
            <tr>
             <td class="midcolora"></td>
              <td class="midcolora" colspan="3">
              <asp:CheckBox ID="chkInsuranceCondition3" runat="server" Text="Transhipment"></asp:CheckBox>
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
                    Text="Save" OnClick="btnSave_Click"></cmsb:CmsButton>
                  
                 
                            </td>
                        </tr>


            <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>
			<input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>
			<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>			
			<input id="hid_ID" type="hidden" value="0" name="hid_ID" runat="server"/> 
			<input id="hidQQ_ID" type="hidden" value="0" name="hidQQ_ID" runat="server"/> 
			<input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server"/>
			<input id="hidOldData" type="hidden" name="Hidden1" runat="server"/> 
			<input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server"/>			
			<input id="hidOldXML" type="hidden" name="hidOldXML" runat="server"/>
			<input id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server"/>
			<input id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server"/> 
			<input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server"/>
			<input id="hidMsg" type="hidden" name="hidMsg" runat="server"/>
			<input id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server"/> 
			<input id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication" runat="server"/>
			<input id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server"/>	
			<input id="hidMODEL" type="hidden" name="hidMODEL" runat="server"/>
			<input id="hidMAKE" type="hidden" name="hidMAKE" runat="server"/>
			<input id="hidMODEL_TYPE" type="hidden" name="hidMODEL_TYPE" runat="server"/>
			


            </table>
            </form>  
            
</body>
</html>

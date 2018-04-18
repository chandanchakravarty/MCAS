<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPortMaster.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddPortMaster" %>

<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  
    <title>MNT_PORT_MASTER</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/DwellingInfo.js"></script>
         <script src="/cms/cmsweb/scripts/calendar.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
        <script language="javascript">
            var jsaAppDtFormat = "<%=aAppDtFormat%>";
          function AddData() {
		        ChangeColor();
		        DisableValidators();
		        document.getElementById('hidDETAIL_TYPE_ID').value = 'New';
		        document.getElementById('txtPort_Code').value = 'To be generated';
		       
		        if (document.getElementById('btnActivateDeactivate'))
		            document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
		       
		    }
		    function populateXML() {
		        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {


		            var tempXML;
		            if (document.getElementById('hidOldData') != null) {
		                tempXML = document.getElementById('hidOldData').value;

		                if (tempXML != "" && tempXML != 0) {
		                    populateFormData(tempXML, MNT_PORT_MASTER);
		                }
		                else {

		                    if (document.getElementById('btnActivateDeactivate') != null)
		                        if (document.getElementById("btnActivateDeactivate"))
		                            document.getElementById('btnActivateDeactivate').style.display = "none";
		                    AddData();
		                }
		            }

		        }
		        return false;
		    }
		    function ResetForm() {
		        //    temp = 1;
		        DisableValidators();
		        document.MNT_PORT_MASTER.reset();
		        //    DisplayPreviousYearDesc();
		        populateXML();
		        //    BillType();
		       
		        ChangeColor();


		        return false;
		    }
		    function CompareEffDateWithExpDate(objSource, objArgs) {

		        var effdate = document.MNT_PORT_MASTER.txtFrom_Date.value;
		        var expdate = document.MNT_PORT_MASTER.txtTo_Date.value;
		        if (document.MNT_PORT_MASTER.txtFrom_date != null && document.MNT_PORT_MASTER.txtTo_Date != null && expdate != "" && effdate != "") {
		            objArgs.IsValid = CompareTwoDate(expdate, effdate, jsaAppDtFormat);
		        }
		    }

		    function CompareTwoDate(DateFirst, DateSec, FormatOfComparision) {
		        var saperator = '/';
		        var firstDate, secDate;
		        var strDateFirst = DateFirst.split("/");
		        var strDateSec = DateSec.split("/");
		        if (FormatOfComparision.toLowerCase() == "dd/mm/yyyy") {
		            firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
		            secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0]) + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
		        }
		        if (FormatOfComparision.toLowerCase() == "mm/dd/yyyy") {
		            firstDate = DateFirst
		            secDate = DateSec;
		        }
		        firstDate = new Date(firstDate);
		        secDate = new Date(secDate);
		        firstSpan = Date.parse(firstDate);
		        secSpan = Date.parse(secDate);
		        if (firstSpan > secSpan)
		            return true; // first is greater
		        else
		            return false; // secound is greater
		    }

		    function CompareExpDateWithEffDate(objSource, objArgs) {
		        var effdate = document.MNT_PORT_MASTER.txtFrom_Date.value;
		        var expdate = document.MNT_PORT_MASTER.txtTo_Date.value;
		        if (document.MNT_PORT_MASTER.txtFrom_Date != null && document.MNT_PORT_MASTER.txtTo_Date != null && expdate != "" && effdate != "") {
		            objArgs.IsValid = CompareTwoDate(expdate, effdate, jsaAppDtFormat);
		        }
		    }

		    function setSettllingAgentName() {

		        if (document.getElementById('cmbSettling_Agent_Name').value != "") {
		            
		            document.getElementById('hidSettllingAgent').value = document.getElementById('cmbSettling_Agent_Name').value;
		    
		        }
		    }
        </script>
        
		
</head>
<body onload="Javascript:populateXML();">
    <form id="MNT_PORT_MASTER" runat="server">
    <table cellspacing='0' cellpadding='0' width='100%' border='0'>
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table width='100%' border='0' align='center'>
                    <tr>
                        <td class="pageHeader" colspan="4">
                           <asp:Label ID="capMessages" runat="server">Please note that all fields marked with * are 
									mandatory</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td class='midcolora' width='18%'>
                            <asp:Label ID="capPort_code" runat="server">Port Code</asp:Label><span id="spnPort_Code" runat="server" class="mandatory">*</span>
                          
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtPort_code' runat='server' size='30'  ReadOnly="true"></asp:TextBox><br>
                                           
                        </td>
                        
                          <td class='midcolora' width='18%'>
                            <asp:Label ID="capISO_Code" runat="server">ISO Code</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                          <asp:TextBox ID='txtISO_Code' runat='server' size='30' MaxLength='10'></asp:TextBox><br>
                        </td>
                     
                    </tr>
                    <tr>
                      <td class='midcolora' width='18%'>
                            <asp:Label ID="capPort_Type" runat="server">Port Type</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtPort_Type' runat='server' size='8' MaxLength='10'></asp:TextBox><br>
                            
                         </td>
                           <td class='midcolora' width='18%'>
                            <asp:Label ID="capCountry" runat="server">Country/Port</asp:Label><span id="spnCountry" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtCountry' runat='server' size='30' MaxLength='40'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="txtCountry"
                                ErrorMessage="Please enter Country/ Port." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
 
                    </tr>
                    <tr>
                         <td class='midcolora' width='18%'>
                            <asp:Label ID="capAdditional_War_Rate" runat="server">Additional War Rate</asp:Label><span id="spnAdditional_War_Rate" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtAdditional_War_Rate' runat='server' size='30' MaxLength='10' Text="0.00000000"></asp:TextBox>
                            <br />
                             <asp:RequiredFieldValidator ID="rfvAdditional_War_Rate" runat="server" ControlToValidate="txtAdditional_War_Rate"
                                ErrorMessage="Please enter Additional War Rate." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                         <td class='midcolora' width='18%'>
                         </td>
                         <td class='midcolora' width='32%'></td>
                     
                      
                    </tr>
                    <tr>
                    <td class='midcolora' width='18%'>
                            <asp:Label ID="capFrom_Date" runat="server">From Date</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                          <asp:TextBox ID='txtFrom_Date' runat='server' size='30' MaxLength="15" ></asp:TextBox>
                           <asp:HyperLink  ID="hlkFrom_Date" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgFrom_Date" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                      </asp:Image>
                </asp:HyperLink><br>
                <asp:customvalidator id="csvFrom_Date" Display="Dynamic" ControlToValidate="txtFrom_Date" Runat="server"
										ClientValidationFunction="CompareEffDateWithExpDate" ErrorMessage="Invalid From Date"></asp:customvalidator>
                                        <asp:RequiredFieldValidator ID="rfvFrom_Date" Runat="server" Display="Dynamic" ControlToValidate="txtFrom_Date" ErrorMessage="Please enter From Date."></asp:RequiredFieldValidator>
                        </td>
                      <td class='midcolora' width='18%'>
                            <asp:Label ID="capTo_Date" runat="server">To Date</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtTo_Date' runat='server' size='30' MaxLength='70'></asp:TextBox>
                             <asp:HyperLink  ID="hlkTo_Date" runat="server" CssClass="HotSpot">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                      </asp:Image>
                </asp:HyperLink><br />
                 <asp:customvalidator id="csvTo_Date" Display="Dynamic" ControlToValidate="txtTo_Date"
									Runat="server" ClientValidationFunction="CompareExpDateWithEffDate" ErrorMessage="Invalid To Date"></asp:customvalidator>
                                    <asp:RequiredFieldValidator ID="rfvTo_Date" Runat="server" Display="Dynamic" ControlToValidate="txtTo_Date" ErrorMessage="Please enter To Date."></asp:RequiredFieldValidator>
                        </td>
              
                    </tr>
                    <tr>
                     <td class='midcolora' width='18%'>
                            <asp:Label ID="capSettlement_Agent_Code" runat="server">Settlement Agent Code</asp:Label><span id="spnSettlement_Agent_Code" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtSettlement_Agent_Code' runat='server' size='30' MaxLength='10' ></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvSettlement_Agent_Code" runat="server" ControlToValidate="txtSettlement_Agent_Code"
                                ErrorMessage="Please enter Settlement Agent Code." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                           <td class="midcolora" width="18%">
                           <asp:Label ID="capSettling_Agent_Name" Runat="server">Settling Agent Name</asp:Label><span class="mandatory">*</span></td>
									<td class="midcolora" colspan="3">
										<asp:DropDownList ID="cmbSettling_Agent_Name" onchange="javascript:setSettllingAgentName();"  Runat="server" ></asp:DropDownList><br>
										<asp:RequiredFieldValidator ID="rfvSettling_Agent_Name" Runat="server" Display="Dynamic" ControlToValidate="cmbSettling_Agent_Name" ErrorMessage="Please enter Settlement Agent Name." ></asp:RequiredFieldValidator>
                        </td>
                      
                       
                    </tr>
                    
                   
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                          
                            <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" runat="server"></cmsb:CmsButton>
                          </td>
                          
                        <td class='midcolorr' colspan="2">
                          <cmsb:CmsButton class="clsButton" ID='btnSave' runat="server" Text='Save' ></cmsb:CmsButton>
                        </td>
                    </tr>
                 <tr>
                                       
                </table>
            </td>
        </tr>
    </table>
    
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidDIV_ID" type="hidden" name="hidDIV_ID" runat="server">
    <input id="hidExt" type="hidden" name="hidExt" runat="server">
     <input id="hidDIV_COUNTRY" type="hidden" name="hidDIV_COUNTRY" runat="server">
    <input id="hidSTATE" type="hidden" name="hidSTATE" runat="server">    
    <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>    
    <input id="hidLOOKUP" type="hidden" value="0" name="hidLOOKUP" runat="server">
    <input id="hidHOLDER_NAME" type="hidden" runat="server" NAME="hidHOLDER_NAME">
     <input  type="hidden" runat="server" ID="hidDETAIL_TYPE_ID"  value=""  name="hidDETAIL_TYPE_ID"/>
     <input  type="hidden" runat="server" ID="hidSettllingAgent"  value=""  name="hidSettllingAgent"/>
    </form>
     <script>

         RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDETAIL_TYPE_ID').value);


    </script>
</body>
</html>

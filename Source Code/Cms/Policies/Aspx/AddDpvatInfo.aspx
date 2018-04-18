<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDpvatInfo.aspx.cs" Inherits="Cms.Policies.Aspx.AddDpvatInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>POL_PRODUCT_LOCATION</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		
		<script language="javascript" type="text/javascript">
		    function Init() {
		        ApplyColor();
		        ChangeColor();
		        
		    }
		    function setTab() {

		        if (document.getElementById('hidVEHICLE_ID') != null && document.getElementById('hidVEHICLE_ID').value != '' && document.getElementById('hidVEHICLE_ID').value != 'NEW') {
		            if (document.getElementById('hidCALLED_FROM') != null) {
		                var CalledFrom = document.getElementById('hidCALLED_FROM').value;
		                var RISK_ID = document.getElementById('hidVEHICLE_ID').value;
		                var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
		                var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
		                var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
		                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
		                var tabtitles = TAB_TITLES.split(',');

		                if (CalledFrom != '' && CalledFrom == "DPVA"||"DPVAT2") 
		                {
		                    if (RISK_ID != "NEW") {

		                        Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&";
		                        DrawTab(2, top.frames[1], tabtitles[1], Url);

		                        Url = "/Cms/Policies/aspx/AddPolicyCoverages.aspx?CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CalledFrom=" + CalledFrom + "&RISK_ID=" + RISK_ID + "&";
		                        DrawTab(3, top.frames[1], tabtitles[2], Url);

		                    }
		                }
 
		            }
		        } else {
		            RemoveTab(3, top.frames[1]);
		            RemoveTab(2, top.frames[1]);
		        }
		     }
		     function ResetTheForm() {
		         document.POL_CIVIL_TRANSPORT_VEHICLES.reset();
		     }
        </script>

</head>
<body  leftMargin="0"  rightMargin="0" MS_POSITIONING="GridLayout"  onload="Init();setTab();">
    <form id="POL_CIVIL_TRANSPORT_VEHICLES"  runat="server" name="POL_CIVIL_TRANSPORT_VEHICLES" onsubmit="" method="post">
        <table id="Table1" cellspacing="2" cellpadding="0" width="100%" border="0" >
           <tr>
            <td  class="midcolorc" colspan="3"><asp:Label runat="server" ID="lblDelete" CssClass="errmsg"></asp:Label></td>
           </tr>
            <tbody runat="server" id="tbody">
             <tr>
                   <td  align="right" colspan="3">
	                 <asp:label id="lblFormLoadMessage" runat="server" Visible="False" 
                           CssClass="errmsg" ></asp:label>
	               </td>
	          </tr>
        
             <tr>
               <td class="pageHeader" align="left" colspan="3" >		           
		           <asp:label id="lblManHeader" Runat="server" ></asp:label>
		       </td>
	        </tr>
    	
	    <tr>
		    <td class="midcolorc" colspan="3"><asp:label id="lblMessage" runat="server" 
                    CssClass="errmsg" ></asp:label>
		    </td>
	    </tr>
	    
	 
	  <td>
	  <tr   >
		    <td class="midcolora" Width="33%" >
		         &nbsp;</td>	
          
		    
		     <td class="midcolora" Width="33%" >
		         &nbsp;</td>	
		     <td class="midcolora" Width="33%" >
		         &nbsp;</td>	
	    </tr>
	    
	   
	  <tr   >
		    <td class="midcolora" Width="33%" >
		         <asp:Label ID="capTICKET_NUMBER" runat="server" Text="Ticket #:"></asp:Label><span class="mandatory">*</span><br />
		         <asp:TextBox ID="txtTICKET_NUMBER" runat="server" MaxLength="2"></asp:TextBox><br />
		              <asp:RequiredFieldValidator ID="rfvTICKET_NUMBER" runat="server" 
                    ControlToValidate="txtTICKET_NUMBER" Display="Dynamic"></asp:RequiredFieldValidator>
                       <asp:regularexpressionvalidator id="revTICKET_NUMBER" runat="server" 
                    Display="Dynamic" ControlToValidate="txtTICKET_NUMBER"></asp:regularexpressionvalidator>
              </td>	
          
		    
		     <td class="midcolora" Width="33%" >
		      <asp:Label ID="capCATEGORY" runat="server" Text="Category:"></asp:Label><span class="mandatory">*</span><br />
		          <asp:DropDownList ID="cmbCATEGORY" runat="server" Width="191px" >
                </asp:DropDownList>
                <br />
		              <asp:RequiredFieldValidator ID="rfvCATEGORY" runat="server" 
                    ControlToValidate="cmbCATEGORY" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>	
		     <td class="midcolora" Width="33%" >
		     <asp:Label ID="capSTATE_ID" runat="server" Text="State:"></asp:Label><span class="mandatory">*</span> <br />
		          <asp:DropDownList ID="cmbSTATE_ID" runat="server" Width="191px" >
                </asp:DropDownList>
                 <br />
		              <asp:RequiredFieldValidator ID="rfvSTATE_ID" runat="server" 
                    ControlToValidate="cmbSTATE_ID" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>	
	    </tr>
	    
	   
	  <tr  >
		   <td class="midcolora" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
          
		    
		     <td class="midcolora" Width="33%" >
		         &nbsp;</td>	
		     <td class="midcolora" Width="33%" >
		         &nbsp;</td>	
	    </tr>
	    
	        <td class="midcolora" >
	            <cmsb:CmsButton  runat="server" ID="btnReset" Text="Reset"  CssClass="clsButton"  CausesValidation="false"  />
                <cmsb:CmsButton runat="server" ID="btnActivateDeactivate" 
                    Text="Activate/Deactivate"  CssClass="clsButton" CausesValidation="false" 
                    onclick="btnActivateDeactivate_Click"  />


	        </td> 
	        <td class="midcolorr" colspan="2" > 
	            <input  type="hidden"  runat="server" ID="hidCUSTOMER_ID" value=""  name="hidCUSTOMER_ID"/>
	            <input  type="hidden" runat="server" ID="hidPOLICY_ID"   value=""  name="hidPOLICY_ID"/>
	            <input  type="hidden" runat="server" ID="hidPOLICY_VERSION_ID"  value=""  name="hidPOLICY_VERSION_ID"/>
	            <input  type="hidden" runat="server" ID="hidVEHICLE_ID"   value=""  name="hidVEHICLE_ID"/>
	            <input  type="hidden" runat="server" ID="hidFormSaved"  value=""  name="hidFormSaved"/>  
	            <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	            <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>   
            
                <cmsb:CmsButton  runat="server" ID="btnDelete" Text="Delete"  
                    CssClass="clsButton" CausesValidation="false" onclick="btnDelete_Click"   />&nbsp;&nbsp;
                <cmsb:CmsButton  runat="server" ID="btnSave" Text="Save"  CssClass="clsButton"  
                    CausesValidation="true" onclick="btnSave_Click" />
 	        </td>
 	        </td>
	   
	    </tbody>
	   </table>
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") 
        { try
          {
              RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidVEHICLE_ID').value);
           }
           catch(err){}
        }
	</script>
</body>
</html>

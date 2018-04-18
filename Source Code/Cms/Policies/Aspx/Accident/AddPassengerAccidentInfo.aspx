<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPassengerAccidentInfo.aspx.cs" Inherits="Cms.Policies.Aspx.Accident.AddPassengerAccidentInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>POL_PASSENGERS_PERSONAL_ACCIDENT_INFO</title>
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		
		<script language="javascript" type="text/javascript">
		    function setTab() {

		        var pagefrom = '<%=PAGEFROM %>'
		        var firsttab = '<%=FIRSTTAB %>'
		        if (document.getElementById('hidPERSONAL_ACCIDENT_ID') != null && document.getElementById('hidPERSONAL_ACCIDENT_ID').value != "NEW" && document.getElementById('hidPERSONAL_ACCIDENT_ID').value != "") {
		            if (document.getElementById('hidPERSONAL_ACCIDENT_ID') != null)
		                riskId = document.getElementById('hidPERSONAL_ACCIDENT_ID').value;

		            if (document.getElementById('hidCO_APPLICANT_ID') != null)
		                CO_APPLICANT_ID = document.getElementById('hidCO_APPLICANT_ID').value;
		            
		            var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
		            var tabtitles = TAB_TITLES.split(',');
		            var CalledFrom = document.getElementById('hidCALLED_FROM').value;
		            if (pagefrom == "QAPP") {// changed by sonal to implemt this in quick app

		                Url = parent.document.getElementById('hidFrameUrl').value + riskId + "&";
		                DrawTab(1, top.frames[1], firsttab, Url);
		            }
		            if (CalledFrom != '' && CalledFrom == "PAPEACC") {

		                Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[0] + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID + "&";
		                DrawTab(2, top.frames[1], tabtitles[0], Url);

		                Url = "/Cms/Policies/aspx/AddpolicyCoverages.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[1] + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID + "&";
		                DrawTab(3, top.frames[1], tabtitles[1], Url);
		            }
		        }
		        else {
		            RemoveTab(3, top.frames[1]);
		            RemoveTab(2, top.frames[1]);
		        }
		        return false;
		    }
		    function InitPage() {
		        ApplyColor();
		        ChangeColor();
		        setTab();
		    }
		    function ResetTheForm() {
		        document.POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.reset();
		    }
		    
		    
        </script>
   </head>
<body  leftMargin="0"  rightMargin="0" MS_POSITIONING="GridLayout"  onload="InitPage();setTab();">
    <form id="POL_PASSENGERS_PERSONAL_ACCIDENT_INFO"  runat="server" name="POL_PASSENGERS_PERSONAL_ACCIDENT_INFO" onsubmit="" method="post">
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
	    
	     
	    <tr >
	        <td class="midcolora" Width="33%" >
	        
	            <asp:Label ID="capSTART_DATE" runat="server" Text="Start Date"></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtSTART_DATE" runat="server"></asp:TextBox>
                 <asp:HyperLink ID="hlkSTART_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgSTART_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
                <asp:regularexpressionvalidator id="revSTART_DATE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtSTART_DATE"></asp:regularexpressionvalidator> 
                <asp:RequiredFieldValidator ID="rfvSTART_DATE" runat="server" 
                    ControlToValidate="txtSTART_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
	        
	        </td>
	        <td class="midcolora" Width="34%" >
    	 
	            <asp:Label ID="capEND_DATE" runat="server" Text="End Date"></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtEND_DATE" runat="server"></asp:TextBox>
                 <asp:HyperLink ID="hlkEND_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgEND_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
                
               
                <asp:regularexpressionvalidator id="revEND_DATE" runat="server" 
                    Display="Dynamic" ControlToValidate="txtEND_DATE"></asp:regularexpressionvalidator> 
                <asp:RequiredFieldValidator ID="rfvEND_DATE" runat="server" 
                    ControlToValidate="txtEND_DATE" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:comparevalidator id="cpvEND_DATE" ControlToValidate="txtEND_DATE" Display="Dynamic" Runat="server" ControlToCompare="txtSTART_DATE" Type="Date"
										Operator="GreaterThanEqual" ErrorMessage="End date should be greater than start date"></asp:comparevalidator>
    	 
	        </td>
	        <td class="midcolora" Width="33%" >
	            <asp:Label ID="capNUMBER_OF_PASSENGERS" runat="server" Text="# of Passengers"></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtNUMBER_OF_PASSENGERS" runat="server" MaxLength="10"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvNUMBER_OF_PASSENGERS" runat="server" 
                    ControlToValidate="txtNUMBER_OF_PASSENGERS" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNUMBER_OF_PASSENGERS" runat="server"   
                    ControlToValidate="txtNUMBER_OF_PASSENGERS" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
	    </tr>
	    <tr>
	    
	    <td   class="midcolora" Width="34%" >
        <%--refer itrack 1400 ,modified by naveen--%>
	     <asp:Label ID="capCoApplicant" runat="server" Text=""></asp:Label><span class="mandatory" id="spn_mandatory">*</span><br />
                <asp:DropDownList ID="cmbCO_APPLICANT_ID" runat="server" >
                </asp:DropDownList>
                <br />
                 <asp:requiredfieldvalidator id="rfvCO_APPLICANT" runat="server" ControlToValidate="cmbCO_APPLICANT_ID" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
	    </td>
	   
	   <td class="midcolora" Width="32%" colspan="2">
      <asp:Label ID="capExceededPremium" runat="server" Text=""></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
        
        </tr>

        <tr>
        
        
         <td   colspan="3" class="midcolora" Width="33%" >
	    	     <asp:Label ID="capRISK_ORIGINAL_ENDORSEMENT_NO" Visible="false" runat="server" Text="Endorsement No"></asp:Label><br />
	    	     <asp:Label ID="lblRISK_ORIGINAL_ENDORSEMENT_NO" Visible="false" runat="server" Text=""></asp:Label>

	    
        </td>
        </tr>
	    <tr>
	        <td class="midcolora" >
	            <cmsb:CmsButton  runat="server" ID="btnReset" Text="Reset"  CssClass="clsButton"  CausesValidation="false"/>
                <cmsb:CmsButton runat="server" ID="btnActivateDeactivate" Text="Activate/Deactivate"  CssClass="clsButton" CausesValidation="false" onclick="btnActivateDeactivate_Click" />


	        </td> 
	        <td class="midcolorr" colspan="2" > 
	          <input  type="hidden"  runat="server" ID="hidCUSTOMER_ID" value=""  name="hidCUSTOMER_ID"/>
	            <input  type="hidden" runat="server" ID="hidPOLICY_ID"   value=""  name="hidPOLICY_ID"/>
	            <input  type="hidden" runat="server" ID="hidPOLICY_VERSION_ID"  value=""  name="hidPOLICY_VERSION_ID"/>
	            <input  type="hidden" runat="server" ID="hidFormSaved"  value=""  name="hidFormSaved"/>  
	            <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	            <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>   
	             <input  type="hidden" runat="server" ID="hidPERSONAL_ACCIDENT_ID"  value=""  name="hidPERSONAL_ACCIDENT_ID"/>   
 <input  type="hidden" runat="server" ID="hidCO_APPLICANT_ID"  value=""  name="hidCO_APPLICANT_ID"/>   	             
                                                    
                <cmsb:CmsButton  runat="server" ID="btnDelete" Text="Delete"  CssClass="clsButton" CausesValidation="false" onclick="btnDelete_Click" />&nbsp;
                <cmsb:CmsButton  runat="server" ID="btnSave" Text="Save"  CssClass="clsButton"  CausesValidation="true" onclick="btnSave_Click"/>
 	        </td>
	    </tr>
	    </tbody>
	   </table>
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {
          var pagefrom = '<%=PAGEFROM %>'
          if (pagefrom == "QAPP") {

              parent.BindRisk();
              for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {

                  if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidPERSONAL_ACCIDENT_ID').value)
                      parent.document.getElementById('cmbRisk').options[i].selected = true;

              }
          }
          else {
              try {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPERSONAL_ACCIDENT_ID').value);
              }

              catch (err) {
              }
          }
        }
		</script>
</body>
</html>

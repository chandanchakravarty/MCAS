<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReserveDetails.aspx.cs" Inherits="Cms.Claims.Aspx.AddReserveDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AddReserveDetails</title>
     <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
			 <style type="text/css">
        
    .hiddenColum
    {
        display:none;
    }
    .visColum
    {
        display:block;
    }
                 .style1
                 {
                     width: 100%;
                 }
                 </style>
        
		<script type="text/javascript">
		
		
     function ShowDetails(clickedCell,dataCell) {
        
       
var spDetail = document.getElementById('spDetails');
var DvShowDetails = document.getElementById('DvShowDetails');
        
         var Details = dataCell.innerText;
        
         if (Details != "" && Details.length>0) 
         {
             spDetail.innerText = Details;
             DvShowDetails.style.display = '';


             var xCord = event.clientX;
             var yCord = event.clientY;

            
             DvShowDetails.style.position = "absolute";

             DvShowDetails.style.left = (xCord - 300);
             DvShowDetails.style.top = (yCord +10);
             DvShowDetails.style.visibility = "visible";

            
         }

     }
     function HideDetails(){

         var DvShowDetails = document.getElementById('DvShowDetails');

         DvShowDetails.style.display = 'none';            
     }

		</script>

	<script type="text/javascript">



	    function validateRecoveryAmount(objSource, objArgs) {
	       

	        var Premium = document.getElementById(objSource.controltovalidate).value;

	        if (parseFloat(Premium) > 0) {

	            objArgs.IsValid = true;
	            
	        }
	        else
	            objArgs.IsValid = false;


	    }
		//Custom validator function for premium > 0
	    function validateOutStandingAmount(objSource, objArgs) {



	       
		        var Premium = document.getElementById(objSource.controltovalidate).value;
		           var txtOUTSTANDING = document.getElementById(objSource.controltovalidate).id;
		           var idenx = txtOUTSTANDING.search("txtOUTSTANDING");
		            var HidPrevOus = "";
		            if (idenx > 0)
		                HidLimit = txtOUTSTANDING.substring(0, idenx) + "HidOutstandingLimitValue";

		            var OutstandingLimit = document.getElementById(HidLimit).value;
		            Premium = formatAmount(Premium);
		            
		            Premium = FormatAmountForSum(Premium);
		            OutstandingLimit = FormatAmountForSum(OutstandingLimit);
		            
		                if (parseFloat(Premium) > 0) 
		                {

		                    Premium = parseFloat(Premium);
		                    previousOutstanding = parseFloat(OutstandingLimit);
		                    if (OutstandingLimit >= Premium)
		                        objArgs.IsValid = true;		                    
		                    else 		                        
		                        objArgs.IsValid = false;		                  
		                    
		                }
		                else
		                    objArgs.IsValid = false;


		            }

		            //Custom validator function for DEDUCTIBLE amount
		            function validateDeductibleAmount(txtDEDUCTIBLE_1) {
		               
		                // IF CURRENT DEDUCTIBLE AMOUNT IS GREATER THAN POLICY DEDUCTIBLE AMOUNT SHOW AN ALERT ONLY
		               
		                var idenx = txtDEDUCTIBLE_1.search("txtDEDUCTIBLE_1");
		                var lblPolicyDeductible = "";
		                var txtAdjustedAmount = "";
		                var txtPaymentAmount = "";
		                var hidPAYMENTAMOUNT = "";

		                if (idenx > 0) {
		                    lblPolicyDeductible = txtDEDUCTIBLE_1.substring(0, idenx) + "lblPOLICY_DEDUCTIBLE";
		                    txtAdjustedAmount = txtDEDUCTIBLE_1.substring(0, idenx) + "txtADJUSTED_AMOUNT";
		                    txtPaymentAmount = txtDEDUCTIBLE_1.substring(0, idenx) + "txtPAYMENT_AMOUNT";
		                    hidPAYMENTAMOUNT = txtDEDUCTIBLE_1.substring(0, idenx) + "hidPAYMENTAMOUNT";
		                }
		                
		                var PolicyDeductibleLimit = document.getElementById(lblPolicyDeductible).innerText;
		                var CurrentAdjustedAmount = document.getElementById(txtAdjustedAmount).value;

		                var CurrentDeductibleAmount = 0;  
		                if(document.getElementById(txtDEDUCTIBLE_1)!=null)
		                   CurrentDeductibleAmount= document.getElementById(txtDEDUCTIBLE_1).value; 

		               
		                if (PolicyDeductibleLimit == "")
		                    PolicyDeductibleLimit = 0;

		                if (CurrentDeductibleAmount == "")
		                    CurrentDeductibleAmount = 0;

		                if (CurrentAdjustedAmount == "")
		                    CurrentAdjustedAmount = 0;

		                CurrentDeductibleAmount = FormatAmountForSum(CurrentDeductibleAmount);
		                PolicyDeductibleLimit = FormatAmountForSum(PolicyDeductibleLimit);
		                CurrentAdjustedAmount = FormatAmountForSum(CurrentAdjustedAmount);

		                if (parseFloat(CurrentDeductibleAmount) >= 0) {

		                    
		                 
		                    CurrentDeductibleAmount = parseFloat(CurrentDeductibleAmount);
		                    PolicyDeductibleLimit = parseFloat(PolicyDeductibleLimit);
		                    CurrentAdjustedAmount = parseFloat(CurrentAdjustedAmount);

		                    var CalculatedPaymentAmount = CurrentAdjustedAmount - CurrentDeductibleAmount;
		                    if (isNaN(CalculatedPaymentAmount) == false && CalculatedPaymentAmount != 0) {
		                        document.getElementById(txtPaymentAmount).value = formatAmount(CalculatedPaymentAmount);
		                        document.getElementById(hidPAYMENTAMOUNT).value = formatAmount(CalculatedPaymentAmount);
		                    }
		                    else 
		                    {
		                        document.getElementById(txtPaymentAmount).value = '';
		                        document.getElementById(hidPAYMENTAMOUNT).value = '';
		                    }
		                    if (PolicyDeductibleLimit < CurrentDeductibleAmount) 
		                        alert(document.getElementById('hidDeductibleAlertMessage').value);
		                   
		                }

		             


		            }
		    function FormatAmountForSum(num) {
		        num = ReplaceAll(num, sGroupSep, '');
		        num = ReplaceAll(num, sDecimalSep, '.');
		        return num;
		    }
		    function validatePaymentAmount(objSource, objArgs) {

		        
		            var Premium = document.getElementById(objSource.controltovalidate).value;
		            var TxtPaymentAmount = document.getElementById(objSource.controltovalidate).id;
		            var idenx = TxtPaymentAmount.search("txtPAYMENT_AMOUNT");
		            var HidPrevOus = "";
		            if (idenx > 0)
		                HidPrevOus = TxtPaymentAmount.substring(0, idenx) + "HidPreviousOutstandingValue";

		            var previousOutstanding = document.getElementById(HidPrevOus).value;
		           
		            
		            Premium = FormatAmountForSum(Premium);
		            previousOutstanding = FormatAmountForSum(previousOutstanding);
		            
		            if (parseFloat(Premium) >= (-9999999999*previousOutstanding)) {



		                Premium = parseFloat(Premium);
		                previousOutstanding = parseFloat(previousOutstanding);
		                if (previousOutstanding >= Premium) 
		                    objArgs.IsValid = true;		               
		                else 
		                    objArgs.IsValid = false;
		               
		            }
		            else {
		                IsValidAction = false;
		                objArgs.IsValid = false;

		            }
		      
		    }
		    
		    
		    function Init()
			{

			  
				ApplyColor();
				ChangeColor();
				setfirstTime();	
				SetMenu();
				findMouseIn();
				top.topframe.main1.mousein = false;

				// CHANGE THE TAB NAME ACCRODING TO THE ACTIVITY TYPE

				DrawTab(1, this.parent, document.getElementById("hidPARENT_TAB_NAME").value, "AddReserveDetails.aspx" + document.getElementById("hidQueryString").value);
				//parent.document.getElementById('_tab0').innerHTML = document.getElementById('hidPARENT_TAB_NAME').value; 
				
				
				
				
//				ActivityClientID = '<%=ActivityClientID%>'
//				ActivityTotalPaymentClientID = '<%=ActivityTotalPaymentClientID%>'						
//				if(document.getElementById(ActivityTotalPaymentClientID))
//					document.getElementById(ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_OUTSTANDING").value);
			}
			function SetMenu()
			{
				//if(document.getElementById("hidOldData").value=="" || document.getElementById("hidOldData").value=="0")
			    if (document.getElementById("hidACTIVITY_ID").value != "" && document.getElementById("hidACTIVITY_ID").value != "0")
				{
					//document.getElementById("btnReserveBreakdown").style.display="none";
					top.topframe.enableMenu('2,2,3'); //Done for Itrack Issue 6752 on 28 Jan 2010
					//top.topframe.enableMenu('2,2,2');
				}
				//else
				{
					//top.topframe.disableMenu('2,2,3');
					//top.topframe.disableMenu('2,2,2');
				}
}


function GoToActivity() {
  
    
          strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
         this.parent.location.href = strURL;
        return false;
      }


function GoToBeneficiary() {

//    var strURL = "PayeeIndex.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTIVITY_REASON=" + document.getElementById("hidACTIVITY_TYPE").value;

//    strURL = "PayeeIndex.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTIVITY_REASON=" + document.getElementById("hidACTIVITY_TYPE").value;
//    parent.location.href = strURL;

    this.parent.changeTab(0,1);
          return false;
      }







      function PartialPaymentCheck() {
         
          Page_ClientValidate("PAYMENT");
         
          if (!Page_IsValid)
              return Page_IsValid;
              
        var ValueCoutnter=0;
        var TxtValue = "";
        var IsValid = true;

       var ActivtityType=document.getElementById('hidACTIVITY_TYPE').value;
       var ActionOnPayment = document.getElementById('hidACTIVITY_ACTION_ON_PAYMENT').value;
       var AlertMessage = ""; //document.getElementById('hidMESSAGE').value;
       var TotalPayment = "";
       if (ActivtityType == "11775")// FOR PAYMENT
       {
           AlertMessage=document.getElementById('hidMESSAGE').value;
           TxtValue = "txtPAYMENT_AMOUNT"
       }
         
       if(ActivtityType=="11773")// FOR RESERVE
         TxtValue="txtOUTSTANDING"

     if (ActivtityType == "11776")// FOR RECOVERY
     {
         AlertMessage = document.getElementById('hidRECOVERY_MESSAGE').value;
         TxtValue = "txtRECOVERY_AMOUNT"
     }
         
          var TargetBaseControl = document.getElementById('<%=grdClaimCoverages.ClientID %>'); 			
              		
              		
                    
              			var Inputs = TargetBaseControl.getElementsByTagName("input");

              			for (var n = 0; n < Inputs.length; ++n) 
              			{

              			    if (Inputs[n].type == 'text' && Inputs[n].id.indexOf(TxtValue, 0) >= 0 ) 
              			      {
              			           if (Inputs[n].value != "") 
              			               {
             			                 ValueCoutnter=ValueCoutnter+1;
              			               }
              			       }

              		     }

           // FOR PARTIAL / FULL PAYMENT ACTIVITY (payment amount can be set against one coverage only)
           if (ActivtityType == "11775" && ValueCoutnter > 1 && (ActionOnPayment == '180' || ActionOnPayment == '181')) {
               alert(AlertMessage);               
               return false;
           }
           else if (ValueCoutnter > 1 && (ActionOnPayment == '190' || ActionOnPayment == '192')) {
               alert(AlertMessage);
               return false;
           }

           if (ValueCoutnter < 1) {
               alert(document.getElementById('HidEmptyRecordMessage').value);               
               return false;
           }
         

           return true; 
               }


               function OpenPayeeTab() {

                   var PAYEE_TAB_CAPTION = document.getElementById('hidPAYEE_TAB_CAPTION').value;
                  // MODIFIED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK:1029)
                   var strURL = "AddPayee.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTIVITY_REASON=" + document.getElementById("hidACTIVITY_TYPE").value;

                   // NOTE IF ACTIVITY TYPE IS PAYMENT PARTIAL (ACTION ON PAYMENT IS 180) AND
                   // IF USER CAN FILL THE DATA IN ANY ONE TEXTBOX. IF USER FILL DATA IN EXTRA 6 COVERAGES (COVREGES NOT COPIED FROM POLICY)
                   // THEN THIS WOULD BE EXPENSE PAYMENT , FOR SUCH COMDITION I ADDDING A QUERYSTRING PARAMETER USING JAVASCRIPT
                   // TO RECOGNISE ON PAYEE TAB
                 
                   if (document.getElementById('hidIS_PAYMENT_EXPENSE_TYPE').value == "Y")
                       strURL += "&IS_PAYMENT_EXPENSE=Y";

                  
                  DrawTab(2, this.parent, PAYEE_TAB_CAPTION, strURL);

                  }
                  
                  function ChangeActivityAmount() {
                    
                      var PageCulture = document.getElementById('hidPAGE_CULTURE').value;
                      var ActivityType = document.getElementById('hidACTIVITY_TYPE').value;
                      var TotalAmount = 0;
                     
                      if (ActivityType == "11775") // FOR PAYMENT
                          TotalAmount = document.getElementById("hidTOTAL_PAYMENT").value;
                      else if(ActivityType == "11776") // FOR RECOVERY
                          TotalAmount = document.getElementById("hidTOTAL_RECOVERY").value;
                      else
                          TotalAmount = document.getElementById("hidTOTAL_OUTSTANDING").value;

                      
                      if (PageCulture ="1")// for "en-US"
                          TotalAmount = formatAmount(TotalAmount);
                      else
                          TotalAmount = formatAmount(TotalAmount);
                     if( parent.document.getElementById(parent.ActivityTotalPaymentClientID)!=null)
                        parent.document.getElementById(parent.ActivityTotalPaymentClientID).innerText = TotalAmount;

                }

                function ResetTheForm() {
                    document.AddReserveDetails.reset();
                    return false;
                }
                //Added by Pradeep for itrack 1512/tfs#240
                function fnFormatAmountForSum(num) {
                    num = ReplaceAll(num, sGroupSep, '');
                    num = ReplaceAll(num, sDecimalSep, '.');
                    return num;
                }
                function validateLimitRange(sender, args) {
                    var input = args.Value;
                    input = formatAmount(args.Value)
                    input = fnFormatAmountForSum(input)
                    var max = 999999999.99;
                    if (parseFloat(input) <= parseFloat(max)) {
                        args.IsValid = true;
                    }
                    else {
                        args.IsValid = false;
                    }
                }
                //Added till here 
         
    </script>
    
</head>
<body oncontextmenu="return true;" onkeydown = "return (event.keyCode!=13)" onload="Init();">
    <form id="AddReserveDetails" runat="server">
    <!-- To add bottom menu -->
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
		
    	<TABLE cellSpacing="0" cellPadding="0" width="95%">
					<tr>
						<TD class="pageHeader" id="tdClaimTop" colSpan="4">
						
						    <webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
						
						</TD>
					</tr>
					<tr>
						<td id="tdWorkflow" class="pageHeader" colspan="4">
							
						    <webcontrol:claimtop id="cltClaimTop" runat="server" width="100%"></webcontrol:claimtop>
							
						</td>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4">
							<webcontrol:ClaimActivityTop id="cltClaimActivityTop" runat="server"></webcontrol:ClaimActivityTop>
						</TD>
					</tr>
					
					<tr>
					 <td>
					   <table cellpadding="0" cellspacing="0" style="width:100%" >
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="headereffectCenter">
                <asp:Label ID="lblTitle" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
        
                &nbsp;</td>
        </tr>
        <tr>
            <td class="midcolorc">
                <asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True" 
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolorc">
            <asp:Panel ID="pnlPersonalInjury" runat="server">
                <table cellpadding="0" cellspacing="0" class="style1">
                <TBODY>
                    <tr>
                        <td width="15%" class="midcolora">
                      
                <asp:Label ID="CapPersonalInjury" runat="server" Font-Bold="True" ></asp:Label>
                        </td>
                        <td class="midcolora">
                            <asp:CheckBox ID="chkPersonalInjury" runat="server" />
                        </td>
                    </tr>
                    </TBODY>
                </table>
             </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="grdClaimCoverages" runat="server" AutoGenerateColumns="False" 
                   Width="100%" onrowdatabound="grdClaimCoverages_RowDataBound" >
                   <HeaderStyle CssClass="headereffectWebGrid" />
                   <RowStyle CssClass="midcolora" />
                   <EmptyDataRowStyle CssClass="midcolora" />
                  
                <Columns>
                
                
                    <asp:BoundField DataField="COVERAGE_ID" HeaderText="Coverage Code ID"/>
                    <asp:BoundField DataField="RISK_ID" HeaderText="RISK_ID"   />
                    <asp:BoundField DataField="RESERVE_ID" HeaderText="RESERVE_ID"   />
                  
                  
                    <asp:BoundField DataField="COV_DES" HeaderText="Coverage"   />
                    <asp:BoundField DataField="LIMIT" HeaderText="Limt" ItemStyle-HorizontalAlign="Right"  />                   
                    
					
                     <asp:TemplateField HeaderText="Deductible"  ItemStyle-HorizontalAlign="Left" >
						<ItemTemplate>
						<table>
						<tr>
						
						 <td   valign="top" class="midcolora">
						 <asp:TextBox ID="txtDEDUCTIBLE_1"  CssClass="INPUTCURRENCY" Runat="server" size="18" MaxLength="10" Text='<%# Eval("DEDUCTIBLE_1") %>' ></asp:TextBox>
							 <asp:Label ID="lblDeductibleAmountMessage" runat="server" ></asp:Label>
						 <asp:RegularExpressionValidator ID="revDEDUCTIBLE_1" Runat="server" ControlToValidate="txtDEDUCTIBLE_1"
										Display="Dynamic"></asp:RegularExpressionValidator>											
						 
						 <asp:Label ID="lblPOLICY_DEDUCTIBLE" runat="server" Text='<%# Eval("DEDUCTIBLE") %>' style="display:none;"></asp:Label>
					     
						
					
						 </td>
						
						 <td align="left">
						 	<asp:Image ID="imgDEDUCTIBLE_1" ImageUrl="../../cmsweb/images/Details.jpg" runat="server" style="cursor:pointer" />

						 </td>
						 
						 </tr>
						</table>					
						
							
						</ItemTemplate>
					</asp:TemplateField>
					
					
                  <asp:BoundField DataField="PREV_OUTSTANDING" HeaderText="PREV_OUTSTANDING" ItemStyle-HorizontalAlign="Right"  />
					<asp:BoundField DataField="OUTSTANDING" HeaderText="Outstanding" ItemStyle-HorizontalAlign="Right"  />
                    <asp:TemplateField HeaderText="Outstanding" ItemStyle-Width="10%">                   
						<ItemTemplate>
                           <%-- changed by praveer itrack no 1512/TFS#240--%>
							<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="18" MaxLength="12" Text='<%# Eval("OUTSTANDING") %>' ></asp:TextBox>
							<asp:RegularExpressionValidator ID="revOUTSTANDING" Runat="server" ControlToValidate="txtOUTSTANDING"
											Display="Dynamic"></asp:RegularExpressionValidator>											
							<asp:CustomValidator ID="csvOUTSTANDING" runat="server" ControlToValidate="txtOUTSTANDING" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateOutStandingAmount" ValidationGroup="PAYMENT"></asp:CustomValidator>	
                            <%-- Added by Pradeep itrack no 1512/TFS#240--%>
                            <asp:CustomValidator ID="csvOUTSTANDING1" runat="server" ControlToValidate="txtOUTSTANDING" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
                           <INPUT id="HidOutstandingLimitValue" type="hidden" value="0" name="HidOutstandingLimitValue" runat="server">
						</ItemTemplate>
					</asp:TemplateField>
					
					  <asp:TemplateField HeaderText="Adjusted Amount" ItemStyle-Width="10%">                   
						<ItemTemplate>
                           <%-- changed by praveer itrack no 1512/TFS#240--%>
							<asp:TextBox ID="txtADJUSTED_AMOUNT"  CssClass="INPUTCURRENCY" Runat="server" size="18" MaxLength="12" Text='<%# Eval("ADJUSTED_AMOUNT") %>' ></asp:TextBox>
							<asp:RegularExpressionValidator ID="revADJUSTED_AMOUNT" Runat="server" ControlToValidate="txtADJUSTED_AMOUNT" Display="Dynamic"></asp:RegularExpressionValidator>											
                            <%-- Added by Pradeep itrack no 1512/TFS#240--%>
                            <asp:CustomValidator ID="csvADJUSTED_AMOUNT" runat="server" ControlToValidate="txtADJUSTED_AMOUNT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
                           
						</ItemTemplate>
					</asp:TemplateField>
					
					<asp:TemplateField HeaderText="PaymentAmount" ItemStyle-Width="10%">                   
						<ItemTemplate>
							<asp:TextBox ID="txtPAYMENT_AMOUNT"   CssClass="INPUTCURRENCY" Runat="server" size="18" MaxLength="10" Text='<%# Eval("PAYMENT_AMOUNT") %>' ></asp:TextBox>
							<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" Runat="server" ControlToValidate="txtPAYMENT_AMOUNT"
											Display="Dynamic"  ValidationGroup="PAYMENT"></asp:RegularExpressionValidator>											
							<asp:CustomValidator ID="csvPAYMENT_AMOUNT" runat="server" ControlToValidate="txtPAYMENT_AMOUNT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validatePaymentAmount" ValidationGroup="PAYMENT"></asp:CustomValidator>								
							 <INPUT id="HidPreviousOutstandingValue" type="hidden" value="0" name="HidPreviousOutstandingValue" runat="server">
							  <INPUT id="hidPAYMENTAMOUNT" type="hidden" value="0" name="hidPAYMENTAMOUNT" runat="server">
						</ItemTemplate>
					</asp:TemplateField>
					
					<asp:TemplateField HeaderText="RecovaryAmount" ItemStyle-Width="10%">                   
						<ItemTemplate>
							<asp:TextBox ID="txtRECOVERY_AMOUNT"  CssClass="INPUTCURRENCY" Runat="server" size="18" MaxLength="10" Text='<%# Eval("RECOVERY_AMOUNT") %>' ></asp:TextBox><br>
							<asp:RegularExpressionValidator ID="revRECOVERY_AMOUNT" Runat="server" ControlToValidate="txtRECOVERY_AMOUNT"
											Display="Dynamic"></asp:RegularExpressionValidator>											
							<asp:CustomValidator ID="csvRECOVERY_AMOUNT" runat="server" ControlToValidate="txtRECOVERY_AMOUNT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateRecoveryAmount"></asp:CustomValidator>	
							
						</ItemTemplate>
					</asp:TemplateField>
					
                   <asp:TemplateField HeaderText="RI Reserve" ItemStyle-Width="10%">
						<ItemTemplate>
							<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" Enabled="false" size="15" MaxLength="10" Text='<%# Eval("RI_RESERVE") %>' ></asp:TextBox>
							
						</ItemTemplate>
					</asp:TemplateField>
                   <asp:TemplateField HeaderText="COI Reserve" >
						<ItemTemplate>
							<asp:TextBox ID="txtCO_RESERVE" CssClass="INPUTCURRENCY" Runat="server" Enabled="false" size="15" MaxLength="10" Text='<%# Eval("CO_RESERVE") %>' ></asp:TextBox>
							<asp:Image ID=imgVIEW_COVERAGE_DETAILS ImageUrl="../../cmsweb/images/ViewReserveDetails.jpg" runat="server" style="cursor:pointer" />
							
						</ItemTemplate>
					</asp:TemplateField>
					
					 <asp:BoundField DataField="TOTAL_PAYMENT_AMOUNT" HeaderText="TotalPaymentAmount"   />
					 <asp:BoundField DataField="LIMIT_OVERRIDE" HeaderText="LimitOverride"   />
					 <asp:BoundField DataField="IS_RISK_COVERAGE" HeaderText="IS_RISK_COVERAGE"   />
				
					 
                <asp:BoundField DataField="DEDUCTIBLE1_AMOUNT_TEXT" HeaderText="DEDUCTIBLE1_TEXT"   />
                  <asp:BoundField DataField="VICTIM_ID" HeaderText="VICTIM_ID"   />
					   <asp:BoundField DataField="VICTIM" HeaderText="VICTIM"   />
					    <asp:BoundField DataField="MAX_DEDUCTIBLE" HeaderText="MAX_DEDUCTIBLE"   />
					    <asp:BoundField DataField="PERSONAL_INJURY" HeaderText="PERSONAL_INJURY"   />
					     <asp:BoundField DataField="ACTUAL_COVERAGE_ID" HeaderText="ACTUAL_COVERAGE_ID"   />
					      <asp:BoundField DataField="IS_RECOVERY_PENDING" HeaderText="IS_RECOVERY_PENDING"   />
				          <asp:BoundField DataField="RESERVE_TYPE" HeaderText="RESERVE_TYPE"   />


                </Columns>
               
                
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" width="50%">
                                                
                                                <cmsb:cmsbutton class="clsButton" id="btnBackToActivity" OnClientClick="javascript:return GoToActivity();" runat="server" Text="Save"></cmsb:cmsbutton>
                                                <cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnReset" runat="server" Text="Reset" 
                                                     OnClientClick="javascript:return ResetTheForm();" />
                                            </td>
                        <td align="right">
                                                <cmsb:cmsbutton  class="clsButton" id="btnSaveAndContinue" runat="server" Text="Save" 
                                                    onclick="btnSaveAndContinue_Click" 
                                                    OnClientClick="return PartialPaymentCheck();" ></cmsb:cmsbutton>
                                                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                                                    onclick="btnSave_Click" OnClientClick="return PartialPaymentCheck();" ></cmsb:cmsbutton>
                                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
					 </td>
					</tr>
    </TABLE>
    
        </div>
        
        
        
        
               <%-- DIV TO SHOW DETAILS IN POPUP--%>
                            <div ID="DvShowDetails" style="border: 1px solid #000000; display:none; background-color: #FFFFEA; width: 300px;">
                                                        <table cellspacing="0"  width="100%">
                                                            <tr>
                                                                <td align="left" 
                                                                    
                                                                    
                                                                    
                                                                    style="background-image: url('../../cmsweb/images/PopupTitleBar.gif'); background-repeat: repeat-x; padding-left: 10px; font-family: Verdana; font-size: 8pt;">
                                                                   
                                                                    <asp:Label ID="LblPopupDetails" runat="server" Font-Bold="True" 
                                                                        Text=""></asp:Label>
                                                                </td>
                                                                
                                                                <td align="right" style="background-image: url('../../cmsweb/images/PopupTitleBar.gif'); background-repeat: repeat-x"  width="30%">
                                                                     <asp:Image ID="ImgCloseDetails" runat="server" 
                                                                         onmouseover="this.style.cursor='pointer'" onclick="HideDetails();"
                                                                         ImageUrl="../../cmsweb/images/Close.jpg" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td  align="left" valign="top" colspan="2">
                                                                <div style="margin: 2px 5px 10px 5px" align="left">
                                                                    <span ID="spDetails" style="font-family: Tahoma; font-size: 12px"></span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                   </div>
                   
                 <INPUT id="hidCALLED_FROM" type="hidden" value="0" name="hidCALLED_FROM" runat="server">
                <INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
                 <INPUT id="hidACTIVITY_TYPE" type="hidden" value="0" name="hidACTIVITY_TYPE" runat="server">
                 <INPUT id="hidACTIVITY_ACTION_ON_PAYMENT" type="hidden" value="0" name="hidACTIVITY_ACTION_ON_PAYMENT" runat="server">
                 <INPUT id="hidACTIVITY_STATUS" type="hidden" value="0" name="hidACTIVITY_STATUS" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			    <INPUT id="hidIS_RESERVE_EXIST" type="hidden" value="N" name="hidLOCATION_STATE" runat="server">
			    <INPUT id="hidMESSAGE" type="hidden" value="" name="hidMESSAGE" runat="server">
			    <INPUT id="hidRECOVERY_MESSAGE" type="hidden" value="" name="hidRECOVERY_MESSAGE" runat="server">
			    
			    <INPUT id="hidTOTAL_OUTSTANDING" type="hidden" value="0" name="hidTOTAL_OUTSTANDING" runat="server">
			     <INPUT id="hidTOTAL_PAYMENT" type="hidden" value="0" name="hidTOTAL_PAYMENT" runat="server">
			     <INPUT id="hidTOTAL_RECOVERY" type="hidden" value="0" name="hidTOTAL_RECOVERY" runat="server">
			      <INPUT id="hidPAYEE_TAB_CAPTION" type="hidden" value="" name="hidPAYEE_TAB_CAPTION" runat="server">
			       <INPUT id="hidIS_PAYMENT_EXPENSE_TYPE" type="hidden" value="" name="hidIS_PAYMENT_EXPENSE_TYPE" runat="server">
			        <INPUT id="HidEmptyRecordMessage" type="hidden" value="" name="HidEmptyRecordMessage" runat="server">
			        
			        <INPUT id="hidPAGE_CULTURE" type="hidden" value="" name="hidPAGE_CULTURE" runat="server">
			        <INPUT id="hidDeductibleAlertMessage" type="hidden" value="" name="hidDeductibleAlertMessage" runat="server">
			         <INPUT id="hidPARENT_TAB_NAME" type="hidden" value="" name="hidPARENT_TAB_NAME" runat="server">
			         <INPUT id="hidQueryString" type="hidden" value="" name="hidQueryString" runat="server">
			        
			         <INPUT id="hidIsVictimEnabled" type="hidden" value="" name="hidIsVictimEnabled" runat="server">
			        
    </form>
</body>
</html>

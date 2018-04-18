<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false  language="c#"  Codebehind="AddPlanBudget.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddPlanBudget" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ADD_PLAN_BUDGET</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		function populateXML()
			{	
				var tempXML = document.getElementById('hidOldData').value;	
					if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
					{
						//Added for Display Total Amount
						if(document.getElementById('lblTOTAL_AMOUNT')!=null)
						document.getElementById('lblTOTAL_AMOUNT').innerHTML = document.getElementById('hidTotalAmt').value;
									
									if(tempXML!="" && tempXML!="<NewDataSet />")
									{   
										populateFormData(tempXML,ADD_PLAN_BUDGET);
										BlankZero();
									}
									else
									{
										
										AddData();
									}
							
						
					}  
					else
					{ 
						AddData();
					}
	    	
			}
		
		function ResetBudgetPlan()
		{	
			DisableValidators();
			document.ADD_PLAN_BUDGET.reset();
			populateXML();
			//Added For Itrack Issue #6366.
			FormatAll();
			ChangeColor();
			focus();
			return false;
		}
		
		function BlankZero()
		{
			if(document.getElementById('txtJAN_BUDGET').value == '0')
			    document.getElementById('txtJAN_BUDGET').value = ''
			if(document.getElementById('txtFEB_BUDGET').value == '0')
			    document.getElementById('txtFEB_BUDGET').value = ''
			if(document.getElementById('txtMARCH_BUDGET').value == '0')
			    document.getElementById('txtMARCH_BUDGET').value = ''
			if(document.getElementById('txtAPRIL_BUDGET').value == '0')
			    document.getElementById('txtAPRIL_BUDGET').value = ''
			if(document.getElementById('txtMAY_BUDGET').value == '0')
			    document.getElementById('txtMAY_BUDGET').value = ''
			if(document.getElementById('txtJUNE_BUDGET').value == '0')
			    document.getElementById('txtJUNE_BUDGET').value = ''
			if(document.getElementById('txtJULY_BUDGET').value == '0')
			    document.getElementById('txtJULY_BUDGET').value = ''
			if(document.getElementById('txtAUG_BUDGET').value == '0')
			    document.getElementById('txtAUG_BUDGET').value = ''
			if(document.getElementById('txtSEPT_BUDGET').value == '0')
			    document.getElementById('txtSEPT_BUDGET').value = ''
			if(document.getElementById('txtOCT_BUDGET').value == '0')
			    document.getElementById('txtOCT_BUDGET').value = ''
			if(document.getElementById('txtNOV_BUDGET').value == '0')
			    document.getElementById('txtNOV_BUDGET').value = ''
			if(document.getElementById('txtDEC_BUDGET').value == '0')
			    document.getElementById('txtDEC_BUDGET').value = ''
		}

		function AddData()
			{
    
					    document.getElementById('txtJAN_BUDGET').value = '';
						document.getElementById('txtFEB_BUDGET').value = '';
						document.getElementById('txtMARCH_BUDGET').value = '';
						document.getElementById('txtAPRIL_BUDGET').value = '';
						document.getElementById('txtMAY_BUDGET').value = '';
						document.getElementById('txtJUNE_BUDGET').value = '';
						document.getElementById('txtJULY_BUDGET').value = '';
						document.getElementById('txtAUG_BUDGET').value = '';
						document.getElementById('txtSEPT_BUDGET').value = '';
						document.getElementById('txtOCT_BUDGET').value = '';
						document.getElementById('txtNOV_BUDGET').value = '';
						document.getElementById('txtDEC_BUDGET').value = '';					
					if(document.getElementById('cmbBUDGET_CATEGORY_ID')!=null)
						document.getElementById('cmbBUDGET_CATEGORY_ID').options.selectedIndex = -1;
						document.getElementById('hidFormSaved').value=="0";
						document.getElementById('hidIDEN_ROW_ID').value=="0";
				
			}
	  function OnDeleteClick()
			{
				var retVal = confirm("Are you sure you want to delete the selected record ?");
				return retVal;
			}
			//Formats the amount and convert 111 into 1.11
			
		function InsertDecimalWithOutDecimal(AmtValues)
		{
			AmtValues = ReplaceAll(AmtValues,".","");
			//alert(AmtValues + "AmtValues");
			DollarPart = AmtValues.substring(0, AmtValues.length - 2);
			CentPart = AmtValues.substring(AmtValues.length - 2);
			
			//tmp = formatCurrency( DollarPart) + "" + CentPart;
			tmp = formatCurrency( DollarPart + "" + CentPart);
			return tmp;
			
		}
		function addCommas(nStr) 
		{ 
			nStr += ''; 
			x = nStr.split('.'); 
			x1 = x[0]; 
			x2 = x.length > 1 ? '.' + x[1] : ''; 
			var rgx = /(\d+)(\d{3})/; 
			while (rgx.test(x1)) { 
				x1 = x1.replace(rgx, '$1' + ',' + '$2'); 
			} 
			return x1 + x2; 
		}

		
		function FormatAmount(txtAmount)
		{			
			if (txtAmount.value != "" )
			{
				amt = txtAmount.value;		
				amt = ReplaceAll(amt,".","");
				/*if (amt.length == 1)
					amt ="0" + amt + "0";
				if (amt.length == 2)
					amt ="0" + amt ;*/
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);				
					CentPart = amt.substring(amt.length - 2);					
					txtAmount.value = InsertDecimalWithOutDecimal(amt);
				}
			}
		}
		
		function FormatAmountLabel(txtAmount)
		{	
			if (txtAmount.value != "" )
			{
				amt = txtAmount.innerHTML;
				
				amt = ReplaceAll(amt,".","");
				
				/*if (amt.length == 1)
					amt ="0" + amt + "0";
				if (amt.length == 2)
					amt ="0" + amt ;*/
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
									
					txtAmount.innerHTML = InsertDecimalWithOutDecimal(amt);
				}
			}
		}
		function focus()
		{
			try
			{
			document.getElementById('cmbGL_ID').focus();
			}
			catch(e)
			{
				return false;
			}
		}
		function FormatAll()
		{
			FormatAmount(document.getElementById('txtJAN_BUDGET'));
			FormatAmount(document.getElementById('txtFEB_BUDGET'));
			FormatAmount(document.getElementById('txtMARCH_BUDGET'));
			FormatAmount(document.getElementById('txtAPRIL_BUDGET'));				
			FormatAmount(document.getElementById('txtMAY_BUDGET'));
			FormatAmount(document.getElementById('txtJUNE_BUDGET'));
			FormatAmount(document.getElementById('txtJULY_BUDGET'));
			FormatAmount(document.getElementById('txtAUG_BUDGET'));
			FormatAmount(document.getElementById('txtSEPT_BUDGET'));
			FormatAmount(document.getElementById('txtOCT_BUDGET'));
			FormatAmount(document.getElementById('txtNOV_BUDGET'));
			FormatAmount(document.getElementById('txtDEC_BUDGET'));
			FormatAmountLabel( document.getElementById('lblTOTAL_AMOUNT'));
		}
		function fnDistributeAmount()
		{
			var amtToDist;
			var initialAmt;
			var totalDistAmt;
			var adjustedAmount;			
			var adjustedAmountStr = new String();			
			initialAmt = parseFloat(ReplaceAll(document.getElementById('txtDISTRIBUTE_AMOUNT').value,",", ""));
			if(document.getElementById('txtDISTRIBUTE_AMOUNT').value.indexOf('.')!=-1)
			{
				alert('Please enter only integer.');
				return false;
			}		
			if(document.getElementById('txtDISTRIBUTE_AMOUNT').value.indexOf('-')!=-1)
			{
				alert('Please enter only +ve integer.');
				return false;
			}		
			if(initialAmt!= "")
			{
			
				if(isNaN(initialAmt))
				{
					alert('Please enter only integer values.');
					return false;
				}
				
				amtToDist     = Math.round(initialAmt / 12);
				totalDistAmt = amtToDist * 12 ;
				adjustedAmount = parseFloat(totalDistAmt - initialAmt);
				adjustedAmountStr = adjustedAmount.toString();
				glbAmt  = amtToDist; //test
			//	glbAmtJan = amtToDist + adjustedAmountStr; //test
				if(adjustedAmountStr.indexOf('-')== 0)
				   {
					adjustedAmountStr = parseFloat(adjustedAmountStr.substr(1,adjustedAmountStr.length))
					document.getElementById('txtJAN_BUDGET').value  = parseFloat(amtToDist) + parseFloat(adjustedAmountStr.toFixed(2));				
					FormatAmount(document.getElementById('txtJAN_BUDGET'));
					}
				else
				   {
					adjustedAmountStr = parseFloat(adjustedAmountStr);					
					document.getElementById('txtJAN_BUDGET').value  = parseFloat(amtToDist) - parseFloat(adjustedAmountStr.toFixed(2));				
					FormatAmount(document.getElementById('txtJAN_BUDGET'));
					}
			//	alert(adjustedAmountStr)			
			//	document.getElementById('lblTOTAL_AMOUNT').innerHTML = parseFloat(totalDistAmt) + parseFloat(adjustedAmountStr.toFixed(2));
			//	document.getElementById('txtJAN_BUDGET').value  = parseFloat(amtToDist) + parseFloat(adjustedAmountStr.toFixed(2));
			    document.getElementById('lblTOTAL_AMOUNT').innerHTML = parseFloat(initialAmt);
			    FormatAmountLabel( document.getElementById('lblTOTAL_AMOUNT'));
			// document.getElementById('txtJAN_BUDGET').value  = parseFloat(amtToDist) - parseFloat(adjustedAmountStr.toFixed(2));
				document.getElementById('txtFEB_BUDGET').value  = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtFEB_BUDGET'));
				
				document.getElementById('txtMARCH_BUDGET').value = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtMARCH_BUDGET'));
				
				document.getElementById('txtAPRIL_BUDGET').value = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtAPRIL_BUDGET'));				
				
				document.getElementById('txtMAY_BUDGET').value  = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtMAY_BUDGET'));
				
				document.getElementById('txtJUNE_BUDGET').value = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtJUNE_BUDGET'));
				
				document.getElementById('txtJULY_BUDGET').value = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtJULY_BUDGET'));
				
				document.getElementById('txtAUG_BUDGET').value  = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtAUG_BUDGET'));
				
				document.getElementById('txtSEPT_BUDGET').value = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtSEPT_BUDGET'));
				
				document.getElementById('txtOCT_BUDGET').value  = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtOCT_BUDGET'));
				
				document.getElementById('txtNOV_BUDGET').value  = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtNOV_BUDGET'));
				
				document.getElementById('txtDEC_BUDGET').value  = parseFloat(amtToDist);
				FormatAmount(document.getElementById('txtDEC_BUDGET'));
			}	
			return false;		
		}
		//Addedf by uday : User can also change the amounts Manually
		function recalculated()
		{
			var glbAmt;
			if(document.getElementById('txtJAN_BUDGET').value=="")
			{
				document.getElementById('txtJAN_BUDGET').value=0;		 
			}
			if(document.getElementById('txtFEB_BUDGET').value=="")
			{
				document.getElementById('txtFEB_BUDGET').value=0;
			}
			if(document.getElementById('txtMARCH_BUDGET').value=="")
			{
				document.getElementById('txtMARCH_BUDGET').value=0;
			}
			if(document.getElementById('txtAPRIL_BUDGET').value=="")
			{
				document.getElementById('txtAPRIL_BUDGET').value=0;
			}
			if(document.getElementById('txtMAY_BUDGET').value=="")
			{
				document.getElementById('txtMAY_BUDGET').value=0;
			}
			if(document.getElementById('txtJUNE_BUDGET').value=="")
			{
				document.getElementById('txtJUNE_BUDGET').value=0;
			}
			if(document.getElementById('txtJULY_BUDGET').value=="")
			{
				document.getElementById('txtJULY_BUDGET').value=0;
			}
			if(document.getElementById('txtAUG_BUDGET').value=="")
			{
				document.getElementById('txtAUG_BUDGET').value=0;
			}
			if(document.getElementById('txtSEPT_BUDGET').value=="")
			{
				document.getElementById('txtSEPT_BUDGET').value=0;
			}
			if(document.getElementById('txtOCT_BUDGET').value=="")
			{
				document.getElementById('txtOCT_BUDGET').value=0;
			}
			if(document.getElementById('txtNOV_BUDGET').value=="")
			{
				document.getElementById('txtNOV_BUDGET').value=0;
			}
			if(document.getElementById('txtDEC_BUDGET').value=="")
			{
				document.getElementById('txtDEC_BUDGET').value=0;
			}	
		
			glbglbAmt=parseFloat(ReplaceAll(document.getElementById('txtJAN_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtFEB_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtMARCH_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtAPRIL_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtMAY_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtJUNE_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtJULY_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtAUG_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtSEPT_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtOCT_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtNOV_BUDGET').value,",", ""))+parseFloat(ReplaceAll(document.getElementById('txtDEC_BUDGET').value,",", ""));			
			if(isNaN(glbglbAmt) != true)
				document.getElementById('lblTOTAL_AMOUNT').innerHTML=glbglbAmt;
				FormatAmountLabel( document.getElementById('lblTOTAL_AMOUNT'));
				
			return glbglbAmt;
		
			
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="focus();populateXML();ApplyColor();ChangeColor();FormatAll();">
		<FORM id="ADD_PLAN_BUDGET" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
											mandatory</TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
									<tr>
										<td>
											<TABLE id="tblBody" width="100%" align="center" border="0" runat="server">
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capGL_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
													<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbGL_ID" onfocus="SelectComboIndex('cmbGL_ID')" runat="server"></asp:dropdownlist><br>
														<asp:requiredfieldvalidator id="rfvGL_ID" Runat="server" ControlToValidate="cmbGL_ID"></asp:requiredfieldvalidator></TD>
														<td class="midcolora" width="18%"></td>
																<td class="midcolora" width="32%"></td>
													
												</TR>
												<tr>
													<TD class="midcolora" id="cellBudgetCategory1" width="18%"><asp:label id="capBUDGET_CATEGORY_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
													<TD class="midcolora" id="cellBudgetCategory2" width="32%"><asp:dropdownlist id="cmbBUDGET_CATEGORY_ID" onfocus="SelectComboIndex('cmbBUDGET_CATEGORY_ID')" runat="server"></asp:dropdownlist><br>
														<asp:requiredfieldvalidator id="rfvBUDGET_CATEGORY_ID" Runat="server" ControlToValidate="cmbBUDGET_CATEGORY_ID"></asp:requiredfieldvalidator></TD>
														<td class="midcolora" width="18%"></td>
																<td class="midcolora" width="32%"></td>
												</tr>
												<tr>
													<td class="midcolora" width="18%"><asp:label id="capDISTRIBUTE_AMOUNT" Runat="server"></asp:label></td>
													<td class="midcolora" width="32%"><asp:textbox id="txtDISTRIBUTE_AMOUNT" Runat="server" CssClass="InputCurrency" style="TEXT-ALIGN:right"
															MaxLength="12"></asp:textbox><cmsb:cmsbutton class="clsButton" id="btnDistribute" runat="server"  CausesValidation="True" Text="Distribute"></cmsb:cmsbutton><br>
															<asp:regularexpressionvalidator id="revDISTRIBUTE_AMOUNT" runat="server" ControlToValidate="txtDISTRIBUTE_AMOUNT" ErrorMessage="Please enter non zero integer value."
											Display="Dynamic"></asp:regularexpressionvalidator></td>
													<td class="midcolora" width="18%"><asp:label id="capTOTAL_AMOUNT" Runat="server"></asp:label></td>
													<td class="midcolora" width="32%"><asp:label id="lblTOTAL_AMOUNT" Runat="server"></asp:label></td>
												</tr>
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capJAN_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtJAN_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revJAN_BUDGET" runat="server" ControlToValidate="txtJAN_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="18%"><asp:label id="capFEB_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtFEB_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revFEB_BUDGET" runat="server" ControlToValidate="txtFEB_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
												</TR>
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capMARCH_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtMARCH_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revMARCH_BUDGET" runat="server" ControlToValidate="txtMARCH_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="20%"><asp:label id="capAPRIL_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtAPRIL_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revAPRIL_BUDGET" runat="server" ControlToValidate="txtAPRIL_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
												</TR>
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capMAY_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtMAY_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revMAY_BUDGET" runat="server" ControlToValidate="txtMAY_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="18%"><asp:label id="capJUNE_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtJUNE_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revJUNE_BUDGET" runat="server" ControlToValidate="txtJUNE_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
												</TR>
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capJULY_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtJULY_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revJULY_BUDGET" runat="server" ControlToValidate="txtJULY_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="18%"><asp:label id="capAUG_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtAUG_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revAUG_BUDGET" runat="server" ControlToValidate="txtAUG_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
												</TR>
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capSEPT_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtSEPT_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revSEPT_BUDGET" runat="server" ControlToValidate="txtSEPT_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="18%"><asp:label id="capOCT_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtOCT_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revOCT_BUDGET" runat="server" ControlToValidate="txtOCT_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
												</TR>
												<TR>
													<TD class="midcolora" width="18%"><asp:label id="capNOV_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtNOV_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revNOV_BUDGET" runat="server" ControlToValidate="txtNOV_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="18%"><asp:label id="capDEC_BUDGET" runat="server"></asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtDEC_BUDGET" style="TEXT-ALIGN: right" runat="server"
															maxlength="9" onblur="adjustAmount(this);"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revDEC_BUDGET" runat="server" ControlToValidate="txtDEC_BUDGET" Display="Dynamic"
															ErrorMessage="Please enter non zero integer value."></asp:regularexpressionvalidator></TD>
												</TR>
												<TR>
													<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
													<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
												</TR>
											</TABLE>
										</td>
									</tr>
								</TBODY>
								<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
								<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidGL_ID" type="hidden" name="hidGL_ID" runat="server">
								<INPUT id="hidTotalAmt" type="hidden" name="hidTotalAmt" runat="server">
								<INPUT id="hidIDEN_ROW_ID" type="hidden" name="hidIDEN_ROW_ID" runat="server"> <INPUT id="hidBudgetCategory" type="hidden" name="hidBudgetCategory" runat="server">
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,false);
		</script>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></BODY>
</HTML>

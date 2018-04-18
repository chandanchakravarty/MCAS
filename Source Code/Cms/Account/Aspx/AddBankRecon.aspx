<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AddBankRecon.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddBankRecon" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>ACT_BANK_RECONCILIATION</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
		<script language="javascript">
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{
						
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtReceiptAmount.value = InsertDecimal(amt);
				}
			}
		}
		
	function AddData()
	{
		ChangeColor();
		DisableValidators();
		document.getElementById('hidAC_RECONCILIATION_ID').value	=	'New';
		var element = document.getElementById('cmbACCOUNT_ID')
		try{
		document.getElementById('cmbACCOUNT_ID').focus();
		}
		catch(er)
		{}
		document.getElementById('cmbACCOUNT_ID').options.selectedIndex = -1;
		document.getElementById('txtSTATEMENT_DATE').value  =  '<%=DateTime.Now.ToString("MM/dd/yyyy")%>';
		document.getElementById('txtSTARTING_BALANCE').value  = '';
		document.getElementById('txtENDING_BALANCE').value  = '';		
		document.getElementById('txtBANK_CHARGES_CREDITS').value  = '';
		if(document.getElementById('btnDistribute')!=null)
			document.getElementById('btnDistribute').setAttribute('disabled',true);
		if(document.getElementById('btnDelete')!=null)
			document.getElementById('btnDelete').setAttribute('disabled',true);
		if(document.getElementById('btnCommit')!=null)
			document.getElementById('btnCommit').setAttribute('disabled',true);
		if(document.getElementById('btnItemsToReconcile')!=null)
			document.getElementById('btnItemsToReconcile').setAttribute('disabled',true);
		if(document.getElementById('btnImport')!=null)
			document.getElementById('btnImport').setAttribute('disabled',true);
		if(document.getElementById('btnReProcessItemsToReconcile')!=null)	
			document.getElementById('btnReProcessItemsToReconcile').setAttribute('disabled',true);
		if(document.getElementById('btnRptItemsToRecon')!=null)
		  document.getElementById('btnRptItemsToRecon').setAttribute('disabled',true);
       if(document.getElementById('btnRptOustandings')!=null)
          document.getElementById('btnRptOustandings').setAttribute('disabled',true);		
		document.getElementById("lblUPLOAD_FILE").style.display='inline';
		document.getElementById("hidUPLOAD_FILE").value="Y";
		document.getElementById("hidStarting_bal").value = "";
		ChangeColor();
	}
	function populateXML()
	{
		var tempXML = document.getElementById('hidOldData').value;
		if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
		{
			if(tempXML!="")
			{
			
				
				populateFormData(tempXML,ACT_BANK_RECONCILIATION);
				//alert(tempXML)
				var fileName1 = document.getElementById("lblUPLOAD_FILE").innerText;
				var RootPath = document.getElementById('hidRootPath').value;
				//alert(fileName1);
				if(fileName1 != "")
				{
					document.getElementById("txtUPLOAD_FILE").style.display='none';
					document.getElementById("hidUPLOAD_FILE").value="N";
				}
				else
				{
					document.getElementById("hidUPLOAD_FILE").value="Y";
					//Added on 11 oct 2007
					if(document.getElementById('btnImport')!=null)
					document.getElementById('btnImport').setAttribute('disabled',true);
				}
					
				document.getElementById("lblUPLOAD_FILE").style.display='inline';
				//document.getElementById("lblUPLOAD_FILE").innerHTML ="<a href = '" + RootPath +  "/" + fileName1 + "' target='blank'>" + fileName1 + "</a>";
				document.getElementById("lblUPLOAD_FILE").innerHTML ="<a href = '" + document.getElementById("hidfileLink").value+ "' target='blank'>" + fileName1 + "</a>";
				FormatAmount(document.getElementById('txtENDING_BALANCE'));
                FormatAmount(document.getElementById('txtSTARTING_BALANCE'));
				FormatAmount(document.getElementById('txtBANK_CHARGES_CREDITS'));
						
			}
			else
			{
				AddData();
			}
		
		}
		else if(document.getElementById('hidAC_RECONCILIATION_ID').value	==	'New' && document.getElementById('hidFormSaved').value == '3')
		{
			if(document.getElementById('btnDistribute')!=null)
				document.getElementById('btnDistribute').setAttribute('disabled',true);
			if(document.getElementById('btnCommit')!=null)
				document.getElementById('btnCommit').setAttribute('disabled',true);
			if(document.getElementById('btnItemsToReconcile')!=null)
				document.getElementById('btnItemsToReconcile').setAttribute('disabled',true);
			if(document.getElementById('btnImport')!=null)
				document.getElementById('btnImport').setAttribute('disabled',true);
			if(document.getElementById('btnReProcessItemsToReconcile')!=null)
				document.getElementById('btnReProcessItemsToReconcile').setAttribute('disabled',true);	
			document.getElementById("hidUPLOAD_FILE").value="Y";
		}

	}
	
	function OpenDetailReport(Called_From)
	{

		//alert(Called_From);
		var acID = document.getElementById('cmbACCOUNT_ID').options[document.getElementById('cmbACCOUNT_ID').selectedIndex].value;
		var url="BankReconDetails.aspx?RECON_ID="+document.getElementById('hidAC_RECONCILIATION_ID').value
				+"&ACCOUNT_ID="+acID + "&CALLED_FROM=" + Called_From;	
		ShowPopup(url,'Recon_Details',900,600);	
		return false;

	}
	function OpenItemsTobeReconcilied()
	{
		
		populateXML();
		
		var acID = document.getElementById('cmbACCOUNT_ID').options[document.getElementById('cmbACCOUNT_ID').selectedIndex].value;
		var url="ItemsToReconcile.aspx?AC_RECONCILIATION_ID="+document.getElementById('hidAC_RECONCILIATION_ID').value+"&ACCOUNT_ID="+acID;	
		ShowPopup(url,'ItemsToReconcile',900,600);	
		return false;
	}
	function OpenDistributeReconciliation()
	{
			populateXML();
			//var amount = parseFloat(document.getElementById('txtBANK_CHARGES_CREDITS').value);
			var amount = document.getElementById('txtBANK_CHARGES_CREDITS').value;
			var url="DistributeCashReceipt.aspx?GROUP_ID="+document.getElementById('hidAC_RECONCILIATION_ID').value+"&GROUP_TYPE=BRN&DISTRIBUTION_AMOUNT="+amount;	
			ShowPopup(url,'DistributeCheck',900,600);	
			return false;
	}

	function formReset()
	{
		var tempXML = document.getElementById('hidOldData').value;
		//NewDataset Added By RaghavFor Itrack Issue #5270
		if(tempXML!=""  && tempXML!="<NewDataSet />")
		{
			populateFormData(tempXML,ACT_BANK_RECONCILIATION);
			document.getElementById('txtSTARTING_BALANCE').value= formatCurrency(document.getElementById('txtSTARTING_BALANCE').value);			
			document.getElementById('txtENDING_BALANCE').value= formatCurrency(document.getElementById('txtENDING_BALANCE').value);
			document.getElementById('txtBANK_CHARGES_CREDITS').value= formatCurrency(document.getElementById('txtBANK_CHARGES_CREDITS').value);
		}
		else
		{

			AddData();
		}
		
			DisableValidators();
			ChangeColor();
			return false;
	}
	//Ajax Call
	function CallbackFun(AJAXREsponse)
		{
		
			if(AJAXREsponse != "")
			{
			
				var newval = document.getElementById("txtSTARTING_BALANCE").value;
				var tmp = AJAXREsponse;
				
				document.getElementById("txtSTARTING_BALANCE").value = AJAXREsponse;
				document.getElementById("hidStarting_bal").value = AJAXREsponse;
				
				document.getElementById('cmbACCOUNT_ID').disabled = false;
				
				if(AJAXREsponse.length>0)
				{
					document.getElementById('txtSTARTING_BALANCE').readOnly = true;					
					document.getElementById("txtSTARTING_BALANCE").value = AJAXREsponse;
				}
				else
				{
					document.getElementById('txtSTARTING_BALANCE').readOnly = false;
				}
					
				
				document.getElementById('hidFormSaved').value = "3";
							
			}	
			 if(AJAXREsponse.length == "0")
			 {
					document.getElementById('txtSTARTING_BALANCE').disabled = false;
					document.getElementById("txtSTARTING_BALANCE").value = "";
					document.getElementById("hidStarting_bal").value = "";
					//changes by uday on 19 Oct
					document.getElementById('txtSTARTING_BALANCE').readOnly = false;
			 }
		}
	
		function ChangeStartingBalance()
		{
			var ParamArray = new Array();
			obj1=new Parameter('ACC_ID',document.getElementById('cmbACCOUNT_ID').value);
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'START_BAL';
			_SendAJAXRequest(objRequest,Action,ParamArray,CallbackFun);
		}
		
		function DisableButtonOnImport()
		{
			DisableButton(document.getElementById('btnImport'));
		}	
		
		
        function confirmCommit()
		{
			var confirmAction = confirm("Do you really want to commit?");
			if(confirmAction)
				return true;
			else
				return false;
		}
		
		

		</script>
</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();">
		<FORM id="ACT_BANK_RECONCILIATION" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td colSpan="4">
									<table id="tblBody" width="100%" align="center" border="0" runat="server">
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capGL_NAME" runat="server">Accounting Entity - G/L</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtGL_NAME" style="BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none"
													runat="server" CssClass="midcolora" ReadOnly="True" size="30" maxlength="2"></asp:textbox></TD>
											<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ID" runat="server">Account</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbACCOUNT_ID" onfocus="SelectComboIndex('cmbACCOUNT_ID')" runat="server" AutoPostBack="false" onchange="ChangeStartingBalance();">
													<asp:ListItem Value="0">0</asp:ListItem>
												</asp:dropdownlist><BR>
												<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" ControlToValidate="cmbACCOUNT_ID" ErrorMessage="ACCOUNT_ID can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator></TD>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capSTATEMENT_DATE" runat="server">Statement Date</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtSTATEMENT_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkSTATEMENT_DATE" runat="server" CssClass="HotSpot">
													<asp:image id="imgSTATEMENT_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
												</asp:hyperlink><BR>
												<asp:requiredfieldvalidator id="rfvSTATEMENT_DATE" runat="server" ControlToValidate="txtSTATEMENT_DATE" ErrorMessage="STATEMENT_DATE can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSTATEMENT_DATE" runat="server" ControlToValidate="txtSTATEMENT_DATE" ErrorMessage="RegularExpressionValidator"
													Display="Dynamic"></asp:regularexpressionvalidator></TD>
											<TD class="midcolora" width="18%"><asp:label id="capSTARTING_BALANCE" runat="server">Starting Balance</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtSTARTING_BALANCE" onblur="this.value=formatCurrency(this.value);"
													runat="server" size="16" maxlength="9"></asp:textbox><BR>
												<asp:requiredfieldvalidator id="rfvSTARTING_BALANCE" runat="server" ControlToValidate="txtSTARTING_BALANCE"
													ErrorMessage="STARTING_BALANCE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSTARTING_BALANCE" runat="server" ControlToValidate="txtSTARTING_BALANCE"
													ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator></TD>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capENDING_BALANCE" runat="server">Ending Balance</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtENDING_BALANCE" onblur="this.value=formatCurrency(this.value);"
													runat="server" size="16" maxlength="9"></asp:textbox><BR>
												<asp:requiredfieldvalidator id="rfvENDING_BALANCE" runat="server" ControlToValidate="txtENDING_BALANCE" ErrorMessage="ENDING_BALANCE can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revENDING_BALANCE" runat="server" ControlToValidate="txtENDING_BALANCE" ErrorMessage="RegularExpressionValidator"
													Display="Dynamic"></asp:regularexpressionvalidator></TD>
											<TD class="midcolora" width="18%"><asp:label id="capBANK_CHARGES_CREDITS" runat="server">Total of Bank Charges & Credit</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtBANK_CHARGES_CREDITS" onblur="this.value=formatCurrency(this.value);"
													runat="server" size="16" maxlength="9"></asp:textbox><BR>
												<asp:regularexpressionvalidator id="revBANK_CHARGES_CREDITS" runat="server" ControlToValidate="txtBANK_CHARGES_CREDITS"
													ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator></TD>
										</tr>
										<tr>
											<td class="midcolora" width="18%"><asp:label id="capUPLOAD_FILE" Runat="server">Upload File</asp:label></td>
											<td class="midcolora" width="32%" colSpan="1"><input id="txtUPLOAD_FILE" type="file" runat="server">
												<asp:label id="lblUPLOAD_FILE" Runat="server"></asp:label>
												
											</td>
											<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnImport" runat="server" Text="Import File"></cmsb:cmsbutton></td>
										</tr>
										<tr>
											<td class="midcolora" colspan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
												<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" Text="Commit/Post"></cmsb:cmsbutton>
												<cmsb:cmsbutton class="clsButton" id="btnItemsToReconcile" runat="server" Text="Items Cleared"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDistribute" runat="server" Text="Distribute"></cmsb:cmsbutton>
												<cmsb:cmsbutton class="clsButton" id="btnReProcessItemsToReconcile" runat="server" Text="Update Items Cleared"></cmsb:cmsbutton>
											</td>
											<td class="midcolorr" colspan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
											</td>
										</tr>
										<tr>
											<td class="midcolora" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnRptItemsToRecon" runat="server" Text="Print Items to be Reconciled"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnRptOustandings" runat="server" Text="Print Outstanding Bank Transactions"></cmsb:cmsbutton>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidAC_RECONCILIATION_ID" type="hidden" name="hidAC_RECONCILIATION_ID" runat="server">
			<INPUT id="hidUPLOAD_FILE" type="hidden" name="hidUPLOAD_FILE" runat="server"> <INPUT id="hidRootPath" type="hidden" name="hidRootPath" runat="server">
			<INPUT id="hidREF_FILE_ID" type="hidden" name="hidREF_FILE_ID" runat="server">
			<INPUT id="hidStarting_bal" type="hidden" name="hidStarting_bal" runat="server">
			<INPUT id="hidfileLink" type="hidden" value="" name="hidfileLink" runat="server">
		</FORM>
		<script>
			
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAC_RECONCILIATION_ID').value);
			function RefreshPageWebGrid()
			{	
			   RefreshWebGrid(1,document.getElementById('hidAC_RECONCILIATION_ID').value);
			}
		</script>
	</BODY>
</HTML>
<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AddVendorInvoice.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddVendorInvoice" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_VENDOR_INVOICES</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealtrans(Duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		
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

DisableValidators();
document.getElementById('hidINVOICE_ID').value	=	'New';
if(document.getElementById('cmbVENDOR_ID')!=null)
	document.getElementById('cmbVENDOR_ID').focus();
document.getElementById('cmbVENDOR_ID').options.selectedIndex = -1;
document.getElementById('txtINVOICE_NUM').value  = '';
document.getElementById('txtREF_PO_NUM').value  = '';
document.getElementById('txtTRANSACTION_DATE').value  =  '<%=DateTime.Now.ToString("MM/dd/yyyy")%>';
document.getElementById('txtDUE_DATE').value  = '';
document.getElementById('txtINVOICE_AMOUNT').value  = '';
document.getElementById('txtNOTE').value  = '';
if(document.getElementById('btnDistribute'))
	document.getElementById('btnDistribute').setAttribute("disabled",true);
if(document.getElementById('btnCommitPost'))
	document.getElementById('btnCommitPost').setAttribute("disabled",true);
if(document.getElementById('btnDelete'))
	document.getElementById('btnDelete').setAttribute("disabled",true);
//if(document.getElementById('btnApprove'))	
//	document.getElementById('btnApprove').setAttribute("disabled",true);
ChangeColor();

}
function populateXML()
{
	//Added by Shikha #5534
	if(document.getElementById('btnCommitting')!=null)
		{
			document.getElementById('btnCommitting').style.display="none";
		}	
	//End of addition.
	DisableValidators();
	var tempXML = document.getElementById('hidOldData').value;
	//FormatAmount(document.getElementById('txtINVOICE_AMOUNT'));
	//alert(tempXML);
	//AddData();
	if(document.getElementById('hidFormSaved').value == '0' )
	{
		if(tempXML!="")
		{
			populateFormData(tempXML,ACT_VENDOR_INVOICES);
			FormatAmount(document.getElementById('txtINVOICE_AMOUNT'));
			
			
		}
		else
		{
			AddData();
		}
		
	}
	//
		var vendor_id = document.getElementById('hidVENDOR_ID').value; 
		var comb     =  document.getElementById('cmbVENDOR_ID');
		if (vendor_id=="" && comb.selectedIndex!=0 && comb.selectedIndex!=-1)
		{
				var VendorValue = new String(comb.options[comb.selectedIndex].value);
				var VendorAr = VendorValue.split('~');
				if(VendorAr.length!=0)
				vendor_id = VendorAr[0];
		}
		if (vendor_id !="")
		{
			for (i=0;i<comb.length;i++)
			{
				var VendorData = new String(comb.options[i].value);
				var VendorArray = VendorData.split('~');
				if(VendorArray.length==0)
					continue;
				VendorID = VendorArray[0];
				if (VendorID==vendor_id)
				{
				comb.selectedIndex=i;
				document.getElementById("hidVENDOR_DETAILS").value = comb.options[i].text;
				ShowVendorAddress('lblVENDORADDRESS','cmbVENDOR_ID')
				break;
				}
			}
		} 
//

return false;
}
function OpenDistributeWindow()
{
	var VendorName, InvoiceNumber;
	if(document.getElementById("cmbVENDOR_ID").selectedIndex!=null && document.getElementById("cmbVENDOR_ID").options[document.getElementById("cmbVENDOR_ID").selectedIndex].value!="")
		VendorName = document.getElementById("cmbVENDOR_ID").options[document.getElementById("cmbVENDOR_ID").selectedIndex].text;	
	//Added For Itrack Issue # 6677.
	VendorName = VendorName.replace("&","%26")
	var url="DistributeCashReceipt.aspx?GROUP_ID="+document.getElementById('hidINVOICE_ID').value+"&GROUP_TYPE=VEN&DISTRIBUTION_AMOUNT="+document.getElementById('txtINVOICE_AMOUNT').value + "&VENDOR_NAME=" + VendorName + "&INVOICE_NUMBER=" + escape(document.getElementById("txtINVOICE_NUM").value) + "&";
	ShowPopup(url,'DistributeVendorInvoice',960,400);	
	return false;
}		
function ShowPopup(url, winname, width, height) 
	{
		var MyURL = url;
		var MyWindowName = winname;
		var MyWidth = width;
		var MyHeight = height;
		var MyScrollBars = 'Yes';
		var MyResizable = 'Yes';
		var MyMenuBar = 'No';
		var MyToolBar = 'No';
		var MyStatusBar = 'No';

			if (document.all)
			    var xMax = screen.width, yMax = screen.height;
			else
			    if (document.layers)
			        var xMax = window.outerWidth, yMax = window.outerHeight;
			    else
			        var xMax = 640, yMax=480;

			var xOffset = (xMax - MyWidth)/2, yOffset = (yMax - MyHeight)/2;

			MyWin = window.open(MyURL,MyWindowName,'width=' + MyWidth + ',height=' + MyHeight + ',screenX= ' + xOffset + ',screenY=' + yOffset + ',top=' + yOffset + ',left=' + xOffset + ',scrollbars=' + MyScrollBars + ',resizable=' + MyResizable + ',menubar=' + MyMenuBar + ',toolbar=' + MyToolBar + ',status=' + MyStatusBar + '' );
			MyWin.focus();
							
	}	
		
		function ChkDueDate(objSource , objArgs)
		{
			var duedate = document.ACT_VENDOR_INVOICES.txtDUE_DATE.value;
			var tranDate = document.ACT_VENDOR_INVOICES.txtTRANSACTION_DATE.value;
			objArgs.IsValid = DateComparer(duedate,tranDate,jsaAppDtFormat);			
		}	
		// Shows Vendor Address in next column when Vendor is selected from dropdown
		function ShowVendorAddress(lblVendorAddress,cmbVendor)
		{
			combo = document.getElementById(cmbVendor);
			if(combo==null || combo.selectedIndex==-1 || combo.selectedIndex==0)
				return;
			var VendorData = new String(combo.options[combo.selectedIndex].value);
			var VendorArray = VendorData.split('~');
			if(VendorArray.length==0)
				return;
			VendorID = VendorArray[0];
			// Assign corresponding Vendor Address into the Label field
			document.getElementById(lblVendorAddress).innerText = VendorArray[2] + ' ' + VendorArray[3] + ' ' + VendorArray[4] + ' ' + VendorArray[5];
		}
	
		//Client validation function checking trans date
		function ChkFiscalDate(objSource , objArgs)
		{
			if(document.getElementById("revTRANSACTION_DATE").isvalid==true)
			{
				if (document.getElementById("cmbFISCAL_ID").selectedIndex >= 0)
				{
					var fdate = document.getElementById("cmbFISCAL_ID") .options[document.getElementById("cmbFISCAL_ID").selectedIndex].text;
					
					d1 = fdate.substring(fdate.indexOf("(") + 1, fdate.indexOf("(") + 11);
					d2 = fdate.substring(fdate.indexOf("-") + 1,fdate.indexOf("-") + 12);
					
					tranDate = document.getElementById(objSource.controltovalidate).value;
					
					d1 = new Date(d1);
					d2 = new Date(d2);
					tranDate = new Date(tranDate);
					
					d1 = Date.parse(d1);
					d2 = Date.parse(d2);
					tranDate = Date.parse(tranDate);
					
					if (tranDate < d1 || tranDate > d2)
						objArgs.IsValid = false;
					else
						objArgs.IsValid = true;
					
				}
			}
			else
				objArgs.IsValid = true;
		}
		function AddVendor()
		{		
			top.botframe.location.href = "/cms/cmsweb/Maintenance/VendorIndex.aspx?AddNew=1&";
			return false;
		}
		
		//Added for #5534.
		function HideShowCommit()
		{
			if(document.getElementById('btnCommitPost')!=null)
				document.getElementById('btnCommitPost').style.display="none";
				
			if(document.getElementById('btnCommitting')!=null)
				{
					document.getElementById('btnCommitting').style.display="inline";
					document.getElementById('btnCommitting').disabled="true";
				}
		}
		//End of addition.
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="ACT_VENDOR_INVOICES" method="post" runat="server">
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
												<tr>
													<TD class="midcolora" width="18%"><asp:label id="capVENDOR_ID" runat="server">Vendor Name</asp:label><span class="mandatory">*</span></TD>
													<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVENDOR_ID" onfocus="SelectComboIndex('cmbVENDOR_ID')" runat="server">
															<asp:ListItem Value='0'></asp:ListItem>
														</asp:dropdownlist>
														<BR>
														<asp:requiredfieldvalidator id="rfvVENDOR_ID" runat="server" ControlToValidate="cmbVENDOR_ID" ErrorMessage="VENDOR_ID can't be blank."
															Display="Dynamic"></asp:requiredfieldvalidator>
															<br><a href="javascript:return AddVendor();" onClick="javascript:return AddVendor();">Add Vendor</a>
															</TD>
													<TD class="midcolora" width="18%"><asp:label id="capVENDORADDRESS" runat="server">Vendor Address</asp:label></TD>
													<TD class="midcolora" width="32%"><asp:label id="lblVENDORADDRESS" runat="server"></asp:label></TD>
												</tr>
												<tr>
													<TD class="midcolora" width="18%"><asp:label id="capINVOICE_NUM" runat="server">Invoice #</asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox id="txtINVOICE_NUM" runat="server" size="20" maxlength="20"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revINVOICE_NUM" runat="server" ControlToValidate="txtINVOICE_NUM" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator></TD>
													<TD class='midcolora' width='18%'><asp:Label id="capFISCAL_ID" runat="server">General Ledger</asp:Label><span class="mandatory">*</span></TD>
													<TD class='midcolora' width='32%'><asp:DropDownList id='cmbFISCAL_ID' runat='server'></asp:DropDownList>
													<br><asp:RequiredFieldValidator ID="rfvFISCAL_ID" Runat="server" ControlToValidate="cmbFISCAL_ID"></asp:RequiredFieldValidator>
													</TD>
												</tr>
												<tr>
													<TD class="midcolora" width="18%"><asp:label id="capREF_PO_NUM" runat="server">Reference/PO #</asp:label></TD>
													<TD class="midcolora" width="32%"><asp:textbox id="txtREF_PO_NUM" runat="server" size="32" maxlength="25"></asp:textbox><BR>
														<asp:regularexpressionvalidator id="revREF_PO_NUM" runat="server" ControlToValidate="txtREF_PO_NUM" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator></TD>
													<TD class="midcolora" width="18%"><asp:label id="capTRANSACTION_DATE" runat="server">Transaction Date</asp:label><span class="mandatory">*</span></TD>
													<TD class="midcolora" width="32%"><asp:textbox id="txtTRANSACTION_DATE" runat="server" size="12" maxlength="10"></asp:textbox>
														<asp:hyperlink id="hlkTran_DATE" runat="server" CssClass="HotSpot">
															<asp:image id="imgTran_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
														</asp:hyperlink>
														<BR>
														<asp:requiredfieldvalidator id="rfvTRANSACTION_DATE" runat="server" ControlToValidate="txtTRANSACTION_DATE"
															ErrorMessage="TRANSACTION_DATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
														<asp:regularexpressionvalidator id="revTRANSACTION_DATE" runat="server" ControlToValidate="txtTRANSACTION_DATE"
															ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
														<asp:customvalidator id="csvTRANSACTION_DATE" runat="server" Display="Dynamic" ErrorMessage="Transaction Date should be between financial year dates."
															ControlToValidate="txtTRANSACTION_DATE" ClientValidationFunction="ChkFiscalDate"></asp:customvalidator></TD>
												</tr>
												<tr>
													<TD class="midcolora" width="18%"><asp:label id="capDUE_DATE" runat="server">Due Date</asp:label><span class="mandatory">*</span></TD>
													<TD class="midcolora" width="32%"><asp:textbox id="txtDUE_DATE" runat="server" size="12" maxlength="10"></asp:textbox>
														<asp:hyperlink id="hlkDue_DATE" runat="server" CssClass="HotSpot">
															<asp:image id="imgDue_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
														</asp:hyperlink>
														<BR>
														<asp:requiredfieldvalidator id="rfvDUE_DATE" runat="server" ControlToValidate="txtDUE_DATE" ErrorMessage="DUE_DATE can't be blank."
															Display="Dynamic"></asp:requiredfieldvalidator>
														<asp:regularexpressionvalidator id="revDUE_DATE" runat="server" ControlToValidate="txtDUE_DATE" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator>
														<asp:customvalidator id="csvDUE_DATE" Display="Dynamic" ControlToValidate="txtDUE_DATE" Runat="server"
															ClientValidationFunction="ChkDueDate"></asp:customvalidator>
													</TD>
													<TD class="midcolora" width="18%"><asp:label id="capINVOICE_AMOUNT" runat="server">Invoice amount</asp:label><span class="mandatory">*</span></TD>
													<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtINVOICE_AMOUNT" runat="server"
															size="16&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;" maxlength="9"></asp:textbox><BR>
														<asp:requiredfieldvalidator id="rfvINVOICE_AMOUNT" runat="server" ControlToValidate="txtINVOICE_AMOUNT" ErrorMessage="INVOICE_AMOUNT can't be blank."
															Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revINVOICE_AMOUNT" runat="server" ControlToValidate="txtINVOICE_AMOUNT" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator></TD>
												</tr>
												<tr>
													<TD class="midcolora" width="18%"><asp:label id="capNOTE" runat="server">Note</asp:label></TD>
													<TD class="midcolora" colSpan="2"><asp:textbox id="txtNOTE" runat="server" size="70" maxlength="70"></asp:textbox></TD>
													<TD class="midcolora" colSpan="1"></TD>
												</tr>
												<tr>
													<td class="midcolora" colSpan="3">
													<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
													<cmsb:cmsbutton class="clsButton" id="btnDistribute" runat="server" Text="Distribute"></cmsb:cmsbutton>&nbsp;
													<cmsb:cmsbutton class="clsButton" id="btnCommitPost" runat="server" Text="Commit/Post"></cmsb:cmsbutton>
													<cmsb:cmsbutton class="clsButton" id="btnCommitting" runat="server" Text="Committing/Posting"></cmsb:cmsbutton>
													&nbsp;<cmsb:cmsbutton class="clsButton" id="btnDelete" CausesValidation="false" runat="server" Text="Delete"></cmsb:cmsbutton>
													</td>
													<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
												</tr>
											</TABLE>
										</td>
									</tr>
									<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
									<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidINVOICE_ID" type="hidden" name="hidINVOICE_ID" runat="server">
									<INPUT id="hidVENDOR_ID" type="hidden" name="hidVENDOR_ID" runat="server"></TBODY>
									<INPUT id="hidVENDOR_DETAILS" type="hidden" name="hidVENDOR_DETAILS" runat="server"></TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
		<script>
		/*function RefreshGrid()
		{
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidINVOICE_ID').value);
		}*/
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidINVOICE_ID').value);
		</script>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</BODY>
</HTML>

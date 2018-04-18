<%@ Page language="c#" Codebehind="AddInlandMarineBottom.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.Homeowner.AddInlandMarineBottom" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<HTML>
  <HEAD>
		<title>POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS </title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js">
		</script>
		<script src="/cms/cmsweb/scripts/common.js">
		</script>
		<script src="/cms/cmsweb/scripts/form.js">
		</script>
		<script language='javascript'>
function AddData()
{
	
	//For auto incrementing Item Number
	if (document.getElementById('txtITEM_NUMBER').value == '')
	{
		document.getElementById('txtITEM_NUMBER').value  = document.getElementById('hidNEW_ITEM_NUMER_FOR_ADD').value; 
	}
		
	document.getElementById('hidITEM_DETAIL_ID').value	=	'NEW';		
	document.getElementById('txtITEM_DESCRIPTION').value  = '';
	document.getElementById('txtITEM_SERIAL_NUMBER').value  = '';
	document.getElementById('txtITEM_INSURING_VALUE').value  = '';
	document.getElementById('cmbITEM_APPRAISAL_BILL').options.selectedIndex = -1;
	document.getElementById('cmbITEM_PICTURE_ATTACHED').options.selectedIndex = -1;
	
	if (document.getElementById('btnActivateDeactivate'))
		document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		
	if (document.getElementById('btnDelete'))
		document.getElementById('btnDelete').setAttribute('disabled',true);
	
	ChangeColor();
	ApplyColor();
	DisableValidators();
}
function populateXML()
{
	if (document.getElementById('hidOldData').value != "" && document.getElementById('hidOldData').value != "<NewDataSet />")
	{
			//Enabling the activate deactivate button
		if (document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
		//Enabling the btnDelete button
		if (document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',false); 
		
		populateFormData(document.getElementById('hidOldData').value,POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS);
		document.getElementById('txtITEM_INSURING_VALUE').value =  formatAmount(document.getElementById('txtITEM_INSURING_VALUE').value).split('.')[0];
		//UpdateTopGridAmount();
	}
	else
		AddData();	
}
/*
function UpdateTopGridAmount()
{
	var UpdatedAmount="No Coverage&nbsp;&nbsp;";
	
	var tmpXML = document.getElementById('hidOldData').value
				
	if(tmpXML!=null && tmpXML!="")
	{
		var objXmlHandler = new XMLHandler();
		var tree = (objXmlHandler.quickParseXML(tmpXML).getElementsByTagName('NewDataSet')[0]);
		if(tree)
		{
			UpdatedAmount = tree.childNodes[0].childNodes[12].childNodes[0].text;
			UpdatedAmount = formatAmount(UpdatedAmount).split('.')[0];
			window.parent.parent.document.getElementById('Row_' + document.getElementById("hidITEM_ID").value).getElementsByTagName("td")[2].childNodes[0].innerHTML = UpdatedAmount + "&nbsp;&nbsp;";
		}
	}
}
*/
function ShowHideControlsBasedOnCategory()
{
	var CategoryID;
	var ItemID;
	
	//check for state based on that +- 28 to ItemID 
	ItemID = document.getElementById("hidITEM_ID").value;
	
	if (parseInt(ItemID) < 874)
	{
		ItemID = parseInt(ItemID) + parseInt(28);
	}
	
	if (ItemID == "874" || ItemID == "875" || ItemID == "881" || ItemID == "888" || ItemID == "889" || ItemID == "892" || ItemID == "899" || ItemID == "901")
	{
		CategoryID = "1";
	}
	else if (ItemID == "876" || ItemID == "880" || ItemID == "882" || ItemID == "883" || ItemID == "884" || ItemID == "886" || ItemID == "890" || ItemID == "891" || ItemID == "893" || ItemID == "894" || ItemID == "895" || ItemID == "896" || ItemID == "897"|| ItemID == "898" || ItemID == "900")	
	{
		CategoryID = "2";
	}
	else if (ItemID == "879"  || ItemID == "877" || ItemID == "878" ) //Moved ItemID 877 and 878 to CategoryID 3, Itrack 6488, 7-Oct-09, Charles.	
	{
		CategoryID = "3";
	}
	else if (ItemID == "885")
	{
		CategoryID = "4";
	}
	else if (ItemID == "887")	
	{
		CategoryID = "5";
	}
	else if (ItemID == "10027" || ItemID == "10028" || ItemID == "10029" || ItemID == "10030")//Added by Charles on 7-Oct-09 for Itrack 6488
	{
		CategoryID = "6";
	}
	else
	{
		CategoryID = "0";
	}
	
	switch(CategoryID)
	{
		case '1':
			document.getElementById("trITEM_PICTURE_ATTACHED").style.display="none";
			document.getElementById("trITEM_APPRAISAL_BILL").style.display="none";				
			break;
		
		case '2':
			document.getElementById("trITEM_PICTURE_ATTACHED").style.display="none";
			document.getElementById("trITEM_APPRAISAL_BILL").style.display="none";
			document.getElementById("trITEM_SERIAL_NUMBER").style.display="none";
			break;
			
		case '3':
			document.getElementById("trITEM_PICTURE_ATTACHED").style.display="none";				
			document.getElementById("trITEM_SERIAL_NUMBER").style.display="none";
			break;	 
		
		case '4':				
			document.getElementById("trITEM_SERIAL_NUMBER").style.display="none";
			break;	 
		
		case '5':
			document.getElementById("trITEM_PICTURE_ATTACHED").style.display="none";				
			document.getElementById("trITEM_SERIAL_NUMBER").style.display="none";
			document.getElementById("trITEM_APPRAISAL_BILL").style.display="none";//Added by Charles on 7-Oct-09 for Itrack 6488
			//document.getElementById("capITEM_APPRAISAL_BILL").innerText="Played for remuneration"; //Commented by Charles on 7-Oct-09 for Itrack 6488
			break;	 
		case '6': //Added by Charles on 7-Oct-09 for Itrack 6488
			document.getElementById("trITEM_PICTURE_ATTACHED").style.display="none";
			document.getElementById("trITEM_APPRAISAL_BILL").style.display="none";
			document.getElementById("spnITEM_SERIAL_NUMBER").style.display="inline";
			document.getElementById('rfvITEM_SERIAL_NUMBER').setAttribute('enabled',true);
			break; //Added till here	
		default:
			break;
	}
	
	
}//ShowHideControlsBasedOnCategory

function ChkTextAreaLength(source, arguments)
{
	var txtArea = arguments.Value;
	if(txtArea.length > 255 ) 
	{
		arguments.IsValid = false;
		return;   // invalid userName
	}
}
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();ShowHideControlsBasedOnCategory();'>
		<div id="bodyHeight" class="pageContent">
			<FORM id='POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS' method='post' runat='server'>
				<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
					<TR>
						<TD>
							<TABLE width='100%' border='0' align='center'>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4">
										<asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
									</td>
								</tr>
								<tr id="trBody" runat="server">
									<td>
										<table>
												<tr>
													<TD class='midcolora' width='18%'>
														<asp:Label id="capITEM_NUMBER" runat="server">Item #</asp:Label>
														<span class="mandatory">*</span>
													</TD>
													<TD class='midcolora' width='32%'>
														<asp:textbox id='txtITEM_NUMBER' runat='server' size='30' maxlength='10'></asp:textbox>
														<BR>
														<asp:requiredfieldvalidator id="rfvITEM_NUMBER" runat="server" ControlToValidate="txtITEM_NUMBER" ErrorMessage="ITEM_NUMBER can't be blank."
															Display="Dynamic"></asp:requiredfieldvalidator>
														<asp:regularexpressionvalidator id="revITEM_NUMBER" runat="server" ControlToValidate="txtITEM_NUMBER" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator>
													</TD>
													<TD class='midcolora' width='18%' rowspan="2" style="VERTICAL-ALIGN: middle">
														<asp:Label id="capITEM_DESCRIPTION" runat="server">Description(Max characters cannot exceed 255)</asp:Label>
														<span class="mandatory">*</span>
													</TD>
													<TD class='midcolora' width='32%' rowspan="2">
														<asp:textbox onkeypress="MaxLength(this,100)" id='txtITEM_DESCRIPTION' runat='server' TextMode="MultiLine"
															Rows="4" Columns="35"></asp:textbox>
														<BR>
														<asp:requiredfieldvalidator id="rfvITEM_DESCRIPTION" runat="server" ControlToValidate="txtITEM_DESCRIPTION"
														ErrorMessage="ITEM_DESCRIPTION can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
														<asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtITEM_DESCRIPTION"
														Runat="server" Display="Dynamic" ID="csvITEM_DESCRIPTION"></asp:CustomValidator>	
													</TD>
												</tr>
												<tr>
													<TD class='midcolora' width='18%'>
														<asp:Label id="capITEM_INSURING_VALUE" runat="server">Insuring Value </asp:Label>
														<span class="mandatory">*</span>
													</TD>
													<TD class='midcolora' width='32%'>
														<asp:textbox id='txtITEM_INSURING_VALUE' onblur="this.value=formatAmount(this.value).split('.')[0]"
															runat='server' size='30' maxlength='10' CssClass="INPUTCURRENCY"></asp:textbox>
														<BR>
														<asp:requiredfieldvalidator id="rfvITEM_INSURING_VALUE" runat="server" ControlToValidate="txtITEM_INSURING_VALUE"
															ErrorMessage="ITEM_INSURING_VALUE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>										
														<asp:RangeValidator ID="rngINSURING_VALUE" Runat="server" ControlToValidate="txtITEM_INSURING_VALUE"
															MinimumValue="0" MaximumValue="9999999999" ErrorMessage="Please enter numeric value." Display="Dynamic"
															Type="Currency"></asp:RangeValidator>
												   </TD>
												</tr>
												<tr id="trITEM_SERIAL_NUMBER">
													<TD class='midcolora' width='18%'>
														<asp:Label id="capITEM_SERIAL_NUMBER" runat="server">Serial #</asp:Label>
														<span class="mandatory" id="spnITEM_SERIAL_NUMBER" style="display:none">*</span><!-- Added by Charles on 7-Oct-09 for Itrack 6488 -->
													</TD>
													<TD class='midcolora' width='32%'>
														<asp:textbox id='txtITEM_SERIAL_NUMBER' runat='server' size='30' maxlength='30'></asp:textbox>
														<BR>
														<asp:RequiredFieldValidator id="rfvITEM_SERIAL_NUMBER" ControlToValidate="txtITEM_SERIAL_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Please enter Serial #" Enabled="false"></asp:RequiredFieldValidator><!-- Added by Charles on 7-Oct-09 for Itrack 6488 -->
														<%--<asp:regularexpressionvalidator id="revITEM_SERIAL_NUMBER" runat="server" ControlToValidate="txtITEM_SERIAL_NUMBER"
															ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>--%>
													</TD>
													<td class='midcolora' colspan='2'>&nbsp;</td>
												</tr>
												<tr id="trITEM_APPRAISAL_BILL">
													<TD class='midcolora' width='18%'>
														<asp:Label id="capITEM_APPRAISAL_BILL" runat="server">Appraisal/Bill of Sale Attached ? </asp:Label>
													</TD>
													<TD class='midcolora' width='32%'>
														<asp:DropDownList id='cmbITEM_APPRAISAL_BILL' OnFocus="SelectComboIndex('cmbITEM_APPRAISAL_BILL')"
															runat='server'>
															<asp:ListItem Value='0'>&nbsp;</asp:ListItem>
															<asp:ListItem Value='1'>No</asp:ListItem>
															<asp:ListItem Value='2'>Yes</asp:ListItem>
														</asp:DropDownList>
													</TD>
													<td class='midcolora' colspan='2'>&nbsp;</td>
												</tr>
												<tr id="trITEM_PICTURE_ATTACHED">
													<TD class='midcolora' width='18%'>
														<asp:Label id="capITEM_PICTURE_ATTACHED" runat="server">Picture Attached</asp:Label>
													</TD>
													<TD class='midcolora' width='32%'>
														<asp:DropDownList id='cmbITEM_PICTURE_ATTACHED' OnFocus="SelectComboIndex('cmbITEM_PICTURE_ATTACHED')"
															runat='server'>
															<asp:ListItem Value='0'>&nbsp;</asp:ListItem>
															<asp:ListItem Value='1'>No</asp:ListItem>
															<asp:ListItem Value='2'>Yes</asp:ListItem>
														</asp:DropDownList>
													</TD>
													<td class='midcolora' colspan='2'>&nbsp;</td>
												</tr>
												<tr>
													<td class='midcolora' colspan='2'>
														<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
														<cmsb:cmsbutton  class="clsButton" id='btnDelete' runat="server" Text='Delete'></cmsb:cmsbutton>
														<cmsb:cmsbutton  class="clsButton" id="btnActivateDeactivate" runat="server" text="Activate/Deactivate"></cmsb:cmsbutton>
												
													</td>
													<td class='midcolorr' colspan='2'>
														<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
													</td>
												</tr>
										</table>
									</td>
								</tr>
						 </TABLE>
						</TD>
					</TR>
				</TABLE>
				<INPUT id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server"> <INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidITEM_NUMBER" type="hidden" name="hidITEM_NUMBER" runat="server">
				<INPUT id="hidITEM_ID" type="hidden" name="hidITEM_ID" runat="server"> <INPUT id="hidITEM_DETAIL_ID" type="hidden" name="hidITEM_DETAIL_ID" runat="server">
				<INPUT id="hidNEW_ITEM_NUMER_FOR_ADD" type="hidden" name="hidNEW_ITEM_NUMER_FOR_ADD" runat="server">
			</FORM>
		</div>
		<script>
			ShowHideControlsBasedOnCategory();
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidITEM_DETAIL_ID').value, false);
		</script>
	</BODY>
</HTML>

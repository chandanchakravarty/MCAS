<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolMiscellaneousEquipmentValuesDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolMiscellaneousEquipmentValuesDetails" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Miscellaneous Equipment</title>
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
		function ResetPage()
		{
			DisableValidators();
			document.forms[0].reset();
			Init();
			return false;			
		}
		function FormatAll()
		{
			for(var i=1;i<=<%=TOTAL_ROWS%>;i++)
			{
				txtID = "txt" + '<%=ITEM_VALUE_TEXT.ToString()%>' + i;				
				if(document.getElementById(txtID)==null) continue;
				document.getElementById(txtID).value = formatCurrency(document.getElementById(txtID).value);
			}
			if (typeof(Page_ClientValidate) == 'function')
				 Page_ClientValidate(); 
		}
		
		function CalculateTotal()
		{
			FormatAll();
			var Sum = parseFloat(0);			
			for(var i=1;i<=<%=TOTAL_ROWS%>;i++)
			{
				txtID = "txt" + '<%=ITEM_VALUE_TEXT.ToString()%>' + i;				
				if(document.getElementById(txtID)==null) continue;
				if(document.getElementById(txtID).value=="0") 
					document.getElementById(txtID).value="";
				txtValue = new String(document.getElementById(txtID).value);				
				txtValue = replaceAll(txtValue,",","");
				if(txtValue!="" && !isNaN(txtValue))
				{
					Sum+=parseFloat(txtValue);
				}
			}
			
			if(Sum!="" && !isNaN(Sum))
				document.getElementById("txtTOTAL_VALUE").value = formatCurrency(Sum);
			else
				document.getElementById("txtTOTAL_VALUE").value = "";	
				
				
			return false;		
		}		
		function Init()
		{
			CalculateTotal();
			document.getElementById("txtITEM_DESCRIPTION_1").focus();
		}
		function CheckDescription(RowID)
		{
			txtValueID = "txt" + '<%=ITEM_VALUE_TEXT.ToString()%>' + RowID;				
			txtDescID = "txt" + '<%=ITEM_DESCRIPTION_TEXT.ToString()%>' + RowID;		
			spnValueID = "spn" + '<%=ITEM_VALUE_TEXT.ToString()%>' + RowID;				
			spnDescID = "spn" + '<%=ITEM_DESCRIPTION_TEXT.ToString()%>' + RowID;		
			if(document.getElementById(txtDescID).value!="")
			{
				if(document.getElementById(txtValueID).value!="" && document.getElementById(txtValueID).value!="0")
				{			
					document.getElementById(spnValueID).style.display="none";
					document.getElementById(spnDescID).style.display="none";									
				}
				else
				{	
					document.getElementById(spnValueID).style.display="inline";					
					return false;
				}
			} 
			else
			{
				if(document.getElementById(txtValueID).value!="" && document.getElementById(txtValueID).value!="0")
				{
					document.getElementById(spnDescID).style.display="inline";					
					return false;
				}
				else
				{
					document.getElementById(spnDescID).style.display="none";
					document.getElementById(spnValueID).style.display="none";					
				}
			}		
			CalculateTotal();
			return true;			
		}
		function CheckAllDescription()
		{
			var FinalResult=true;
			for(var count=1;count<=<%=TOTAL_ROWS%>;count++)
			{	
				//Done for Itrack Issue 6409 on 18 Sept 09
				//document.getElementById('btnsave').focus();
				ReturnVal = CheckDescription(count);				
				if(!ReturnVal)				
				{
					FinalResult = false;
					return (Page_IsValid && FinalResult);
				}
			}
			return (Page_IsValid && FinalResult);
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="POL_MISCELLANEOUS_EQUIPMENT_VALUES" method="post" runat="server">
			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr class="headereffectWebGrid">
									<td><asp:label id="capITEM_ID" Runat="server" Width="5%"></asp:label></td>
									<td colSpan="2" width="50%"><asp:label id="capITEM_DESCRIPTION" Runat="server"></asp:label></td>
									<td width="45%"><asp:label id="capITEM_VALUE" Runat="server"></asp:label></td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_1" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_1" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_1" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_1" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_1" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_1" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_1" ControlToValidate="txtITEM_VALUE_1" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_2" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_2" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_2" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_2" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_2" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_2" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_2" ControlToValidate="txtITEM_VALUE_2" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_3" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_3" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_3" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_3" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_3" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_3" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_3" ControlToValidate="txtITEM_VALUE_3" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_4" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_4" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_4" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_4" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_4" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_4" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_4" ControlToValidate="txtITEM_VALUE_4" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_5" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_5" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_5" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_5" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_5" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_5" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_5" ControlToValidate="txtITEM_VALUE_5" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_6" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_6" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_6" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_6" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_6" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_6" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_6" ControlToValidate="txtITEM_VALUE_6" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_7" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_7" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_7" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_7" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_7" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_7" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_7" ControlToValidate="txtITEM_VALUE_7" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_8" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_8" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_8" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_8" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_8" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_8" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_8" ControlToValidate="txtITEM_VALUE_8" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_9" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_9" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_9" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_9" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_9" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_9" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_9" ControlToValidate="txtITEM_VALUE_9" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<!--<td class="midcolora"><asp:label id="capITEM_ID_10" Runat="server"></asp:label></td>-->
									<td class="midcolora"><asp:Checkbox id="chkITEM_ID_10" Runat="server"></asp:Checkbox></td>
									<td class="midcolora" colSpan="2"><asp:textbox id="txtITEM_DESCRIPTION_10" Runat="server" size="80" MaxLength="150"></asp:textbox><br>
										<span id="spnITEM_DESCRIPTION_10" style="DISPLAY:none;COLOR:red">Please enter 
											description</span>
									</td>
									<td class="midcolora"><asp:textbox id="txtITEM_VALUE_10" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="8"></asp:textbox><br>
										<span id="spnITEM_VALUE_10" style="DISPLAY:none;COLOR:red">Please enter amount</span>
										<asp:regularexpressionvalidator id="revITEM_VALUE_10" ControlToValidate="txtITEM_VALUE_10" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<td class="midcolorr" colSpan="3"><b><asp:label id="capTOTAL_VALUE" Runat="server"></asp:label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
									<td class="midcolora"><asp:textbox id="txtTOTAL_VALUE" Runat="server" size="19" ReadOnly="True" CssClass="INPUTCURRENCY"
											MaxLength="8"></asp:textbox></td>
								</tr>
								<tr>
									<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
									</td>
								</tr>
								<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
								<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
								<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
								<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
								<INPUT id="hidVEHICLE_ID" type="hidden" value="0" name="hidVEHICLE_ID" runat="server">
								<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
								<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
								<%--Added by Sibin for Itrack Issue 5114 on 11 Dec 08--%>
								<INPUT id="hidITEM_ID_1" type="hidden" value="0" name="hidITEM_ID_1" runat="server">
								<INPUT id="hidITEM_ID_2" type="hidden" value="0" name="hidITEM_ID_2" runat="server">
								<INPUT id="hidITEM_ID_3" type="hidden" value="0" name="hidITEM_ID_3" runat="server">
								<INPUT id="hidITEM_ID_4" type="hidden" value="0" name="hidITEM_ID_4" runat="server">
								<INPUT id="hidITEM_ID_5" type="hidden" value="0" name="hidITEM_ID_5" runat="server">
								<INPUT id="hidITEM_ID_6" type="hidden" value="0" name="hidITEM_ID_6" runat="server">
								<INPUT id="hidITEM_ID_7" type="hidden" value="0" name="hidITEM_ID_7" runat="server">
								<INPUT id="hidITEM_ID_8" type="hidden" value="0" name="hidITEM_ID_8" runat="server">
								<INPUT id="hidITEM_ID_9" type="hidden" value="0" name="hidITEM_ID_9" runat="server">
								<INPUT id="hidITEM_ID_10" type="hidden" value="0" name="hidITEM_ID_10" runat="server">
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAPP_MVR_ID').value,true);
		</script>
	</BODY>
</HTML>

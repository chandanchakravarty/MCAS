<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="SpecialAcceptanceAmount.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.SpecialAcceptanceAmount" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SpecialAcceptanceAmount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/menubar.js"></script>
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		
	 
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<script language="javascript" type="text/javascript">
		   

			function AddData()
			{	
				ChangeColor();
				DisableValidators();
				document.getElementById('hid_SPECIAL_ACCEPTANCE_LIMIT_ID').value	=	'New';
				if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
				//document.getElementById('cmbLOB_ID').focus();
				document.getElementById('txtSPECIAL_ACCEPTANCE_LIMIT').value  = '';
			}
			
			//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{ 
		    
			
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				
				amt = ReplaceAll(amt,".","");
				amt = ReplaceAll(amt, ",", "");
				
				if (amt.length == 1)
					amt = "0" + amt + "0";
				if (amt.length == 2)
					amt ="0" + amt ;
	            
	            
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtReceiptAmount.value = InsertDecimal(amt);
				}
			}
		} 
		
			function populateXML()
			{ 
				//if(document.getElementById('hidFormSaved').value == '0')
				//{
					//var tempXML;
					//if(top.frames[1].strXML!="")
					if(document.getElementById('hidOldData').value!="")
					{
						//document.getElementById('btnReset').style.display='none';
						//tempXML=top.frames[1].strXML;
					     
						//Storing the XML in hidRowId hidden fields 
						//document.getElementById('hidOldData').value		=	 tempXML;
						FormatAmount(document.getElementById('txtSPECIAL_ACCEPTANCE_LIMIT'));
						//populateFormData(tempXML,MNT_REINSURANCE_CONTRACT);
					}
					else
					{
						AddData();
					}
				//}
				return false;
			}
			function Reset()
			{
				document.MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT.reset();
				DisableValidators();
				setTimeout('Focus()',500);	
				ChangeColor();
				return false;
			}
			
			function findMouseIn()
	{
		if(!top.topframe.main1.mousein)
		{
			top.topframe.main1.mousein = true;
		}
		setTimeout('findMouseIn()',5000);
	}
	function Focus()
		{
		document.getElementById('cmbLOB_ID').focus();
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload="populateXML();ApplyColor();ChangeColor();setTimeout('Focus()',500);">
		<form id="MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="center">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<td>
							<table id="tblBody" width="100%" align="center" border="0" runat="server">
								<TBODY>
									<tr>
										<td class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></td>
									</tr>
									
									<tr>
										
										<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">Product</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" Runat="server"></asp:dropdownlist><br>
											<%--<asp:requiredfieldvalidator id="rfvLOB_ID" ControlToValidate="cmbLOB_ID" ErrorMessage="Please Select LOB" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>--%>
												</TD>
										<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server">Effective Date</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" size="12" maxlength="10" ></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../../Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink><br>
											<%--<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" ErrorMessage="Effective Date can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>--%>
											<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATE"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
											</TD>
									</tr>
									<tr>
										<td class="midcolora" width="18%">
											<asp:label id="capSPECIAL_ACCEPTANCE_LIMIT" runat="server">Special Acceptance Limit</asp:label><span class="mandatory">*</span>
										</td>
										<td class="midcolora" width="32%" colSpan="3">
											<asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtSPECIAL_ACCEPTANCE_LIMIT" runat="server" maxlength="12" size="35"></asp:textbox>
											<br>
											<asp:RequiredFieldValidator id="rfvSPECIAL_ACCEPTANCE_LIMIT" Runat="server" ControlToValidate="txtSPECIAL_ACCEPTANCE_LIMIT"
												ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revSPECIAL_ACCEPTANCE_LIMIT" Display="Dynamic" runat="server" ErrorMessage="Please Enter Positive Numeric Value"
												ControlToValidate="txtSPECIAL_ACCEPTANCE_LIMIT"></asp:RegularExpressionValidator>
										</td><%--Please enter Special Acceptance Limit.--%>
									</tr>
									<tr>
										<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" CausesValidation="false" ></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
					<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
					<INPUT id="hidOldData" type="hidden" value="" name="hidOldData" runat="server">
					<INPUT id="hid_SPECIAL_ACCEPTANCE_LIMIT_ID" type="hidden" value="0" name="hid_SPECIAL_ACCEPTANCE_LIMIT_ID"
						runat="server">
				</TBODY>
			</table>
		</form>
		<script language="javascript">
					RefreshWebGrid(1,document.getElementById('hid_SPECIAL_ACCEPTANCE_LIMIT_ID').value, false);
	
		//RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hid_SPECIAL_ACCEPTANCE_LIMIT_ID').value);
		</script>
	</body>
</HTML>

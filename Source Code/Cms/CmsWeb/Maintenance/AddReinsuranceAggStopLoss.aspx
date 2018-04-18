<%@ Page language="c#" Codebehind="AddReinsuranceAggStopLoss.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsuranceAggStopLoss" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>AddReinsuranceAggStopLoss</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		function AddData()
		{
			DisableValidators();
			document.getElementById('cmbREINSURANCE_COMPANY').focus();
			document.getElementById('cmbREINSURANCE_COMPANY').options.selectedIndex = -1;
			document.getElementById('txtACCOUNT_NUMBER').value = '';
			document.getElementById('txtCONTRACT_NUMBER').value = '';
			document.getElementById('txtEFFECTIVE_DATE').value  = '';
			document.getElementById('txtEXPIRATION_DATE').value  = '';
			document.getElementById('txtCOVERAGE_CODE').value  = '';
			document.getElementById('txtCLASS_CODE').value  = '';
			document.getElementById('cmbPERIL').options.selectedIndex = -1;
			document.getElementById('cmbLINE_OF_BUSINESS').options.selectedIndex = -1;
			document.getElementById('txtSTATED_AMOUNT').value  = '';
			document.getElementById('txtSPECIFIED_LOSS_RATIO').value  = '';
		}
		
		function populateXML()
		{
			if(document.getElementById('hidOldData').value == '0')
			{
				AddData();
				ChangeColor();
			}
			return false;
		}
		
		function formatCurrencyOnLoad()
		{
			document.getElementById('txtSTATED_AMOUNT').value		= formatCurrency(document.getElementById('txtSTATED_AMOUNT').value);	
			return false;
		}
		
		function CompareExpDateWithEffDate(objSource , objArgs)
		{
			var effdate=document.MNT_REINSURANCE_AGGREGATE.txtEFFECTIVE_DATE.value;
			var expdate=document.MNT_REINSURANCE_AGGREGATE.txtEXPIRATION_DATE.value;
			if ( document.MNT_REINSURANCE_AGGREGATE.txtEFFECTIVE_DATE!=null && document.MNT_REINSURANCE_AGGREGATE.txtEXPIRATION_DATE !=null && expdate !=""  && effdate != "")
			{
				objArgs.IsValid = CompareTwoDate(expdate,effdate,jsaAppDtFormat);
			}
		}	

		function CompareEffDateWithExpDate(objSource , objArgs)
		{
			var effdate=document.MNT_REINSURANCE_AGGREGATE.txtEFFECTIVE_DATE.value;
			var expdate=document.MNT_REINSURANCE_AGGREGATE.txtEXPIRATION_DATE.value;
			if ( document.MNT_REINSURANCE_AGGREGATE.txtEFFECTIVE_DATE!=null && document.MNT_REINSURANCE_AGGREGATE.txtEXPIRATION_DATE !=null && expdate !=""  && effdate != "")
			{
				objArgs.IsValid = CompareTwoDate(expdate,effdate,jsaAppDtFormat);
			}
		}	
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			var saperator = '/';
			var firstDate, secDate;
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");
			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{			
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}
			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{				
				firstDate = DateFirst
				secDate = DateSec;
			}
			firstDate = new Date(firstDate);
			secDate = new Date(secDate);
			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);
			if(firstSpan > secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}	
		
		function Validate(objSource , objArgs)
		{	
			var comm = parseFloat(document.getElementById('txtSPECIFIED_LOSS_RATIO').value);
			if(comm < 0 || comm > 100)
			{
				document.getElementById('txtSPECIFIED_LOSS_RATIO').select();
				objArgs.IsValid=false;
			}
			else
				objArgs.IsValid=true;
		}		
		
		function ShowToolTip(show)
		{
			spDiv1.innerHTML = "";
			popCtrl = document.MNT_REINSURANCE_AGGREGATE.cmbPERIL;			
			if(show){
				var oWording = document.getElementById('cmbPERIL');
				if(oWording.selectedIndex>0){
					var point = fGetXY(popCtrl);					
					with (VicPopCal1.style) {
  					left = point.x;
					top  = point.y + popCtrl.offsetHeight;
					width = VicPopCal1.offsetWidth;
					height = VicPopCal1.offsetHeight;
					visibility = 'visible';
					}
				
					spDiv1.innerHTML = document.getElementById('cmbPERIL').item(document.getElementById('cmbPERIL').selectedIndex).text;
					divCover1.style.visibility	= 'visible';
					divCover1.style.width		= VicPopCal1.offsetWidth;
					divCover1.style.top			= VicPopCal1.offsetTop;
					divCover1.style.left		= VicPopCal1.offsetLeft;
					divCover1.style.height		= VicPopCal1.offsetHeight;
					window.setTimeout("ShowToolTip(false);",3000);
				}
			}
			else
			{
				VicPopCal1.style.visibility = 'Hidden';
				divCover1.style.visibility = 'Hidden';
				divCover1.style.width = '0px';
				divCover1.style.top = '0px';
				divCover1.style.left = '0px';
				divCover1.style.height = '0px';
			}
		}
		
		with (document) {
			write("<html>");
			write("<head>");		
			write("</head>");
			write("<Div id='divCover1' Style='visibility:Hidden;Position:Absolute;background-Color:Black;Height:0;width:0;Border:0px Solid black;z-index:0;Position:Absolute;overflow:hidden;'>");
			write("	<IFrame Scrolling='No' Width='500' Height='100px;' Style='Position:Absolute;' FrameBorder='1'></IFrame>");
			write("</Div>");
			write("<Div id='VicPopCal1' style='z-Index:10;POSITION:absolute;VISIBILITY:hidden;border:1px ridge;width:500;background-image: url(../images/tile1.gif)'>");
			write("<table bgcolor='infobackground' style='BORDER-RIGHT:black 1px solid;WIDTH:100%;BORDER-TOP:black 1px solid; BORDER-LEFT:black 1px solid; BORDER-BOTTOM:black 1px solid'>");
			write("<tr>");
			write("<td><span Style='FONT-SIZE:10px;Color:InfoText;FONT-FAMILY:Verdana' id='spDiv1'>");
			write("</span></td>");
			write("</tr>");
			write("</TABLE></Div>");
			write("</html>");
		}
	</script>
  </head>
  <body leftMargin='0' topMargin='0' onload="populateXML();ApplyColor();formatCurrencyOnLoad();">
		<form id="MNT_REINSURANCE_AGGREGATE" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			 <TBODY>
				<tr>
					<td>
						<table align='center' width="100%" border='0'>
						<TBODY>
							<tr>
								<td class="pageHeader" colSpan="4">Please note that all fields marked with * are mandatory</td>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capREINSURANCE_COMPANY" runat="server">Reinsurance Company</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
									<asp:dropdownlist id="cmbREINSURANCE_COMPANY" onfocus="SelectComboIndex('cmbREINSURANCE_COMPANY')"
										runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:RequiredFieldValidator ID="rfvREINSURANCE_COMPANY" ControlToValidate="cmbREINSURANCE_COMPANY" Runat="server"
										ErrorMessage="REINSURANCE_COMPANY can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
								</td>
								<td class="midcolora" width="18%">
									<asp:Label id="capACCOUNT_NUMBER" runat="server">Account Number</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
									<asp:textbox id="txtACCOUNT_NUMBER" runat="server" size="20" maxlength="20" ReadOnly="true"></asp:textbox><IMG id="imgAccountNumber" style="CURSOR: hand" src="../../cmsweb/images/selecticon.gif" runat="server">
									<INPUT id="hidACCOUNT_NUMBER_ID" type="hidden" value="0" name="hidACCOUNT_NUMBER_ID" runat="server"><BR>
									<asp:RequiredFieldValidator ID="rfvACCOUNT_NUMBER" ControlToValidate="txtACCOUNT_NUMBER" Runat="server" ErrorMessage="ACCOUNT_NUMBER can't be blank."
										Display="Dynamic"></asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capCONTRACT_NUMBER" runat="server">Contract Number</asp:Label>
								</td>
								<td class="midcolora" width="32%">
									<asp:textbox id="txtCONTRACT_NUMBER" runat="server" size="20" maxlength="20" ReadOnly="True"></asp:textbox><br>									
								</td>	
								<td class="midcolora" width="18%"><asp:label id="capCLASS_CODE" runat="server">Class Code</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtCLASS_CODE" runat="server" maxlength="20" size="20" ReadOnly="True"></asp:textbox><IMG id="imgClassCode" style="CURSOR: hand" src="../../cmsweb/images/selecticon.gif" runat="server">
									<INPUT id="hidCLASS_CODE" type="hidden" value="0" name="hidCLASS_CODE" runat="server"><BR>
									<asp:requiredfieldvalidator id="rfvCLASS_CODE" runat="server" Display="Dynamic" ErrorMessage="CLASS_CODE can't be blank."
										ControlToValidate="txtCLASS_CODE"></asp:requiredfieldvalidator>
								</td>														
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server">Effective Date</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" Display="Dynamic" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEffectiveDate" runat="server" CssClass="HotSpot">
										<asp:image id="imgEffectiveDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
											valign="middle"></asp:image>
									</asp:hyperlink><br>
									<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtEFFECTIVE_DATE"></asp:regularexpressionvalidator>
									<asp:customvalidator id="csvEFFECTIVE_DATE" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATE" Runat="server"
										ClientValidationFunction="CompareEffDateWithExpDate"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" Display="Dynamic" ErrorMessage="EFFECTIVE_DATE can't be blank."
										ControlToValidate="txtEFFECTIVE_DATE"></asp:requiredfieldvalidator>
								</td>
								<td class="midcolora" width="18%"><asp:label id="capEXPIRATION_DATE" runat="server">Expiration Date</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtEXPIRATION_DATE" runat="server" Display="Dynamic" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkExpirationDate" runat="server" CssClass="HotSpot">
									<asp:image id="imgExpirationDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
										valign="middle"></asp:image>
								</asp:hyperlink><br>
								<asp:regularexpressionvalidator id="revEXPIRATION_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
									ControlToValidate="txtEXPIRATION_DATE"></asp:regularexpressionvalidator><asp:customvalidator id="csvEXPIRATION_DATE" Display="Dynamic" ControlToValidate="txtEXPIRATION_DATE"
									Runat="server" ClientValidationFunction="CompareExpDateWithEffDate"></asp:customvalidator><asp:requiredfieldvalidator id="rfvEXPIRATION_DATE" runat="server" Display="Dynamic" ErrorMessage="EXPIRATION_DATE can't be blank."
									ControlToValidate="txtEXPIRATION_DATE"></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capLINE_OF_BUSINESS" runat="server">Line of Business</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbLINE_OF_BUSINESS" onfocus="SelectComboIndex('cmbLINE_OF_BUSINESS')" runat="server">
									<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvLINE_OF_BUSINESS" runat="server" Display="Dynamic" ErrorMessage="LINE_OF_BUSINESS can't be blank."
									ControlToValidate="cmbLINE_OF_BUSINESS"></asp:requiredfieldvalidator>
								</td>							
								<td class="midcolora" width="18%"><asp:label id="capCOVERAGE_CODE" runat="server">Coverage Code</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtCOVERAGE_CODE" runat="server" size="20" MaxLength="20" ReadOnly="true"></asp:textbox><IMG id="imgCoverageCode" style="CURSOR: hand" src="../../cmsweb/images/selecticon.gif" runat="server">
									<INPUT id="hidCOVERAGE_ID" type="hidden" value="0" name="hidCOVERAGE_ID" runat="server"><BR>
									<asp:requiredfieldvalidator id="rfvCOVERAGE_CODE" runat="server" Display="Dynamic" ErrorMessage="COVERAGE_CODE can't be blank."
										ControlToValidate="txtCOVERAGE_CODE"></asp:requiredfieldvalidator>
								</td>
							</tr>							
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capSTATED_AMOUNT" runat="server">Stated Amount</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
									<asp:textbox id="txtSTATED_AMOUNT" runat="server" CssClass="INPUTCURRENCY" size="20" maxlength="9" onblur="this.value=formatAmount(this.value);">
									</asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvSTATED_AMOUNT" ControlToValidate="txtSTATED_AMOUNT" Runat="server" ErrorMessage="STATED_AMOUNT can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revSTATED_AMOUNT" Display="Dynamic" ControlToValidate="txtSTATED_AMOUNT" Runat="server"></asp:regularexpressionvalidator>
								</td>							
								<td class="midcolora" width="18%">
									<asp:Label id ="capSPECIFIED_LOSS_RATIO" runat="server">Specified Loss Ratio</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
									<asp:textbox id="txtSPECIFIED_LOSS_RATIO" runat="server" size="10" maxlength="5"></asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvSPECIFIED_LOSS_RATIO" ControlToValidate="txtSPECIFIED_LOSS_RATIO" Runat="server" ErrorMessage="SPECIFIED_LOSS_RATIO can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revSPECIFIED_LOSS_RATIO" Display="Dynamic" ControlToValidate="txtSPECIFIED_LOSS_RATIO" Runat="server"></asp:regularexpressionvalidator>
									<asp:CustomValidator ID="csvSPECIFIED_LOSS_RATIO" Runat=server ControlToValidate="txtSPECIFIED_LOSS_RATIO" ClientValidationFunction="Validate" Display=Dynamic ></asp:CustomValidator>
								</td>
							</tr>
							<tr>				
								<td class="midcolora" width="18%"><asp:label id="capPERIL" runat="server">Peril</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbPERIL" onfocus="SelectComboIndex('cmbPERIL')" Width="39%" runat="server" onChange="ShowToolTip(true);" onMouseOver="ShowToolTip(true);" onMouseOut="ShowToolTip(false);">
									<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvPERIL" runat="server" Display="Dynamic" ErrorMessage="PERIL can't be blank."
									ControlToValidate="cmbPERIL"></asp:requiredfieldvalidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" colspan="1">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="false"></cmsb:cmsbutton></td>
								<td class="midcolorr" colspan="3">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>									
								</td>
							</tr>									
						</tbody>
						</table>
					</td>
				</tr>   				
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
				<INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">				
				<INPUT id="hidAGGREGATE_ID" type="hidden" value="0" name="hidAGGREGATE_ID" runat="server">
			</tbody>
		</table>	
	</FORM>
</BODY>
</html>

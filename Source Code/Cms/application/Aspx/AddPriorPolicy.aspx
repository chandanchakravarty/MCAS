<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" AutoEventWireup="false" CodeBehind  = "AddPriorPolicy.aspx.cs" Inherits="Cms.Application.Aspx.AddPriorPolicy" validateRequest = "false" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>APP_PRIOR_CARRIER_INFO</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			document.getElementById('hidAPP_PRIOR_CARRIER_INFO_ID').value	=	'New';
			document.getElementById('txtOLD_POLICY_NUMBER').value  = '';
			document.getElementById('txtCARRIER').value  = '';
			
			//if (document.getElementById('cmbLOB').options.count > 0)
				document.getElementById('cmbLOB').options.selectedIndex = 0;
				document.getElementById('cmbASSIGNEDRISKYN').options.selectedIndex = 0;
				
			
			//if (document.getElementById('cmbSUB_LOB_NAME').options.count > 0)
				//document.getElementById('cmbSUB_LOB_NAME').options.selectedIndex = 0;
				
			document.getElementById('txtEFF_DATE').value  = '';
			document.getElementById('txtEXP_DATE').value  = '';
			
			//if (document.getElementById('cmbPOLICY_CATEGORY').options.count > 0)
				document.getElementById('cmbPOLICY_CATEGORY').options.selectedIndex = 0;
			
			//if (document.getElementById('cmbPOLICY_TERM_CODE').options.count > 0)
				document.getElementById('cmbPOLICY_TERM_CODE').options.selectedIndex = 0;
			
			document.getElementById('txtPOLICY_TYPE').value  = '';
			document.getElementById('txtYEARS_PRIOR_COMP').value  = '';
			document.getElementById('txtACTUAL_PREM').value  = '';
			
			//if (document.getElementById('cmbPRIOR_PRODUCER_INFO_ID').options.count > 0)
				document.getElementById('cmbPRIOR_PRODUCER_INFO_ID').options.selectedIndex = 0;
			
			document.getElementById('chkRISK_NEW_AGENCY').checked = false;
			document.getElementById('txtMOD_FACTOR').value  = '';
			document.getElementById('txtANNUAL_PREM').value  = '';
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',true);
			//Added for Itrack issue 6449 on 23 Oct 09
			document.getElementById('txtPRIOR_BI_CSL_LIMIT').value = '';
			document.getElementById('txtPRIOR_BI_CSL_LIMIT').setAttribute('readOnly',true);
			//document.getElementById('txtOLD_POLICY_NUMBER').focus();
			FillSubLOB();
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML;
				
				if(document.getElementById('hidOldData').value !="")
				{
					
					//document.getElementById('btnReset').style.display='none';
					tempXML = document.getElementById('hidOldData').value;
					//Enabling the activate deactivate button
					if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').setAttribute('disabled',false); 
					
					//Populating the controls with values from xml
					populateFormData(tempXML,APP_PRIOR_CARRIER_INFO);
				}
				else
				{
					AddData();
				}
			}
			
			var subLobId = document.getElementById("hidSUB_LOB").value;
			FillSubLOB();
			//SelectComboOption("cmbSUB_LOB_NAME",subLobId);
			//SetSubLBId();
			return false;
		}
		
		//Added by Mohit Agarwal 14-Aug-2008
		function SetExpDateValidator(LOBId)
		{
			if(LOBId == 2) //Automobile
			{
			//red star visible
				document.getElementById("spnEXP_DATE").style.visibility="visible";
			//req fld validator enable
				document.getElementById('rfvEXP_DATE').setAttribute('enabled',true); 
				//document.getElementById('rfvEXP_DATE').style.display = 'inline';
			
			//text len>0 txt box back color =white else color = yellow
			if (document.getElementById("txtEXP_DATE").value=="")
				document.getElementById("txtEXP_DATE").style.backgroundColor="#FFFFD1";
			else
				document.getElementById("txtEXP_DATE").style.backgroundColor="white";
			
			}
			else
			{
			//red star visible=false
				document.getElementById("spnEXP_DATE").style.visibility="hidden";
			//req fld validator disable
				document.getElementById('rfvEXP_DATE').setAttribute('enabled',false); 
				document.getElementById('rfvEXP_DATE').style.display = 'none';
			//text box back color =white
				document.getElementById("txtEXP_DATE").style.backgroundColor="white";
			
			}
			
		}

		function expDateChange()
		{
			var LOBId = document.getElementById('cmbLOB').options[document.getElementById('cmbLOB').selectedIndex].value;
			
			//Added by Mohit Agarwal 14-Aug-2008
			//SetExpDateValidator(LOBId);
		}
		
		//This function populates the sub lob id combo on selectection of lobid
		function FillSubLOB()
		{
			//document.getElementById('cmbSUB_LOB_NAME').innerHTML = '';
			var Xml = document.getElementById('hidLOBXML').value;
			var LOBId = document.getElementById('cmbLOB').options[document.getElementById('cmbLOB').selectedIndex].value;
			
			//Added by Mohit Agarwal 14-Aug-2008
			//SetExpDateValidator(LOBId);
			
			//LOB id is not selected then returning 
			if(document.getElementById('cmbLOB').selectedIndex == -1)
			{
				document.getElementById('hidLOBId').value = '';
				return false;
			}
			
			//Inserting the lobid in hidden control
			document.getElementById('hidLOBId').value = document.getElementById('cmbLOB').options[document.getElementById('cmbLOB').selectedIndex].value;
			
			var objXmlHandler = new XMLHandler();
			var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
			
			//Adding the empty item
			oOption = document.createElement("option");
			oOption.value = "";
			oOption.text = "";
		//	document.getElementById('cmbSUB_LOB_NAME').add(oOption);
								
			for(i=0; i<tree.childNodes.length; i++)
			{
				nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
				if (nodValue != null)
				{
					if (nodValue[0].firstChild == null)
						continue
						
					if (nodValue[0].firstChild.text == LOBId)
					{
						
						SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
						SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
						
						if (SubLobId != null && SubLobDesc != null)
						{
							if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
								&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
							{
								oOption = document.createElement("option");
								oOption.value = SubLobId[0].firstChild.text;
								oOption.text = SubLobDesc[0].firstChild.text;
								//document.getElementById('cmbSUB_LOB_NAME').add(oOption);
							}
						}
					}
				}
			}
			//SetSubLBId();
		}
		
		//This function Set the selected sub lob id in hidden control
		/*
		function SetSubLBId()
		{
			//Sub LOB id is not selected then returning 
			if(document.getElementById('cmbSUB_LOB_NAME').selectedIndex == -1)
			{
				document.getElementById('hidSUB_LOB').value = '';
				return false;
			}
			
			//Inserting the lobid in hidden control
			document.getElementById('hidSUB_LOB').value = document.getElementById('cmbSUB_LOB_NAME').options[document.getElementById('cmbSUB_LOB_NAME').selectedIndex].value;
		}*/
	function ChkDate(objSource , objArgs)
	{
		var effdate=document.APP_PRIOR_CARRIER_INFO.txtEFF_DATE.value;
		var expdate=document.APP_PRIOR_CARRIER_INFO.txtEXP_DATE.value;
		if(effdate != '')
			objArgs.IsValid = DateComparer(expdate, effdate, jsaAppDtFormat);
	}
	function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbLOB":
						lookupMessage	=	"LOBCD.";
						break;
					case "cmbPOLICY_CATEGORY":
						lookupMessage	=	"%POLGRY.";
						break;
					case "cmbPOLICY_TERM_CODE":
						lookupMessage	=	"PHIND.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
		//Added for Itrack issue 6449 on 23 Oct 09	
		function showPRIOR_BI_CSL_LIMIT()
		{
			if(document.getElementById('cmbPOLICY_CATEGORY').selectedIndex == 1)
			{
				document.getElementById('trPRIOR_BI_CSL_LIMIT').style.display = 'inline';
				document.getElementById('rfvPRIOR_BI_CSL_LIMIT').setAttribute('isValid',true);
				document.getElementById('rfvPRIOR_BI_CSL_LIMIT').setAttribute('enabled',true);
				document.getElementById('txtPRIOR_BI_CSL_LIMIT').setAttribute('readOnly',true);
			}
			else
			{
				document.getElementById('txtPRIOR_BI_CSL_LIMIT').setAttribute('readOnly',true);
				document.getElementById('rfvPRIOR_BI_CSL_LIMIT').setAttribute('isValid',false);
				document.getElementById('rfvPRIOR_BI_CSL_LIMIT').setAttribute('enabled',false);
				document.getElementById('rfvPRIOR_BI_CSL_LIMIT').style.display = 'none';
				document.getElementById('trPRIOR_BI_CSL_LIMIT').style.display = 'none';
			}
		}
		</script>
</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();showPRIOR_BI_CSL_LIMIT();"><%--Added for Itrack issue 6449 on 23 Oct 09--%>
		<FORM id="APP_PRIOR_CARRIER_INFO" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:label ID="capMessage" runat="server"></asp:label></TD><%--Please note that all fields marked with * are mandatory--%>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4">
									<asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
								</td>
							</tr>
							<TR>
								<TD class="midcolora" width="33%"><asp:label id="capOLD_POLICY_NUMBER" runat="server">Policy</asp:label><span class="mandatory">*</span>
                                <br /><asp:textbox id="txtOLD_POLICY_NUMBER" runat="server" maxlength="70" size="35"></asp:textbox><br>
								<asp:RequiredFieldValidator ID="rfvOLD_POLICY_NUMBER" Runat="server" ControlToValidate="txtOLD_POLICY_NUMBER" Display="Dynamic"></asp:RequiredFieldValidator>
								</TD>
                                <TD class="midcolora" width="33%"><asp:label id="capCARRIER" runat="server">Carrier</asp:label><span class="mandatory">*</span>
                                <br /><asp:textbox id="txtCARRIER" runat="server" maxlength="100" size="35"></asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvCARRIER" Runat="server" ControlToValidate="txtCARRIER" Display="Dynamic"></asp:RequiredFieldValidator></TD>
                                <TD class="midcolora"width="33%">
                                    <asp:label id="capPOLICY_CATEGORY" runat="server">Category</asp:label><span class="mandatory">*</span>
                                    <br />
                                    <asp:dropdownlist id="cmbPOLICY_CATEGORY" OnChange="showPRIOR_BI_CSL_LIMIT();" runat="server"></asp:dropdownlist>
								    <a class="calcolora" href="javascript:showPageLookupLayer('cmbPOLICY_CATEGORY')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0" ></a>
								    <br><asp:RequiredFieldValidator ID="rfvPOLICY_CATEGORY" Runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_CATEGORY"></asp:RequiredFieldValidator>
								</TD>
                                
							</TR>
							<tr>								
                                <TD class="midcolora"width="33%"><asp:label id="capLOB" runat="server">Lob</asp:label><span class="mandatory">*</span>
                                <br /><asp:dropdownlist id="cmbLOB" runat="server"></asp:dropdownlist>
								<a class="calcolora" href="javascript:showPageLookupLayer('cmbLOB')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0" ></a>
								<br>
								<asp:RequiredFieldValidator id="rfvLOB" Runat="server" Display="Dynamic" ControlToValidate="cmbLOB"></asp:RequiredFieldValidator>
								</TD>
                                <TD class="midcolora"width="33%"><asp:label id="capRISK_NEW_AGENCY" runat="server">New</asp:label>								
                                <br /><asp:checkbox id="chkRISK_NEW_AGENCY" runat="server"></asp:checkbox></TD>


                                <td class="midcolora"width="33%"></td>
							</tr>
							<%--Added for Itrack issue 6449 on 23 Oct 09--%>
							<tr id="trPRIOR_BI_CSL_LIMIT">
								<TD class="midcolora" colspan="2"><asp:label id="capPRIOR_BI_CSL_LIMIT" runat="server">Prior BI/CSL Limit</asp:label><span class="mandatory">*</span>
                                <br /><asp:textbox id="txtPRIOR_BI_CSL_LIMIT" runat="server" maxlength="100" size="35"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" alt="" src="~/cmsweb/images/selecticon.gif" runat="server"><br>
								<asp:RequiredFieldValidator ID="rfvPRIOR_BI_CSL_LIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtPRIOR_BI_CSL_LIMIT"></asp:RequiredFieldValidator>	
								</TD>
							</tr>
							
							<tr>
								<TD class="midcolora"width="33%"><asp:label id="capEFF_DATE" runat="server">Date</asp:label>
                                <br />
									<asp:textbox id="txtEFF_DATE" runat="server" maxlength="10" width="70"></asp:textbox>
									<asp:hyperlink id="hlkEFF_DATE" runat="server" CssClass="HotSpot">
										<asp:image id="imgEFF_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink>
									<BR>
									<asp:regularexpressionvalidator id="revEFF_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtEFF_DATE"></asp:regularexpressionvalidator>
								</TD>
								<TD class="midcolora"width="33%"><asp:label id="capEXP_DATE" runat="server">Date</asp:label><span id="spnEXP_DATE" class="mandatory">*</span><br />
                                <asp:textbox id="txtEXP_DATE" OnBlur="expDateChange();" runat="server" maxlength="10" width="70"></asp:textbox>
									<asp:hyperlink id="hlkEXP_DATE" runat="server" CssClass="HotSpot">
										<asp:image id="imgEXP_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink>
									<BR>
									<asp:regularexpressionvalidator id="revEXP_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtEXP_DATE"></asp:regularexpressionvalidator>
									<asp:customvalidator id="csvEXP_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtEXP_DATE"
										ClientValidationFunction="ChkDate"></asp:customvalidator>
									<asp:RequiredFieldValidator ID="rfvEXP_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtEXP_DATE" ErrorMessage="Please enter Expiration Date."></asp:RequiredFieldValidator></TD>
                                    <td  class="midcolora"width="33%"></td>
							</tr>
							<tr>
								<TD class="midcolora"width="33%"><asp:label id="capPOLICY_TERM_CODE" runat="server">Term</asp:label>
                                <br /><asp:dropdownlist id="cmbPOLICY_TERM_CODE" runat="server"></asp:dropdownlist>
								<a class="calcolora" href="javascript:showPageLookupLayer('cmbPOLICY_TERM_CODE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0" ></a>
								</TD>
								<TD class="midcolora"width="33%"><asp:label id="capPOLICY_TYPE" runat="server">Type</asp:label>
								
								
                                <br /><asp:textbox id="txtPOLICY_TYPE" runat="server" maxlength="30" width="150"></asp:textbox><BR>
									<asp:requiredfieldvalidator Enabled="False" id="rfvPOLICY_TYPE" runat="server" Display="Dynamic" ErrorMessage="POLICY_TYPE can't be blank."
										ControlToValidate="txtPOLICY_TYPE"></asp:requiredfieldvalidator>
										</TD>
                                <TD class="midcolora"width="33%"><asp:label id="capYEARS_PRIOR_COMP" runat="server">Prior</asp:label>
                                <br /><asp:textbox id="txtYEARS_PRIOR_COMP" runat="server" maxlength="2" size="3"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revYEARS_PRIOR_COMP" runat="server" Display="Dynamic" ControlToValidate="txtYEARS_PRIOR_COMP"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>								
								<TD class="midcolora"width="33%"><asp:label id="capASSIGNEDRISKYN" runat="server">Assignedriskyn</asp:label>
                                <br />
									<asp:DropDownList ID="cmbASSIGNEDRISKYN" Runat="server">										
									</asp:DropDownList>
								</TD>
                                <TD class="midcolora"width="33%"><asp:label id="capACTUAL_PREM" runat="server">Prem</asp:label>
                                <br /><asp:textbox id="txtACTUAL_PREM" runat="server" CssClass="INPUTCURRENCY" maxlength="9" width="90"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revACTUAL_PREM" runat="server" Display="Dynamic" ControlToValidate="txtACTUAL_PREM"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora"width="33%"><asp:label id="capANNUAL_PREM" runat="server">Prem</asp:label>
                                <br /><asp:textbox id="txtANNUAL_PREM" runat="server" CssClass="INPUTCURRENCY" maxlength="10" width="100"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revANNUAL_PREM" runat="server" Display="Dynamic" ControlToValidate="txtANNUAL_PREM"></asp:regularexpressionvalidator></TD>
							</tr>
							
							<tr>
								<TD class="midcolora"width="33%"><asp:label id="capMOD_FACTOR" runat="server">Factor</asp:label>
                                <br /><asp:textbox id="txtMOD_FACTOR" runat="server" maxlength="10" width="75"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revMOD_FACTOR" Runat="server" ControlToValidate="txtMOD_FACTOR" Display="Dynamic"></asp:RegularExpressionValidator>
								</TD>
								<TD class="midcolora"width="33%"><asp:label id="capPRIOR_PRODUCER_INFO_ID" runat="server">Producer</asp:label>
                                <br /><asp:dropdownlist id="cmbPRIOR_PRODUCER_INFO_ID" runat="server"></asp:dropdownlist></TD>
                                <td class="midcolora"width="33%"></td>
							</tr>
							<tr>
								<td class="midcolora"width="33%"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" colspan="2">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidAPP_PRIOR_CARRIER_INFO_ID" type="hidden" value="0" name="hidAPP_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidAPP_ID" runat="server">
			<INPUT id="hidLOBXML" type="hidden" value="0" name="hidAPP_ID" runat="server"> <input id="hidLOBId" type="hidden" runat="server">
			<input id="hidSUB_LOB" type="hidden" runat="server" language="javascript" onbeforeeditfocus="return hidSUB_LOB_onbeforeeditfocus()">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		if (document.getElementById('hidFormSaved').value == "5")
		{
			document.getElementById('hidFormSaved').value = 1;
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAPP_PRIOR_CARRIER_INFO_ID').value,false);
			document.getElementById('hidFormSaved').value = 0;
		}
		else
		{
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAPP_PRIOR_CARRIER_INFO_ID').value,false);
		}
		</script>
	</BODY>
</HTML>

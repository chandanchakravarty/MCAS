<%@ Page language="c#" Codebehind="AddMinimumPremAmt.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.AddMinimumPremAmt" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Mimimum Premium Amount </title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript" type="text/javascript">
	function Validate(objSource , objArgs)
	{	
	  
		var comm = parseFloat(document.getElementById('txtPREMIUM_AMT').value);
		if(comm < 0 || comm > 1000)
		{
		//	alert("Commission percent must be between 0 - 100.000 range.");
			document.getElementById('txtPREMIUM_AMT').select();
			objArgs.IsValid=false;
		}
		else
			objArgs.IsValid=true;
	}
	function AddData()
	{
	DisableValidators();
	document.getElementById('hidROW_ID').value	=	'New';

		
	if("<%= Request.QueryString["ROW_ID"]%>"!="")
	{
		//document.getElementById('agencyRow').style.display="none";

        //Commented by Ruchika for 
        //Error: Can't move focus to the control because it is invisible, not enabled, or of a type that does not accept the focus.
        //while resolving TFS Bug #836
		//document.getElementById('cmbSTATE_ID').focus();
	}
	else
	//document.getElementById('cmbSTATE_ID').options.selectedIndex = -1;
	document.getElementById('cmbLOB_ID').options.selectedIndex = -1;
	document.getElementById('cmbSUB_LOB_ID').options.selectedIndex = -1;
	document.getElementById('txtEFFECTIVE_FROM_DATE').value  = '';
	document.getElementById('txtEFFECTIVE_TO_DATE').value  = '';
	document.getElementById('txtPREMIUM_AMT').value  = '';
	if(document.getElementById('btnActivateDeactivate'))
	document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
	ChangeColor();
	}
		
	function populateXML()
	{
	
	var tempXML = document.getElementById('hidOldData').value;
	if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
	{
	
		if(tempXML!="")
		{
		   
			populateFormData(tempXML,ACT_MINIMUM_PREM_CANCEL);
            FillSub_LOB();
            SelectComboOption("cmbSUB_LOB_ID",document.getElementById('hidSUB_LOB_ID').value)
//			for(i=0;i<document.getElementById('cmbSTATE_ID').options.length;i++)
//			{
//				if(document.getElementById('cmbSTATE_ID').options[i].value==document.getElementById('hidState').value)	
//					document.getElementById('cmbSTATE_ID').options[i].selected=true;
//			}				
		}
		else
		{
			AddData();
		}
	}
	else
	{
		
        FillSub_LOB();
        SelectComboOption("cmbSUB_LOB_ID",document.getElementById('hidSUB_LOB_ID').value)
	}
		document.getElementById('txtPREMIUM_AMT').value=formatBaseCurrencyAmount(document.getElementById('txtPREMIUM_AMT').value);	

//	SetPageViewForPolicyCredit();
	return false;
}
function FillSubLOB()
		{
				
//			var stID="";
//			document.getElementById('cmbSUB_LOB_ID').innerHTML = '';
//			var Xml = document.getElementById('hidLOBXML').value;
//			//alert(Xml)
//			//var LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
//			
//			//LOB id is not selected then returning 
//			if(document.getElementById('cmbLOB_ID').selectedIndex == -1)
//			{
//				document.getElementById('hidSUB_LOB_ID').value = '';
//				return false;
//			}
//			var LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
//			stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
//			//Inserting the lobid in hidden control
//			//document.getElementById('hidLOBId').value = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
//		
//			var objXmlHandler = new XMLHandler();
//			var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
//			
//			//adding a blank option
//			oOption = document.createElement("option");
//			oOption.value = "";
//			oOption.text = "";
//			document.getElementById('cmbSUB_LOB_ID').add(oOption);
//				
//			//adding a all option
//			oOption = document.createElement("option");
//			oOption.value = "0";
//			oOption.text = "All";
//			document.getElementById('cmbSUB_LOB_ID').add(oOption);
//			
//			for(i=0; i<tree.childNodes.length; i++)
//			{
//				nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
//				stateValue = tree.childNodes[i].getElementsByTagName('STATE_ID');
//				if (nodValue != null)
//				{
//					if (nodValue[0].firstChild == null)
//						continue;
//					
//					if (stateValue[0].firstChild == null)
//						continue;

//					//alert(stID);
//					//alert(document.getElementById('cmbSTATE_ID').selectedIndex);
//					if(stID!="0")
//					{						
//					if (nodValue[0].firstChild.text == LOBId && stateValue[0].firstChild.text==stID)
//					{
//						
//						SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
//						SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
//						
//						if (SubLobId != null && SubLobDesc != null)
//						{
//							if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
//								&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
//							{
//						
//								oOption = document.createElement("option");
//								oOption.value = SubLobId[0].firstChild.text;
//								oOption.text = SubLobDesc[0].firstChild.text;
//								document.getElementById('cmbSUB_LOB_ID').add(oOption);
//								////alert(oOption.value);
//								////alert(oOption.text);
//							}
//						}
//					}
//					}
//					else
//					{
//					
//						if (nodValue[0].firstChild.text == LOBId)
//						{
//						SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
//						SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
//						
//						if (SubLobId != null && SubLobDesc != null)
//						{
//							if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
//								&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
//							{
//								
//								oOption = document.createElement("option");
//								oOption.value = SubLobId[0].firstChild.text;
//								oOption.text = SubLobDesc[0].firstChild.text;
//								document.getElementById('cmbSUB_LOB_ID').add(oOption);
//								////alert(oOption.value);
//								////alert(oOption.text);
//							}
//						}
//						}
//					}
//				}
//			}			
//			document.getElementById('cmbSUB_LOB_ID').selectedIndex=-1;
//			//PopulateClassDropDown();
//			//validatorControlsfail();
			
		}


        //Added on 31-Jan-2012 for TFS Bug # 836
        function FillSub_LOB()
        {       
            var a = document.getElementById('cmbLOB_ID').value;       
            var result = AddMinimumPremAmt.AjaxGetSubLobByLob(a.toString());
            fillDTCombo(result.value, document.getElementById('cmbSUB_LOB_ID'), 'SUB_LOB_ID', 'SUB_LOB_DESC', 0);           
        }


        function fillDTCombo(objDT, combo,valID, txtDesc, tabIndex) {            
            combo.innerHTML = "";
            if (objDT != null) {

                for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

                    if (i == 0) {
                        oOption = document.createElement("option");
                        oOption.value = "";
                        oOption.text = "";
                        combo.add(oOption);
                    }
                    oOption = document.createElement("option");
                    oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
                    oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
                    combo.add(oOption);
                }
            }
        }
        //Added till here


		function setHidSubLob()
		{
			document.getElementById('hidSUB_LOB_ID').value = document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value;
		}
		//date comparision
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function ChkDate(objSource , objArgs)
		{
			var fromDate=document.getElementById('txtEFFECTIVE_FROM_DATE').value;				
			var toDate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = DateComparer(toDate,fromDate, jsaAppDtFormat);
		}	

        //Commented by Ruchika Chauhan for TFS # 836        
		function formReset()
		{
			document.location.href = document.location.href; 
			return false;
		}
		
		/*function validatorControlsfail()
		{
			var IndianaStateID = 14;
			if(document.getElementById("cmbSTATE_ID").options[document.getElementById("cmbSTATE_ID").selectedIndex].value == IndianaStateID)
			{
			document.getElementById("rfvSUB_LOB_ID").setAttribute('enabled',true);
			document.getElementById("spnSUB_LOB_ID").style.display="inline";			
			}
			else
			{
			document.getElementById("rfvSUB_LOB_ID").setAttribute('enabled',false);
			document.getElementById("spnSUB_LOB_ID").style.display="none";
			}			
		
		}*/
		
		function FormatAmount(txtAmount)
		{
						
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();FillSubLOB();ApplyColor();">
		<form id="ACT_MINIMUM_PREM_CANCEL" method="post" runat="server" name="ACT_MINIMUM_PREM_CANCEL">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:Label ID="capCOUNTRY" runat="server"></asp:Label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblCOUNTRY_NAME" runat="server"></asp:label></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSTATE_ID" runat="server">State</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" AutoPostBack="True">
										<asp:ListItem Value="0">0</asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" ControlToValidate="cmbSTATE_ID" ErrorMessage="STATE_ID can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">Line of Business</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server" onchange="FillSub_LOB();" OnBlur="FillSub_LOB();">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" ErrorMessage="LOB_ID can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" id="subLOBBlankCell" style="DISPLAY: none" width="18%" colSpan="2">
								<TD class="midcolora" id="subLOBCell" width="18%"><asp:label id="capSUB_LOB_ID" runat="server">Sub Line of Business</asp:label><SPAN class="mandatory" id="spnSUB_LOB_ID" runat="server">*</SPAN></TD>
								<TD class="midcolora" id="subLOBCell2" width="32%"><asp:dropdownlist id="cmbSUB_LOB_ID" onfocus="SelectComboIndex('cmbSUB_LOB_ID')" runat="server" onchange="setHidSubLob()">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvSUB_LOB_ID" runat="server" ControlToValidate="cmbSUB_LOB_ID" ErrorMessage="SUB_LOB_ID can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_FROM_DATE" runat="server">Effective From Date</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_FROM_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFROM_DATE" runat="server" CssClass="HotSpot">
										<asp:image id="Image1" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><br>
									<asp:requiredfieldvalidator id="rfvEFFECTIVE_FROM_DATE" runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
										ErrorMessage="EFFECTIVE_FROM_DATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_FROM_DATE" runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
										ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TO_DATE" runat="server">Effective To Date</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_TO_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkTO_DATE" runat="server" CssClass="HotSpot">
										<asp:image id="imgCHECK_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvEFFECTIVE_TO_DATE" runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
										ErrorMessage="EFFECTIVE_TO_DATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_TO_DATE" runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
										ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvCHECK_DATE" ControlToValidate="txtEFFECTIVE_TO_DATE" Display="Dynamic" Runat="server"
										ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capPREMIUM_AMT" runat="server">Minimum Premium Amount</asp:label><SPAN class="mandatory">*</SPAN>
								</td>
								<TD class="midcolora" width="32%"><asp:textbox class="InputCurrency" id="txtPREMIUM_AMT" runat="server" size="10" maxlength="5" style="Text-Align:Right"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvPREMIUM_AMT" runat="server" ControlToValidate="txtPREMIUM_AMT" ErrorMessage="PREMIUM_AMT can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revPREMIUM_AMT" runat="server" ControlToValidate="txtPREMIUM_AMT" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvPREMIUM_AMT" ControlToValidate="txtPREMIUM_AMT" Display="Dynamic" Runat="server"
										Enabled="False" ClientValidationFunction="Validate"></asp:customvalidator></TD>
								<td class="midcolora" width="18%" colSpan="2"></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" Visible="false"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
							<INPUT id="hidCOMM_ID" type="hidden" name="hidCOMM_ID" runat="server"> <INPUT id="hidROW_ID" type="hidden" name="hidROW_ID" runat="server">
							<INPUT id="hidCOUNTRY_ID" type="hidden" name="hidCOUNTRY_ID" runat="server"> <INPUT id="hidLOBXML" type="hidden" name="hidLOBXML" runat="server">
							<INPUT id="hidSUB_LOB_ID" type="hidden" name="hidSUB_LOB_ID" runat="server"> <INPUT id="hidState" type="hidden" value="3" name="hidState" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script>
		
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCOMM_ID').value,true);
		
		</script>
	</BODY>
</HTML>

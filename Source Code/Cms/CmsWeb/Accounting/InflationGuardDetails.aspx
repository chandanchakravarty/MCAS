<%@ Page language="c#" Codebehind="InflationGuardDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.InflationGuardDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Inflation Guard Details</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		var GlobalError=false;
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function ChkDate(objSource , objArgs)
		{
		var fromDate=document.getElementById('txtEFFECTIVE_DATE').value;				
			var toDate=document.getElementById('txtEXPIRY_DATE').value;
			objArgs.IsValid = DateComparer(toDate,fromDate, jsaAppDtFormat);
		}
		
		function ValidateFactor(objSource, objArgs)
		{
		var tranNum = document.getElementById('txtFACTOR').value;
		if(tranNum < "1")
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		}
		function AddData()
		{
			DisableValidators();
			document.getElementById('hidOldData').value	=	'';
			document.getElementById('cmbLOB_ID').options.selectedIndex = -1;
			document.getElementById('cmbSTATE_ID').options.selectedIndex = -1;
			document.getElementById('txtEFFECTIVE_DATE').value  = '';
			document.getElementById('txtEXPIRY_DATE').value  = '';
			document.getElementById('txtFACTOR').value  = '';
			
			document.getElementById('txtZIP_CODE').value = '';
			ChangeColor();
		}
		function EnableDisableZip(flag)
		{
			if(document.getElementById('cmbSTATE_ID').options.selectedIndex == -1 ||document.getElementById('cmbSTATE_ID').options.selectedIndex ==0 )
				{
					document.getElementById('txtZIP_CODE').readOnly=true;
				}
			else
			{
					document.getElementById('txtZIP_CODE').readOnly=false;					
					if(flag)
						ValidatorValidate(document.getElementById('csvZIP_CODE'));
			}
		}
				
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
				{
			if(document.getElementById('hidOldData').value!="")
						{
							populateFormData(document.getElementById('hidOldData').value,INFLATION_COST_FACTORS);
							if(document.getElementById("btnDelete"))
								document.getElementById("btnDelete").style.display = "inline";
						}  
					else
					{
						if(document.getElementById("btnDelete"))
							document.getElementById("btnDelete").style.display = "none";
						AddData();
					}
				}
			
			return false;
		}

		function formReset()
		{	
			document.INFLATION_COST_FACTORS.reset();	
			populateXML();
			DisableValidators();			
			ChangeColor();
			return false;
		}
		
		/*function ValidateZip(objSource, objArgs)
		{
		var boolval = ValidateZipForInflation(document.getElementById('txtZIP_CODE'));
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
			
			
		}*/
		
		/*function GetZipForState()
		{		    
			GlobalError=true;
			if(document.getElementById('txtZIP_CODE').value!="")
			{
				if(isNaN(document.getElementById('txtZIP_CODE').value))				
				return;				
				var intStateID = document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').options.selectedIndex].value;
				var strZipID = document.getElementById('txtZIP_CODE').value;	
				var co=myTSMain1.createCallOptions();
				co.funcName = "FetchZipForState";
				co.async = false;
				co.SOAPHeader= new Object();
				var oResult = myTSMain1.FetchZip.callService(co,intStateID,strZipID);
				handleResult(oResult);				
				if(GlobalError)
				{
					//document.getElementById('csvZIP_CODE').setAttribute('enabled',true);
					//document.getElementById('csvZIP_CODE').setAttribute('isValid',true);
					//document.getElementById('csvZIP_CODE').style.display = 'inline';
					return false;
				}
				else
				{
					//document.getElementById('csvZIP_CODE').setAttribute('enabled',false); 
			   		//document.getElementById('csvZIP_CODE').setAttribute('isValid',false);
					//document.getElementById('csvZIP_CODE').style.display = 'none';
			
					//if(window.event.srcElement.id == "btnSave")
						//document.forms[0].submit();
					return true;
				}
			}	
			return false;		
		}*/
	
		function handleResult(res) 
		{
		if(!res.error)
			{
			if (res.value!="" && res.value!=null ) 
				{
					GlobalError=false;
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}
		}

		function ChkResult(objSource , objArgs)
		{
		if(isNaN(document.getElementById('txtZIP_CODE').value))
			{
				document.getElementById('csvZIP_CODE').innerHTML = "Zip code has to be numeric";
				objArgs.IsValid = false;
				return;
			}
			if(parseInt(document.getElementById('txtZIP_CODE').value)<0)
			{
				document.getElementById('csvZIP_CODE').innerHTML = "Zip has to be + ve";
				objArgs.IsValid = false;
				return;
			}
			var boolval = ValidateZipForInflation(document.getElementById('txtZIP_CODE'));
			if(boolval == false)
			{
				objArgs.IsValid = false;
				document.getElementById('csvZIP_CODE').innerHTML = "Zip code must be numeric and of exactly 3 digits";
				
			}
			else
				objArgs.IsValid = true;
			/*if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetZipForState();
				if(objArgs.IsValid == false)
					document.getElementById('csvZIP_CODE').innerHTML = "The zip code does not belong to the state";				
			}*/
			return;
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
			document.getElementById("btnSave").click();
		}	
		function Init()
		{
			populateXML();
			if(document.getElementById('hidINFLATION_ID').value != '-1')
				EnableDisableZip(false);	
			ApplyColor();
			ChangeColor();
		}	
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="INFLATION_COST_FACTORS" method="post" runat="server">			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tbody id="tbDetail" runat="server">
									<tr>
										<TD class="midcolora" width="18%"><asp:Label ID="capCOUNTRY" runat="server"></asp:Label></TD>
										<TD class="midcolora" width="32%" colspan="3"><asp:label id="lblCOUNTRY_NAME" runat="server"></asp:label></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capSTATE_ID" runat="server">State</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" onChange="EnableDisableZip(true);">
												<asp:ListItem Value="14">Indiana</asp:ListItem>
												<asp:ListItem Value="22">Michigan</asp:ListItem>
											</asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" ControlToValidate="cmbSTATE_ID" ErrorMessage="STATE_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capZIP_CODE" runat="server">Zip Code</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtZIP_CODE" runat="server" size="5" maxlength="3" ></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvZIP_CODE" runat="server" ControlToValidate="txtZIP_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:customvalidator id="csvZIP_CODE" Runat="server"  ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtZIP_CODE"></asp:customvalidator>
											<%--<asp:customvalidator id="csvZIP_CODE" Runat="server" Display="Dynamic" ControlToValidate="txtZIP_CODE" ErrorMessage="Enter valid zip code"></asp:customvalidator>
										<asp:regularexpressionvalidator id="revZIP_CODE" runat="server" Display="Dynamic"
													ControlToValidate="txtZIP_CODE"></asp:regularexpressionvalidator>--%>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">Line of Business</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server">
												<asp:ListItem Value="0">Home & Rental</asp:ListItem>
												<asp:ListItem Value="1">Homeowners</asp:ListItem>
												<asp:ListItem Value="6">Rental</asp:ListItem>
											</asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID" ErrorMessage="LOB_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora" width="18%"><asp:label id="capFACTOR" runat="server">Factor</asp:label><SPAN class="mandatory">*</SPAN>
										</td>
										<TD class="midcolora" width="32%"><asp:textbox id="txtFACTOR" runat="server" size="10" maxlength="5"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvFACTOR" runat="server" ControlToValidate="txtFACTOR" ErrorMessage="FACTOR can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFACTOR" runat="server" ControlToValidate="txtFACTOR" ErrorMessage="RegularExpressionValidator"
												Display="Dynamic"></asp:regularexpressionvalidator>
												<asp:customvalidator id="csvFACTOR" Runat="server" ClientValidationFunction="ValidateFactor"
											ErrorMessage=" " Display="Dynamic" ControlToValidate="txtFACTOR"></asp:customvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server">Effective Date</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFROM_DATE" runat="server" CssClass="HotSpot">
												<asp:image id="Image1" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" ErrorMessage="EFFECTIVE_DATE can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" ErrorMessage="RegularExpressionValidator"
												Display="Dynamic"></asp:regularexpressionvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capEXPIRY_DATE" runat="server">Effective Date</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEXPIRY_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkEXPIRY_DATE" runat="server" CssClass="HotSpot">
												<asp:image id="Image2" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvEXPIRY_DATE" runat="server" ControlToValidate="txtEXPIRY_DATE" ErrorMessage="EXPIRY_DATE can't be blank."
												Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revEXPIRY_DATE" runat="server" ControlToValidate="txtEXPIRY_DATE" ErrorMessage="RegularExpressionValidator"
												Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:customvalidator id="csvEXPIRY_DATE" ControlToValidate="txtEXPIRY_DATE" Display="Dynamic" Runat="server"
												ClientValidationFunction="ChkDate" ErrorMessage="Expiry date can't be less than effective date"></asp:customvalidator>
										</TD>
									</tr>
									<tr>
										<td class="midcolora">
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
									</tr>
								</tbody>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"> <INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
			<INPUT id="hidZIP_CODE" type="hidden" name="hidZIP_CODE" runat="server">
			<INPUT id="hidINFLATION_ID" type="hidden" name="hidINFLATION_ID" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
					RefreshWebGrid(document.getElementById('hidFormSaved').value,"<%=primaryKeyValues%>", true);
					
		</script>
	</BODY>
</HTML>

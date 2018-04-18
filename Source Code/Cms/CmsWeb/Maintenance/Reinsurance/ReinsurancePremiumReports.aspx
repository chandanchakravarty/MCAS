<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="ReinsurancePremiumReports.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.ReinsurancePremiumReports" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReinsurancePremiumReports</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">			
		function findMouseIn()
			{
				if(!top.topframe.main1.mousein)
				{
					//createActiveMenu();
					top.topframe.main1.mousein = true;
				}
				setTimeout('findMouseIn()',5000);
			}
			function selectAll()
			{
				/*if(document.getElementById('chkSelectAll').checked == true)
				{
					for (var i=document.getElementById('cmbFROMTRANSACTION_TYPE').options.length-1;i>=0;i--)
					{
							document.getElementById('cmbFROMTRANSACTION_TYPE').options[i].selected = true;
					
					
					}
				}*/
				
				if(document.getElementById('chkSelectAll').checked == false)
				{
					document.getElementById('trTransaction').style.display = "inline";
				}
				else
				{
					document.getElementById('trTransaction').style.display = "none";
					for (var i=document.getElementById('cmbFROMTRANSACTION_TYPE').options.length-1;i>=0;i--)
					{
							document.getElementById('cmbFROMTRANSACTION_TYPE').options[i].selected = true;
					
					
					}
				}
			}
			
		// For multiselect box Transaction Type : Start
		
		function setTransactionType()
		{
			document.REIN_PREMIUM_REPORT.hidTransactionType.value = '';
			for (var i=0;i< document.getElementById('cmbTRANSACTION_TYPE').options.length;i++)
			{
				document.REIN_PREMIUM_REPORT.hidTransactionType.value = document.REIN_PREMIUM_REPORT.hidTransactionType.value + document.getElementById('cmbTRANSACTION_TYPE').options[i].value + ',';
			}
			Page_ClientValidate();						
			var returnVal = funcValidateTransactionType();
			return Page_IsValid && returnVal;
		}
		
		function setTransactionTypes(tranvalue)
		{
			for(s = document.getElementById('cmbFROMTRANSACTION_TYPE').length-1; s >=0;s--)
			{
				if(document.getElementById('cmbFROMTRANSACTION_TYPE').options[s].value == tranvalue)
				{	
					document.getElementById('cmbTRANSACTION_TYPE').options[document.getElementById('cmbTRANSACTION_TYPE').length-1].text = document.getElementById('cmbFROMTRANSACTION_TYPE').options[s].text;
					document.getElementById('cmbFROMTRANSACTION_TYPE').options[s]=null;
					break;
				}
			}	
		}
		function addTransactionType()
		{
		
			var Transactions = document.getElementById("hidTransactionType").value;
			var Transaction = Transactions.split(",");
			for(j = document.getElementById('cmbTRANSACTION_TYPE').length-1; j >=0;j--)
			{
				document.getElementById('cmbTRANSACTION_TYPE').options[j].value= null;
			}
			for(j = 0; j < Transaction.length-1 ;j++)
			{
				document.getElementById('cmbTRANSACTION_TYPE').options.length=document.getElementById('cmbTRANSACTION_TYPE').length+1;
				document.getElementById('cmbTRANSACTION_TYPE').options[document.getElementById('cmbTRANSACTION_TYPE').length-1].value=Transaction[j];
				setTransactionTypes(Transaction[j]);
			}
		}
		
		function selectTransactionType()
		{
			for (var i=document.getElementById('cmbFROMTRANSACTION_TYPE').options.length-1;i>=0;i--)
			{
					if (document.getElementById('cmbFROMTRANSACTION_TYPE').options[i].selected == true)
					{
						addOption(document.getElementById('cmbTRANSACTION_TYPE'), document.getElementById('cmbFROMTRANSACTION_TYPE').options[i].text, document.getElementById('cmbFROMTRANSACTION_TYPE').options[i].value);
						document.getElementById('cmbFROMTRANSACTION_TYPE').options[i] = null;
					}
		  	}
		  	setTransactionType();
		  	if(document.getElementById('chkSelectAll').checked == true)
				{
					document.getElementById('chkSelectAll').checked =false;
				}
			//chkCheckSelect();
			return false;
		  	
		}
		
		function deselectTransactionType()
		{
		  for (var i=document.getElementById('cmbTRANSACTION_TYPE').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbTRANSACTION_TYPE').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMTRANSACTION_TYPE'), document.getElementById('cmbTRANSACTION_TYPE').options[i].text, document.getElementById('cmbTRANSACTION_TYPE').options[i].value);
					document.getElementById('cmbTRANSACTION_TYPE').options[i] = null;
				}
				
		  	}
		  	//chkCheckDeSelect();	
		  	return false;			
		
		}
		
		function funcValidateTransactionType()
		{
			if(document.getElementById('cmbTRANSACTION_TYPE').options.length == 0)
			{
				document.getElementById('cmbTRANSACTION_TYPE').className = "MandatoryControl";
				document.getElementById("cmbTRANSACTION_TYPE").style.display="inline";
				document.getElementById("csvTRANSACTION_TYPE").style.display="inline";
				document.getElementById("csvTRANSACTION_TYPE").innerText = "Please select Transaction Type";
				return false;
			}
			else
			{
				document.getElementById('cmbTRANSACTION_TYPE').className = "none";
				document.getElementById("csvTRANSACTION_TYPE").style.display="none";
				document.getElementById("csvTRANSACTION_TYPE").setAttribute("enabled",false);					
				document.getElementById("csvTRANSACTION_TYPE").setAttribute("isValid",false);
				return true;
			}
		}
		
		function addOption(selectElement, text, value) 
		{ 
			var foundIndex = selectElement.options.length; 

			for (i = 0; i < selectElement.options.length; i++) 
			{ 
				if (selectElement.options[i].innerText.toLowerCase() > text.toLowerCase()) 
				{ 
					foundIndex = i; 
					break; 
				} 
			} 

			var oOption = new Option(text,value); 
			selectElement.options.add(oOption, foundIndex); 

		} 
		// For multiselect box Transaction Type: End
		
		function ShowHideInsurence()
		{
			if(document.getElementById("cmbTOTAL_INSURANCE").options[document.getElementById("cmbTOTAL_INSURANCE").selectedIndex].value!=1)
			{
				document.getElementById("capVALUE_FROM").style.display="none";
				document.getElementById("txtVALUE_FROM").style.display="none";
				document.getElementById("capVALUE_TO").style.display="none";
				document.getElementById("txtVALUE_TO").style.display="none";
				document.getElementById("spnVALUE_FROM").style.display="none";
				document.getElementById("spnVALUE_TO").style.display="none";
				EnableValidator('rfvVALUE_FROM',false);
				EnableValidator('rfvVALUE_TO',false);
			} 
			else
			{
				document.getElementById("capVALUE_FROM").style.display="inline";
				document.getElementById("txtVALUE_FROM").style.display="inline";
				document.getElementById("capVALUE_TO").style.display="inline";
				document.getElementById("txtVALUE_TO").style.display="inline";
				document.getElementById("spnVALUE_FROM").style.display="inline";
				document.getElementById("spnVALUE_TO").style.display="inline";
				EnableValidator('rfvVALUE_FROM',true);
				EnableValidator('rfvVALUE_TO',true);
			}
		}
		function DisplayReport()
		{
			if(document.getElementById('chkSelectAll').checked == true)
				{
				document.REIN_PREMIUM_REPORT.hidTransactionType.value = '-1';
				}
			//setTransactionType();
			window.open('ReinsuranceReports.aspx?SRTING_VALUE=<%=strValue %>','ReinsuranceReports',"width=1000,height=700,screenX=50,screenY=150,top=70,left=80,scrollbars=yes,resizable=no,menubar=no,toolbar=no,status=no","");
			//window['page1'].document.title = 'test';
			return false;
		}	
		function MandetoryMessage()
		{
			var contractno = document.getElementById('cmbCONTRACT_NUMBER').options[document.getElementById('cmbCONTRACT_NUMBER').selectedIndex].value;
			if(contractno=="")
			{
				alert('Please select Contact Number');
				return false;
			}
		}	
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();ShowHideInsurence();selectAll();">
	<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%">
		<form id="REIN_PREMIUM_REPORT" method="post" runat="server">
			<table width="100%" class = "tableWidth" align="center" border="0">
				<TBODY>
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					
					<tr>
						<td class="pageheader" align="center" colSpan="4">Please select Display/Print Button 
							to view Reinsurance Premium Report</td>
					</tr>
					
					<tr>
						<td class="headereffectcenter" colSpan="4">Reinsurance Premium Report</td>
					</tr>	
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<td>
							<table id="tblBody" width="100%" align="center" border="0" runat="server">
								<TBODY>
									<tr>
										<TD class="pageHeader" width="100%" colSpan="4">Please note that all fields marked 
											with * are mandatory
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCONTRACT_NUMBER" runat="server">Contract #</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCONTRACT_NUMBER" onfocus="SelectComboIndex('cmbCONTRACT_NUMBER')" runat="server" autopostback = "true"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvCONTRACT_NUMBER" Runat="server" Display="Dynamic" ErrorMessage="Please Select Contact number"
												ControlToValidate="cmbCONTRACT_NUMBER"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capCONTRACT_DATES" runat="server">Contract Dates</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCONTRACT_DATES" onfocus="SelectComboIndex('cmbCONTRACT_DATES')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvCONTRACT_DATES" Runat="server" Display="Dynamic" ErrorMessage="Please Select Contract dates"
												ControlToValidate="cmbCONTRACT_DATES"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capTYPE_REPORT" runat="server">Type of Reinsurance Report</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTYPE_REPORT" onfocus="SelectComboIndex('cmbTYPE_REPORT')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvTYPE_REPORT" Runat="server" Display="Dynamic" ErrorMessage="Please Select Contract number"
												ControlToValidate="cmbTYPE_REPORT"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capREPORT" runat="server">Report</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:dropdownlist id="cmbREPORT" onfocus="SelectComboIndex('cmbREPORT')" runat="server"></asp:dropdownlist>
											<br>
											<asp:requiredfieldvalidator id="rfvREPORT" ControlToValidate="cmbREPORT" ErrorMessage="Please Select Report"
												Display="Dynamic" Runat="server"></asp:requiredfieldvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capEND_MONTH" runat="server">Month Ending</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:dropdownlist id="cmbEND_MONTH" onfocus="SelectComboIndex('cmbEND_MONTH')" runat="server"></asp:dropdownlist>
											<br>
											<asp:requiredfieldvalidator id="rfvEND_MONTH" ControlToValidate="cmbEND_MONTH" ErrorMessage="Please Select Month Ending"
												Display="Dynamic" Runat="server"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:dropdownlist id="cmbYEAR" onfocus="SelectComboIndex('cmbYEAR')" runat="server"></asp:dropdownlist>
											<br>
											<asp:requiredfieldvalidator id="rfvYEAR" ControlToValidate="cmbYEAR" ErrorMessage="Please Select Year" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capMAJOR_PART" runat="server">Major Participant</asp:label></TD>
										<TD class="midcolora" width="32%">
											<asp:dropdownlist id="cmbMAJOR_PART" onfocus="SelectComboIndex('cmbMAJOR_PART')" runat="server"></asp:dropdownlist>
											<br>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capMAJOR_DESC" runat="server">Description</asp:label></TD>
										<TD class="midcolora" width="32%">
											<asp:textbox onkeypress="MaxLength(this,50);" id="txtMAJOR_DESC" runat="server" size="30" maxlength="255"
												Rows="5" Columns="40" TextMode="MultiLine"></asp:textbox><br>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capTRANSACTION_TYPE" runat="server">Transaction Type</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<td class="midcolora" width="18%" colspan = "3"><asp:checkbox id="chkSelectAll" runat="server" onClick="selectAll();" checked ="true"></asp:checkbox>Select 
											All<br></td>
									</tr>
									<tr id="trTransaction">
									<TD class="midcolora"></td>
										<TD class="midcolora">
											<asp:listbox id="cmbFROMTRANSACTION_TYPE" Runat="server" Height="79px" width="80px" AutoPostBack="False"
												SelectionMode="Multiple">
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btnSELECT_TRANSACTION_TYPE" Runat="server" Text=">>" CausesValidation="True"></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnDESELECT_TRANSACTION_TYPE" Runat="server" Text="<<" CausesValidation="False"></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capRECIPIENT" Runat="server" style="DISPLAY: none">Recipient</asp:label>
											<asp:listbox id="cmbTRANSACTION_TYPE" onblur="" Runat="server" Height="79px" Width="200px" AutoPostBack="False"
												SelectionMode="Multiple" onChange=""></asp:listbox><br>
											<asp:customvalidator id="csvTRANSACTION_TYPE" Display="Dynamic" ControlToValidate="cmbTRANSACTION_TYPE"
												Runat="server" ClientValidationFunction="funcValidateTransactionType" ErrorMessage="Please select Transaction Type."></asp:customvalidator>
											<span id="spnTRANSACTION_TYPE" style="DISPLAY: none; COLOR: red"></span>
										</td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capSPECIAL_ACCEP" runat="server">Special Acceptance Report Only</asp:label></TD>
										<TD class="midcolora" width="32%">
											<asp:dropdownlist id="cmbSPECIAL_ACCEP" onfocus="SelectComboIndex('cmbSPECIAL_ACCEP')" runat="server"></asp:dropdownlist>
											<br>
										</TD>
										<TD class="midcolora" width="32%" colspan = "2"></TD>
									</tr>
									<tr>
										<TD class="midcolora" rowspan="2" style="VERTICAL-ALIGN:middle"><asp:label id="capTOTAL_INSURANCE" runat="server">Total Insurance Value Report</asp:label></TD>
										<TD class="midcolora" rowspan="2" style="VERTICAL-ALIGN:middle">
											<asp:dropdownlist id="cmbTOTAL_INSURANCE" onfocus="SelectComboIndex('cmbTOTAL_INSURANCE')" runat="server" onchange ="ShowHideInsurence();"></asp:dropdownlist></TD>
										<TD class="midcolora" width="18%"><asp:label id="capVALUE_FROM" runat="server"></asp:label><SPAN id="spnVALUE_FROM"class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtVALUE_FROM"  runat="server" size="15" MaxLength="13" ></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvVALUE_FROM" ControlToValidate="txtVALUE_FROM" ErrorMessage="Please enter Total Insurance Value From" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator><BR>
										<asp:regularexpressionvalidator id="revVALUE_FROM" Display="Dynamic" ControlToValidate="txtVALUE_FROM"
												Runat="server"></asp:regularexpressionvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capVALUE_TO" runat="server"></asp:label><SPAN id="spnVALUE_TO" class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtVALUE_TO"  runat="server" size="15" MaxLength="13" ></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvVALUE_TO" ControlToValidate="txtVALUE_TO" ErrorMessage="Please enter Total Insurance Value To" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator><br>
										<asp:regularexpressionvalidator id="revVALUE_TO" Display="Dynamic" ControlToValidate="txtVALUE_TO"
												Runat="server"></asp:regularexpressionvalidator></TD>

									</tr>
									<tr>
										<td colSpan="4">
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<TD class="midcolora" width="12%"><asp:label id="capSORT_FIRST" runat="server">Sort Option 1st </asp:label></TD>
													<TD class="midcolora" width="21%"><asp:dropdownlist id="cmbSORT_FIRST" onfocus="SelectComboIndex('cmbSORT_FIRST')" runat="server"></asp:dropdownlist></TD>
													<TD class="midcolora" width="12%"><asp:label id="capSORT_SEC" runat="server">Sort Option 2nd </asp:label></TD>
													<TD class="midcolora" width="21%"><asp:dropdownlist id="cmbSORT_SEC" onfocus="SelectComboIndex('cmbSORT_SEC')" runat="server"></asp:dropdownlist></TD>
													<TD class="midcolora" width="12%"><asp:label id="capSORT_THIRD" runat="server">Sort Option 3rd </asp:label></TD>
													<TD class="midcolora" width="21%"><asp:dropdownlist id="cmbSORT_THIRD" onfocus="SelectComboIndex('cmbSORT_THIRD')" runat="server"></asp:dropdownlist></TD>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="midcolora" colSpan="4">
											<cmsb:cmsbutton class="clsButton" id="btnDisplay" runat="server" Text="Display/Print" CausesValidation="false" ></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnExport" runat="server" Text="Export to Excel" CausesValidation="false" ></cmsb:cmsbutton>
										</td>
									</tr>
								<tr>
							<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
						</tr>
						</table>
						
							
						</td>
					</TR>
					<INPUT id="hidTransactionType" type="hidden" value="0" name="hidTransactionType" runat="server">
				
			</table>
		</form>
		<script>
			addTransactionType();
		</script>
		</div>
	</body>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddJournalEntryMaster.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddJournalEntryMaster" ValidateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Add Journal Entry</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script>
	
	//This function activates the details tab
	function OnNextClick()
	{
		this.parent.parent.changeTab(0,1);
		return false;
	}

	function OnDeleteClick()
	{
		var retVal = confirm("Are you sure you want to delete the selected record ?");
		return retVal;
	}	
	
	var jsaAppDtFormat = "<%=aAppDtFormat  %>";
	
	
	//Client validation function checking trans date
	function ChkFiscalDate(objSource , objArgs)
	{
		var currdate = new Date();
		var newDate = (currdate.getMonth()+1)+'/'+currdate.getDate()+'/'+currdate.getFullYear();
		
		if(document.getElementById("revTRANS_DATE").isvalid==true)
		{
			if (document.getElementById("cmbFISCAL_ID").selectedIndex >= 0)
			{
				var fdate = document.getElementById("cmbFISCAL_ID") .options[document.getElementById("cmbFISCAL_ID").selectedIndex].text;
				
				d1 = fdate.substring(fdate.indexOf("(") + 1, fdate.indexOf("(") + 11);
				d2 = fdate.substring(fdate.indexOf("-") + 1,fdate.indexOf("-") + 12);
				
				tranDate = document.getElementById(objSource.controltovalidate).value;
				
				if(objSource.id == 'csvSTART_DATE' ||objSource.id == 'csvEND_DATE')
				{
					objArgs.IsValid = DateComparer(tranDate,newDate,jsaAppDtFormat);
					if(objArgs.IsValid == false)
					{
						if(objSource.id == 'csvSTART_DATE')
							document.getElementById('csvSTART_DATE').innerHTML = "Start Date Can not be Past Date.";
						else if(objSource.id == 'csvEND_DATE')
							document.getElementById('csvEND_DATE').innerHTML = "End Date Can not be Past Date.";
						
						return;
					}
				}
				d1 = new Date(d1);
				d2 = new Date(d2);
				tranDate = new Date(tranDate);
				
				d1 = Date.parse(d1);
				d2 = Date.parse(d2);
				tranDate = Date.parse(tranDate);
				
				if (tranDate < d1 || tranDate > d2)
				{
					if(objSource.id == 'csvSTART_DATE')
						document.getElementById('csvSTART_DATE').innerHTML = "Start Date should be between financial year dates.";
					if(objSource.id == 'csvEND_DATE')
						document.getElementById('csvEND_DATE').innerHTML = "End Date should be between financial year dates.";
					objArgs.IsValid = false;
				}
				else
					objArgs.IsValid = true;
				
			}
		}
		else
			objArgs.IsValid = true;

	}
	
	function SetTab()
	{
		if (document.getElementById("hidOldData").value != "")
		{
			Url="JournalEntryDetailIndex.aspx?JOURNAL_ID=" + document.getElementById("hidJOURNAL_ID").value + "&CalledFrom=" + document.getElementById('hidCalledFrom').value + "&";
			DrawTab(2,this.parent.parent,'Journal Entry Details',Url);
			//showing the copy , next and delete button
			if(document.getElementById("btnNext"))
				document.getElementById("btnNext").style.display = "inline";
			if(document.getElementById("btnDelete"))	
				document.getElementById("btnDelete").style.display = "inline";
			if(document.getElementById("btnCopyTemplate"))
				document.getElementById("btnCopyTemplate").style.display = "none";
		}
		else
		{
			RemoveTab(2,this.parent.parent);
			if(document.getElementById("btnCopy"))
				document.getElementById("btnCopy").style.display = "none";
			if(document.getElementById("btnNext"))
				document.getElementById("btnNext").style.display = "none";
			if(document.getElementById("btnDelete"))	
				document.getElementById("btnDelete").style.display = "none";
			if(document.getElementById("hidGROUP_TYPE").value == "ML")
			{
				if(document.getElementById("btnCopyTemplate"))
					document.getElementById("btnCopyTemplate").style.display = "inline";
			}
			else
				document.getElementById("btnCopyTemplate").style.display = "none";
		}
	}
	
	//Defaulting the financial year (General Leder)
	function SetFiscalYear()
	{
		tranDate = document.getElementById("txtTRANS_DATE").value;
		tranDate = new Date(tranDate);
		tranDate = Date.parse(tranDate);
		
		cmbFiscal = document.getElementById("cmbFISCAL_ID");
		for(ctr = 0; ctr < cmbFiscal.options.length; ctr++)
		{
			fdate = cmbFiscal.options[ctr].text;
			
			if (fdate.trim() == "")
			{
				continue;
			}
			
			//Getting the financial dates, from financial year
			d1 = fdate.substring(fdate.indexOf("(") + 1, fdate.indexOf("(") + 11);
			d2 = fdate.substring(fdate.indexOf("-") + 1,fdate.indexOf("-") + 12);
			
			d1 = new Date(d1);
			d2 = new Date(d2);
			
			d1 = Date.parse(d1);
			d2 = Date.parse(d2);
		
			if (tranDate >= d1 && tranDate <= d2)		
			{
				//Transaction date is in between financial dates
				//Hence selecting this fiscal year
				cmbFiscal.selectedIndex = ctr;
				break;
			}		
		}
	}
	
	function AddData()
	{
		
		document.getElementById('hidJOURNAL_ID').value	=	'New';
		var TempDate = new Date();
		var CurDate =  (TempDate.getMonth()+1) + "/" +  TempDate.getDate()+ "/" + TempDate.getYear();
		
		document.getElementById('cmbFISCAL_ID').selectedIndex = 1;	
			
		if(document.getElementById('hidGROUP_TYPE').value != "13")
			document.getElementById('txtTRANS_DATE').value  = CurDate;	
		else	
			document.getElementById('txtTRANS_DATE').value  = document.getElementById('hidEndFiscalDate').value ;
		
		
		document.getElementById('txtDESCRIPTION').value  = '';
		
		if(document.getElementById('cmbDIV_ID').options.length > 0)
			document.getElementById('cmbDIV_ID').selectedIndex = 1;		
			
		document.getElementById('cmbFREQUENCY').selectedIndex = 0;
		document.getElementById('txtSTART_DATE').value = '';		
		document.getElementById('txtEND_DATE').value = '';
		document.getElementById('cmbDAY_OF_THE_WK').selectedIndex = 0;
		if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		
		//Defaulting the fiscal year
		SetFiscalYear();		
		
		ChangeColor();
		DisableValidators();
		
	}
	function populateXML()
	{
		
		if(document.getElementById('hidFormSaved').value == '0')
		{
			var tempXML = document.getElementById('hidOldData').value;
			if(tempXML != "")
			{
				populateFormData(tempXML,ACT_JOURNAL_MASTER);	
			}
			else
			{
				AddData();	
						
			}
		}
		
		HideInfo();
		cmbFREQUENCY_Change();
		SetTab();
		if(document.getElementById('txtDESCRIPTION') && document.getElementById('hidFormSaved').value != "5")
					document.getElementById('txtDESCRIPTION').focus();	
		return false;
	}
	
	//Hides the information which should be visible only if je type if recurring
	function HideInfo()
	{
		if (document.getElementById("hidGROUP_TYPE").value != "RC")
		{
			//JE type is not recurring
			document.getElementById("trRecurr1").style.display = "none";
			document.getElementById("trRecurr2").style.display = "none";
			document.getElementById("trRecurr3").style.display = "none";
			
			document.getElementById("rfvFREQUENCY").setAttribute("enabled", false);
			document.getElementById("rfvSTART_DATE").setAttribute("enabled", false);
			document.getElementById("rfvEND_DATE").setAttribute("enabled", false);
			document.getElementById("rfvDAY_OF_THE_WK").setAttribute("enabled", false);
		}
		if(document.getElementById('hidGROUP_TYPE').value != '13')
		{
			if(document.getElementById("btnCopy"))
				document.getElementById("btnCopy").style.display = "inline";
		}
		else
		{
			if(document.getElementById("btnCopy"))
				document.getElementById("btnCopy").style.display = "none";
		}
	}
	
	//Calls when frequency changes
	function cmbFREQUENCY_Change()
	{
		if (document.getElementById("cmbFREQUENCY").selectedIndex == 0)
		{
			document.getElementById("cmbDAY_OF_THE_WK").style.display = "inline";
			document.getElementById("spnDAY_OF_THE_WK").style.display = "none";
		}
		else
		{
			document.getElementById("cmbDAY_OF_THE_WK").style.display = "none";
			document.getElementById("spnDAY_OF_THE_WK").style.display = "inline";
		}
	}
	
	function formReset()
	{
		
		var tempXML = document.getElementById('hidOldData').value;
		
		if(tempXML != "")
		{
			document.location.href = "AddJournalEntryMaster.aspx?GROUP_TYPE=" + document.getElementById("hidGROUP_TYPE").value
											+ "&JOURNAL_ID=" + document.getElementById("hidJOURNAL_ID").value + "&";
		}
		else
		{
			AddData();				
		}
		cmbFREQUENCY_Change();
		DisableValidators();
		ChangeColor();
		return false;
		
		//document.location.href = document.location.href;
	}
	// set the trans. date 
	function SetTransDate()
	{
		var fdate = document.getElementById("cmbFISCAL_ID") .options[document.getElementById("cmbFISCAL_ID").selectedIndex].value;
		var strXML = document.getElementById('hidGenLedgerXML').value;	
		if(strXML != "")
		{
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(strXML).getElementsByTagName('NewDataSet')[0]);
			var i=0;	
								
			for(i=0;i<tree.childNodes.length;i++)
			{
				var nodeName = tree.childNodes[i].childNodes[2].firstChild.text;
				if(fdate == i+2)
					document.getElementById('txtTRANS_DATE').value=nodeName;
			}
		}
	}
	function ChkTextAreaLength(source, arguments)
	{
		var txtArea = arguments.Value;
		if(txtArea.length > 100 ) 
		{
			arguments.IsValid = false;
			return;   // invalid userName
		}
	}	
	
	function CopyRecords()
	{
		window.open('CopyTemplate.aspx?CalledFrom=' + document.getElementById("hidGROUP_TYPE").value + "&",'CopyTemplate',"width=800,height=600,screenX=50,screenY=150,top=80,left=80,scrollbars=yes,resizable=no,menubar=no,toolbar=no,status=no","");
		return false;
	}	
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="ACT_JOURNAL_MASTER" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="pageHeader">Please note that all fields marked with * are mandatory</TD>
				</tr>
				<tr id="trRecurrMsg" runat="server">
					<TD class="pageHeader">Recurring JE batches with proof not equal to zero or with no 
						detail items will not be posted.</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="right"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="tblBody" width="100%" align="center" border="0" runat="server">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capJOURNAL_ENTRY_NO" runat="server">Journal Entry No</asp:label></TD>
								<TD class="midcolora" width="32%">
									<!-- <asp:label id="txtlJOURNAL_ENTRY_NO" runat="server" CssClass="LabelFont"></asp:label> --><asp:textbox id="txtJOURNAL_ENTRY_NO" style="TEXT-ALIGN: left" runat="server" size="8" BorderWidth="0"
										ReadOnly="True" cssclass="midcoloraReadOnlyTextBox"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capGL_ID" runat="server">General Ledger</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFISCAL_ID" onfocus="SelectComboIndex('cmbFISCAL_ID')" runat="server">
										<ASP:LISTITEM Value="0"></ASP:LISTITEM>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvGL_ID" runat="server" Display="Dynamic" ErrorMessage="GL_ID can't be blank."
										ControlToValidate="cmbFISCAL_ID"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTRANS_DATE" runat="server">Transaction Date</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtTRANS_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkTRANS_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgTRANS_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvTRANS_DATE" runat="server" Display="Dynamic" ErrorMessage="TRANS_DATE can't be blank."
										ControlToValidate="txtTRANS_DATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTRANS_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtTRANS_DATE"></asp:regularexpressionvalidator><asp:customvalidator id="csvTRANS_DATE" runat="server" Display="Dynamic" ErrorMessage="TRANS_DATE can't be blank."
										ControlToValidate="txtTRANS_DATE" ClientValidationFunction="ChkFiscalDate"></asp:customvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDIV_ID" runat="server">Accounting Entity</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDIV_ID" onfocus="SelectComboIndex('cmbDIV_ID')" runat="server">
										<ASP:LISTITEM Value="0"></ASP:LISTITEM>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvDIV_ID" runat="server" Display="Dynamic" ErrorMessage="DIV_ID can't be blank."
										ControlToValidate="cmbDIV_ID"></asp:requiredfieldvalidator></TD>
										
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDESCRIPTION" runat="server">Description</asp:label></TD>
								<TD class="midcolora" colSpan="3"><asp:textbox id="txtDESCRIPTION" runat="server" maxlength="100" size="100" TextMode="MultiLine"
										Height="48px" Width="280px"></asp:textbox><br>
									<asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtDESCRIPTION"
										Runat="server" Display="Dynamic" ID="csvDESCRIPTION"></asp:CustomValidator>
								</TD>
							</tr>
							<tr id="trRecurr1">
								<TD class="midcolora" width="18%"><asp:label id="capFREQUENCY" runat="server">Frequency</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFREQUENCY" onchange="cmbFREQUENCY_Change();" Runat="server" EnableViewState="True"></asp:dropdownlist><asp:requiredfieldvalidator id="rfvFREQUENCY" Display="Dynamic" ErrorMessage="Select" ControlToValidate="cmbFREQUENCY"
										Runat="server"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSTART_DATE" runat="server">Start Date</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSTART_DATE" size="12" Runat="server" MaxLength="10"></asp:textbox><asp:hyperlink id="hlkSTART_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgSTART_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvSTART_DATE" runat="server" Display="Dynamic" ErrorMessage="start date can't be blank."
										ControlToValidate="txtSTART_DATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSTART_DATE" runat="server" Display="Dynamic" ErrorMessage="Format is wrong."
										ControlToValidate="txtSTART_DATE"></asp:regularexpressionvalidator><asp:customvalidator id="csvSTART_DATE" runat="server" Display="Dynamic" ErrorMessage="TRANS_DATE can't be blank."
										ControlToValidate="txtSTART_DATE" ClientValidationFunction="ChkFiscalDate"></asp:customvalidator></TD>
							</tr>
							<tr id="trRecurr2">
								<td class="midcolora" width="18%"><asp:label id="capEND_DATE" Runat="server">End Date</asp:label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%"><asp:textbox id="txtEND_DATE" size="12" Runat="server" MaxLength="10"></asp:textbox><asp:hyperlink id="hlkEND_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgEND_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvEND_DATE" runat="server" Display="Dynamic" ErrorMessage="End date can't be blank."
										ControlToValidate="txtEND_DATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEND_DATE" runat="server" Display="Dynamic" ErrorMessage="Format is wrong."
										ControlToValidate="txtEND_DATE"></asp:regularexpressionvalidator><asp:customvalidator id="csvEND_DATE" runat="server" Display="Dynamic" ErrorMessage="TRANS_DATE can't be blank."
										ControlToValidate="txtEND_DATE" ClientValidationFunction="ChkFiscalDate"></asp:customvalidator><asp:comparevalidator id="cpvEND_DATE" Display="Dynamic" ControlToValidate="txtEND_DATE" Runat="server"
										Operator="GreaterThan" Type="Date" ControlToCompare="txtSTART_DATE"></asp:comparevalidator></td>
								<td class="midcolora" width="18%"><asp:label id="capDAY_OF_THE_WK" Runat="server">Day of week</asp:label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbDAY_OF_THE_WK" Runat="server">
										<ASP:LISTITEM Value="1">Sunday</ASP:LISTITEM>
										<ASP:LISTITEM Value="2">Monday</ASP:LISTITEM>
										<ASP:LISTITEM Value="3">Tuesday</ASP:LISTITEM>
										<ASP:LISTITEM Value="4">Wednesday</ASP:LISTITEM>
										<ASP:LISTITEM Value="5">Thursday</ASP:LISTITEM>
										<ASP:LISTITEM Value="6">Friday</ASP:LISTITEM>
										<ASP:LISTITEM Value="7">Saturday</ASP:LISTITEM>
									</asp:dropdownlist><asp:requiredfieldvalidator id="rfvDAY_OF_THE_WK" runat="server" Display="Dynamic" ErrorMessage="Please select day of week."
										ControlToValidate="cmbDAY_OF_THE_WK"></asp:requiredfieldvalidator><span class="labelfont" id="spnDAY_OF_THE_WK">N.A.</span>
								</td>
							</tr>
							<tr id="trRecurr3">
								<td class="midcolora" width="18%"><asp:label id="capLAST_PROCESSED_DATE" runat="server">Last Processed Date</asp:label></td>
								<td class="midcolora" width="32%"><asp:label id="lblLAST_PROCESSED_DATE" CssClass="labelfont" Runat="server"></asp:label></td>
								<td class="midcolora" width="18%"><asp:label id="capLAST_TRANSACTION_DATE" runat="server">Last Transaction Date</asp:label></td>
								<td class="midcolora" width="32%"><asp:label id="lblLAST_TRANSACTION_DATE" CssClass="labelfont" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnNext" runat="server" Text="Next"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
										CausesValidation="false"></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" Text="Commit"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnCopyTemplate" runat="server" Text="Copy From Template"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnRecurr"  visible= "False" runat="server" Text="Run Recurring Journal Entries"></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidEndFiscalDate" type="hidden" value="0" name="hidEndFiscalDate" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidJOURNAL_ID" type="hidden" name="hidJOURNAL_ID" runat="server"> <input id="hidGROUP_TYPE" type="hidden" name="hidGROUP_TYPE" runat="server">
			<input id="hidGenLedgerXML" type="hidden" name="hidGenLedgerXML" runat="server">
		</FORM>
		<script>
	/*	if (document.getElementById('hidFormSaved').value == "5")
		{
			document.getElementById('hidFormSaved').value = 1;
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidJOURNAL_ID').value, false);
			document.getElementById('hidFormSaved').value = 5;
		}
		else
		{*/
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidJOURNAL_ID').value, false);
	//	}
		</script>
	</BODY>
</HTML>

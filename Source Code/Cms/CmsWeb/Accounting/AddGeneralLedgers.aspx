<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AddGeneralLedgers.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.AddGeneralLedgers" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-General Ledger</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript" type="text/javascript">
		    var mode = '<%=Request.QueryString["mode"]%>';

	function AddData()
	{
		DisableValidators();
		document.getElementById('hidFISCAL_ID').value	=	'New';
		document.getElementById('txtLEDGER_NAME').focus();

		if(mode != null && mode!="NewFiscalYear")
		{
		document.getElementById('cmbFISCAL_BEGIN_MONTH').value  = '';
		document.getElementById('txtLEDGER_NAME').value  = '';

		}

		document.getElementById('cmbFISCAL_END_MONTH').value  = '';
		document.getElementById('cmbMONTH_BEGINING').value  = '';
		document.getElementById('txtSMALL_BALANCE').value  = '';
		document.getElementById('lblFISCAL_BEGIN_DATE').innerHTML  = '';
		document.getElementById('chkFORBID_POSTING').checked  = false;
		//document.getElementById('btnCreateNewFiscalYear').setAttribute("disabled",true);
		ChangeColor();
       }


     function formatCurrencyRate(num) {  //Added by Aditya on 17-oct-2011 for TFS Bug # 1844
         
      num = isNaN(num) || num === '' || num === null ? 0.00 : num;
      return parseFloat(num).toFixed(2);
      
    }

    function formatRateTextValue(val, num) {    //Added by Aditya on 17-oct-2011 for TFS Bug # 1844
    if (val == "100" || val == "100.0" || val == "100,0") {
        if (num == 1) {
            val = "100.00";
        }
        else {
            val = "100,00";
        }

    }
    else {
        if (num == 1) {
            if (val.indexOf(".") > -1 || val.indexOf(",") > -1) {
                val = val.replace(",", ".");
                val = val.substring(0, val.indexOf(".") + 3);
            }
        }
        else {
            if (val.indexOf(".") > -1 || val.indexOf(",") > -1) {
                val = val.replace(".", ",");
                val = val.substring(0, val.indexOf(",") + 3);
            }
        }

    }
   
    return val;
   
}
	function populateXML()
	{
		
		DisableValidators();
		var tempXML = document.getElementById('hidOldData').value;
		//alert(tempXML);
		//alert(document.getElementById('hidFormSaved').value);
		if(mode != null && mode=="NewFiscalYear")
		{
		AddData();
		}
		if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
		{
			if(tempXML!="")
			{   
				populateFormData(tempXML,ACT_GENERAL_LEDGER);
				document.getElementById('cmbFISCAL_BEGIN_MONTH').setAttribute('disabled', true);
				$("#txtSMALL_BALANCE").blur();
			}
			else
			{
				AddData();
			}
			
			// -- Set 'Fiscal Year' to the current year
			// -- if next fiscal year has been created then end date of fiscal year will be readonly
			var CurDate  = new Date();
			var CurYear  = CurDate.getUTCFullYear();
			var cmbFisYr = document.getElementById('cmbFiscalYearFrom');	
		/*	if(cmbFisYr != null && cmbFisYr.options.length>0)
			{			    
				SelectComboOptionByText("cmbFiscalYearFrom",CurYear)				
				var cmbLastFiscalYr = document.getElementById('cmbFiscalYearFrom').options.lastChild.innerText;	
				if(cmbLastFiscalYr != CurYear)				
					document.getElementById('cmbFISCAL_END_MONTH').setAttribute('disabled',true)				    
				   // SelectComboOptionByText("cmbFiscalYearFrom",cmbFisYr)				 
			}
		 */
			// -- end 			
			
		}
		
		if(document.getElementById('cmbFISCAL_BEGIN_MONTH').selectedIndex!=-1)
			SetBegindate();

		setTab();
		return false;
	}
		function ShowCreateNewFiscalYear()
		{
			populateXML();
			var url;
			var ledgerName=document.getElementById('txtLEDGER_NAME').value;
			url="AddGeneralLedgers.aspx?mode=NewFiscalYear&LEDGER_NAME="+ledgerName;	
			ShowPopup(url,'CreateNewFiscalYear',900,400);	
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
		function CheckIfPopup()
		{
			if(document.getElementById('btnCreateNewFiscalYear')!=null)
				{
					top.topframe.main1.mousein = false;
					//findMouseIn();
					showScroll();
				}
		}
		function setTab()
			{
			    var str;
			    var str1;
			    str = document.getElementById('hiddentab').value;
			    str1 = document.getElementById('hdientab_LockPosting').value;
				if(!(top.frames[1]==null))
				{
					if (document.getElementById('hidOldData').value	!= '')
					{			
						Url="GlAccountInformationIndex.aspx?";
						DrawTab(2,top.frames[1],str,Url);		
						Url="../../Account/aspx/LockGLPostingDates.aspx?";
						DrawTab(3,top.frames[1],str1,Url);									
					}
					else
					{							
						RemoveTab(2,top.frames[1]);			
					}
				}
			}
		function SetBegindate() {
		    
			if(document.getElementById('cmbFiscalYearFrom')!=null && document.getElementById('cmbFiscalYearFrom')!= "undefined")
					year  =  document.getElementById('cmbFiscalYearFrom').options[document.getElementById('cmbFiscalYearFrom').selectedIndex].text;
			else
				year  =  document.getElementById('txtFiscalYearFrom').value;
			var month =  document.getElementById('cmbFISCAL_BEGIN_MONTH').options[document.getElementById('cmbFISCAL_BEGIN_MONTH').selectedIndex].value;
			if (month <= 9) month = "0" + month;
			//Added by Pradeep Kushwaha on 02-March-2011
			if (iLangID == 2) //If the language id is 2 then
			{
			    document.getElementById('lblFISCAL_BEGIN_DATE').innerHTML = "01/" + month+"/" +year;
			    document.getElementById('hidFISCAL_BEGIN_DATE').value = "01/" + month+"/" + year;
			}
			else {
			    document.getElementById('lblFISCAL_BEGIN_DATE').innerHTML = month + "/01/" + year;
			    document.getElementById('hidFISCAL_BEGIN_DATE').value = month + "/01/" + year;
			}
			
			//Added till here 
			
			if(document.getElementById('cmbFISCAL_END_MONTH').selectedIndex!=-1) SetEndDate();
			
		}
		function SetEndDate() {
		     
			if(document.getElementById('cmbFiscalYearFrom')!=null && document.getElementById('cmbFiscalYearFrom')!= "undefined")
				year  =  document.getElementById('cmbFiscalYearFrom').options[document.getElementById('cmbFiscalYearFrom').selectedIndex].text;
			else
				year  =  document.getElementById('txtFiscalYearFrom').value;
			var begMonth =  document.getElementById('cmbFISCAL_BEGIN_MONTH').options[document.getElementById('cmbFISCAL_BEGIN_MONTH').selectedIndex].value;
			var month =  document.getElementById('cmbFISCAL_END_MONTH').options[document.getElementById('cmbFISCAL_END_MONTH').selectedIndex].value;
			month = (parseInt(begMonth)-1)+parseInt(month)
			if(month>12)
			{
				month =	parseInt(month)-12
				year++;
			}
			var days = GetDays(month,year);
			if (month <= 9) month = "0" + month;
			//Added by Pradeep Kushwaha on 02-March-2011
			if (iLangID == 2 || iLangID==3) //If the language id is 2 then
			{
			    document.getElementById('lblFISCAL_END_DATE').innerHTML = days + "/" + month + "/" + year;
			    document.getElementById('hidFISCAL_END_DATE').value =  days + "/" + month + "/" + year;
			}
			else {
			    document.getElementById('lblFISCAL_END_DATE').innerHTML = month + "/" + days + "/" + year;
			    document.getElementById('hidFISCAL_END_DATE').value = month + "/" + days + "/" + year;
			}
			//Added till here 
			
		}
		function GetDays(month,year)
		{
			var days=30;
			if(month!=2)
			{
				switch(month)
				{
					case 1:
					case 3:
					case 5:
					case 7:
					case 8:
					case 10:
					case 12:
						days = 31;
				}
			}
			else
			{
				days=28;
				if((year%400)==0)
					days=29;
				else if((year%4)==0 && (year%100)!=0)	
					days=29;
			}
			return days;
		}
		function IsValidMonthBeginning(objSource , objArgs)
		{
			var begMonth =  parseInt(document.getElementById('cmbFISCAL_BEGIN_MONTH').options[document.getElementById('cmbFISCAL_BEGIN_MONTH').selectedIndex].value);
			var month =  parseInt(document.getElementById('cmbFISCAL_END_MONTH').options[document.getElementById('cmbFISCAL_END_MONTH').selectedIndex].value);
			month = (parseInt(begMonth)-1)+parseInt(month)
			var mobeg =  parseInt(document.getElementById('cmbMONTH_BEGINING').options[document.getElementById('cmbMONTH_BEGINING').selectedIndex].value);
			//aa
			if(month>12)
			{
				month =	parseInt(month)-12
				if(mobeg<begMonth && mobeg>month) objArgs.IsValid = false;
			}
			else if(mobeg<begMonth) 
					objArgs.IsValid =false;
			else if(mobeg>month) 
					objArgs.IsValid =false;
			else 
				objArgs.IsValid =true;
			
		}
	function ShowPrint()
	{
			var url="ChartOfAccountsPrint.aspx";	
			ShowPopup(url,'ChartofAccounts',900,400);	
			return false;
	}
	
	function ResetScreen()
	{
		document.location.href="AddGeneralLedgers.aspx";
		return false; 
	}


		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();CheckIfPopup();">
		<div class="pageContent" id="bodyHeight">
			<FORM id="ACT_GENERAL_LEDGER" method="post" runat="server">
				<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<tr>
									<TD class="headereffectCenter" colSpan="5"><asp:Label ID="capCHATACC" runat ="server"></asp:Label></TD> <%--Chart of Accounts--%>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMANDATORY" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLEDGER_NAME" runat="server">General Ledger Name</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLEDGER_NAME" runat="server" maxlength="50" size="30"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvLEDGER_NAME" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtLEDGER_NAME"></asp:requiredfieldvalidator></TD><%--LEDGER_NAME can't be blank.--%>
									<TD class="midcolora" width="18%" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="Label1" runat="server">Select Fiscal Year</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFiscalYearFrom" onfocus="SelectComboIndex('cmbFiscalYearFrom')" Runat="server"
											AutoPostBack="True"></asp:dropdownlist><asp:textbox id="txtFiscalYearFrom" runat="server" Visible="False" ReadOnly="True"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvFiscalYearFrom" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbFiscalYearFrom"></asp:requiredfieldvalidator></TD><%--FISCAL_BEGIN_DATE can't be blank.--%>
									<TD class="midcolora" width="18%" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capFISCAL_BEGIN_DATE" runat="server">Fiscal Period Beginning Month</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFISCAL_BEGIN_MONTH" onfocus="SelectComboIndex('cmbFISCAL_BEGIN_MONTH');SetBegindate();"
											Runat="server" onchange="SetBegindate();"></asp:dropdownlist>&nbsp;&nbsp;<asp:label id="lblFISCAL_BEGIN_DATE" runat="server"></asp:label>
										<br>
										<asp:requiredfieldvalidator id="rfvFISCAL_BEGIN_DATE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbFISCAL_BEGIN_MONTH"></asp:requiredfieldvalidator></TD><%--FISCAL_BEGIN_DATE can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capFISCAL_END_DATE" runat="server">Fiscal Period Ending Month</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFISCAL_END_MONTH" onfocus="SelectComboIndex('cmbFISCAL_END_MONTH');SetEndDate();"
											Runat="server" onchange="SetEndDate();"></asp:dropdownlist>&nbsp;&nbsp;<asp:label id="lblFISCAL_END_DATE" runat="server"></asp:label>
										<br>
										<asp:requiredfieldvalidator id="rfvFISCAL_END_DATE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbFISCAL_END_MONTH"></asp:requiredfieldvalidator></TD><%--FISCAL_END_DATE can't be blank.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMONTH_BEGINING" runat="server">Month Beginning</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMONTH_BEGINING" onfocus="SelectComboIndex('cmbMONTH_BEGINING')" Runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvMONTH_BEGINING" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbMONTH_BEGINING"></asp:requiredfieldvalidator><%--MONTH_BEGINING can't be blank.--%><asp:customvalidator id="csvMONTH_BEGINING" Display="Dynamic" ControlToValidate="cmbMONTH_BEGINING" Runat="server"
											ClientValidationFunction="IsValidMonthBeginning"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSMALL_BALANCE" runat="server">Small Balance Write Off Amount</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtSMALL_BALANCE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="13"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revSMALL_BALANCE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtSMALL_BALANCE"></asp:regularexpressionvalidator></TD><%--RegularExpressionValidator--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capFORBID_POSTING" runat="server">Forbid Posting if Out of Balance</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkFORBID_POSTING" runat="server"></asp:checkbox></TD>
									<TD class="midcolora" width="18%" colSpan="2"></TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" Text="Print"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnCreateNewFiscalYear" runat="server" Text="Create New Fiscal Year"
											CausesValidation="False"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<tr>
									<td class="iframsHeightMedium"></td>
								</tr>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">&nbsp;
				<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidFISCAL_ID" type="hidden" value="New" name="hidFISCAL_ID" runat="server">
				<INPUT id="hidGL_ID" type="hidden" value="New" name="hidGL_ID" runat="server"> <INPUT id="hidFISCAL_END_DATE" type="hidden" value="111" name="hidFISCAL_END_DATE" runat="server">
				<INPUT id="hidFISCAL_BEGIN_DATE" type="hidden" value="111" name="hidFISCAL_BEGIN_DATE"
					runat="server">
			    <input id="hiddentab" type="hidden" runat="server" />
			    <input id="hdientab_LockPosting" type="hidden" runat ="server" />
			</FORM>
		</div>
		<script>
	
//			RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFISCAL_ID').value);
		</script>
	</BODY>
</HTML>

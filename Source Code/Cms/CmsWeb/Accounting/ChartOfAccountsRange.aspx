<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="ChartOfAccountsRange.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.ChartOfAccountsRange" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-Chart of Accounts Ranges</title>
<meta content="Microsoft Visual Studio 7.0" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>

<script src="/cms/cmsweb/scripts/form.js"></script>

<script language=javascript>
function AddData(index)
{
	DisableValidators();
	/*document.getElementById('txtRANGE_FROM_Asset'+index).value  = '';
	document.getElementById('cmbRANGE_FROM_Asset1'+index).value  = '';
	document.getElementById('txtRANGE_TO_Asset'+index).value  = '';
	document.getElementById('cmbRANGE_TO_Asset1'+index).value  = '';
	
	document.getElementById('txtRANGE_FROM_Liability'+index).value  = '';
	document.getElementById('cmbRANGE_FROM_Liability1'+index).value  = '';
	document.getElementById('txtRANGE_TO_Liability'+index).value  = '';
	document.getElementById('cmbRANGE_TO_Liability1'+index).value  = '';
	
	document.getElementById('txtRANGE_FROM_Equity'+index).value  = '';
	document.getElementById('cmbRANGE_FROM_Equity1'+index).value  = '';
	document.getElementById('txtRANGE_TO_Equity'+index).value  = '';
	document.getElementById('cmbRANGE_TO_Equity1'+index).value  = '';
	
	document.getElementById('txtRANGE_FROM_Income'+index).value  = '';
	document.getElementById('cmbRANGE_FROM_Income1'+index).value  = '';
	document.getElementById('txtRANGE_TO_Income'+index).value  = '';
	document.getElementById('cmbRANGE_TO_Income1'+index).value  = '';
	
	document.getElementById('txtRANGE_FROM_Expense'+index).value  = '';
	document.getElementById('cmbRANGE_FROM_Expense1'+index).value  = '';
	document.getElementById('txtRANGE_TO_Expense'+index).value  = '';
	document.getElementById('cmbRANGE_TO_Expense1'+index).value  = '';*/
	ChangeColor();
}
var MaxRows = 5;
function populateXML()
{
	
	if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
	{
		var tempXML="";
		if(xmlArray != null)
		{
			for(var i=0;i<MaxRows;i++)
			{
				tempXML = xmlArray[i];
				if(tempXML!="")
				{
					populateFormData(tempXML,ACT_GL_ACCOUNT_RANGES);
				}
			}
		}
	}
return false;
}

function IsNumeric(objEvent) 
	{
	    objEvent = (objEvent) ? objEvent : event;
	    var charCode = (objEvent.charCode) ? objEvent.charCode : ((objEvent.keyCode) ? objEvent.keyCode : ((objEvent.which) ? objEvent.which : 0));
	    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
	        return false;
	    }
	    return true;
	}
	function CheckIsNull(objText)
	{
		if(objText.value=="")
			objText.value ="0";
	}
	function CheckForOverlapping()
	{
		//Asset
		var whole		=  document.getElementById('txtRANGE_FROM_Asset').value;
		var fraction	=  document.getElementById('cmbRANGE_FROM_Asset1').options[document.getElementById('cmbRANGE_FROM_Asset1').selectedIndex].value;
		var assetFrom = parseFloat(whole+fraction);
		whole		=  document.getElementById('txtRANGE_TO_Asset').value;
		fraction	=  document.getElementById('cmbRANGE_TO_Asset1').options[document.getElementById('cmbRANGE_TO_Asset1').selectedIndex].value;	
		var assetTo = parseFloat(whole+fraction);
		if(assetFrom>assetTo)	
		{
			document.getElementById('txtRANGE_FROM_Asset').focus();
			alert("Asset: From value must be less than Through value!");
			return false;
		}
		
		//liability
		var whole		=  document.getElementById('txtRANGE_FROM_Liability').value;
		var fraction	=  document.getElementById('cmbRANGE_FROM_Liability1').options[document.getElementById('cmbRANGE_FROM_Liability1').selectedIndex].value;
		var liabilityFrom = parseFloat(whole+fraction);
		whole		=  document.getElementById('txtRANGE_TO_Liability').value;
		fraction	=  document.getElementById('cmbRANGE_TO_Liability1').options[document.getElementById('cmbRANGE_TO_Liability1').selectedIndex].value;	
		var liabilityTo = parseFloat(whole+fraction);

		if(liabilityFrom<assetTo)
		{
			document.getElementById('txtRANGE_FROM_Liability').focus();
			alert("Liability: From value must be more than Asset's Through value!");
			return false;
		}
		if(liabilityTo<liabilityFrom)	
		{
			document.getElementById('txtRANGE_FROM_Liability').focus();
			alert("Liability: From value must be less than Through value!");
			return false;
		}
		
		//Equity
		var whole		=  document.getElementById('txtRANGE_FROM_Equity').value;
		var fraction	=  document.getElementById('cmbRANGE_FROM_Equity1').options[document.getElementById('cmbRANGE_FROM_Equity1').selectedIndex].value;
		var EquityFrom = parseFloat(whole+fraction);
		whole		=  document.getElementById('txtRANGE_TO_Equity').value;
		fraction	=  document.getElementById('cmbRANGE_TO_Equity1').options[document.getElementById('cmbRANGE_TO_Asset1').selectedIndex].value;	
		var EquityTo = parseFloat(whole+fraction);
		
		if(EquityFrom<liabilityTo)
		{
			document.getElementById('txtRANGE_TO_Equity').focus();
			alert("Equity: From value must be more than Liability's Through value!");
			return false;
		}
		if(EquityFrom>EquityTo)	
		{
			document.getElementById('txtRANGE_FROM_Equity').focus();
			alert("Equity: From value must be less than Through value!");
			return false;
		}
		
		//income
		var whole		=  document.getElementById('txtRANGE_FROM_Income').value;
		var fraction	=  document.getElementById('cmbRANGE_FROM_Income1').options[document.getElementById('cmbRANGE_FROM_Income1').selectedIndex].value;
		var IncomeFrom = parseFloat(whole+fraction);
		whole		=  document.getElementById('txtRANGE_TO_Income').value;
		fraction	=  document.getElementById('cmbRANGE_TO_Income1').options[document.getElementById('cmbRANGE_TO_Income1').selectedIndex].value;	
		var IncomeTo = parseFloat(whole+fraction);
		
		if(IncomeFrom<EquityTo)
		{
			document.getElementById('txtRANGE_TO_Income').focus();
			alert("Income: From value must be more than Equity's Through value!");
			return false;
		}
		if(IncomeFrom>IncomeTo)	
		{
			document.getElementById('txtRANGE_FROM_Income').focus();
			alert("Income: From value must be less than Through value!");
			return false;
		}
		
		//Expense
		var whole		=  document.getElementById('txtRANGE_FROM_Expense').value;
		var fraction	=  document.getElementById('cmbRANGE_FROM_Expense1').options[document.getElementById('cmbRANGE_FROM_Expense1').selectedIndex].value;
		var ExpenseFrom = parseFloat(whole+fraction);
		whole		=  document.getElementById('txtRANGE_TO_Expense').value;
		fraction	=  document.getElementById('cmbRANGE_TO_Expense1').options[document.getElementById('cmbRANGE_TO_Expense1').selectedIndex].value;	
		var ExpenseTo = parseFloat(whole+fraction);
		
		if(ExpenseFrom<IncomeTo)
		{
			document.getElementById('txtRANGE_TO_Expense').focus();
			alert("Expense: From value must be more than Income's Through value!");
			return false;
		}
		if(ExpenseFrom>ExpenseTo)	
		{
			document.getElementById('txtRANGE_FROM_Expense').focus();
			alert("Expense: From value must be less than Through value!");
			return false;
		}
		Page_ClientValidate(); 
	}
function ShowDefineSubRanges()
	{
		parent.changeTab(0,1);
		/*var url="SubRangesIndex.aspx";
		ShowPopup(url,'PrintSubRanges',800,650);	*/
		return false;
	}
function ShowPrintRanges()
{
		var url="PrintChartOfAccountRanges.aspx";	
		ShowPopup(url,'DefineSubRanges',900,600);	
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
		</script>
</HEAD>
<BODY oncontextmenu = "return false;" leftMargin=0 topMargin=0 onload="top.topframe.main1.mousein = false;showScroll();">
<div class=pageContent id=bodyHeight>
<FORM id=ACT_GL_ACCOUNT_RANGES method=post runat="server">
<TABLE class=tableWidthHeader cellSpacing=0 cellPadding=0 align=center border=0>
  <tr><td>
  <webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
  </td></tr>
  <TR>
    <TD>
      <TABLE width="100%" align=center border=0>
        <tr>
          <TD class=headereffectCenter colSpan=5><asp:Label ID="capChartofaccountrange" runat="server"></asp:Label></TD></TR><%--Chart of 
            Account Ranges--%>
        <tr>
          <td class=midcolorc align=right colSpan=5><asp:label id=lblMessage runat="server" Visible="False" CssClass="errmsg"></asp:label></TD></TR>
        <tr>
          <TD class=midcolora  width="5%" 
          ><asp:label id=lblCATEGORY_DESC_Asset runat="server"></asp:Label></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_FROM_Asset runat="server">From</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_FROM_Asset onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_FROM_Asset1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_FROM_Asset runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_FROM_Asset" Display="Dynamic"></asp:RegularExpressionValidator></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_TO_Asset runat="server">Through</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_TO_Asset onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_TO_Asset1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_TO_Asset runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_TO_Asset" Display="Dynamic"></asp:RegularExpressionValidator></TD></TR>
        <tr>
          <TD class=midcolora  width="5%" 
          ><asp:label id=Label1 runat="server">Liability</asp:Label></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_FROM_Liability runat="server">From</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_FROM_Liability onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_FROM_Liability1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_FROM_Liability runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_FROM_Liability" Display="Dynamic"></asp:RegularExpressionValidator></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_TO_Liability runat="server">Through</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_TO_Liability onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_TO_Liability1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_TO_Liability runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_TO_Liability" Display="Dynamic"></asp:RegularExpressionValidator></TD></TR>
        <tr>
          <TD class=midcolora  width="5%" 
          ><asp:label id=Label4 runat="server">Equity</asp:Label></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_FROM_Equity runat="server">From</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_FROM_Equity onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_FROM_Equity1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_FROM_Equity runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_FROM_Equity" Display="Dynamic"></asp:RegularExpressionValidator></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_TO_Equity runat="server">Through</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_TO_Equity onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_TO_Equity1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_TO_Equity runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_TO_Equity" Display="Dynamic"></asp:RegularExpressionValidator></TD></TR>
        <tr>
          <TD class=midcolora  width="5%" 
          ><asp:label id=Label7 runat="server">Income</asp:Label></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_FROM_Income runat="server">From</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_FROM_Income onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_FROM_Income1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_FROM_Income runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_FROM_Income" Display="Dynamic"></asp:RegularExpressionValidator></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_TO_Income runat="server">Through</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_TO_Income onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_TO_Income1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_TO_Income runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_TO_Income" Display="Dynamic"></asp:RegularExpressionValidator></TD></TR>
        <tr>
          <TD class=midcolora  width="5%" 
          ><asp:label id=Label10 runat="server">Expense</asp:Label></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_FROM_Expense runat="server">From</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_FROM_Expense onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_FROM_Expense1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_FROM_Expense runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_FROM_Expense" Display="Dynamic"></asp:RegularExpressionValidator></TD>
          <TD class=midcolora  width="13%" 
          ><asp:label id=capRANGE_TO_Expense runat="server">Through</asp:Label></TD>
          <TD class=midcolora  width="32%" 
          ><asp:textbox onkeypress="return IsNumeric(event);" id=txtRANGE_TO_Expense onblur=CheckIsNull(this); runat="server" CssClass="INPUTCURRENCY" maxlength="5" size="8"></asp:textbox><asp:dropdownlist id=cmbRANGE_TO_Expense1 runat="server"></asp:DropDownList><BR><asp:regularexpressionvalidator id=revRANGE_TO_Expense runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtRANGE_TO_Expense" Display="Dynamic"></asp:RegularExpressionValidator></TD></TR>
        <tr>
          <td class=midcolora colspan="3">
          <cmsb:cmsbutton class=clsButton id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
          <cmsb:cmsbutton class=clsButton id=btnPrintRanges runat="server" Text="Print Ranges"></cmsb:cmsbutton>
          <cmsb:cmsbutton class=clsButton id=btnDefine_Ranges runat="server" Text="Next"></cmsb:cmsbutton>
          </TD>
          <td class=midcolorr colSpan=3><cmsb:cmsbutton class=clsButton id=btnSave runat="server" Text="Save"></cmsb:cmsbutton></TD></TR>
        <tr>
          <td class=iframsHeightMedium></TD></TR><INPUT 
        id=hidFormSaved type=hidden value=0 name=hidFormSaved 
         runat="server"> <INPUT id=hidOldData type=hidden 
        value=0 name=hidOldData runat="server"> <INPUT 
        id=hidCATEGORY_ID type=hidden value=0 name=hidCATEGORY_ID 
         runat="server"> 
</TABLE></TD></TR></TABLE></FORM></DIV>
	</BODY>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" validateRequest=false Codebehind="ReserveBreakDownDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ReserveBreakDownDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Claims Reserve Breakdown</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>

<script src="/cms/cmsweb/scripts/form.js"></script>

<script src="/cms/cmsweb/scripts/Calendar.js"></script>

<script language=javascript>
		var PERCENTAGE = "11789", ABSOLUTE="11790"
		
		function AddMore()
		{
			document.getElementById("hidRESERVE_BREAKDOWN_ID").value = "";
			document.location.href = document.location.href;
		}
		function GoBack()
		{
			top.botframe.location.href = "ReserveDetails.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
			return false;
		}

		function CloseWindow()
		{
			top.window.close();
			return false;
		}
		function Init()
		{
			ApplyColor();
			ChangeColor();
		}
		function CalculateAmount()
		{
			var TotalAmount=0;
			var PercentageMaxLength =6;
			var AbsoluateMaxLength = 12;
			combo = document.getElementById("cmbBASIS");
			
			//var Amount = 10000;
			//document.getElementById("lblTOTAL_OUTSTANDING").innerText = formatCurrency(Amount);
			
			val = new String(document.getElementById("txtVALUE").value);			
			val = val.replace(",","");
			document.getElementById("txtVALUE").value = formatCurrency(val);
			
			if(combo==null || combo.selectedIndex==-1)	
				return;
			
			if(val!="")
				val = parseInt(val);
			
			if(combo.options[combo.selectedIndex].value==PERCENTAGE)
			{
				document.getElementById("txtVALUE").maxLength = PercentageMaxLength;
				if(document.getElementById("lblTOTAL_OUTSTANDING").innerText!="" && !isNaN(parseInt(val)))
				{
					TotalOutstanding = new String(document.getElementById("lblTOTAL_OUTSTANDING").innerText);
					TotalOutstanding = TotalOutstanding.replace(",","");
					TotalOutstanding = (parseInt(TotalOutstanding) * parseInt(val))/100;
					TotalAmount = parseInt(TotalOutstanding);
				}
				else
					TotalAmount = 0;
				
			}
			else if(combo.options[combo.selectedIndex].value==ABSOLUTE)
			{
				document.getElementById("txtVALUE").maxLength = AbsoluateMaxLength;				
				if(!isNaN(parseInt(val)))
					TotalAmount = val;
				else
					TotalAmount = 0;
			}
			document.getElementById("txtAMOUNT").value = formatCurrency(TotalAmount);
			
			return false;
		}
		</script>
</HEAD>
<body onload=Init(); MS_POSITIONING="GridLayout">
<div class=pageContent id=bodyHeight>
<form id=Form1 method=post runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%">
  <TR>
    <TD><webcontrol:gridspacer id=grdspacer runat="server"></webcontrol:gridspacer></TD></TR>
  <TR id=trBody runat="server">
    <TD>
      <table cellSpacing=0 cellPadding=0 width="100%">
        <tr>
          <td class=headereffectCenter colSpan=4><asp:label id=lblTitle runat="server">Claims Reserve Breakdown</asp:label></TD></TR>
        <tr>
          <td align=center colSpan=4><asp:label id=lblMessage runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></TD></TR>
        <tr>
          <td class=midcolora width="18%"><asp:label id=capTOTAL_OUTSTANDING Runat="server"></asp:label></TD>
          <td class=midcolora width="32%"><asp:label id=lblTOTAL_OUTSTANDING Runat="server"></asp:label></TD>
          <td class=midcolora colSpan=2></TD></TR>
        <tr>
			<TD colSpan="4" height="10"></TD>
		</tr>
        <tr>
          <td class=midcolora width="18%"><asp:label id=capTRANSACTION_CODE Runat="server"></asp:label><span 
            class=mandatory>*</SPAN> </TD>
          <td class=midcolora width="32%"><asp:dropdownlist id=cmbTRANSACTION_CODE Runat="server"></asp:dropdownlist><br 
            ><asp:requiredfieldvalidator id=rfvTRANSACTION_CODE Runat="server" ControlToValidate="cmbTRANSACTION_CODE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
          <td class=midcolora colSpan=2></TD></TR>
        <tr>
			<TD colSpan="4" height="10"></TD>
		</tr>
        <tr>
          <td class=midcolora width="18%"><asp:label id=capBASIS Runat="server"></asp:label><span 
            class=mandatory>*</SPAN> </TD>
          <td class=midcolora width="32%"><asp:dropdownlist id=cmbBASIS Runat="server"></asp:dropdownlist><br 
            ><asp:requiredfieldvalidator id=rfvBASIS Runat="server" ControlToValidate="cmbBASIS" Display="Dynamic"></asp:requiredfieldvalidator></TD>
          <td class=midcolora width="18%"><asp:label id=capVALUE Runat="server"></asp:label><span 
            class=mandatory>*</SPAN> </TD>
          <td class=midcolora width="32%"><asp:textbox id=txtVALUE Runat="server" MaxLength="8" size="12"></asp:textbox><br 
            ><asp:requiredfieldvalidator id=rfvVALUE Runat="server" ControlToValidate="txtVALUE" Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id=rngVALUE Runat="server" ControlToValidate="txtVALUE" Display="Dynamic" Type="Currency" MaximumValue="99999999" MinimumValue="1"></asp:rangevalidator></TD></TR>
        <tr>
          <td class=midcolora width="18%"><asp:label id=capAMOUNT Runat="server"></asp:label></TD>
          <td class=midcolora width="32%"><asp:textbox id=txtAMOUNT Runat="server" MaxLength="10" size="12" ReadOnly="True"></asp:textbox></TD>
          <td class=midcolora colSpan=2></TD></TR>
        <tr>
			<TD colSpan="4" height="10"></TD>
		</tr>
        <tr>
          <td class=midcolora colSpan=2><cmsb:cmsbutton class=clsButton id=btnBack runat="server" Text="Back"></cmsb:cmsbutton></TD>
          <td class=midcolorr colSpan=2><cmsb:cmsbutton class=clsButton id=btnSave runat="server" Text="Save"></cmsb:cmsbutton></TD></TR></TABLE></TD></TR></TABLE><INPUT 
id=hidIS_ACTIVE type=hidden name=hidIS_ACTIVE 
runat="server"> <INPUT id=hidOldData type=hidden name=hidOldData runat="server"> <INPUT id=hidCLAIM_ID type=hidden 
name=hidCLAIM_ID runat="server">
				<INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidRESERVE_BREAKDOWN_ID" type="hidden" name="hidRESERVE_BREAKDOWN_ID" runat="server"> </FORM></DIV>
		<script>
			RefreshWebGrid(1,document.getElementById('hidRESERVE_BREAKDOWN_ID').value,true);			
		</script>


	</body>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyAddFireProt.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyAddFireProt" validateRequest=false %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-FIRE PROTECTION/CLEANING</title>
<meta content="Microsoft Visual Studio 7.0" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>

<script src="/cms/cmsweb/scripts/form.js"></script>

<script language=javascript>
			function AddData()
			{
				document.getElementById('cmbIS_SMOKE_DETECTOR').focus();
				document.getElementById('cmbIS_SMOKE_DETECTOR').options.selectedIndex = -1;
				document.getElementById('cmbIS_PROTECTIVE_MAT_FLOOR').options.selectedIndex = -1;
				document.getElementById('cmbIS_PROTECTIVE_MAT_WALLS').options.selectedIndex = -1;
				document.getElementById('txtPROT_MAT_SPACED').value  = '';
				document.getElementById('txtSTOVE_SMOKE_PIPE_CLEANED').value  = '';
				document.getElementById('txtSTOVE_CLEANER').value  = '';
				document.getElementById('txtREMARKS').value  = '';
				DisableValidators();
				ChangeColor();
			}
			function populateXML()
			{
			    //alert(document.getElementById('hidOldData').value);
				if(document.getElementById('hidFormSaved').value == '0')
				{
	
					if(document.getElementById('hidOldData').value!="")
					{
						populateFormData(document.getElementById('hidOldData').value,POL_HOME_OWNER_FIRE_PROT_CLEAN);
					}    
		       
					else
					{
						 AddData();
					}
				}
				return false;
			}
			
			// set the max lenght 
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 255 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}
		</script>
</HEAD>
<BODY leftMargin=0 topMargin=0 onload=populateXML();ApplyColor();>
<FORM id=POL_HOME_OWNER_FIRE_PROT_CLEAN method=post 
runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TR>
    <TD>
      <TABLE width="100%" align=center border=0>
        <tr>
          <TD class=pageHeader colSpan=4><webcontrol:workflow id=myWorkFlow runat="server"></webcontrol:WorkFlow>Please 
            note that all fields marked with * are mandatory </TD></TR>
        <tr>
          <td class=midcolorc align=right colSpan=4><asp:label id=lblMessage runat="server" Visible="False" CssClass="errmsg"></asp:label></TD></TR>
        <tr>
          <TD class=midcolora width="18%"><asp:label id=capIS_SMOKE_DETECTOR runat="server">Is there a smoke detector in the dwelling?Detector</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:dropdownlist id=cmbIS_SMOKE_DETECTOR onfocus="SelectComboIndex('cmbIS_SMOKE_DETECTOR')" runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='N'>No</asp:ListItem>
										<asp:ListItem Value='Y'>Yes</asp:ListItem>
									</asp:DropDownList></TD>
          <TD class=midcolora width="18%"><asp:label id=capIS_PROTECTIVE_MAT_FLOOR runat="server">Is there protective material on the floor Mat Floor</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:dropdownlist id=cmbIS_PROTECTIVE_MAT_FLOOR onfocus="SelectComboIndex('cmbIS_PROTECTIVE_MAT_FLOOR')" runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='N'>No</asp:ListItem>
										<asp:ListItem Value='Y'>Yes</asp:ListItem>
									</asp:DropDownList></TD></TR>
        <tr>
          <TD class=midcolora width="18%"><asp:label id=capIS_PROTECTIVE_MAT_WALLS runat="server">Is there protective material on the walls</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:dropdownlist id=cmbIS_PROTECTIVE_MAT_WALLS onfocus="SelectComboIndex('cmbIS_PROTECTIVE_MAT_WALLS')" runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='N'>No</asp:ListItem>
										<asp:ListItem Value='Y'>Yes</asp:ListItem>
									</asp:DropDownList></TD>
          <TD class=midcolora width="18%"><asp:label id=capPROT_MAT_SPACED runat="server">Describe protective material and indicate whether it is spaced and if so how far.</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:textbox id=txtPROT_MAT_SPACED runat="server" Width="272px" Height="67px" TextMode="MultiLine" maxlength="255" size="30"></asp:textbox><br 
            ><asp:customvalidator id=csvPROT_MAT_SPACED Display="Dynamic" Runat="server" ControlToValidate="txtPROT_MAT_SPACED" ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator></TD></TR>
        <tr>
          <TD class=headerEffectSystemParams colSpan=4 
            >Cleaning</TD></TR>
        <tr>
          <TD class=midcolora width="18%"><asp:label id=capSTOVE_SMOKE_PIPE_CLEANED runat="server">How often is the stove chimney and stove/smoke pipe cleaned and inspected?</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:textbox id=txtSTOVE_SMOKE_PIPE_CLEANED runat="server" maxlength="20" size="22"></asp:textbox></TD>
          <TD class=midcolora width="18%"><asp:label id=capSTOVE_CLEANER runat="server">Who performs the cleaning/inspection?</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:textbox id=txtSTOVE_CLEANER runat="server" maxlength="20" size="22"></asp:textbox></TD></TR>
        <tr>
          <TD class=midcolora width="18%"><asp:label id=capREMARKS runat="server">Remarks</asp:Label></TD>
          <TD class=midcolora width="32%"><asp:textbox id=txtREMARKS runat="server" size="30" Rows="4" TextMode="MultiLine" maxlength="255"></asp:textbox><br 
            ><asp:customvalidator id=csvREMARKS Display="Dynamic" Runat="server" ControlToValidate="txtREMARKS" ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator></TD>
          <TD class=midcolora colSpan=2></TD></TR>
        <tr>
          <td class=midcolora colSpan=2><cmsb:cmsbutton class=clsButton id=btnReset runat="server" Text="Reset"></cmsb:cmsbutton></TD>
          <td class=midcolorr colSpan=2><cmsb:cmsbutton class=clsButton id=btnSave runat="server" Text="Save"></cmsb:cmsbutton></TD></TR></TABLE></TD></TR></TABLE><INPUT 
id=hidFormSaved type=hidden value=0 name=hidFormSaved 
runat="server"> <INPUT id=hidOldData type=hidden name=hidOldData runat="server"> <INPUT id=hidFUEL_ID type=hidden value=0 
name=hidFUEL_ID runat="server"> <INPUT id=hidPOL_ID 
type=hidden name=hidPOL_ID runat="server"> <INPUT 
id=hidPOL_VERSION_ID type=hidden name=hidPOL_VERSION_ID 
runat="server"> <INPUT id=hidCUSTOMER_ID type=hidden name=hidCUSTOMER_ID runat="server"> </FORM>
<script>
 //  RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFUEL_ID').value, false);
		</script>

	</BODY>
</HTML>

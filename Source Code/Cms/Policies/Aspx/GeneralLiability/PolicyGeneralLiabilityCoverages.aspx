<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyGeneralLiabilityCoverages.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.GeneralLiability.PolicyGeneralLiabilityCoverages" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GeneralLiabilityCoverages</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
			function ResetForm()
			{
				DisableValidators();
				ChangeColor();
				document.GENERAL_LIABILITY_COVERAGES.reset();
				return false;			
			}
			
			function Initialise()
			{
				document.getElementById("txtAGGREGATE_L").readOnly=true;
				document.getElementById("txtAGGREGATE_O").readOnly=true;
				document.getElementById("txtAGGREGATE_TOTAL").readOnly=true;
				document.getElementById("txtCOVERAGE_M_EA").readOnly=true;
			
			}
			function CalculateTotalAggregate()
			{
				var aggL,aggO,covM_EP,covM_EA,totalAggregate;
				if(document.getElementById("txtAGGREGATE_L").value != "")
				{
					aggL=document.getElementById("txtAGGREGATE_L").value;
				}
				else
				{
					aggL=0;
				}
				if(document.getElementById("txtAGGREGATE_O").value != "")
				{
					aggO=document.getElementById("txtAGGREGATE_O").value;
				}
				else
				{
					aggO=0;
				}
				
				if(document.getElementById("cmbCOVERAGE_M_EP").selectedIndex > 0)
				{
					covM_EP=document.getElementById("cmbCOVERAGE_M_EP").item(document.getElementById("cmbCOVERAGE_M_EP").selectedIndex).text;
					covM_EA=document.getElementById("txtCOVERAGE_M_EA").value;
				}
				else
				{
					covM_EP=0;
					covM_EA=0;
				}
				
				totalAggregate=parseInt(aggL)+(parseInt(aggO)+parseInt(covM_EP) *2) + (parseInt(covM_EA) * 2) ;
				if(totalAggregate > 0)
					document.getElementById("txtAGGREGATE_TOTAL").value=totalAggregate;
				else
					document.getElementById("txtAGGREGATE_TOTAL").value="";
			
			}
			function Coverage_L_Change()
			{
				var coverage;
				if(document.getElementById("cmbCOVERAGE_L").selectedIndex <= 0)
				{
					document.getElementById("txtAGGREGATE_L").value="";
				}
				else
				{
					coverage = document.getElementById("cmbCOVERAGE_L").item(document.getElementById("cmbCOVERAGE_L").selectedIndex).text;
					document.getElementById("txtAGGREGATE_L").value=coverage * 2;
				}
				CalculateTotalAggregate();
			}
			
			function Coverage_O_Change()
			{
				var coverage;
				if(document.getElementById("cmbCOVERAGE_O").selectedIndex <= 0)
				{
					document.getElementById("txtAGGREGATE_O").value="";
				}
				else
				{
					coverage = document.getElementById("cmbCOVERAGE_O").item(document.getElementById("cmbCOVERAGE_O").selectedIndex).text;
					document.getElementById("txtAGGREGATE_O").value=coverage * 2;
				}
				CalculateTotalAggregate();
			}
			
			function COVERAGE_M_EP_Change()
			{
				if(document.getElementById("cmbCOVERAGE_M_EP").selectedIndex > 0)
					document.getElementById("txtCOVERAGE_M_EA").value=document.GENERAL_LIABILITY_COVERAGES.hidCOVERAGE_M_EA.value;
				else
					document.getElementById("txtCOVERAGE_M_EA").value="";
				CalculateTotalAggregate();
			}
			
			
		
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Initialise();">
		<FORM id="GENERAL_LIABILITY_COVERAGES" method="post" runat="server">
			<DIV id="myTSMain1" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="32%"><asp:label id="capCOVERALE_L" runat="server"></asp:label></TD>
								<TD class="midcolora" width="28%"><asp:dropdownlist id="cmbCOVERAGE_L" Runat="server" OnChange="Coverage_L_Change();" Width="60"></asp:dropdownlist></TD>
								<td class="midcolora" width="12%"><asp:label id="capAGGREGATE_L" Runat="server"></asp:label></td>
								<td class="midcolora" width="28%"><asp:textbox id="txtAGGREGATE_L" Runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<TD class="midcolora" width="32%"><asp:label id="capCOVERAGE_O" runat="server"></asp:label></TD>
								<TD class="midcolora" width="28%"><asp:dropdownlist id="cmbCOVERAGE_O" Runat="server" OnChange="Coverage_O_Change();" Width="60"></asp:dropdownlist></TD>
								<td class="midcolora" width="12%"><asp:label id="capAGGREGATE_O" Runat="server"></asp:label></td>
								<td class="midcolora" width="28%"><asp:textbox id="txtAGGREGATE_O" Runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<TD class="midcolora" width="32%"><asp:label id="capCOVERAGE_M_EP" runat="server"></asp:label></TD>
								<TD class="midcolora" width="28%"><asp:dropdownlist id="cmbCOVERAGE_M_EP" Runat="server" OnChange="COVERAGE_M_EP_Change();" Width="60"></asp:dropdownlist></TD>
								<td class="midcolora" width="12%"><asp:label id="capAGGREGATE_TOTAL" Runat="server"></asp:label></td>
								<td class="midcolora" width="28%"><asp:textbox id="txtAGGREGATE_TOTAL" Runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<TD class="midcolora" width="32%"><asp:label id="capCOVERAGE_M_EA" runat="server"></asp:label></TD>
								<TD class="midcolora" width="28%" colSpan="3"><asp:textbox id="txtCOVERAGE_M_EA" Runat="server" Width="60"></asp:textbox></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<input id="hidCOVERAGE_M_EA" type="hidden" name="hidCOVERAGE_M_EA" runat="server">
		</FORM>
	</BODY>
</HTML>

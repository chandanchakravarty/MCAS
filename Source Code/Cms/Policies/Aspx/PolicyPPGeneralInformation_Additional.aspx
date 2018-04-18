<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" validateRequest="false" Codebehind="PolicyPPGeneralInformation_Additional.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyPPGeneralInformation_Additional" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
    <title>POL_UNDERWRITING_TIER</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/Calendar.js"></script>
	<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";				
	
			function AddData()
			{								
				document.getElementById('hidCUSTOMER_ID').value	=	'New';
				document.getElementById('txtUNDERWRITING_TIER').focus();	
							
				if(document.getElementById('hidAppEffDate').value!='' || document.getElementById('hidAppEffDate').value!='0')			
					document.getElementById('txtUNTIER_ASSIGNED_DATE').value= document.getElementById('hidAppEffDate').value; 
				else document.getElementById('txtUNTIER_ASSIGNED_DATE').value='';
				
				if(document.getElementById('PanelHide'))
				{			
					document.getElementById('chkCAP_INC').checked=false;
					document.getElementById('txtMAX_RATE_INC').value= '';				
					document.getElementById('chkCAP_DEC').checked=false;
					document.getElementById('txtMAX_RATE_DEC').value= ''; 			
					document.getElementById('txtCAP_RATE_CHANGE_REL').value= '';			
					document.getElementById('txtCAP_MIN_MAX_ADJUST').value= ''; 			
					document.getElementById('txtACL_PREMIUM').value= ''; 			
				}
				
				ChangeColor();
				DisableValidators();			
			
				return false;
			}

			function populateXML()
			{
				if(document.getElementById('hidFormSaved')!=null && document.getElementById('hidFormSaved').value == '0' ||document.getElementById('hidFormSaved').value == '1')
				{				
					var tempXML;				
					
					if(document.getElementById('hidOldData').value!="")
					{				
						tempXML=document.getElementById('hidOldData').value;						
						populateFormData(tempXML,POL_UNDERWRITING_TIER);	
						if(document.getElementById('PanelHide'))
						{
							document.getElementById('txtACL_PREMIUM').value=formatCurrency(document.getElementById('txtACL_PREMIUM').value);
						}
					}
					else
					{
						AddData();
					}
				}				
				return false;
			}		
	
		</script>
  </head>
  <BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="ApplyColor();populateXML();ChangeColor();">
	
		<div class="pageContent" id="bodyHeight">
			<FORM id="POL_UNDERWRITING_TIER" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
					<tr id="messageID">
						<td align="center" colSpan="3"><asp:label id="capMessage" Visible="False" CssClass="errmsg" Runat="server"></asp:label></td>
					</tr>
					<TR id="trMessage" runat="server">
						<TD>
							<TABLE class="tableWidthHeader" id="mainTable" align="center" border="0">
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow isTop="false" id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
								</tr>
								<!--
								<tr>
									<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></TD>
								</tr>								
								-->
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capUNDERWRITING_TIER" runat="server"></asp:label></TD>									
									<TD class="midcolora" width="32%"><asp:textbox id="txtUNDERWRITING_TIER" runat="server" size="3" MaxLength="3" style="text-transform:uppercase"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revUNDERWRITING_TIER" runat="server" Display="Dynamic" ControlToValidate="txtUNDERWRITING_TIER"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capUNTIER_ASSIGNED_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtUNTIER_ASSIGNED_DATE" runat="server" size="12" MaxLength="10"></asp:textbox>
									<asp:hyperlink id="hlkCalandarDate" runat="server" CssClass="HotSpot" Display="Dynamic">
									<asp:image id="imgCalenderExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" valign="middle" Display="Dynamic"></asp:image>
									</asp:hyperlink><br>
									<asp:regularexpressionvalidator id="revUNTIER_ASSIGNED_DATE" runat="server" Display="Dynamic" ControlToValidate="txtUNTIER_ASSIGNED_DATE"></asp:regularexpressionvalidator></TD>
								</tr>
								<asp:panel id="PanelHide" Runat="server" visible="false">
								<tr>
									<TD class="pageHeader" colSpan="4">Rate Cap Calculation</TD>
								</tr>								
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capCAP_INC" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:checkbox id="chkCAP_INC" runat="server" onChange="" ></asp:checkbox></TD>
									<TD class="midcolora" width="32%"><asp:label id="capMAX_RATE_INC" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMAX_RATE_INC" runat="server" size="6" MaxLength="3" readonly="true"></asp:textbox></TD>									
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capCAP_DEC" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:checkbox id="chkCAP_DEC" runat="server" onChange="" ></asp:checkbox></TD>
									<TD class="midcolora" width="32%"><asp:label id="capMAX_RATE_DEC" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMAX_RATE_DEC" runat="server" size="6" MaxLength="3" readonly="true"></asp:textbox></TD>									
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4">Relativity Factors</TD>
								</tr>		
								<tr> 
									<TD class="midcolora" width="32%"><asp:label id="capCAP_RATE_CHANGE_REL" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtCAP_RATE_CHANGE_REL" runat="server" size="6" MaxLength="3"></asp:textbox></TD>
									<TD class="midcolora" width="32%"><asp:label id="capCAP_MIN_MAX_ADJUST" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtCAP_MIN_MAX_ADJUST" runat="server" size="6" MaxLength="3"></asp:textbox></TD>																		
								</tr>
								<tr> 
									<TD class="midcolora" width="32%"><asp:label id="capACL_PREMIUM" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtACL_PREMIUM" runat="server" size="6" MaxLength="3" onblur="this.value=formatCurrency(this.value)"></asp:textbox></TD>
									<TD class="midcolora" colspan="2"></TD>																											
								</tr>			
								</asp:panel>
								<tr>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="2"></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<tr>
									<td><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
										<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">																		
										<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
										<INPUT id="hidAppEffDate" type="hidden" value="0" name="hidAppEffDate" runat="server">									
									</td>
								</tr>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</div>
	</BODY>
</html>

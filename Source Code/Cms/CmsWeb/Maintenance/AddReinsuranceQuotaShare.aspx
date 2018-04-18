<%@ Page language="c#" Codebehind="AddReinsuranceQuotaShare.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsuranceQuotaShare" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>AddReinsuranceQuotaShare</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
		<script language='javascript'>
			function AddData()
			{	
				ChangeColor();
				DisableValidators();
				document.getElementById('hidEXCESS_ID').value	=	'New';
				document.getElementById('txtPARTICIPATION_AMOUNT').focus();
				document.getElementById('txtPARTICIPATION_AMOUNT').value  = '';
				document.getElementById('txtPRORATA_AMOUNT').value  = '';
				document.getElementById('txtLAYER_PREMIUM').value  = '';
				document.getElementById('txtCEDING_COMMISSION').value  = '';
				document.getElementById('txtAC_PREMIUM').value  = '';
			}
			
			function populateXML()
			{ 
				if(document.getElementById('hidFormSaved').value == '0')
				{
					var tempXML;
					if(this.parent.strXML != '')
					{
						tempXML = this.parent.strXML;
					}
					if(tempXML != "" && tempXML != null)
					{
						//document.getElementById('btnReset').style.display='none';
						tempXML=top.frames[1].strXML;
						
						//Storing the XML in hidRowId hidden fields 
						document.getElementById('hidOldData').value		=	 tempXML;
						
						//populateFormData(tempXML,MNT_REINSURANCE_CONTRACT);
					}
					else
					{
						AddData();
					}
				}
				return false;
			}
			
			function Validate(objSource , objArgs)
			{	
				var comm = parseFloat(document.getElementById('txtCEDING_COMMISSION').value);
				if(comm < 0 || comm > 100)
				{
					document.getElementById('txtCEDING_COMMISSION').select();
					objArgs.IsValid=false;
				}
				else
					objArgs.IsValid=true;
			}
			
			function formatCurrencyOnLoad()
			{
				document.getElementById('txtPARTICIPATION_AMOUNT').value	= formatCurrency(document.getElementById('txtPARTICIPATION_AMOUNT').value);	
				document.getElementById('txtPRORATA_AMOUNT').value			= formatCurrency(document.getElementById('txtPRORATA_AMOUNT').value);
				document.getElementById('txtLAYER_PREMIUM').value			= formatCurrency(document.getElementById('txtLAYER_PREMIUM').value);
				document.getElementById('txtAC_PREMIUM').value				= formatCurrency(document.getElementById('txtAC_PREMIUM').value);
				return false;
			}
			
		</script>
  </head>
  <body leftMargin='0' topMargin='0' onload="populateXML();ApplyColor();formatCurrencyOnLoad();">	
    <form id="MNT_REINSURANCE_PRORATA" method="post" runat="server">
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
			 <TBODY>
				<tr>
					<td>
						<table align='center' width="100%" border='0'>
						<TBODY>
							<tr>
								<td class="pageHeader" colSpan="4">Please note that all fields marked with * are mandatory</td>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capPARTICIPATION_AMOUNT" runat="server">Participation Amount</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtPARTICIPATION_AMOUNT" runat="server" CssClass="INPUTCURRENCY" size="20" maxlength="9" onblur="this.value=formatAmount(this.value);">
									</asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvPARTICIPATION_AMOUNT" ControlToValidate="txtPARTICIPATION_AMOUNT" Runat="server" ErrorMessage="PARTICIPATION_AMOUNT can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revPARTICIPATION_AMOUNT" Display="Dynamic" ControlToValidate="txtPARTICIPATION_AMOUNT" Runat="server"></asp:regularexpressionvalidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capPRORATA_AMOUNT" runat="server">Prorata Amount</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtPRORATA_AMOUNT" runat="server" CssClass="INPUTCURRENCY" size="20" maxlength="9" onblur="this.value=formatAmount(this.value);">
									</asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvPRORATA_AMOUNT" ControlToValidate="txtPRORATA_AMOUNT" Runat="server" ErrorMessage="PRORATA_AMOUNT can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revPRORATA_AMOUNT" Display="Dynamic" ControlToValidate="txtPRORATA_AMOUNT" Runat="server"></asp:regularexpressionvalidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capLAYER_PREMIUM" runat="server">Layer Premium</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtLAYER_PREMIUM" runat="server" CssClass="INPUTCURRENCY" size="20" maxlength="9" onblur="this.value=formatAmount(this.value);"></asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvLAYER_PREMIUM" ControlToValidate="txtLAYER_PREMIUM" Runat="server" ErrorMessage="LAYER_PREMIUM can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revLAYER_PREMIUM" Display="Dynamic" ControlToValidate="txtLAYER_PREMIUM" Runat="server"></asp:regularexpressionvalidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capCEDING_COMMISSION" runat="server">Ceding Commission</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtCEDING_COMMISSION" runat="server" size="10" maxlength="5"></asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvCEDING_COMMISSION" ControlToValidate="txtCEDING_COMMISSION" Runat="server" ErrorMessage="CEDING_COMMISSION can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revCEDING_COMMISSION" Display="Dynamic" ControlToValidate="txtCEDING_COMMISSION" Runat="server"></asp:regularexpressionvalidator>
									<asp:CustomValidator ID="csvCEDING_COMMISSION" Runat=server ControlToValidate="txtCEDING_COMMISSION" ClientValidationFunction="Validate" Display=Dynamic ></asp:CustomValidator>
								</td>
							</tr>							
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capAC_PREMIUM" runat="server">A/C Premium</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtAC_PREMIUM" runat="server" CssClass="INPUTCURRENCY" size="20" maxlength="9" onblur="this.value=formatAmount(this.value);"></asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvAC_PREMIUM" ControlToValidate="txtAC_PREMIUM" Runat="server" ErrorMessage="AC_PREMIUM can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									<asp:regularexpressionvalidator id="revAC_PREMIUM" Display="Dynamic" ControlToValidate="txtAC_PREMIUM" Runat="server"></asp:regularexpressionvalidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" colspan="1">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="false"></cmsb:cmsbutton></td>
								<td class="midcolorr" colspan="3">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>									
								</td>
							</tr>									
						</tbody>
						</table>
					</td>
				</tr>   				
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
				<INPUT id="hidEXCESS_ID" type="hidden" value="0" name="hidEXCESS_ID" runat="server">
				<INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">
				<INPUT id="hidLAYER_TYPE" type="hidden" value="0" name="hidLAYER_TYPE" runat="server">
			</tbody>
		</table>
     </form>    
     <script language="javascript">
		RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidEXCESS_ID').value);
	</script>                                                        	
  </body>
</html>

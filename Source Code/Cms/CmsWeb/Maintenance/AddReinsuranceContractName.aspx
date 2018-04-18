<%@ Page language="c#" Codebehind="AddReinsuranceContractName.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsuranceContractName" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>AddReinsuranceContractName</title>
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
				document.getElementById('hid_CONTRACT_NAME_ID').value	=	'New';
				document.getElementById('txtCONTRACT_NAME').focus();
				document.getElementById('txtCONTRACT_NAME').value  = '';
				document.getElementById('txtCONTRACT_DESCRIPTION').value  = '';
			}
			
			function populateXML()
			{ 
				if(document.getElementById('hidFormSaved').value == '0')
				{
					var tempXML;
					if(top.frames[1].strXML!="")
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
	</script>
  </head>
  <body leftMargin='0' topMargin='0' onload="populateXML();ApplyColor();">	
    <form id="MNT_REINSURANCE_CONTRACTNAME" method="post" runat="server">
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
									<asp:Label id ="capCONTRACT_NAME" runat="server">Contract Name</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtCONTRACT_NAME" runat="server" size="35" maxlength="150"> 
									</asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvCONTRACT_NAME" ControlToValidate="txtCONTRACT_NAME" Runat="server" ErrorMessage="CONTRACT_NAME can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>									
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%">
									<asp:Label id ="capCONTRACT_DESCRIPTION" runat="server">Contract Description</asp:Label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%" colSpan="3">
									<asp:textbox id="txtCONTRACT_DESCRIPTION" runat="server" TextMode="MultiLine" Rows="5" Width="30%" MaxLength="480">
									</asp:textbox><br>
									<asp:RequiredFieldValidator ID="rfvCONTRACT_DESCRIPTION" ControlToValidate="txtCONTRACT_DESCRIPTION" Runat="server" ErrorMessage="CONTRACT_DESCRIPTION can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>									
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
				<INPUT id="hid_CONTRACT_NAME_ID" type="hidden" value="0" name="hid_CONTRACT_NAME_ID" runat="server">
			</tbody>
		</table>	
     </form>        	
     <script language="javascript">
		RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hid_CONTRACT_NAME_ID').value);
	</script>
  </body>
</html>

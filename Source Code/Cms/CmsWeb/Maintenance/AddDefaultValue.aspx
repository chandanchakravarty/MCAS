<%@ Page language="c#" Codebehind="AddDefaultValue.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddDefaultValue" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddDefaultValue</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
			//This function is used for refreshing the form 
			//To be called in while user clicks on Reset button 
			//and also after the form get saved
			function AddData()
			{
				//alert('sdfsd')
				ChangeColor();
				document.getElementById('txtDEFV_ENTITY_NAME').value				='';
				document.getElementById('txtDEFV_VALUE').value						='';
					
				document.getElementById('hidDEFV_ID').value							='NEW';
				document.getElementById('hidFormSaved').value						='0';
				
				DisableValidators();				
			}

		
		
		
		//This function will be called from the grid object while user double clicks
			//on any record in Grid
			
			function populateXML()
			{
					//alert(document.getElementById('hidFormSaved').value)
				/*
				if(document.getElementById('hidFormSaved').value == '0')
				{
					DisableValidators();
					var tempXML;			
					//alert(top.frames[1].strXML)
					if(top.frames[1].strXML!="")
					{
						//document.getElementById('btnReset').style.display='none';
						
						tempXML=top.frames[1].strXML;
						
						//Storing the XML in hidOldData hidden fields 
						document.getElementById('hidOldData').value		=	 tempXML;
						populateFormData(tempXML,Form1);
						*/
var tempXML = document.getElementById('hidOldData').value;
//alert(tempXML);
if(document.getElementById('hidFormSaved').value == '0')
{
var tempXML;
//Enabling the activate deactivate button
if(tempXML!="")
{
//document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
populateFormData(tempXML,Form1);				
					}
					else
					{
						AddData();
						
					}
				}
				else
				{
					DisableValidators();					
					
			
				}				
				return false;
			}
			
			
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 200 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}
		</script>
	</HEAD>
	<body class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" align="center" border="0">
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4">Please note that all fields marked 
						with * are mandatory
					</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="center" width="100%" colSpan="4">
						<asp:label class="errmsg" id="lblMessage" Runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td width="50%" colspan="2" class="midcolora">
						<asp:Label ID="capDEFV_ENTITY_NAME" Runat="server"></asp:Label><span class="mandatory">*</span>
					</td>
					<td width="50%" colspan="2" class="midcolora">
						<asp:TextBox ID="txtDEFV_ENTITY_NAME" Runat="server" MaxLength="50"></asp:TextBox><br>
						<asp:RequiredFieldValidator ID="rfvDEFV_ENTITY_NAME" Runat="server" Display="Dynamic" ControlToValidate="txtDEFV_ENTITY_NAME"></asp:RequiredFieldValidator>
					</td>
				</tr>
				<tr>
					<td width="50%" colspan="2" class="midcolora">
						<asp:Label ID="capDEFV_VALUE" RunaT="server"></asp:Label><span class="mandatory">*</span>
					</td>
					<td width="50%" colspan="2" class="midcolora">
						<asp:TextBox ID="txtDEFV_VALUE" Runat="server"  Columns=70 Rows=7   TextMode="MultiLine"></asp:TextBox><br>
						<asp:RequiredFieldValidator ID="rfvDEFV_VALUE" Runat="server" Display="Dynamic" ControlToValidate="txtDEFV_VALUE"></asp:RequiredFieldValidator>
						<asp:CustomValidator ID="csvDEFV_VALUE" Runat="server" ControlToValidate="txtDEFV_VALUE" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator>
					</td>
				</tr>
				<tr>
					<td class="midcolora" width="50%" colSpan="2">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" causesValidation="false" Text="Reset"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr" width="50%" colSpan="2">
						<cmsb:cmsbutton class="clsButton" id="btnSave" tabIndex="1" Runat="server" text="Save"></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidDEFV_ID" type="hidden" name="hidDEFV_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
		</form>
		<script>
			RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDEFV_ID').value);
		</script>
	</body>
</HTML>

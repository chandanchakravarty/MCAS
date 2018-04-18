<%@ Page language="c#" Codebehind="AddLookup.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddLookup" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>MNT_LOOKUP_VALUES</title>
<%--		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR' />
		<meta content='C#' name='CODE_LANGUAGE' />
		<meta content='JavaScript' name='vs_defaultClientScript' />
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema' />--%>
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet' />
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" language='javascript'>
function AddData()
{
	ChangeColor();
	DisableValidators();
	document.getElementById('hidLOOKUP_UNIQUE_ID').value = 'New';
	try {
	    document.getElementById('txtLOOKUP_VALUE_CODE').focus();
	}
	catch (err)
	{ }
	document.getElementById('txtLOOKUP_VALUE_CODE').value  = '';
	document.getElementById('txtLOOKUP_VALUE_DESC').value  = '';
	if(document.getElementById('btnActivateDeactivate'))
	document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
}
function populateXML()
{
		if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{
					var tempXML='';
					tempXML=document.getElementById('hidOldData').value;
					if(tempXML!="" && tempXML!=0)
					{						
						if (document.getElementById('hidLookUpValue_Desc').value != "0" || document.getElementById('hidCalledForEnter').value == "true" )
						{													
							document.getElementById('lblLooKUpDesc').style.display="inline";
							document.getElementById('cmbLOOKUP_ID').style.display="none";
						}
						else 
						{
							document.getElementById('lblLooKUpDesc').style.display="none";
							document.getElementById('cmbLOOKUP_ID').style.display="inline";
						}
						populateFormData(tempXML,MNT_LOOKUP_VALUES);
						
					}
					else
					{
						if (document.getElementById('hidCalledForEnter').value == "true")
						{						
							document.getElementById('lblLooKUpDesc').style.display="inline";
							document.getElementById('cmbLOOKUP_ID').style.display="none";
						}
						else
						{
							document.getElementById('lblLooKUpDesc').style.display="none";
							document.getElementById('cmbLOOKUP_ID').style.display="inline";
						}
						AddData();
					}
				}
			return false;

}
		</script>
	</head>
	<body oncontextmenu = "javascript:return false;" style="margin-left:0; margin-top:0" onload="javascript:populateXML();ApplyColor();">
		<form id='MNT_LOOKUP_VALUES' method='post' runat='server'>
			<table cellspacing='0' cellpadding='0' width='100%' border='0'>
				<tr>
					<td>
						<table width='100%' border='0' align='center'>
							<tbody>
								<tr>
									<td class="pageHeader" colspan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></td>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colspan="4">
										<asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
									</td>
								</tr>
								<tr>
									<td class='midcolora' width='18%'>
										<asp:Label ID="capLOOKUP_DESCRIPTION" Runat="server">LOOKUP DESCRIPTION</asp:Label><span class="mandatory">*</span>
									</td>
									<td class='midcolora' width='32%'>
										<asp:Label ID="lblLooKUpDesc" Runat="server"></asp:Label>
										<asp:DropDownList ID="cmbLOOKUP_ID" Runat="server"></asp:DropDownList>
										<br />
										<asp:RequiredFieldValidator ID="rfvLOOKUP_ID" ControlToValidate="cmbLOOKUP_ID" Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
									</td>
									<td class='midcolora' width='50%' colspan="2">&nbsp;</td>
									<!-- <td class='midcolora' width='18%'>
										<asp:Label ID="capLOOKUP_NAME" Runat="server">CATEGORY CODE</asp:Label></td>
									<td class='midcolora' width='32%'>
										<asp:Label ID="lblCategoryCode" Runat="server"></asp:Label></td> -->
								</tr>
								<tr>
									<td class='midcolora' width='18%'>
										<asp:Label id="capLOOKUP_VALUE_CODE" runat="server">LOOKUP VALUE CODE</asp:Label><span class="mandatory">*</span></td>
									<td class='midcolora' width='32%'>
										<asp:textbox id='txtLOOKUP_VALUE_CODE' runat='server' size='10' maxlength='8'></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvLOOKUP_VALUE_CODE" runat="server" ControlToValidate="txtLOOKUP_VALUE_CODE"
											ErrorMessage="LOOKUP_VALUE_CODE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
									<td class='midcolora' width='18%'>
										<asp:Label id="capLOOKUP_VALUE_DESC" runat="server">DESCRIPTION</asp:Label><span class="mandatory">*</span></td>
									<td class='midcolora' width='32%'>
										<asp:textbox id='txtLOOKUP_VALUE_DESC' runat='server' size='50' maxlength='0'></asp:textbox>
										<a class="calcolora" href="#">
										    <img id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif" border="0" autopostback="false" runat="server" />
										</a>
										<br />
										<asp:requiredfieldvalidator id="rfvLOOKUP_VALUE_DESC" runat="server" ControlToValidate="txtLOOKUP_VALUE_DESC"
											ErrorMessage="LOOKUP_VALUE_DESC can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
								</tr>
								<tr>
									<td class='midcolora' colspan='2'>
										<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'
											CausesValidation="False"></cmsb:cmsbutton>
									</td>
									<td class='midcolorr' colspan="2">
										<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
									</td>
								</tr>
								<tr>
								<td>
								<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
								<input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server" />
								<input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server" />
								<input id="hidLOOKUP_UNIQUE_ID" type="hidden" value="0" name="hidLOOKUP_UNIQUE_ID" runat="server" />
								<input id="hidLookUp_ID" type="hidden" value="0" name="hidLookUp_ID" runat="server" />
								<input id="hidLookUp_Name" type="hidden" value="0" name="hidLookUp_Name" runat="server" />
								<input id="hidLookUp_Desc" type="hidden" value="0" name="hidLookUp_Desc" runat="server" />
								<input id="hidLookUpValue_Desc" type="hidden" value="0" name="hidLookUpValue_Desc" runat="server" />
								<input id="hidCalledForEnter" type="hidden" value="0" name="hidCalledForEnter" runat="server" />
								</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script type="text/javascript" language="javascript">
		if (document.getElementById('hidCalledForEnter').value != "true")
		{
			RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLOOKUP_UNIQUE_ID').value);
		}
        //Added by Charles on 3-Jun-10 for Itrack 19
		function refreshFromPopUp() {		   
		   RefreshWindowsGrid(1, document.getElementById('hidLOOKUP_UNIQUE_ID').value);		    
		}
		</script>
	</body>
</html>
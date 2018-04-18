<%@ Page language="c#" Codebehind="AddReinsuranceContractType.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsuranceContractType" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>AddReinsuranceContractType</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
			function AddData()
			{	
				ChangeColor();
				DisableValidators();
				document.getElementById('hid_CONTRACT_TYPE_ID').value	=	'New';
				//document.getElementById('txtCONTRACT_TYPE').focus();
				//document.getElementById('txtCONTRACT_TYPE').value  = '';
				//Itrack No.2299: Added By Manoj Rathore on 8 Aug 2007
			   if(document.getElementById('btnActivateDeactivate'))
				 document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);	
			}
			
			function populateXML()
			{ 
			
			
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML = document.getElementById('hidOldData').value;
				if(tempXML != "" && tempXML!="0")
				{
					populateFormData(tempXML,MNT_REINSURANCE_CONTRACTTYPE);
					//addCatagories();
				}
				else
				{
					AddData();	
				}
			}
				/*if(document.getElementById('hidFormSaved').value == '1')
				{
					var tempXML;
				
					if(top.frames[1].strXML!="")
					{
						//document.getElementById('btnReset').style.display='none';
						tempXML=top.frames[1].strXML;
						
						//Storing the XML in hidRowId hidden fields 
						//document.getElementById('hidOldData').value		=	 tempXML;
						tempXML=document.getElementById('hidOldData').value;	 
						//if(document.getElementById('btnActivateDeactivate'))
						//	document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);
						alert(tempXML);
						if (tempXML!= '0') 	
							populateFormData(tempXML,MNT_REINSURANCE_CONTRACTTYPE);
					}
					else
					{
						AddData();
					}
				}
				else
				AddData();
				return false;*/
			}
			function showHide()
			{
				if(document.getElementById('RowId').value!='New')
				{
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);  
					//document.getElementById('btnReset').style.display="none";
				}
			}	
			function SaveData()
			{
				/*if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);*/	
			}
			
			function Reset()
			{
				DisableValidators();
				document.MNT_REINSURANCE_CONTRACTTYPE.reset();
				ChangeColor();
				return false;
			}
			function ActivateDeactivate() { 
			 var strdec=document.getElementById('hiddec').value;
			 var stract=document.getElementById('hidact').value;
				if(document.getElementById('hid_CONTRACT_TYPE_ID').value!=	'New' && document.getElementById("btnActivateDeactivate")!=null)//Conditon added by Sibin to remove runtime error when Page right of aspx page is only read - Done on 14 Jan 09
				{
				 if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);
	if (document.getElementById("hidIS_ACTIVE").value != 'N') {
	    document.getElementById("btnActivateDeactivate").value = strdec;    //'Deactivate';
	}
	else
	    document.getElementById("btnActivateDeactivate").value = stract;  // 'Activate';
				}
			}
		</script>
</HEAD>
	<body oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload="populateXML();ApplyColor();">
		<form id="MNT_REINSURANCE_CONTRACTTYPE" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td>
							<table align='center' width="100%" border='0'>
								<TBODY>
									<tr>
										<td class="pageHeader" colSpan="4"><asp:label ID="capMessages" runat="server"></asp:label></td>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
									<tr>
										<td class="midcolora" width="18%">
											<asp:Label id="capCONTRACT_TYPE" runat="server">Contract Type</asp:Label><span class="mandatory">*</span>
										</td>
										<td class="midcolora" width="32%" colSpan="3">
											<asp:textbox id="txtCONTRACT_TYPE" runat="server" size="35" maxlength="25"></asp:textbox><br>
											<asp:RequiredFieldValidator ID="rfvCONTRACT_TYPE" ControlToValidate="txtCONTRACT_TYPE" Runat="server" ErrorMessage="CONTRACT_TYPE can't be blank."
												Display="Dynamic"></asp:RequiredFieldValidator>
										</td>
									</tr>
									<tr>
										<td class="midcolora" colspan="1">
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="false"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" ID="btnActivateDeactivate" Text="Activate/Deactivate" Runat="server"></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" colspan="3">
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
					<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
					<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
					<INPUT id="hid_CONTRACT_TYPE_ID" type="hidden" value="0" name="hid_CONTRACT_NAME_ID" runat="server">
					<INPUT id="hidact" type="hidden" runat="server">
					<INPUT id="hiddec" type="hidden" runat="server">
				</TBODY>
			</table>
		</form>
		<script language="javascript">
		RefreshWindowsGrid(1,document.getElementById('hid_CONTRACT_TYPE_ID').value);
		ActivateDeactivate();
		</script>
	</body>
</HTML>

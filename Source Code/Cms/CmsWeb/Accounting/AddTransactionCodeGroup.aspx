<%@ Page validateRequest=false language="c#" Codebehind="AddTransactionCodeGroup.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.AddTransactionCodeGroup" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_TRAN_CODE_GROUP</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
function AddData()
{
ChangeColor();
DisableValidators();
document.getElementById('hidTRAN_GROUP_ID').value	=	'New';
document.getElementById('cmbSTATE_ID').focus();
//document.getElementById('cmbSTATE_ID').options.selectedIndex = -1;
document.getElementById('cmbLOB_ID').options.selectedIndex = -1;
document.getElementById('cmbSUB_LOB_ID').options.selectedIndex = -1;
document.getElementById('cmbCLASS_RISK').options.selectedIndex = -1;
document.getElementById('chkNEW_BUSINESS').checked = false;
document.getElementById('chkRENEWAL').checked = false;
document.getElementById('chkREINSTATE_SAME_TERM').checked = false;
document.getElementById('chkCHANGE_IN_NEW_BUSINESS').checked = false;
document.getElementById('chkCHANGE_IN_RENEWAL').checked = false;
document.getElementById('chkREINSTATE_NEW_TERM').checked = false;
document.getElementById('chkCANCELLATION').checked = false;
document.getElementById('trProcessTransactionType').style.display = "none";
}
function ResetPage()
{
	//parent.r(0, 0);
	//ResetForm('ACT_TRAN_CODE_GROUP');
	//populateXML();FillSubLOB();populateXML();
	//return false;
	
	document.ACT_TRAN_CODE_GROUP.reset();
	populateXML();
	DisableValidators();
	ChangeColor();
	return false;

}
function populateXML()
{
var tempXML = document.getElementById('hidOldData').value;
//alert(document.getElementById('hidFormSaved').value);
//alert(tempXML);
if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
{
	if(tempXML!="")
	{
		populateFormData(tempXML,ACT_TRAN_CODE_GROUP);
		
	}
	else
	{
		AddData();
	}	
}
document.getElementById('trProcessTransactionType').style.display = "none";
setTab();
return false;
}
function FillSubLOB()
		{
				
			var stID="";
			document.getElementById('cmbSUB_LOB_ID').innerHTML = '';
			var Xml = document.getElementById('hidLOBXML').value;
			//var LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			
			//LOB id is not selected then returning 
			if(document.getElementById('cmbLOB_ID').selectedIndex == -1)
			{
				document.getElementById('hidSUB_LOB_ID').value = '';
				return false;
			}
			var LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
			//Inserting the lobid in hidden control
			//document.getElementById('hidLOBId').value = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			
			var objXmlHandler = new XMLHandler();
			var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
			
			//adding a blank option
			oOption = document.createElement("option");
			oOption.value = "";
			oOption.text = "";
			document.getElementById('cmbSUB_LOB_ID').add(oOption);
				
			//adding a all option
			oOption = document.createElement("option");
			oOption.value = "0";
			oOption.text = "All";
			document.getElementById('cmbSUB_LOB_ID').add(oOption);
			
			if(document.getElementById('cmbLOB_ID').selectedIndex==1)
			{
				document.getElementById('cmbCLASS_RISK').selectedIndex = 0;
				document.getElementById('cmbSUB_LOB_ID').options.selectedIndex = 1;
				document.getElementById('cmbCLASS_RISK').disabled = true;
				return;
			}
			else
			{
				document.getElementById('cmbCLASS_RISK').disabled = false;
			}
			
			for(i=0; i<tree.childNodes.length; i++)
			{
				nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
				stateValue = tree.childNodes[i].getElementsByTagName('STATE_ID');
				if (nodValue != null)
				{
					if (nodValue[0].firstChild == null)
						continue
						
					if ((nodValue[0].firstChild.text == LOBId)&& stateValue[0].firstChild.text==stID)
					{
						
						SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
						SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
						
						if (SubLobId != null && SubLobDesc != null)
						{
							if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
								&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
							{
								oOption = document.createElement("option");
								oOption.value = SubLobId[0].firstChild.text;
								oOption.text = SubLobDesc[0].firstChild.text;
								document.getElementById('cmbSUB_LOB_ID').add(oOption);
								////alert(oOption.value);
								////alert(oOption.text);
							}
						}
					}
				}
			}
			document.getElementById('cmbSUB_LOB_ID').selectedIndex=-1;
		}
		function setHidSubLob()
		{
					document.getElementById('hidSUB_LOB_ID').value = document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value;
				}
		function setTab()
		{
			if (document.getElementById('hidOldData').value	!= '')
			{			
				Url="AddTransactionCodeGroupDetails.aspx?";
				DrawTab(2,top.frames[1],'Transaction Code Group Details',Url);	
			}
			else
			{		
				RemoveTab(2,top.frames[1]);					
			}
		}
		function Validate()
		{
		if(document.getElementById('chkNEW_BUSINESS').checked 
			|| document.getElementById('chkRENEWAL').checked 
			|| document.getElementById('chkREINSTATE_SAME_TERM').checked 
			|| document.getElementById('chkCANCELLATION').checked
			|| document.getElementById('chkCHANGE_IN_NEW_BUSINESS').checked
			|| document.getElementById('chkCHANGE_IN_RENEWAL').checked
			|| document.getElementById('chkREINSTATE_NEW_TERM').checked)
			{
			Page_ClientValidate();
			document.getElementById('csvProcessTransactionType').style.visibility = "hidden";
			document.getElementById('trProcessTransactionType').style.display = "none";
			return true;
			}
			else
			{
				Page_ClientValidate();
				document.getElementById('csvProcessTransactionType').style.visibility = "visible";
				document.getElementById('trProcessTransactionType').style.display = "inline";
				return false;
			}
			
		}
			function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbCLASS_RISK":
						lookupMessage	=	"DRTCD.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
				}
				showLookupLayer(controlId,lookupMessage);							
			}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();FillSubLOB();populateXML();ApplyColor();">
		<FORM id="ACT_TRAN_CODE_GROUP" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSTATE_ID" runat="server">State</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" AutoPostBack="True">
										<asp:ListItem Value="0">0</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">Line of Business</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server" onchange="FillSubLOB();">
										<asp:ListItem Value=''></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" Display="Dynamic" ErrorMessage="LOB_ID can't be blank."
										ControlToValidate="cmbLOB_ID"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSUB_LOB_ID" runat="server">Sub Line of Business</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSUB_LOB_ID" onfocus="SelectComboIndex('cmbSUB_LOB_ID')" runat="server" onchange="setHidSubLob()">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvSUB_LOB_ID" runat="server" Display="Dynamic" ErrorMessage="SUB_LOB_ID can't be blank."
										ControlToValidate="cmbSUB_LOB_ID"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCLASS_RISK" runat="server">Risk/Class</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCLASS_RISK" onfocus="SelectComboIndex('cmbCLASS_RISK')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist>
									<a class="calcolora" href="javascript:showPageLookupLayer('cmbCLASS_RISK')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0" /></a>
									</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPOLICY_TYPE" runat="server">Type of Associated Policy</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:radiobutton id="rdbPOLICY_TYPEM" runat="server" TextAlign="Left" GroupName="POLICY_TYPE" Text="Mono"
										Checked="True"></asp:radiobutton><asp:radiobutton id="rdbPOLICY_TYPEP" runat="server" TextAlign="Left" GroupName="POLICY_TYPE" Text="Package"></asp:radiobutton></TD>
								<TD class="midcolora" width="18%"></TD>
								<TD class="midcolora" width="32%"></TD>
							</tr>
							<TR>
								<TD class="headRow" width="18%" colSpan="4">Process Transaction Type </TD>
							</TR>
							<tr id ="trProcessTransactionType">
								<td class="midcolora" width="18%" colSpan="4"><SPAN class="mandatory" id="spnProcessTransactionType">*</SPAN><asp:CustomValidator id="csvProcessTransactionType" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator></td>
							</tr>
							<TR>
								<TD class="midcolora" width="18%"><asp:label id="capNEW_BUSINESS" runat="server">New Business</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkNEW_BUSINESS" runat="server"></asp:checkbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCHANGE_IN_NEW_BUSINESS" runat="server">Change in New Business</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkCHANGE_IN_NEW_BUSINESS" runat="server"></asp:checkbox></TD>
							</TR>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capRENEWAL" runat="server">Renewal</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkRENEWAL" runat="server"></asp:checkbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCHANGE_IN_RENEWAL" runat="server">Change in Renewal</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkCHANGE_IN_RENEWAL" runat="server"></asp:checkbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capREINSTATE_SAME_TERM" runat="server">Reinstate  - Same Term</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkREINSTATE_SAME_TERM" runat="server"></asp:checkbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capREINSTATE_NEW_TERM" runat="server">Reinstate – New Term</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkREINSTATE_NEW_TERM" runat="server"></asp:checkbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" style="HEIGHT: 22px" width="18%"><asp:label id="capCANCELLATION" runat="server">Cancellation</asp:label></TD>
								<TD class="midcolora" style="HEIGHT: 22px" width="32%"><asp:checkbox id="chkCANCELLATION" runat="server"></asp:checkbox></TD>
								<TD class="midcolora" style="HEIGHT: 22px" width="18%"></TD>
								<TD class="midcolora" style="HEIGHT: 22px" width="32%"></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
								<td class="midcolorR" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidTRAN_GROUP_ID" type="hidden" name="hidTRAN_GROUP_ID" runat="server">
							<INPUT id="hidLOBXML" type="hidden" name="hidLOBXML" runat="server"> <INPUT id="hidSUB_LOB_ID" type="hidden" name="hidSUB_LOB_ID" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000;Display: none"
				width="0px" height="0px" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidTRAN_GROUP_ID').value);
		</script>
	</BODY>
</HTML>

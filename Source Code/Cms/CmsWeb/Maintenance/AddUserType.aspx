<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddUserType.aspx.cs" validateRequest=false  AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.clsAddUserType" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>MNT_USER_TYPES</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function SetTab()
		{
		    if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != "")) 
			{
			    var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
			    var tabtitles = TAB_TITLES.split(',');			
				Url="AddSecurityRights.aspx?UserTypeId="+document.getElementById('hidUSER_TYPE_ID').value + "&";
				DrawTab(2,top.frames[1],tabtitles[0],Url);
			}
			else
			{							
				RemoveTab(2,top.frames[1]);
			}
		}
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			document.getElementById('hidUSER_TYPE_ID').value	=	'New';
			//document.getElementById('txtUSER_TYPE_CODE').focus();// itrack no 1521
			document.getElementById('txtUSER_TYPE_CODE').value  = '';
			document.getElementById('txtUSER_TYPE_DESC').value  = '';
			
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		}
		function populateXML()
		{
			var tempXML = document.getElementById('hidOldData').value;
			//alert(tempXML);
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML;
				//Enabling the activate deactivate button
				if(tempXML!="")
				{
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					populateFormData(tempXML,MNT_USER_TYPES);
					SetActive();
					setStatusLabel();
				}
				else
				{
					AddData();
				}
			}
			SetTab();
			return false;
		}
		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor(); SetTab();">
		<FORM id="MNT_USER_TYPES" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label runat="server" ID="capMessages"/></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capUSER_TYPE_CODE" runat="server">User Type Code</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_TYPE_CODE" runat="server" maxlength="5" size="7"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvUSER_TYPE_CODE" runat="server" Display="Dynamic" ErrorMessage="USER_TYPE_CODE can't be blank."
										ControlToValidate="txtUSER_TYPE_CODE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capUSER_TYPE_DESC" runat="server">User Type Description</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_TYPE_DESC" runat="server" maxlength="50" size="50"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvUSER_TYPE_DESC" runat="server" Display="Dynamic" ErrorMessage="USER_TYPE_DESC can't be blank."
										ControlToValidate="txtUSER_TYPE_DESC"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capStatus" runat="server">Status</asp:label><span id="spnStatus" runat="server" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblStatus" CssClass="LabelFont" Runat="server"></asp:label></TD>
								<TD class="midcolora" width="18%"></TD>
								<TD class="midcolora" width="32%"></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
							<INPUT id="hidUSER_TYPE_ID" type="hidden" value="0" name="hidUSER_TYPE_ID" runat="server">
							<INPUT id="hidUSER_TYPE_SYSTEM" type="hidden" value="0" name="hidUSER_TYPE_SYSTEM" runat="server">
							<input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 
							<input  type="hidden" runat="server" ID="hidActive"  value=""  name="hidActive"/>
							<input  type="hidden" runat="server" ID="hidDeActive"  value=""  name="hidDeActive"/> 
						    <input id="hidactivate" type="hidden" name="hidactivate" runat="server" />
					        <input id="hidDeactivate" type="hidden" name="hidactivate" runat="server" /> 
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
				RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidUSER_TYPE_ID').value);
				
		if(document.getElementById('hidFormSaved').value == '1')
			{
			//***********************************
			setStatusLabel();
		//alert(document.getElementById('hidFormSaved').value);
			//*********************************
			
				//Form get saved refreshing the grid
				//top.frames[1].document.gridObject.refreshcompletegrid();
				//document.userType.btnReset.style.display = "none"	
				//Enabling the activate deactivate button
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
				//document.getElementById("lblStatus").innerText = "Active";
				SetActive();
			}
			else if(document.getElementById('hidFormSaved').value == '2')
			{
				//Reset should not be visible
				//document.userType.btnReset.style.display = "none"	
				//Enabling the activate deactivate button
				//document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
			}
			function setStatusLabel()
			{
				var xml		=	document.getElementById('hidOldData').value;
			//alert(xml+"   aaaa");
		if(xml!="")
		{
		//alert(xml);	
		//alert(document.getElementById('hidSystemUser').value);						
		var objXmlHandler = new XMLHandler();
		var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
		var i=0;
		for(i=0;i<tree.childNodes.length;i++)
		{
			if(!tree.childNodes[i].firstChild) continue;
			var nodeName = tree.childNodes[i].nodeName;
			var nodeValue = tree.childNodes[i].firstChild.text;
			var inactive = document.getElementById("hidDeActive").value;
			var active = document.getElementById("hidActive").value;
			var activate = document.getElementById("hidactivate").value;
			var Deactivate = document.getElementById("hidDeactivate").value;
			if(nodeName.toUpperCase() == "IS_ACTIVE") 
			{
			//alert(nodeValue.toUpperCase());
				if(nodeValue.toUpperCase() == "Y" || nodeValue == "1")
				{
				    document.getElementById("lblStatus").innerText = inactive;// changes by praveer for itrack no 1521
					if(document.getElementById('btnActivateDeactivate'))
						document.getElementById('btnActivateDeactivate').setAttribute('value',Deactivate); 
					}
				else
				{
				    document.getElementById("lblStatus").innerText = active; // changes by praveer for itrack no 1521
					document.getElementById('lblStatus').style.color = 'Red';
					if(document.getElementById('btnActivateDeactivate'))
					    document.getElementById('btnActivateDeactivate').setAttribute('value', activate); // changes by praveer for itrack no 1521
				}
				continue;
			}
		}
	}
			}
			
			function SetActive() {
			    var inactive = document.getElementById("hidDeActive").value;
			    var active = document.getElementById("hidActive").value;
			    var activate = document.getElementById("hidactivate").value;
			    var Deactivate = document.getElementById("hidDeactivate").value;
			if (document.getElementById("hidIS_ACTIVE").value == "Y")
						{
						    document.getElementById("lblStatus").innerText = inactive; // changes by praveer for itrack no 1521
							if(document.getElementById('btnActivateDeactivate'))
								document.getElementById('btnActivateDeactivate').setAttribute('value',Deactivate);
						}
						else
						{
						    document.getElementById("lblStatus").innerText = active; // changes by praveer for itrack no 1521
							if(document.getElementById('btnActivateDeactivate'))
							    document.getElementById('btnActivateDeactivate').setAttribute('value', activate); // changes by praveer for itrack no 1521
						}
			}
		</script>
	</BODY>
</HTML>













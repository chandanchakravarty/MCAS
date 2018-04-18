<%@ Page language="c#" Codebehind="Fax.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.Fax" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FAX</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function ClearText(e)
			{
				event.returnValue=false;
				return false;			
			}	
			
		function populateXML()
		{
			var tempXML = document.getElementById('hidOldData').value;						
			
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				if(document.getElementById('hidFormSaved').value == '1')
				{
					document.getElementById('trBody').style.display="inline";	
				}
				//Enabling the activate deactivate button
				if(tempXML!="" && tempXML!="0")
				{
					//document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 					
					populateFormData(tempXML,FAX);
					if (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == 'Y')
					{
					   //alert('in yes');
						document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
						document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
						document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
						document.getElementById('trDIARY_ITEM_TO').style.display='inline'; 
						if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
							{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",true);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							}
						if (document.getElementById('spnFOLLOW_UP_DATE') != null)
							{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
							}	
					}
					else
					{
						if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
							{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							}
						if (document.getElementById('spnFOLLOW_UP_DATE') != null)
							{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
							}	
					}
					document.getElementById('trBody').style.display="none";
					var objXmlHandler = new XMLHandler();
					
					var tree = (objXmlHandler.quickParseXML(document.getElementById('hidOldData').value).getElementsByTagName('Table')[0]);
					
					var i=0;
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(!tree.childNodes[i].firstChild) 
							continue;
						
						var nodeName = tree.childNodes[i].nodeName;
						var nodeValue = tree.childNodes[i].firstChild.text;
						//alert(nodeValue);
						var fileName;	
									
						switch(nodeName)
						{
							case "ATTACHMENT":								
								//document.getElementById("lblATTACHMENT").innerHTML = nodeValue ;
								//alert(nodeValue.substring(41,nodeValue.length));
								filename = nodeValue.substring(41,nodeValue.length); //Eliminating GUID and _FAX_
								document.getElementById("lblATTACHMENT").innerHTML = "<a href='" + document.FAX.hidRootPath.value + "\\" + nodeValue + "' target='blank'>" + filename + "</a>";
								document.getElementById("txtATTACHMENT").style.display = "none";
								break;
							default:
								document.getElementById("txtATTACHMENT").style.display = "none";
								break;
						}
					}
				}
				else 
				{
					var dt =new Date();
					document.getElementById('txtFOLLOW_UP_DATE').value = dt.getMonth()+1 +  '/' + dt.getDate() + '/' + dt.getFullYear(); 

				}				
			}
			if (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == 'Y')
					{
					   //alert('in yes');
						document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
						document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
						document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
						document.getElementById('trDIARY_ITEM_TO').style.display='inline'; 
						if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
							{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",true);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							}
						if (document.getElementById('spnFOLLOW_UP_DATE') != null)
							{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
							}	
					}
					else
					{
						if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
							{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							}
						if (document.getElementById('spnFOLLOW_UP_DATE') != null)
							{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
							}	
					}
			//
		    if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
					document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
					document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
					}						
			return false;
		}
		function ShowFOLLOW_UP_DATE()
		{
			if  (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == 'Y')
			{
				document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
				document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
				document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
				document.getElementById('trDIARY_ITEM_TO').style.display='inline'; 
				if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute('enabled',true);
						if (document.getElementById('rfvFOLLOW_UP_DATE').isvalid == false)
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'inline';
					} 	
		         
				if (document.getElementById('spnFOLLOW_UP_DATE') != null)
					{
						document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
					}
			}  
			else
		    {
				  document.getElementById('txtFOLLOW_UP_DATE').style.display='none';
		          document.getElementById('capFOLLOW_UP_DATE').style.display='none';
		          document.getElementById('imgFOLLOW_UP_DATE').style.display='none';
				  document.getElementById('trDIARY_ITEM_TO').style.display='none'; 
					if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("isvalid",true);
						document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
						
					}
				 if (document.getElementById('spnFOLLOW_UP_DATE') != null)
					{
						document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
					}	
		          
		    
		     }     
		}
		
		function ChkDate(objSource , objArgs)
		{
			
			var expdate=document.getElementById('txtFOLLOW_UP_DATE').value;
			objArgs.IsValid = DateComparer(expdate,"<%=System.DateTime.Now.AddDays(-1)%>",'mm/dd/yyyy');   //jsaAppDtFormat
			
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();">
		<form id="FAX" method="post" runat="server" enctype="multipart/form-data">
			<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
				<TR>
					<TD>
						<TABLE class="tableWidthHeader" width="100%" align="center" border="0">
							<TR>
								<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage1" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
							</TR>
							<TR>
								<TD class="midcolora" colSpan="2"><asp:label id="capFROM_NAME" runat="server">From Name</asp:label><SPAN class="mandatory">*</SPAN>
								</TD>
								<TD class="midcolora" colSpan="2">
									<P><asp:textbox id="txtFROM_NAME" runat="server" size="30" maxlength="50"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvFROM_NAME" runat="server" ControlToValidate="txtFROM_NAME" Display="Dynamic"></asp:requiredfieldvalidator></P>
								</TD></tr>
							<TR>
								<TD class="midcolora" colSpan="2"><asp:label id="capFROM_FAX" runat="server">From Fax</asp:label><SPAN class="mandatory">*</SPAN>
								</TD>
								<TD class="midcolora" colSpan="2"><asp:textbox id="txtFROM_FAX" runat="server" size="30" maxlength="50"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvFROM_FAX" runat="server" ControlToValidate="txtFROM_FAX" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFROM_FAX" ControlToValidate="txtFROM_FAX" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator></TD>
							</TR>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="2"><asp:label id="capTO" runat="server">To (Recipient Name)</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" colSpan="2"><asp:textbox id="txtTO" runat="server" size="30" maxlength="50"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvTO" runat="server" ControlToValidate="txtTO" Display="Dynamic"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="2"><asp:label id="capTO_NUMBER" runat="server">Recipient Fax Number<br>(Including Prefix)</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" colSpan="2"><asp:textbox id="txtTO_NUMBER" runat="server" size="30" maxlength="50"  onblur="FormatBrazilPhone()"></asp:textbox><BR>
						<asp:regularexpressionvalidator id="revTO_NUMBER" ControlToValidate="txtTO_NUMBER" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
						<asp:requiredfieldvalidator id="rfvTO_NUMBER" runat="server" ControlToValidate="txtTO_NUMBER" Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</TR>
				<tr>
					<TD class="midcolora" colSpan="2"><asp:label id="capSubject" runat="server">Subject</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" colSpan="2"><asp:textbox id="txtSUBJECT" runat="server" size="30" maxlength="50"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvSUBJECT" runat="server" ControlToValidate="txtSUBJECT" Display="Dynamic"></asp:requiredfieldvalidator></TD>
				</tr>
				<TR>
					<TD class="midcolora" colSpan="2"><asp:label id="capMESSAGE" runat="server">Message</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" colSpan="2"><asp:textbox onkeypress="MaxLength(txtMESSAGE,500)" id="txtMESSAGE" runat="server" size="30"
							Width="264px" Height="96px" TextMode="MultiLine"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvMESSAGE" runat="server" ControlToValidate="txtMESSAGE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
				</TR>
				<tr>
					<TD class="midcolora" colSpan="2"><asp:label id="capATTACHMENT" runat="server">Attachment</asp:label></TD>
					<TD class="midcolora" colSpan="2">
						<input onkeypress="ClearText(event)" id="txtATTACHMENT" type="file" size="42" name="txtATTACHMENT"
							runat="server">
						<asp:label id="lblATTACHMENT" Runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:label id="capDIARY_ITEM_REQ" runat="server">Diary Item Required</asp:label></TD>
					<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:dropdownlist id="cmbDIARY_ITEM_REQ" onfocus="SelectComboIndex('cmbDIARY_ITEM_REQ')" runat="server">
							<asp:ListItem Value='N'>No</asp:ListItem>
							<asp:ListItem Value='Y'>Yes</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD class="midcolora" colSpan="1"><asp:label id="capFOLLOW_UP_DATE" runat="server" style="DISPLAY:none">Follow up date</asp:label><span class="mandatory" id="spnFOLLOW_UP_DATE" style="DISPLAY:none">*</span></TD>
					<TD id="tdFollowuptxt" class="midcolora" colSpan="1"><asp:textbox id="txtFOLLOW_UP_DATE" runat="server" maxlength="10" size="12" style="DISPLAY:none"></asp:textbox>
					<asp:hyperlink id="hlkFOLLOW_UP_DATE" runat="server" CssClass="HotSpot">
							<ASP:IMAGE id="imgFOLLOW_UP_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif" style="display:none"></ASP:IMAGE>
						</asp:hyperlink><br>
						<asp:requiredfieldvalidator id="rfvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revFOLLOW_UP_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
							ControlToValidate="txtFOLLOW_UP_DATE"></asp:regularexpressionvalidator>
						<asp:customvalidator id="csvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"
							ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
				</tr>
				<tr id ="trDIARY_ITEM_TO" runat="server" style="DISPLAY:none">
				<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:label id="capDIARY_ITEM_TO" runat="server">To</asp:label></TD>
					<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:dropdownlist id="cmbDIARY_ITEM_TO" onfocus="SelectComboIndex('cmbDIARY_ITEM_TO')" runat="server"></asp:dropdownlist></TD>
				<TD class="midcolora" colSpan="2" style="HEIGHT: 38px"></TD>
				</tr>
				<tr id="trBody" runat="server">
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSend" runat="server" Text="Send"></cmsb:cmsbutton></td>
				</tr>
				<tr>
					<td class="iframsHeightMedium"></td>
				</tr>
			</TABLE>
			</TD></TR></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server">
			<INPUT id="hidRootPath" type="hidden" name="hidRootPath" runat="server">
		</form>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidRowId').value,true);
		</script>
	</body>
</HTML>

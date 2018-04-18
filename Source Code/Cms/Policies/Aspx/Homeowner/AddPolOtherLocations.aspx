<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="AddPolOtherLocations.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Homeowner.AddPolOtherLocations" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>AddPolOtherLocations</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
	 <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			document.getElementById('hidLOCATION_ID').value	=	'New';
			if(document.getElementById('hidLOCATION_ID').value=='New');
				{document.getElementById('txtLOC_NUM').value = document.getElementById('hidLocationCode').value;}
			document.getElementById('txtLOC_NUM').focus();
			document.getElementById('txtLOC_ADD1').value  = '';
			document.getElementById('txtLOC_CITY').value  = '';
			document.getElementById('txtLOC_COUNTY').value= '';
			document.getElementById('cmbLOC_STATE').options.selectedIndex = -1;
			document.getElementById('txtLOC_ZIP').value  = '';
			document.getElementById('cmbPHOTO_ATTACHED').options.selectedIndex = -1;
			document.getElementById('cmbOCCUPIED_BY_INSURED').options.selectedIndex = -1;
			document.getElementById('txtDESCRIPTION').value  = '';
			
			if (document.getElementById('btnActivateDeactivate'))
			{
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			}
		}	
		
		var GlobalError=false;
		var CalledFromSave = false;
		function Validate()
		{
			var result = GetZipForState(true);
			Page_ClientValidate();
			Page_IsValid = Page_IsValid && result;
			return Page_IsValid;
		}
		
		//Added For Itrack Issue #6584.
		////////AJAX ZIP IMPLEMENTATION			
		function GetZipForState()
		{
			GlobalError = true;
		
				if(document.getElementById('cmbLOC_STATE').value==14 ||document.getElementById('cmbLOC_STATE').value==22||document.getElementById('cmbLOC_STATE').value==49)
				{ 
					if(document.getElementById('txtLOC_ZIP').value!="")
					{
						var intStateID = parseInt(document.getElementById('cmbLOC_STATE').options[document.getElementById('cmbLOC_STATE').options.selectedIndex].value);
						var strZipID = document.getElementById('txtLOC_ZIP').value;							
						var result = AddPolOtherLocations.AjaxFetchZipForState(intStateID,strZipID);			
						return	AjaxCallFunction_CallBack(result);
										
				  }
				  return false;
			 }
			 else
				{
				        document.getElementById('csvZIP_CODE').setAttribute('enabled',false); 
						document.getElementById('csvZIP_CODE').setAttribute('isValid',false);
						document.getElementById('csvZIP_CODE').style.display = 'none';
				        return true;	
				}
			
		}
		//Added For Itrack Issue #6584.
		function AjaxCallFunction_CallBack(response)
		{		
			if(document.getElementById('cmbLOC_STATE').value==14 ||document.getElementById('cmbLOC_STATE').value==22||document.getElementById('cmbLOC_STATE').value==49)
			{ 
				if(document.getElementById('txtLOC_ZIP').value!="")
				{
					handleResult(response);
					if(GlobalError)
					{
						document.getElementById('csvZIP_CODE').setAttribute('enabled',true);
						document.getElementById('csvZIP_CODE').setAttribute('isValid',true);
						document.getElementById('csvZIP_CODE').style.display = 'inline';
						return false;
									
					}
					else
					{
						document.getElementById('csvZIP_CODE').setAttribute('enabled',false); 
			   			document.getElementById('csvZIP_CODE').setAttribute('isValid',false);
						document.getElementById('csvZIP_CODE').style.display = 'none';
						return true;
					}		
				}
				return false;
			}
			else
				return true;	
		}
		//////////////////////END
		//Added For Itrack Issue #6584.
		function handleResult(res) 
		{
			if(!res.error)
			{
				//if (res.value==true) 
				if (res.value!="") 
				{		
					if(!CalledFromSave)
						document.getElementById("txtLOC_COUNTY").value= res.value;
					GlobalError=false;
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}
		}
		
		//Added For Itrack Issue #6584.
		function ChkResult(objSource , objArgs)
		{
			objArgs.IsValid = true;
			if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetZipForState();
				if(objArgs.IsValid == false)
					document.getElementById('csvZIP_CODE').innerHTML = "The zip code does not belong to the state";				
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			return;
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
			document.getElementById("btnSave").click();
		}	
		
		function populateXML()
		{
		   
		if(document.getElementById('hidIsPostBack').value != 'POST')
		{
			if(document.getElementById('hidFormSaved').value == '0')
				{
					var tempXML;
					if(document.getElementById("hidOldData").value !="")
					{
						//Enabling the activate deactivate button
						if (document.getElementById('btnActivateDeactivate'))
						{
							document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						}
						populateFormData(document.getElementById("hidOldData").value,POL_OTHER_LOCATIONS);
					}
					else
					{
						AddData();
					}
				}
			}
			ChangeColor();
			ApplyColor();
			return false;
			
		}	 
		 
		 
	function formReset()
	{
		document.POL_OTHER_LOCATIONS.reset();
		populateXML();
		DisableValidators();
		ChangeColor();
		return false;
	}
	
	function OpenLookupForm()
	{
		if(document.getElementById('cmbLOC_STATE').selectedIndex != -1) 
	    { 
			var url=document.getElementById('hidURL').value;
			document.getElementById('hidSTATE').value = document.getElementById('cmbLOC_STATE').selectedIndex;
			var state= "@STATE=" + document.getElementById('hidSTATE').value;
			OpenLookup( url,'COUNTY','COUNTY','','txtLOC_COUNTY','COUNTYFORSTATE','County',state);
		}
	}
	
	function ChkTextAreaLength(source, arguments)
	{
		var txtArea = arguments.Value;
		if(txtArea.length > 50 ) 
		{
			arguments.IsValid = false;
			return;   
		}
	}	
	
	
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='POL_OTHER_LOCATIONS' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_NUM" runat="server">Num</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_NUM' runat='server' size='5' maxlength='4'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvLOC_NUM" runat="server" ControlToValidate="txtLOC_NUM" ErrorMessage="LOC_NUM can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revLOC_NUM" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtLOC_NUM"></asp:regularexpressionvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_ADD1" runat="server">Add1</asp:Label><span class="mandatory">*</span></TD><!--Span added by Charles on 16-Sep-09 for Itrack 6406 -->
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_ADD1' runat='server' size='30' maxlength='150'></asp:textbox>
									<br><asp:RequiredFieldValidator id="rfvLOC_ADD1" ControlToValidate="txtLOC_ADD1" runat="server" Display="Dynamic" ErrorMessage="Please enter Address 1"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 16-Sep-09 for Itrack 6406 -->
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_CITY" runat="server">City</asp:Label></TD><!--Modified by avijit for singapore dev -->
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_CITY' runat='server' size='30' maxlength='150' Enabled="false"></asp:textbox>
									<br><asp:RequiredFieldValidator id="rfvLOC_CITY" ControlToValidate="txtLOC_CITY" runat="server" Display="Dynamic" Visible="false" ErrorMessage="Please enter City"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 16-Sep-09 for Itrack 6406 -->
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_STATE" runat="server">State</asp:Label></TD><!--Modified by avijit for singapore dev -->
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbLOC_STATE' OnFocus="SelectComboIndex('cmbLOC_STATE')" runat='server' Enabled="false">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLOC_STATE" runat="server" ControlToValidate="cmbLOC_STATE" ErrorMessage="LOC_STATE can't be blank."
										Enabled="false" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_COUNTY" runat="server">County</asp:Label><span class="mandatory">*</span></TD><!--Span added by Charles on 16-Sep-09 for Itrack 6406 -->
								<TD class='midcolora' width='32%'>
									<%--<asp:textbox id="txtLOC_COUNTY" runat="server" ReadOnly="true" size="30"></asp:textbox>--%>
                                    <asp:DropDownList ID="cmbLOC_COUNTRY" runat="server"></asp:DropDownList>
									<IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../../cmsweb/images/selecticon.gif" runat="server" visible="false">
									<br><asp:RequiredFieldValidator id="rfvLOC_COUNTY" ControlToValidate="cmbLOC_COUNTRY" runat="server" Display="Dynamic" ErrorMessage="Please select County" Enabled ="true"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 16-Sep-09 for Itrack 6406 -->
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_ZIP" runat="server">Zip</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_ZIP' runat='server' size='13' maxlength='10'  OnBlur="GetZipForState();"></asp:textbox><BR>
									<asp:customvalidator id="csvZIP_CODE" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
										Display="Dynamic" ControlToValidate="txtLOC_ZIP"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="LOC_ZIP can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPHOTO_ATTACHED" runat="server">Attached</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbPHOTO_ATTACHED' OnFocus="SelectComboIndex('cmbPHOTO_ATTACHED')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capOCCUPIED_BY_INSURED" runat="server">By</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbOCCUPIED_BY_INSURED' OnFocus="SelectComboIndex('cmbOCCUPIED_BY_INSURED')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capDESCRIPTION" runat="server">Description</asp:Label></TD>
								<TD class='midcolora' ColSpan='3'>
									<asp:textbox id='txtDESCRIPTION' runat='server' maxlength='50' onKeyPress="MaxLength(this,50)" TextMode="MultiLine" Width="170px" Height="50px"></asp:textbox>
									<br><asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtDESCRIPTION"
										Runat="server" Display="Dynamic" ID="csvDESCRIPTION"></asp:CustomValidator>
								</TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Deactivate'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
							<INPUT id="hidLOCATION_ID" type="hidden" value="0" name="hidLOCATION_ID" runat="server">
							<INPUT id="hidIsPostBack" type="hidden" value="0" name="hidIsPostBack" runat="server">
							<input id="hidLocationCode" type="hidden" runat="server" NAME="hidLocationCode">
							<input id="hidSTATE" type="hidden" name="hidSTATE" value="0" runat="server">
							<input id="hidURL" type="hidden" name="hidURL" value="0" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLOCATION_ID').value);
		</script>
	</BODY>
</HTML>

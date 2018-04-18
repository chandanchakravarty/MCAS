<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyWatercraftAddEquipment.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyWatercraftAddEquipment" ValidateRequest = "false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Policy WaterCraft Equipment Details</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function setOtherDesc()
		{		
			if (document.getElementById('cmbEQUIP_TYPE').value != "-1")
			{
				document.getElementById('txtOTHER_DESCRIPTION').value='';
			}
		}
		
		function ShowHideOtherDescTextBox (objDrpDown)
		{		
			if (objDrpDown != null && objDrpDown.value != null && objDrpDown.value != "" && objDrpDown.value == "-1")			
			{
				document.getElementById('OtherDescTextBox').style.display	= "";			
				document.getElementById('OtherDescLabel').style.display		= "";							
				document.all("rfvOTHER_DESCRIPTION").setAttribute("enabled",true);
			}
			else
			{
				document.getElementById('OtherDescTextBox').style.display	= "none";			
				document.getElementById('OtherDescLabel').style.display		= "none";						
				document.all("rfvOTHER_DESCRIPTION").setAttribute("enabled",false);					
				//Gen Issue 2826 - Desc will be blank when option other that 'Others' is selecetd in Description dropdown
				//document.getElementById('txtOTHER_DESCRIPTION').value='';
			}
			ChangeColor();
			ApplyColor();
		}
		
		function RedirectToBoatPage()
		{
			//parent.document.location.href="/cms/policies/Aspx/Watercraft/PolicyAddWatercraftInformation.aspx?CalledFrom=WAT";
			parent.document.location.href="/cms/policies/aspx/watercraft/policywatercraftindex.aspx?calledfrom=WAT";			
		}
		function populateXML()
		{	
			var tempXML;
			tempXML=document.getElementById('hidOldData').value;
						
			if(document.getElementById('hidFormSaved').value == '0')
			{
				if(tempXML!="" && tempXML!="0")	
				{		
					if(document.getElementById('btnActivateDeactivate')!=null)				
					document.getElementById('btnActivateDeactivate').setAttribute('enabled',true);
					populateFormData(tempXML,APP_WATERCRAFT_EQUIP_DETAILLS);	
					ShowHideOtherDescTextBox(document.getElementById("cmbEQUIP_TYPE"));													
					document.getElementById('txtEQUIP_NO').focus();
				}
				else
				{
					AddData();
				}
			}
			ShowHideOtherDescTextBox(document.getElementById("cmbEQUIP_TYPE"));	
			return false;
		}
		
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			
			document.getElementById('hidEQUIP_ID').value	=	'New';			
			document.getElementById('txtEQUIP_NO').focus();
			document.getElementById('cmbSHIP_TO_SHORE').options.selectedIndex = -1;
			document.getElementById('txtYEAR').value  = '';
			document.getElementById('txtMAKE').value  = '';
			document.getElementById('txtMODEL').value  = '';
			document.getElementById('txtSERIAL_NO').value  = '';
			
			document.getElementById('cmbEQUIP_TYPE').options.selectedIndex = -1;
			ShowHideOtherDescTextBox(document.getElementById("cmbEQUIP_TYPE"));
		
			document.getElementById('txtOTHER_DESCRIPTION').value  = '';
			document.getElementById('OtherDescTextBox').style.display = "none";
			
			document.getElementById('txtINSURED_VALUE').value  = '';
			//document.getElementById('cmbASSOCIATED_BOAT').options.selectedIndex = -1;
			if(document.getElementById('btnActivateDeactivate')!=null)		
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			

		}
		
						
		function checkFutureYear(source, arguments)
		{
			var dateEntered = arguments.Value;
			var curDate=new Date();
		
			if(!isNaN(dateEntered))
			{
				if(parseInt(dateEntered)>parseInt(curDate.getFullYear()+1) || parseInt(dateEntered)<1900)
				{
					arguments.IsValid = false;
					return false;
				}
			}				
		}
		
		function CheckBPBRegNo1()
		{
				
			var val;
			if((document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value).length <=6 )
			{				
		
				val=document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value;
				if((event.keyCode >=48 && event.keyCode <= 57) )
				{
					event.returnValue = true; 
				}
				else if(event.keyCode == 46)
				{
				
					event.returnValue = true; 
				}
				else
					event.returnValue = false; 
					
			}
			else if(document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value.length == 7) 
			{
				
				if(document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value.indexOf(".")==-1)
				{
				if(event.keyCode == 46)
				{
				
					event.returnValue = true; 
				}
				else
					event.returnValue = false; 
					}
					else
					if((event.keyCode >=48 && event.keyCode <= 57) )
				{
					event.returnValue = true; 
				}			
			}
			else if((document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value.length > 7) || (document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value.length <=10))
			{
				
				val=document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value;
				if((event.keyCode >=48 && event.keyCode <= 57) )
				{
					event.returnValue = true; 
				}
				
				else
				{
					document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value=''
					event.returnValue = false; 
					
					document.APP_WATERCRAFT_EQUIP_DETAILLS.txtINSURED_VALUE.value=val.substr(0,val.length);
				}
			
			}
			else
			{
				return false;										
			}
		}

			function ShowForm()
			{
				var chkBoat='<%=noBoat%>'
				if(chkBoat<=0)
				{
					document.getElementById('trBoat').style.display="inline";   
					document.getElementById('trform').style.display="none";
				}
				else
				{
					document.getElementById('trBoat').style.display="none";   
					document.getElementById('trform').style.display="inline";
				}
				if(document.getElementById('hidFormSaved').value=="5")
				{
					document.getElementById('trBoat').style.display="none";   
					document.getElementById('trform').style.display="none";
				}
					
			
			}
			function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbEQUIP_TYPE":
						lookupMessage	=	"EQPTYP.";
						break;					 	
					
					case "cmbEQUIPMENT_TYPE":
						lookupMessage	=	"EQTYPE.";
						break;					 		
										
					case "cmbSHIP_TO_SHORE":
						lookupMessage	=	"SHPSHT.";
						break;
					
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ShowForm();">
		<FORM id="APP_WATERCRAFT_EQUIP_DETAILLS" method="post" runat="server">
			<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
			<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<tr id="trBoat" class="midcolorc">
					<td><asp:Label ID="lblNOBOAT" Runat="server">No Boat Available. Click <a href='javascript:RedirectToBoatPage();'>here</a> to add boats.</asp:Label>
					</td>
				</tr>
				<TR id="trform" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							
									<tr>
								<TD class="midcolora" width="18%"><asp:label id="capEQUIP_NO" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEQUIP_NO" runat="server" maxlength="4" Width="35"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvEQUIP_NO" Runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtEQUIP_NO"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEQUIP_NO" Runat="server" Display="Dynamic" ControlToValidate="txtEQUIP_NO"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%">
									<asp:label id="capEQP_TYP" runat="server"></asp:label>
									<span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%">
									<asp:dropdownlist id="cmbEQUIPMENT_TYPE" onfocus="SelectComboIndex('cmbEQUIPMENT_TYPE')" runat="server"></asp:dropdownlist>
									<A class="calcolora" href="javascript:showPageLookupLayer('cmbEQUIPMENT_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
									</TD>
								</tr>
								
										<tr>
								
								<TD class="midcolora" width="18%"><asp:label id="capEQUIP_TYPE" runat="server"></asp:label> <span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEQUIP_TYPE" onchange="Javascript:ShowHideOtherDescTextBox(this)" onfocus="SelectComboIndex('cmbEQUIP_TYPE')" runat="server"></asp:dropdownlist>
									<a class="calcolora" href="javascript:showPageLookupLayer('cmbEQUIP_TYPE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
									<br>								
									<asp:RequiredFieldValidator ID="rfvcmbEQUIP_TYPE" ControlToValidate="cmbEQUIP_TYPE" Display="Dynamic" Enabled=True Runat=server>										
										</asp:RequiredFieldValidator>
									
								</TD>
								
								<TD class="midcolora" width="18%">
									<div id="OtherDescLabel">
										<asp:label id="capOTHER_DESCRIPTION" runat="server"></asp:label>
										<span class="mandatory">*</span>
									</div>
								</TD>
								<TD class="midcolora" width="32%">
								
									<div id="OtherDescTextBox">
										<asp:textbox id="txtOTHER_DESCRIPTION" runat="server" maxlength="35" Width="150"></asp:textbox><br>										
									
									<asp:RequiredFieldValidator ID="rfvOTHER_DESCRIPTION" ControlToValidate="txtOTHER_DESCRIPTION" Display="Dynamic" Enabled=True Runat=server>										
										</asp:RequiredFieldValidator>
										
										</div>
									
								</TD>
								
								
								
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSHIP_TO_SHORE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSHIP_TO_SHORE" onfocus="SelectComboIndex('cmbSHIP_TO_SHORE')" runat="server"></asp:dropdownlist>
									<a class="calcolora" href="javascript:showPageLookupLayer('cmbSHIP_TO_SHORE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" maxlength="4" Width="35"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revYEAR" Display="Dynamic" ControlToValidate="txtYEAR" Runat="server"></asp:regularexpressionvalidator>
									<asp:customvalidator id="csvYEAR" Display="Dynamic" ControlToValidate="txtYEAR" Runat="server" ClientValidationFunction="checkFutureYear"></asp:customvalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" maxlength="65"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" maxlength="65"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capINSURED_VALUE" runat="server"></asp:label></TD>
								<TD class="midcolora" colspan="3"><asp:textbox id="txtINSURED_VALUE" runat="server" onkeypress="CheckBPBRegNo1()" maxlength="7"
										size="9" CssClass="INPUTCURRENCY"></asp:textbox><br>
									<asp:rangevalidator id="rngINSURED_VALUE" Display="Dynamic" ControlToValidate="txtINSURED_VALUE" Runat="server"
										MinimumValue="0.01" MaximumValue="9999999.99" Type="Currency"></asp:rangevalidator></TD>								
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSERIAL_NO" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL_NO" runat="server" size="20" maxlength="6"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEQUIP_AMOUNT" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEQUIP_AMOUNT" onfocus="SelectComboIndex('cmbEQUIP_AMOUNT')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="" CausesValidation="False"></cmsb:cmsbutton></td><%--//Done for Itrack Issue 6666 on 29 Oct 09--%>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" value="" name="hidOldData" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"> <INPUT id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
			<INPUT id="hidEQUIP_ID" type="hidden" value="0" name="hidEQUIP_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
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
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidEQUIP_ID').value);
		</script>
	</BODY>
</HTML>

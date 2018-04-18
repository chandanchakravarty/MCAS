<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page validateRequest="false" CodeBehind="PolicyAddFarmDetail.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyAddFarmDetail" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddUmbrellaFarm</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function ResetForm()
		{
			DisableValidators();
			ChangeColor();
			document.POLICY_UM_FARM_INFO.reset();
			return false;			
		}
		
		//For Reseting form on Activate/Deactivate Click
		function ResetData()
		{
			DisableValidators();
			document.POLICY_UM_FARM_INFO.reset();
		}
		
		function GetTerritory()
		{
			if(document.getElementById('txtZIPCODE').value!="")
			{
				if(isNaN(document.getElementById('txtZIPCODE').value))
					return;
				var strZip = document.getElementById('txtZIPCODE').value;				
				PolicyAddFarmDetail.AjaxGetCountyForZip(strZip,PutTerritory);
				//myTSMain1.GetCountyForZip.callService(PutTerritory, "GetCountyForZip",strZip);
			}			
		}
		function PutTerritory(Result)
		{
			
			if(Result.error)
			{
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        				
				
			}
			else		
				document.getElementById("txtCOUNTY").value= Result.value;
		}	
		// Added by Swarup For checking zip code for LOB: Start
		function GetZipForState()
			{		    
				GlobalError=true;
				if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
				{ 
					if(document.getElementById('txtZIPCODE').value!="")
					{
						var intStateID = document.getElementById('cmbSTATE').options[document.getElementById('cmbSTATE').options.selectedIndex].value;
						var strZipID = document.getElementById('txtZIPCODE').value;	
						var co=myTSMain1.createCallOptions();
						co.funcName = "FetchZipForState";
						co.async = false;
						co.SOAPHeader= new Object();
						var oResult = myTSMain1.FetchZip.callService(co,intStateID,strZipID);
						handleResult(oResult);	
						if(GlobalError)
						{
							return false;
						}
						else
						{
							return true;
						}
					}	
					return false;
				}
				else 
					return true;		
			}
				
			function ChkResult(objSource , objArgs)
			{
				objArgs.IsValid = true;
				if(objArgs.IsValid == true)
				{
					objArgs.IsValid = GetZipForState();
					if(objArgs.IsValid == false)
						document.getElementById('csvZIPCODE').innerHTML = "The zip code does not belong to the state";				
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
			
			function handleResult(res) 
			{
			if(!res.error)
				{
				if (res.value!="" && res.value!=null ) 
					{
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
			function serviceInitialise() 
			{ 
  				//var lstr = "../../cmsweb/webservices/wscmsweb.asmx?WSDL";
  				var lstr="<%=WebServiceURL%>";
  				myTSMain1.useService(lstr.toString(), "FetchZip");
			}

			var oTimer = setInterval("checkBodyLoad()", 2000);
			function checkBodyLoad() 
			{
				try
				{
					if (document.body && typeof myTSMain1.useService != 'undefined') 
					{					
						serviceInitialise();
						clearInterval(oTimer);
					}
				}
				catch(ex)
				{
				}
			}
			
			
		// Added by Swarup For checking zip code for LOB: End	
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();">
		<FORM id="POLICY_UM_FARM_INFO" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<DIV id="myTSMain1" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_NUMBER" runat="server">LOCATION NUMBER</asp:label><span class="mandatory">*</span></TD>
								<TD colspan="3" class="midcolora" width="32%"><asp:textbox id="txtLOCATION_NUMBER" runat="server" MaxLength="4" size="6"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvLOCATION_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtLOCATION_NUMBER"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revLOCATION_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtLOCATION_NUMBER"
										Enabled="False"></asp:regularexpressionvalidator>
									<asp:rangevalidator id="rngLOCATION_NUMBER" Display="Dynamic" ControlToValidate="txtLOCATION_NUMBER"
										Runat="server" MaximumValue="9999" MinimumValue="1" Type="Integer"></asp:rangevalidator>
								</TD>
							</tr>
							<tr>
								<td class="midcolora"><asp:label id="capPullCustomerAddress" runat="server">Would you like to pull customer address</asp:label></td>
								<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capADDRESS_1" runat="server">Address1</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS_1" runat="server" size="30" maxlength="75"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvADDRESS1" ControlToValidate="txtADDRESS_1" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capADDRESS_2" runat="server">Address2</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS_2" runat="server" size="30" maxlength="75"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCITY" runat="server" size="30" maxlength="75"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvCITY" ControlToValidate="txtCITY" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSTATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfv_STATE" ControlToValidate="cmbSTATE" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capZIPCODE" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtZIPCODE" runat="server" size="13" maxlength="10" OnBlur="GetZipForState();"></asp:textbox>
								<%-- Added by Swarup on 30-mar-2007 --%>
									<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
									<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink><BR>
									<asp:customvalidator id="csvZIPCODE" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtZIPCODE"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvZIPCODE" ControlToValidate="txtZIPCODE" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revZIPCODE" runat="server" ControlToValidate="txtZIPCODE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOUNTY" runat="server">County</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOUNTY" runat="server" size="30" maxlength="75" ReadOnly="True"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" alt="" src="/cms/cmsweb/images/selecticon.gif"
										runat="server"><BR>
									<asp:requiredfieldvalidator id="rfvCOUNTY" ControlToValidate="txtCOUNTY" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPHONE_NUMBER" runat="server">Phone</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPHONE_NUMBER" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revPHONE_NUMBER" runat="server" ControlToValidate="txtPHONE_NUMBER" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capFAX_NUMBER" runat="server">Fax</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtFAX_NUMBER" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revFAX_NUMBER" runat="server" ControlToValidate="txtFAX_NUMBER" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capNO_OF_ACRES" Runat="server"></asp:label></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtNO_OF_ACRES" Runat="server" MaxLength="6"></asp:textbox><br>
									<asp:rangevalidator id="rngNO_OF_ACRES" Display="Dynamic" ControlToValidate="txtNO_OF_ACRES" Runat="server"
										MaximumValue="999999" MinimumValue="1" Type="Integer"></asp:rangevalidator>
									<asp:regularexpressionvalidator id="revNO_OF_ACRES" Enabled="False" Display="Dynamic" Runat="server" ControlToValidate="txtNO_OF_ACRES"></asp:regularexpressionvalidator></td>
								<td class="midcolora" width="18%"><asp:label id="capFULL_PART" Runat="server"></asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbFULL_PART" Runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capOCCUPIED" Runat="server"></asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbOCCUPIED" Runat="server"></asp:dropdownlist></td>
								<td class="midcolora" width="18%"><asp:label id="capRENTED" Runat="server"></asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbRENTED" Runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"
										Enabled="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidFARM_ID" type="hidden" value="0" name="hidLOCATION_ID" runat="server">
			<INPUT id="hidCheckZipSubmit" type="hidden" name="hidCheckZipSubmit" runat="server">
			<input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <input id="hidAPP_LOB" type="hidden" name="hidAPP_LOB" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFARM_ID').value,true);
		</script>
	</BODY>
</HTML>

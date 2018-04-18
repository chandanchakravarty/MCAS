<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAddWatercraftTrailer.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftTrailer" ValidateRequest = "false"%>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Policy WaterCraft Trailer Details</title>
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
			document.getElementById('hidTRAILER_ID').value	=	'New';
			document.getElementById('txtTRAILER_NO').focus();
			document.getElementById('txtYEAR').value  = '';
			document.getElementById('txtMODEL').value  = '';
			document.getElementById('txtMANUFACTURER').value  = '';
			document.getElementById('txtSERIAL_NO').value  = '';
			document.getElementById('txtINSURED_VALUE').value  = '';
			document.getElementById('cmbASSOCIATED_BOAT').options.selectedIndex = -1;
			document.getElementById('cmbTRAILER_TYPE').options.selectedIndex = -1;
			document.getElementById('cmbTRAILER_DED_ID').options.selectedIndex = -1;
			if(document.getElementById('btnActivateDeactivate')!=null)		
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		
		}
		
		function cmbTRAILER_TYPE_Changed()
		{
			if(document.getElementById('cmbTRAILER_TYPE').options.selectedIndex >0)
			{
				__doPostBack('TRAILER_TYPE_Changed','0');
			}
		}
		
		function populateXML()
		{
			var tempXML;
			tempXML=document.getElementById('hidOldData').value;
			//alert(tempXML)

			if(document.getElementById('hidFormSaved').value == '0')
			{
			
				if(tempXML!="" && tempXML!="0")	
				{		
					if(document.getElementById('btnActivateDeactivate')!=null)				
					document.getElementById('btnActivateDeactivate').setAttribute('enabled',true);
					populateFormData(tempXML,APP_WATERCRAFT_TRAILER_INFO);
				}
				else
				{
					AddData();
				}
			}
			return false;
		}

		function setTab()
		{  
			var lobStr='<%=lob%>';
			if (document.getElementById('hidTRAILER_ID')!=null && document.getElementById('hidTRAILER_ID').value!="New" && document.getElementById('hidTRAILER_ID').value!="NEW")
			{
				var CalledFrom ='';
				if (document.getElementById('hidCalledFrom')!=null)
				{
					CalledFrom=document.getElementById('hidCalledFrom').value;
					
				}		 
				
				Url="../Automobile/PolicyAdditionalInterestIndex.aspx?PageFrom=" + lobStr +"&CUSTOMER_ID=" + document.APP_WATERCRAFT_TRAILER_INFO.hidCUSTOMER_ID.value  + "&POLICY_ID=" + document.APP_WATERCRAFT_TRAILER_INFO.hidPOLICY_ID.value  + "&POLICY_VERSION_ID=" + document.APP_WATERCRAFT_TRAILER_INFO.hidPOLICY_VERSION_ID.value + "&RISK_ID=" + document.APP_WATERCRAFT_TRAILER_INFO.hidTRAILER_ID.value +  "&CalledFrom=WTR&"; 
				DrawTab(2,top.frames[1],'Additional Interest',Url); 
			}
			else
			{				
				RemoveTab(2,top.frames[1]);	
				
			}
			return false; 
		}

		/*		
			function checkFutureYear(source, arguments)
			{
				var dateEntered = arguments.Value;
				var curDate=new Date();
			
				if(!isNaN(dateEntered))
				{
					if(parseInt(dateEntered)>parseInt(curDate.getFullYear()))
					{
						arguments.IsValid = false;
						return false;
					}
				}				
			}
			
	
			*/
		function CheckPremiumLimit()
		{
					
				var val;
				if((document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value).length <=7 )
				{					
					val=document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value;
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
				else if(document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value.length == 8) 
				{
					
					if(document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value.indexOf(".")==-1)
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
				else if(document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value.length > 8)
				{
					
					val=document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value;
					if((event.keyCode >=48 && event.keyCode <= 57) )
					{
						event.returnValue = true; 
					}
					
					else
					{
						document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value=''
						event.returnValue = false; 
						document.APP_WATERCRAFT_TRAILER_INFO.txtINSURED_VALUE.value=val.substr(0,val.length);
					}
				
				}
				else
				{
					return false;										
				}
			}		
			function formatCurrencyOnLoad()
			{
				document.getElementById("txtINSURED_VALUE").value=formatCurrency(document.getElementById("txtINSURED_VALUE").value);	
				return false;
			}	
			
			
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();setTab();formatCurrencyOnLoad();">
		<FORM id="APP_WATERCRAFT_TRAILER_INFO" method="post" runat="server">
			<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></tr>
							<tr>
								<TD class="pageHeader" align="left" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTRAILER_NO" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtTRAILER_NO" runat="server" maxlength="5" size="6"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvTRAILER_NO" runat="server" ControlToValidate="txtTRAILER_NO" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="regTRAILER_NO" runat="server" ControlToValidate="txtTRAILER_NO" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" maxlength="4" Width="35"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revYEAR" ControlToValidate="txtYEAR" Display="Dynamic" Runat="server" Enabled="True"></asp:regularexpressionvalidator><asp:rangevalidator id="rngYEAR" ControlToValidate="txtYEAR" Display="Dynamic" Runat="server" MinimumValue="1900"
										Type="Integer"></asp:rangevalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTRAILER_TYPE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTRAILER_TYPE" onfocus="SelectComboIndex('cmbTRAILER_TYPE')" onChange="cmbTRAILER_TYPE_Changed();" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvTRAILER_TYPE" runat="server" ControlToValidate="cmbTRAILER_TYPE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSERIAL_NO" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL_NO" runat="server" maxlength="25"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMANUFACTURER" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMANUFACTURER" runat="server" MaxLength="75"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="20" MaxLength="75"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capINSURED_VALUE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtINSURED_VALUE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" Width="85"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvINSURED_VALUE" runat="server" ControlToValidate="txtINSURED_VALUE" Display="Dynamic"></asp:requiredfieldvalidator>
									<%--<asp:regularexpressionvalidator id="revINSURED_VALUE" Display="Dynamic" ControlToValidate="txtINSURED_VALUE" Runat="server"
										Enabled="False"></asp:regularexpressionvalidator>--%>
									<asp:rangevalidator id="rngINSURED_VALUE" ControlToValidate="txtINSURED_VALUE" Display="Dynamic" Runat="server"
										MinimumValue="0,01" Type="Currency" MaximumValue="999999999,99"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capASSOCIATED_BOAT" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbASSOCIATED_BOAT" onfocus="SelectComboIndex('cmbASSOCIATED_BOAT')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvASSOCIATED_BOAT" ControlToValidate="cmbASSOCIATED_BOAT" Display="Dynamic"
										Runat="server"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTRAILER_DED_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><%--asp:dropdownlist id="cmbTRAILER_DED_ID" onfocus="SelectComboIndex('cmbTRAILER_DED_ID')" runat="server"></asp:dropdownlist--%>
									<select id="cmbTRAILER_DED_ID" onfocus="SelectComboIndex('cmbTRAILER_DED_ID')" name="cmbTRAILER_DED_ID"
										runat="server">
									</select>
									<asp:requiredfieldvalidator id="rfvTRAILERDEDID" ControlToValidate="cmbTRAILER_DED_ID" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="32%"></TD>
								<TD class="midcolora" width="32%"><br>
								</TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="" CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"> <INPUT id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
			<INPUT id="hidTRAILER_ID" type="hidden" value="0" name="hidTRAILER_ID" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
		</FORM>
		<script>
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidTRAILER_ID').value);
			if (document.getElementById("hidFormSaved").value == "5")
			 { 		
				
				RemoveTab(2,this.parent);
				RefreshWebGrid("5","1",true,true); 
				document.getElementById('hidTRAILER_ID').value = "NEW";				
			}
			else if (document.getElementById("hidFormSaved").value == "1")
			{
				this.parent.strSelectedRecordXML = "-1";
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidTRAILER_ID').value),true);
				
			}
		</script>
	</BODY>
</HTML>

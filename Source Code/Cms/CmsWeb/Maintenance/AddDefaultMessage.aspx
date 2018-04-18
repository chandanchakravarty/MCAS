<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddDefaultMessage.aspx.cs" AutoEventWireup="false"  validateRequest=false    Inherits="Cms.CmsWeb.Maintenance.AddDefaultMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddDefaultMessage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
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
				document.getElementById('txtMSG_CODE').value						='';
				document.getElementById('txtMSG_DESC').value						='';
				document.getElementById('txtMSG_TEXT').value						='';
				document.getElementById('cmbMSG_POSITION').options.selectedIndex	= -1;
				
				document.getElementById('chkApplyClient').checked				=false;
				document.getElementById('chkApplyAgency').checked				=false;
					
				document.getElementById('hidMSG_ID').value						='NEW';
				document.getElementById('hidFormSaved').value					='0';
				
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

						if(document.getElementById('hidMSG_APPLY_TO').value!="" &&  document.getElementById('hidMSG_APPLY_TO').value!=null)
						{
							var arrApply= document.getElementById('hidMSG_APPLY_TO').value.split(",")
						
							if(arrApply.length>1)
							{
								if(arrApply[0]=="Y")
								{
									document.getElementById('chkApplyClient').checked=true;
								}
								else	
								{
									document.getElementById('chkApplyClient').checked=false;
								}
									
								if(arrApply[1]=="Y")
								{
									document.getElementById('chkApplyAgency').checked=true;
								}	
								else	
								{
									document.getElementById('chkApplyAgency').checked=false;	
								}	
							}
							else if(arrApply.length<=1)
							{
								hideCheckBox();
								if(arrApply[0]=="Y")
								{
									document.getElementById('chkApplyClient').checked=true;
								}
								else	
								{
									document.getElementById('chkApplyClient').checked=false;
								}											
							}
						}
					}
					else
					{
						AddData();
						hideCheckBox();
					}
				}
				else
				{
					DisableValidators();					
					hideCheckBox();
			
				}				
				return false;
			}
			
			
			function hideCheckBox()
			{
				if(document.getElementById("hidMSG_TYPE").value=="L")
				{
					document.getElementById('chkApplyAgency').style.visibility="hidden";
					document.getElementById('capMSG_APPLY_TO').innerText="Set as default"
					document.getElementById('capApplyClient').innerText="";
					document.getElementById('capApplyAgency').innerText="";
				}
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
	<body MS_POSITIONING="GridLayout" onload="populateXML();ApplyColor();"
		MS_POSITIONING="GridLayout">
		<form id="DefaultMessage" method="post" runat="server">
		<TABLE width="100%" align="center" border="0">
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
														<td class="midcolora" width="18%">
															<asp:label id="capMSG_CODE" Runat="server"></asp:label><span class="mandatory">*</span>
														</td>
														<td class="midcolora" width="32%">
															<asp:textbox id="txtMSG_CODE" Runat="server" MaxLength="50"></asp:textbox><br>
															<asp:requiredfieldvalidator id="rfvMSG_CODE" Runat="server" Display="Dynamic" ControlToValidate="txtMSG_CODE"></asp:requiredfieldvalidator>
														</td>
														<td class="midcolora" width="18%">
															<asp:label id="capMSG_POSITION" Runat="server"></asp:label>
														</td>
														<td class="midcolora" width="32%">
															<asp:dropdownlist id="cmbMSG_POSITION" runat="server"></asp:dropdownlist>
														</td>
													</tr>
													<tr>
														<td class="midcolora" width="18%">
															<asp:label id="capMSG_DESC" Runat="server"></asp:label><span class="mandatory">*</span>(Max 
															200 characters)
														</td>
														<td class="midcolora" width="32%">
															<asp:textbox id="txtMSG_DESC" Runat="server" Columns="48" Rows="7" MaxLength="200" TextMode="MultiLine"></asp:textbox><br>
															<asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtMSG_DESC" Runat="server"
																Display="Dynamic" ID="csvMSG_DESC"></asp:CustomValidator>
															<asp:requiredfieldvalidator id="rfvMSG_DESC" Runat="server" Display="Dynamic" ControlToValidate="txtMSG_DESC"></asp:requiredfieldvalidator>
														</td>
														<td class="midcolora" width="18%">
															<asp:label id="capMSG_TEXT" Runat="server"></asp:label><span class="mandatory">*</span><span>(Max 200 characters)</span>
														</td>
														<td class="midcolora" width="32%">
															<asp:textbox id="txtMSG_TEXT" Runat="server" Columns="48" Rows="7" TextMode="MultiLine" MaxLength="200"></asp:textbox><br>
															<asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtMSG_TEXT" Runat="server"
																Display="Dynamic" ID="csvMSG_TEXT"></asp:CustomValidator>
															<asp:requiredfieldvalidator id="rfvMSG_TEXT" Runat="server" Display="Dynamic" ControlToValidate="txtMSG_TEXT"></asp:requiredfieldvalidator>
														</td>
													</tr>
													<tr>
														<td class="midcolora" width="18%" colSpan="1">
															<asp:label id="capMSG_APPLY_TO" Runat="server"></asp:label>
														</td>
														<td class="midcolora" width="32%" colSpan="3">
															<asp:checkbox id="chkApplyClient" Runat="server"></asp:checkbox>&nbsp;
															<asp:Label ID="capApplyClient" Runat="server"></asp:Label>&nbsp;
															<asp:checkbox id="chkApplyAgency" Runat="server"></asp:checkbox>
															<asp:Label ID="capApplyAgency" Runat="server"></asp:Label>&nbsp;
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
											</TD>
										</TR>
										<TR>
											<TD>
												<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"></TD>
											<TD>
												<INPUT id="hidMSG_ID" type="hidden" name="hidMSG_ID" runat="server"></TD>
											<TD>
												<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"></TD>
											<TD>
												<INPUT id="hidMSG_TYPE" type="hidden" name="hidMSG_TYPE" runat="server"></TD>
											<TD>
												<INPUT id="hidMSG_APPLY_TO" type="hidden" name="hidMSG_APPLY_TO" runat="server"></TD>
										</TR>
									</TABLE>
								</form>
							</TD>
						</TR>
						<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidMSG_ID').value,false);
						</script>
					</TABLE>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>

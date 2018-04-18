<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddFinanceCompany.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddFinanceCompany" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_FINANCE_COMPANY_LIST</title>
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
			DisableValidators();
			ChangeColor();
			document.getElementById('hidCOMPANY_ID').value	=	'New';
			document.getElementById('txtCOMPANY_NAME').value  = '';
			document.getElementById('txtCOMPANY_CODE').value  = '';
			document.getElementById('txtCOMPANY_ADD1').value  = '';
			document.getElementById('txtCOMPANY_ADD2').value  = '';
			document.getElementById('txtCOMPANY_CITY').value  = '';
			document.getElementById('cmbCOMPANY_COUNTRY').options.selectedIndex = 0;
			document.getElementById('cmbCOMPANY_STATE').options.selectedIndex = -1;
			document.getElementById('txtCOMPANY_ZIP').value  = '';
			document.getElementById('txtCOMPANY_MAIN_PHONE_NO').value  = '';
			document.getElementById('txtCOMPANY_TOLL_FREE_NO').value  = '';
			document.getElementById('txtCOMPANY_EXT').value  = '';
			document.getElementById('txtCOMPANY_FAX').value  = '';
			document.getElementById('txtCOMPANY_EMAIL').value  = '';
			document.getElementById('txtCOMPANY_WEBSITE').value  = '';
			document.getElementById('txtCOMPANY_MOBILE').value  = '';
			document.getElementById('txtCOMPANY_TERMS').value  = '';
			document.getElementById('txtCOMPANY_TERMS_DESC').value  = '';
			document.getElementById('txtCOMPANY_NOTE').value  = '';
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',true);
			//document.getElementById('txtCOMPANY_NAME').focus();
			
		}
		
		function setTab()
		{
			if (document.getElementById('hidOldData').value	!= '')
			{	
						
				Url="AttachmentIndex.aspx?calledfrom=fin&EntityType=FC&EntityId="+document.getElementById('hidCOMPANY_ID').value + "&";
				DrawTab(3,top.frames[1],'Attachment',Url);	
				Url="AccountingEntityIndex.aspx?calledfrom=fin&EntityType=FC&EntityId="+document.getElementById('hidCOMPANY_ID').value + "&EntityName="+document.getElementById('hidCOMPANY_Name').value + "&";
				DrawTab(2,top.frames[1],'Accounting Entity',Url);										
			}
			else
			{							
				RemoveTab(3,top.frames[1]);
				RemoveTab(2,top.frames[1]);			
			}
		}
		
		function populateXML()
		{
				
					
			if(document.getElementById('hidOldData').value	!= '' && document.MNT_FINANCE_COMPANY_LIST.hidFormSaved.value == "1")
			{
				var tempXML;
						
					//Enabling the activate deactivate button
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').setAttribute('disabled',false);
					//Storing the XML in hidRowId hidden fields 
					//document.getElementById('hidOldData').value		=	 tempXML;					
					tempXML = document.getElementById('hidOldData').value;
					//alert(tempXML);
					populateFormData(tempXML,MNT_FINANCE_COMPANY_LIST);			
				
			 }			 
			 if ( document.MNT_FINANCE_COMPANY_LIST.hidCOMPANY_ID.value == "New" && document.MNT_FINANCE_COMPANY_LIST.hidFormSaved.value == "0")
			 {
				//alert(document.getElementById('hidOldData').value);
				AddData();
			 }			 
			setTab();
			return false;
		}
		
		function generateCode()
			{
			var strname=new String();
				strname=document.getElementById("txtCOMPANY_NAME").value; 
				
				if(document.getElementById('hidCOMPANY_ID').value=='New')
				{
					if(strname.length>6)
						document.getElementById("txtCOMPANY_CODE").value=strname.substring(0,6);
					else
						document.getElementById("txtCOMPANY_CODE").value=strname;					
				}
					  
				
			}
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 999 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}		
		</script>
	</HEAD>
	<BODY class="bodyBackGround" leftMargin="0" topMargin="0" onload="ApplyColor(); ChangeColor(); populateXML();">
		<FORM id="MNT_FINANCE_COMPANY_LIST" method="post" runat="server">
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
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_NAME" Runat="server">Name</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_NAME" runat="server" maxlength="70" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMPANY_NAME" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_NAME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCOMPANY_NAME" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_NAME"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_CODE" Runat="server">Code</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_CODE" runat="server" maxlength="6" size="9"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMPANY_CODE" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_CODE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCOMPANY_CODE" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_CODE"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_ADD1" Runat="server">Address1</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_ADD1" runat="server" maxlength="70" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMPANY_ADD1" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_ADD1"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_ADD2" Runat="server">Address2</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_ADD2" runat="server" maxlength="70" size="30"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_CITY" Runat="server">City</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMPANY_CITY" runat="server" ControlToValidate="txtCOMPANY_CITY" ErrorMessage="COMPANY_CITY can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_COUNTRY" Runat="server">Country</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOMPANY_COUNTRY" onfocus="SelectComboIndex('cmbCOMPANY_COUNTRY')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvCOMPANY_COUNTRY" runat="server" ControlToValidate="cmbCOMPANY_COUNTRY" ErrorMessage="COMPANY_COUNTRY can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_STATE" Runat="server">State</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOMPANY_STATE" onfocus="SelectComboIndex('cmbCOMPANY_STATE')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvCOMPANY_STATE" runat="server" ControlToValidate="cmbCOMPANY_STATE" ErrorMessage="COMPANY_STATE can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_ZIP" Runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_ZIP" runat="server" maxlength="10" size="13"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMPANY_ZIP" runat="server" ControlToValidate="txtCOMPANY_ZIP" ErrorMessage="COMPANY_ZIP can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCOMPANY_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_ZIP"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_MAIN_PHONE_NO" Runat="server">Main Phone No</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_MAIN_PHONE_NO" runat="server" maxlength="13" size="17"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCOMPANY_MAIN_PHONE_NO" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_MAIN_PHONE_NO"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_TOLL_FREE_NO" Runat="server">Toll Free No</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_TOLL_FREE_NO" runat="server" maxlength="13" size="17"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCOMPANY_MAIN_TOLL_FREE_NO" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_TOLL_FREE_NO"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_EXT" Runat="server">Ext.</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_EXT" runat="server" maxlength="4" size="6" ReadOnly="True"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCOMPANY_EXT" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_EXT"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_FAX" Runat="server">Fax</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_FAX" runat="server" maxlength="13" size="17"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCOMPANY_FAX" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_FAX"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_EMAIL" Runat="server">Email</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_EMAIL" runat="server" maxlength="100" size="30"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCOMPANY_EMAIL" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_EMAIL"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_WEBSITE" Runat="server">Website</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_WEBSITE" runat="server" maxlength="140" size="30"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCOMPANY_WEBSITE" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_WEBSITE"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_MOBILE" Runat="server">Mobile No</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_MOBILE" runat="server" maxlength="13" size="17"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCOMPANY_MOBILE" runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_MOBILE"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_TERMS" Runat="server">Terms</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_TERMS" runat="server" maxlength="3" size="5"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_TERMS_DESC" Runat="server">Terms Description</asp:label></TD>
								<TD class='midcolora' width="32%">
									<asp:textbox id='txtCOMPANY_TERMS_DESC' runat='server' size='2000' onkeypress="MaxLength(this,999);"
										TextMode='MultiLine' MaxLength="1000"></asp:textbox>
									<br>
									<asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtCOMPANY_TERMS_DESC"
										Runat="server" Display="Dynamic" ID="csvCOMPANY_TERMS_DESC"></asp:CustomValidator>
								</TD>
								<TD class='midcolora' width="18%">
									<asp:Label Runat="server" ID="capCOMPANY_NOTE">Note</asp:Label>
								</TD>
								<TD class='midcolora' width="32%">
									<asp:textbox id='txtCOMPANY_NOTE' runat='server' size='2000' onkeypress="MaxLength(this,999);"
										TextMode='MultiLine' MaxLength="1000"></asp:textbox>
									<br>
									<asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtCOMPANY_NOTE"
										Runat="server" Display="Dynamic" ID="csvCOMPANY_NOTE"></asp:CustomValidator>
								</TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="1" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCOMPANY_ID" type="hidden" name="hidCOMPANY_ID" runat="server">
			<INPUT id="hidCOMPANY_Name" type="hidden" name="hidCOMPANY_Name" runat="server">
			<script>
				if (document.getElementById("hidFormSaved").value == "5")
				{
					/*Record deleted*/
					/*Refreshing the grid and coverting the form into add mode*/
					/*Using the javascript*/
					RefreshWebGrid("1","1"); 
					document.getElementById("hidFormSaved").value = "0";
					AddData();
				
			    }
			    
			    if(document.getElementById('hidFormSaved').value=='2')
			    {
			    document.getElementById('txtCOMPANY_CODE').value='';
			    document.getElementById('txtCOMPANY_CODE').focus();
			    
			    }
			</script>
		</FORM>
	</BODY>
</HTML>

<%@ Page language="c#" Codebehind="AddAkaDba.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.AddAkaDba" validateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddAkaDba</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script language="javascript">
			function DoBack()
			{
				//this.parent.document.location.href = "customerindex.aspx";
								
				this.parent.parent.document.location.href = "CustomerManagerIndex.aspx";
				return false;
			}		
			function ResetForm()
			{
				DisableValidators();
					
				if ( document.Form1.hidOldData.value == '' )
				{
					AddData();
				}
				else
				{
					//alert('ol' + document.Form1.hidOldData.value);
					populateFormData(document.Form1.hidOldData.value,Form1);
				}
				
				txtAKADBA_WEBSITE_OnChange();
				return false;
			}
		
			function EnableDisable()
			{
				//alert(document.getElementById('txtAKADBA_DISP_ORDER').disabled);
				if ( document.getElementById('chkAKADBA_NAME_ON_FORM').checked == true )
				{
					document.getElementById('txtAKADBA_DISP_ORDER').disabled = false;
				}
				else
				{
					document.getElementById('txtAKADBA_DISP_ORDER').disabled = true;
				}
			}
			
			function Initialize()
			{
				txtAKADBA_WEBSITE_OnChange()
				
				if (document.getElementById('hidOldData').value == '')
				{
					document.Form1.cmbAKADBA_STATE.options.selectedIndex = -1;
					document.Form1.cmbAKADBA_COUNTRY.selectedIndex = -1;
				}
				
			}
			
			function txtAKADBA_WEBSITE_OnChange()
			{
				//alert(value);
				var regEx = document.all["revAKADBA_WEBSITE"].validationexpression;
				//alert(regEx);
				//alert(document.getElementById('txtAKADBA_WEBSITE').value.search(regEx));
				
				//ValidatorValidate(document.getElementById('revAKADBA_WEBSITE'))
				
				
				if ( document.getElementById('txtAKADBA_WEBSITE').value == '' )
				{
					if(document.Form1.btnWebsite)
					document.Form1.btnWebsite.disabled = false;
				}
				else
				{
					if(document.Form1.btnWebsite)
					document.Form1.btnWebsite.disabled = true;
				}
				
				//alert( document.getElementById('txtAKADBA_WEBSITE').value + ' ' + document.getElementById('txtAKADBA_WEBSITE').value.search(regEx));
				if ( document.getElementById('txtAKADBA_WEBSITE').value.match(regEx))
				{
					if(document.Form1.btnWebsite)
					document.Form1.btnWebsite.disabled = false;
				}
				else
				{
					if(document.Form1.btnWebsite)
					document.Form1.btnWebsite.disabled = true;
				}
				
				
			}
			
			function IsWebsiteValid()
			{
				var regEx = document.all["revAKADBA_WEBSITE"].validationexpression;
				
				return document.getElementById('txtAKADBA_WEBSITE').value.search(regEx);
			}
			
			function OpenWebsite()
			{
				if ( document.getElementById('txtAKADBA_WEBSITE').value == '')
				{
					return;
				}
				
				if ( IsWebsiteValid() == -1 ) return;
				
				openWindow(document.getElementById('txtAKADBA_WEBSITE').value);
			}
			
			function AddData()
			{
				DisableValidators();	
				
				document.Form1.txtAKADBA_NAME.value = '' ;
				document.Form1.txtAKADBA_ADD.value = '' ;
				document.Form1.txtAKADBA_ADD2.value = '' ;
				document.Form1.txtAKADBA_CITY.value = '';
				document.Form1.txtAKADBA_ZIP.value = '';
				document.Form1.cmbAKADBA_COUNTRY.selectedIndex = -1;
				document.Form1.txtAKADBA_WEBSITE.value = '';
				document.Form1.txtAKADBA_EMAIL.value = '';
				document.Form1.txtAKADBA_MEMO.value = '';
				document.Form1.cmbAKADBA_TYPE.selectedIndex = -1;
				document.Form1.txtAKADBA_DISP_ORDER.value = '';
				document.Form1.chkAKADBA_NAME_ON_FORM.checked = false;
				document.Form1.cmbAKADBA_STATE.options.selectedIndex = -1;
				document.Form1.cmbAKADBA_LEGAL_ENTITY_CODE.options.selectedIndex = -1;
				
				
				ChangeColor();
			}
			
			function populateXML()
			{
					var tempXML;
					
					tempXML = document.getElementById('hidOldData').value
					if(tempXML != "")
					{
						//Populating the data by the calling the common function in form,.js
						populateFormData(tempXML,Form1);
					}
					else
					{
						AddData();
					}
				
			//setTab();
			return false;
		}
			
		</script>
	</HEAD>
	<body leftMargin="0" onload="Initialize();ApplyColor();ChangeColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td class="pageHeader" colSpan="4">Please note that all fields marked with * are 
						mandatory
					</td>
				</tr>
				<tr>
					<td class="midcolorc" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capNameType" runat="server" EnableViewState="False">Name Type</asp:label><span class="mandatory">*</span>
					</td>
					<td class="midcolora" width="32%">
						<asp:dropdownlist id="cmbAKADBA_TYPE" runat="server" onfocus="SelectComboIndex('cmbAKADBA_TYPE')">
						</asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvAKADBA_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbAKADBA_TYPE"></asp:requiredfieldvalidator></td>
					<td class="midcolora" width="18%"><asp:label id="capLegalEntityCode" runat="server" EnableViewState="False">Legal Entity Code</asp:label></td>
					<td class="midcolora" width="32%">
						<asp:dropdownlist id="cmbAKADBA_LEGAL_ENTITY_CODE" runat="server" onfocus="SelectComboIndex('cmbAKADBA_LEGAL_ENTITY_CODE')">
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capName" runat="server" EnableViewState="False">Name</asp:label><span class="mandatory">*</span></td>
					<td class="midcolora" colSpan="3"><asp:textbox id="txtAKADBA_NAME" runat="server" size="35" MaxLength="255"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvAKADBA_NAME" runat="server" Display="Dynamic" ControlToValidate="txtAKADBA_NAME"></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="midcolora">
						<asp:label id="capPullCustomerAddress" runat="server" EnableViewState="False">Would you like to pull customer address</asp:label>
					</td>
					<td colspan="3" class="midcolora">
						<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton>
					</td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capAddress" runat="server" EnableViewState="False">Address1</asp:label><span class="mandatory">*</span>
					</td>
					<td class="midcolora" width="32%"><asp:textbox id="txtAKADBA_ADD" runat="server" size="30" MaxLength="70"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvAKADBA_ADD" runat="server" Display="Dynamic" ControlToValidate="txtAKADBA_ADD"></asp:requiredfieldvalidator></td>
					<td class="midcolora" width="18%"><asp:label id="capAddress2" runat="server" EnableViewState="False">Address2</asp:label></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtAKADBA_ADD2" runat="server" size="30" MaxLength="70"></asp:textbox></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capCity" runat="server" EnableViewState="False">City</asp:label><span class="mandatory">*</span></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtAKADBA_CITY" runat="server" size="30" MaxLength="40"></asp:textbox><br><asp:RequiredFieldValidator ID="rfvAKADBA_CITY" Runat=server ControlToValidate=txtAKADBA_CITY Display=Dynamic></asp:RequiredFieldValidator></td>
					<td class="midcolora" width="18%"><asp:label id="capCountry" runat="server" EnableViewState="False">Country</asp:label></td>
					<td class="midcolora" width="32%">
						<asp:dropdownlist id="cmbAKADBA_COUNTRY" runat="server" onfocus="SelectComboIndex('cmbAKADBA_COUNTRY')">
						</asp:dropdownlist><br>
						<asp:requiredfieldvalidator Enabled=False   id="rfvAKADBA_COUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbAKADBA_COUNTRY"></asp:requiredfieldvalidator></td>
					</TD></tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capState" runat="server" EnableViewState="False">State</asp:label><span class="mandatory">*</span></td>
					<td class="midcolora" width="32%">
						<asp:dropdownlist id="cmbAKADBA_STATE" runat="server" onfocus="SelectComboIndex('cmbAKADBA_STATE')">
						</asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvAKADBA_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbAKADBA_STATE"></asp:requiredfieldvalidator></td>
					<td class="midcolora" width="18%"><asp:label id="capZip" runat="server" EnableViewState="False">Zip</asp:label><span class="mandatory">*</span>
					</td>
					<td class="midcolora" width="32%"><asp:textbox id="txtAKADBA_ZIP" runat="server" size="12" MaxLength="10"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revAKADBA_ZIP" ControlToValidate="txtAKADBA_ZIP" Runat="server" Display="Dynamic"></asp:regularexpressionvalidator>
						<asp:requiredfieldvalidator id="rfvAKADBA_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtAKADBA_ZIP"
							ErrorMessage="RequiredFieldValidator"></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capWebsite" runat="server" EnableViewState="False">Website</asp:label></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtAKADBA_WEBSITE" runat="server" size="30" MaxLength="150"></asp:textbox>
						<INPUT onclick="javascript:OpenWebsite();" id="btnWebsite" type="button" value="GO" class="clsButton">
						<br>
						<asp:regularexpressionvalidator id="revAKADBA_WEBSITE" Display="Dynamic" ControlToValidate="txtAKADBA_WEBSITE" Runat="server"></asp:regularexpressionvalidator></td>
					<td class="midcolora" width="18%"><asp:label id="capEmail" runat="server" EnableViewState="False">Email</asp:label><span class="mandatory">*</span></td>
					<td class="midcolora" width="32%">
						<asp:textbox id="txtAKADBA_EMAIL" runat="server" size="30" MaxLength="50"></asp:textbox>
						<br>
						<asp:RequiredFieldValidator ID=rfvAKADBA_EMAIL Runat=server ControlToValidate=txtAKADBA_EMAIL Display=Dynamic></asp:RequiredFieldValidator>
						<asp:regularexpressionvalidator id="revCUSTOMER_Email" Display="Dynamic" ControlToValidate="txtAKADBA_EMAIL" Runat="server"></asp:regularexpressionvalidator></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capMemo" runat="server" EnableViewState="False">Memo</asp:label></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtAKADBA_MEMO" runat="server" size="30" MaxLength="35"></asp:textbox></td>
					<td class="midcolora" width="18%"><asp:label id="capNameAppears" runat="server" EnableViewState="False">Name appears on Form</asp:label></td>
					<td class="midcolora" width="32%"><asp:checkbox id="chkAKADBA_NAME_ON_FORM" runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capDisplayOrder" runat="server" EnableViewState="False">Display Order #</asp:label></td>
					<td class="midcolora" width="32%">
						<asp:textbox id="txtAKADBA_DISP_ORDER" runat="server" size="3" MaxLength="3"></asp:textbox>
						<br>
						<asp:rangevalidator id="rngAKADBA_DISP_ORDER" runat="server" ControlToValidate="txtAKADBA_DISP_ORDER"
							ErrorMessage="Display Order # should be between 1 and 100" MinimumValue="1" MaximumValue="100" Type="Integer"></asp:rangevalidator></td>
					<td class="midcolora" width="18%"></td>
					<td class="midcolora" width="32%"></td>
				</tr>
				<tr>
					<td class="midcolora" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back To Customer Assistant"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton></td>
					<td class="midcolorr" align="right" colSpan="2"><INPUT id="hidCustomerID" type="hidden" runat="server"><INPUT id="hidFormSaved" type="hidden" runat="server">&nbsp;
						<INPUT id="hidOldData" type="hidden" runat="server">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
			</table>
		</form>
		<script>
						//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCustomerID').value, false);

		</script>
	</body>
</HTML>
